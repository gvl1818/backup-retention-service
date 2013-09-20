using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
//using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Data.SqlServerCe;

namespace BackupRetention
{
    /// <summary>
    /// Backup Retention Main Service Class
    /// </summary>
    public partial class BackupRetentionService : ServiceBase
    {
        #region "Variables"


        /// <summary>
        /// Default Service Interval
        /// </summary>
        const long ServiceIntervalDefault = 120000; 

        /// <summary>
        /// Timer Object
        /// </summary>
        private System.Timers.Timer _t;

        Thread SyncThread;
        Thread RemoteSyncThread;
        Thread RetentionThread ;
        Thread CompressThread ;

        /// <summary>
        /// if service is shutting down boolean variable
        /// </summary>
        private static bool blShuttingDown = false;
        /// <summary>
        /// Synchronization configuration table
        /// </summary>
        private static DataTable dtSyncConfig;
        /// <summary>
        /// Retention configuration table
        /// </summary>
        private static DataTable dtRetentionConfig;
        /// <summary>
        /// Compress configuration table
        /// </summary>
        private static DataTable dtCompressConfig;

        /// <summary>
        /// Transfer configuration table
        /// </summary>
        private static DataTable dtRemoteConfig;
        
    
        /// <summary>
        /// Event Log Class
        /// </summary>
        private static System.Diagnostics.EventLog _evt;
        
        //private static bool blRunning = false;

        #endregion

        #region "Properties"

        

        private long _serviceInterval = ServiceIntervalDefault;
        /// <summary>
        /// Service Interval Property is the time interval used to fire the timer
        /// </summary>
        public long ServiceInterval
        {
            get
            {
                string str = ConfigurationManager.AppSettings["ServiceInterval"];
                if (!String.IsNullOrEmpty(str))
                {
                    long.TryParse(str, out _serviceInterval);
                }
                else
                {
                    _serviceInterval = ServiceIntervalDefault;
                    SaveAppSetting("ServiceInterval", _serviceInterval.ToString());
                }
                return _serviceInterval;
            }

            set
            {
                _serviceInterval = value;
                if (_serviceInterval < 1000)
                {
                    _serviceInterval = ServiceIntervalDefault;
                }
                SaveAppSetting("ServiceInterval", _serviceInterval.ToString());

            }
        }


        private double _maxDriveSpaceUsedPercent = 0.90;
        public double MaxDriveSpaceUsedPercent
        {
            get
            {
                string str = ConfigurationManager.AppSettings["MaxDriveSpaceUsedPercent"];
                if (!String.IsNullOrEmpty(str))
                {
                    double.TryParse(str, out _maxDriveSpaceUsedPercent);
                    if (_maxDriveSpaceUsedPercent > 1)
                    {
                        _maxDriveSpaceUsedPercent = _maxDriveSpaceUsedPercent / 100;
                    }
                }
                else
                {
                    _maxDriveSpaceUsedPercent = 0.9;
                    SaveAppSetting("MaxDriveSpaceUsedPercent", _maxDriveSpaceUsedPercent.ToString());
                }
                return _maxDriveSpaceUsedPercent;
            }

            set
            {
                _maxDriveSpaceUsedPercent = value;
                if (_maxDriveSpaceUsedPercent > 1)
                {
                    _maxDriveSpaceUsedPercent = _maxDriveSpaceUsedPercent / 100;
                }
                SaveAppSetting("MaxDriveSpaceUsedPercent", _maxDriveSpaceUsedPercent.ToString());

            }
        }

        /*
        private string _emailTo = "";
        public string EmailTo
        {
            get
            {
                string _emailTo = ConfigurationManager.AppSettings["EmailTo"];
                return _emailTo;
            }
            set
            {
                _emailTo = value;

                SaveAppSetting("EmailTo", _emailTo);

            }
        }


        private string _emailFrom = "";
        public string EmailFrom
        {
            get
            {
                string _emailFrom = ConfigurationManager.AppSettings["EmailFrom"];
                return _emailFrom;
            }
            set
            {
                _emailFrom = value;

                SaveAppSetting("EmailFrom", _emailFrom);

            }
        }


        private string _smtpServer = "";
        public string SMTPServer
        {
            get
            {
                string _smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
                return _smtpServer;
            }
            set
            {
                _smtpServer = value;

                SaveAppSetting("SMTPServer", _smtpServer);

            }
        }


        private bool _blsendEmail = false;
        public bool blSendEmail
        {
            get
            {
                int intvalue = 0;
                string str = ConfigurationManager.AppSettings["SendEmail"];
                if (!String.IsNullOrEmpty(str))
                {
                    Int32.TryParse(str, out intvalue);
                    if (intvalue == 1)
                    {
                        _blsendEmail = true;
                    }
                }
                return _blsendEmail;
            }

            set
            {
                _blsendEmail = value;
                int intSendEmail = 0;
                if (_blsendEmail)
                {
                    intSendEmail = 1;
                }
                SaveAppSetting("SendEmail", intSendEmail.ToString());

            }
        }
        */
        

        
        #endregion 


        #region "Methods"

        /// <summary>
        /// Class Initialization
        /// </summary>
        public BackupRetentionService()
        {
            InitializeComponent();
            _evt = Common.GetEventLog;
            
        }

        /// <summary>
        /// Saves App.config application settings and refreshes.
        /// </summary>
        /// <param name="strProperty"></param>
        /// <param name="strValue"></param>
        public static void SaveAppSetting(string strProperty, string strValue)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //SAVE ALL OF THE SETTINGS
            config.AppSettings.Settings.Remove(strProperty);
            config.AppSettings.Settings.Add(strProperty, strValue);

            // Save the config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section. 
            ConfigurationManager.RefreshSection("appSettings");


        }

        /// <summary>
        /// Initializes Sync configuration table
        /// </summary>
        private void init_dtSyncConfig()
        {
            dtSyncConfig = SyncFolder.init_dtConfig();
        }

        /// <summary>
        /// Initializes Retention Configuration table
        /// </summary>
        private void init_dtRetentionConfig()
        {
            dtRetentionConfig = RetentionFolder.init_dtConfig();
        }

        /// <summary>
        /// Initializes Compress configuration table
        /// </summary>
        private void init_dtCompressConfig()
        {
            dtCompressConfig = CompressFolder.init_dtConfig();
        }

        /// <summary>
        /// Initializes Compress configuration table
        /// </summary>
        private void init_dtRemoteConfig()
        {
            dtRemoteConfig = RemoteFolder.init_dtConfig();
        }


        /// <summary>
        /// Service Start Method
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                _t = new System.Timers.Timer();
                blShuttingDown = false;
                _t.Interval = ServiceInterval;
                _t.Elapsed += TimerFired;
                _t.Enabled = true;

                //Load Configuration Files
                init_dtSyncConfig();
                dtSyncConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SyncConfig.xml");

                init_dtRetentionConfig();
                dtRetentionConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RetentionConfig.xml");

                init_dtCompressConfig();
                dtCompressConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompressConfig.xml");

                init_dtRemoteConfig();
                dtRemoteConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RemoteConfig.xml");

                //Timer_Execute();

                SyncThread = new Thread(new ThreadStart(Sync));
                
                RemoteSyncThread = new Thread(new ThreadStart(RemoteSync));

                RetentionThread = new Thread(new ThreadStart(Retention));
                
                CompressThread = new Thread(new ThreadStart(Compress));

                SyncFolder.CompactDatabase();

                _evt.WriteEntry("BackupRetentionService Started", System.Diagnostics.EventLogEntryType.Information, 0, 0);
                  
            }
            catch (Exception ex)
            {

                _t.Elapsed += TimerFired;
                _t.Enabled = false;
                _evt.WriteEntry(ex.Message);
            }

        }

        /// <summary>
        /// Service Stop Method
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                _t.Enabled = false;
                blShuttingDown = true;
                System.Threading.Thread.Sleep(3000);
                _t.Dispose();
                _t = null;
                long lwait = 0;
                while (SyncThread.IsAlive || RemoteSyncThread.IsAlive || RetentionThread.IsAlive || CompressThread.IsAlive)
                {
                    Thread.Sleep(3000);
                    lwait += 3;
                    if (lwait > 3600)
                    {
                        if (SyncThread.IsAlive)
                        {
                            SyncThread.Abort();
                        }
                        if (RemoteSyncThread.IsAlive)
                        {
                            RemoteSyncThread.Abort();
                        }
                        if (RetentionThread.IsAlive)
                        {
                            RetentionThread.Abort();
                        }
                        if (CompressThread.IsAlive)
                        {
                            CompressThread.Abort();
                        }
                        Thread.Sleep(3000);
                        break;
                    }
                }
                
                SyncThread = null;
                RemoteSyncThread = null;
                RetentionThread = null;
                CompressThread = null;
                if (lwait > 3600)
                {
                    _evt.WriteEntry("BackupRetentionService Forced Stopped: " + lwait.ToString(), System.Diagnostics.EventLogEntryType.Information, 0, 0);
                }
                else
                {
                    _evt.WriteEntry("BackupRetentionService Stopped", System.Diagnostics.EventLogEntryType.Information, 0, 0);
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
            }
            

        }

        /// <summary>
        /// Timer Event Fired Main Code  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimerFired(object sender, System.Timers.ElapsedEventArgs e)
        {
            Timer_Execute();
        }

        

        /// <summary>
        /// Checks to see of the current time is within the appropriate time window to execute per RetentionExecutionTime or CompressExecutionTime
        /// </summary>
        /// <param name="strExecutionTime"></param>
        /// <returns></returns>
        public bool TimeToExecute(IFolderConfig folder)
        {
            string strExecutionTime = "";
            bool blTimeStart = false;
            bool blFinalTimeEnd = false;
            TimeSpan timeStart;
            TimeSpan timeEnd;
            TimeSpan FinalTimeEnd;
            int intIntervalSecondsDoubled;
            int intIntervalDoubled;
            int intHours = 0;
            int intMinutes = 0;
            try
            {


                strExecutionTime = folder.Time;
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                
                //Interval in Minutes Doubled and Subtracted by 1 so that the time window code will only execute once
                intIntervalSecondsDoubled = (int)((ServiceInterval) / 1000);
                intIntervalSecondsDoubled += 1;

                intIntervalDoubled = (int)((ServiceInterval) / 1000 / 60);
                if (intIntervalSecondsDoubled <= 60000)
                {
                    intIntervalDoubled = 0;
                }
                else
                {
                    intIntervalSecondsDoubled = 0;
                }
                        
                blTimeStart = TimeSpan.TryParse(strExecutionTime, out timeStart);

                if (blTimeStart)
                {
                    timeEnd = timeStart + new TimeSpan(0, intIntervalDoubled, intIntervalSecondsDoubled);
                }
                else
                {
                    //No Time to Start Specified
                    return false;
                }

                blFinalTimeEnd = TimeSpan.TryParse(folder.EndTime, out FinalTimeEnd);

                if (blTimeStart && currentTime >= timeStart && (currentTime <= timeEnd || (folder.IntervalType == IntervalTypes.Hourly )) && (!blFinalTimeEnd || currentTime <= FinalTimeEnd))
                {
                    switch (folder.IntervalType)
                    {
                        case IntervalTypes.Hourly:
                            //Hourly Repetitive Interval

                            //folder.Interval is in Minutes for Hourly

                            intHours = (int)(folder.Interval / (long)60);
                            intHours = intHours % 24;

                            intMinutes = (int)((double)folder.Interval % (double)60.0);
                           
                            if ((((double)currentTime.Minutes % (double)intMinutes) == 0) && (intHours == 0 || ((double)currentTime.Hours % (double)intHours) == 0))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                                    
                            
                        default:   //Daily or Monthly the repetitive time doesn't matter, should only execute once
                            return true;
                            
                    }
                              
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                return false;
            }
               

        }

        /// <summary>
        /// Nth Day of the Month for example the 3rd Monday of the Month
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private bool NthDayOfMonth(IFolderConfig folder)
        {
            DateTime today = DateTime.Now;
            bool blExecuteToday = false;
            int n = 0;
            n = (int)Math.Abs(folder.Interval);

            if (DayToExecute(folder) && folder.Interval < 0 && folder.IntervalType == IntervalTypes.Monthly)
            {   
                return (today.Day - 1) / 7 == (n - 1);
            }
            return blExecuteToday;
        }

        
        /// <summary>
        /// Day of the Month to Execute for example the 5th day of the month or the 20th of the month
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool DayOfMonthToExecute(IFolderConfig folder)
        {
            DateTime today = DateTime.Now;
            bool blExecuteToday = false;
            if (today.Day == folder.Interval && folder.Interval > 0 && folder.IntervalType == IntervalTypes.Monthly)
            {
                blExecuteToday = true;
            }
            return blExecuteToday;
        }


        /// <summary>
        /// Day to Execute for example if Monday is selected and Today is Monday then this returns true
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public bool DayToExecute(IFolderConfig folder)
        {
            DateTime today =DateTime.Now;
            bool blExecuteToday = false;
            
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    if (folder.Monday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Tuesday:
                    if (folder.Tuesday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Wednesday:
                    if (folder.Wednesday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Thursday:
                    if (folder.Thursday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Friday:
                    if (folder.Friday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Saturday:
                    if (folder.Saturday)
                    {
                        blExecuteToday = true;
                    }
                    break;
                case DayOfWeek.Sunday:
                    if (folder.Sunday)
                    {
                        blExecuteToday = true;
                    }
                    break;
            }
            
            return blExecuteToday;
        }


        private System.Object lockSync = new System.Object();
        /// <summary>
        /// Thread Procedure for Synchronizing LAN Folder and sub folder Contents
        /// </summary>
        public void Sync()
        {
            lock (lockSync)
            {
                //Synchronize Windows or Windows Shares Folders
                foreach (DataRow row in dtSyncConfig.Rows)
                {
                    SyncFolder SFolder = new SyncFolder(row);
                    if (SFolder.Enabled)
                    {
                        if (string.IsNullOrEmpty(SFolder.Time) || TimeToExecute(SFolder))
                        {
                            if (DayToExecute(SFolder) || DayOfMonthToExecute(SFolder) || NthDayOfMonth(SFolder))
                            {
                                if (Common.DriveSpaceUsed(SFolder.DestinationFolder) < MaxDriveSpaceUsedPercent)
                                {
                                    SFolder.ExecuteSyncFolder(ref blShuttingDown);
                                }
                                else
                                {
                                    _evt.WriteEntry("Sync: Max Drive Space Used Percent Exceeded!");
                                }
                            }
                        }
                    }
                    SFolder = null;

                }
            }
        }

        private System.Object lockRemoteSync = new System.Object();
        /// <summary>
        /// Thread Procedure for Remotely synchronizing folders and subfolder contents to a remote ftp,ftps, or sftp site
        /// </summary>
        public void RemoteSync()
        {
            lock (lockRemoteSync)
            {
                //RemoteFolder Synchronizes Remote FTP sites etc.
                foreach (DataRow row in dtRemoteConfig.Rows)
                {
                    RemoteFolder RemFolder = new RemoteFolder(row);
                    if (RemFolder.Enabled)
                    {
                        if (string.IsNullOrEmpty(RemFolder.Time) || TimeToExecute(RemFolder))
                        {
                            if (DayToExecute(RemFolder) || DayOfMonthToExecute(RemFolder) || NthDayOfMonth(RemFolder))
                            {
                                if ((Common.DriveSpaceUsed(RemFolder.BackupFolder) < MaxDriveSpaceUsedPercent) || (RemFolder.TransferDirection == TransferDirectionOptions.Upload))
                                {
                                    RemFolder.ExecuteTransfer(ref blShuttingDown);
                                }
                                else
                                {
                                    _evt.WriteEntry("Remote Sync: Max Drive Space Used Percent Exceeded!");
                                }
                            }
                        }
                    }
                    RemFolder = null;

                }
            }
        }

        private System.Object lockRetention = new System.Object();
        /// <summary>
        /// Thread Procedure for Backup Retention algorithm
        /// </summary>
        public void Retention()
        {
            lock (lockRetention)
            {

                //Retention  Deletes older files based on algorithm
                foreach (DataRow row in dtRetentionConfig.Rows)
                {

                    RetentionFolder RFolder = new RetentionFolder(row);
                    if (RFolder.Enabled)
                    {
                        if (string.IsNullOrEmpty(RFolder.Time) || TimeToExecute(RFolder))
                        {
                            if (DayToExecute(RFolder) || DayOfMonthToExecute(RFolder) || NthDayOfMonth(RFolder))
                            {
                                RFolder.ExecuteRetentionPlan(ref blShuttingDown);
                            }
                        }
                    }
                    RFolder = null;
                }
            }

        }

        private System.Object lockCompress = new System.Object();
        /// <summary>
        /// File Compression thread for files or folder and sub folder contents
        /// </summary>
        public void Compress()
        {
            
            lock(lockCompress)
            {
                //Compress All Files Individually Execution
                foreach (DataRow row in dtCompressConfig.Rows)
                {
                    CompressFolder CFolder = new CompressFolder(row);
                    if (CFolder.Enabled)
                    {
                        if (string.IsNullOrEmpty(CFolder.Time) || TimeToExecute(CFolder))
                        {
                            if (DayToExecute(CFolder) || DayOfMonthToExecute(CFolder) || NthDayOfMonth(CFolder))
                            {
                                //Compress checks each file before compressing for available space
                                CFolder.ExecuteCompressAll(ref blShuttingDown);
                            }
                        }
                    }
                    CFolder = null;
                }
            }
        }

        

        /// <summary>
        /// This code executes the main code of the service
        /// </summary>
        public void Timer_Execute()
        {
            try
            {
               
                //Sync
                if (!SyncThread.IsAlive)
                {
                    SyncThread = new Thread(new ThreadStart(Sync));
                    SyncThread.Start();
                }
                else
                {
                    _evt.WriteEntry("BackupRetentionService Sync: Timer Code has not finished running. This is an additional firing of the TimerFired event. This is normal while compressing, Remote Sync, or Sync if large files or many files are present. Alternatively the Service Interval could be too short.",System.Diagnostics.EventLogEntryType.Information,4000,40);
                }

                //Remote Sync
                if (!RemoteSyncThread.IsAlive)
                {
                    RemoteSyncThread = new Thread(new ThreadStart(RemoteSync));
                    RemoteSyncThread.Start();
                }
                else
                {
                    _evt.WriteEntry("BackupRetentionService Remote Sync: Timer Code has not finished running. This is an additional firing of the TimerFired event. This is normal while compressing, Remote Sync, or Sync if large files or many files are present. Alternatively the Service Interval could be too short.", System.Diagnostics.EventLogEntryType.Information, 1000, 10);
                }

                //Retention
                if (!RetentionThread.IsAlive)
                {
                    RetentionThread = new Thread(new ThreadStart(Retention));
                    RetentionThread.Start();
                }
                else
                {
                    _evt.WriteEntry("BackupRetentionService Retention: Timer Code has not finished running. This is an additional firing of the TimerFired event. This is normal while compressing, Remote Sync, or Sync if large files or many files are present. Alternatively the Service Interval could be too short.", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                }

                //Compression
                if (!CompressThread.IsAlive)
                {
                    CompressThread = new Thread(new ThreadStart(Compress));
                    CompressThread.Start();
                }
                else
                {
                    _evt.WriteEntry("BackupRetentionService Compress: Timer Code has not finished running. This is an additional firing of the TimerFired event. This is normal while compressing, Remote Sync, or Sync if large files or many files are present. Alternatively the Service Interval could be too short.", System.Diagnostics.EventLogEntryType.Information, 5000, 50);
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
            }
            
            
        }

        
        
    

        /*
        /// <summary>
        /// Send Emails using Properties, Subject and Body Passed.
        /// </summary>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        public void SendEmail(string strSubject, string strBody)
        {
            if (blSendEmail)
            {
                //Emails regarding restarting the process server
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SMTPServer);
                mail.From = new MailAddress(EmailFrom);
                mail.To.Add(EmailTo);
                mail.Body = strBody;
                mail.Subject = strSubject;
                SmtpServer.Port = 25;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                //SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }

        }
         * */
        #endregion

    }

    
}

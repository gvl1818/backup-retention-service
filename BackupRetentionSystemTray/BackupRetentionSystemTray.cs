using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace BackupRetention
{
    /// <summary>
    /// BackupRetentionSystemTray Form
    /// </summary>
    public partial class BackupRetentionSystemTray : Form
    {
        const long ServiceIntervalDefault = 120000; 
        ServiceController sc;
        private DataTable dtSyncConfig;
        private DataTable dtRetentionConfig;
        private DataTable dtCompressConfig;
        private DataTable dtRemoteConfig;

        private DataSet dsEvents;
        private BindingSource bs;
       

        private string ep = "ISincerely!HopeThisIsNotUsed@";


        private AboutBox FrmAboutBox;

        public FolderBrowserDialog FolderBrowserD;
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
       

        #region "Properties"




        private long _serviceInterval = ServiceIntervalDefault;
        /// <summary>
        /// Service Interval Property is the time interval used to fire the timer
        /// </summary>
        public long ServiceInterval
        {
            get
            {
                string str = GetAppSetting("ServiceInterval");
                if (!String.IsNullOrEmpty(str))
                {
                    long.TryParse(str, out _serviceInterval);
                }
                else
                {
                    _serviceInterval = ServiceIntervalDefault;
                    //SaveAppSetting("ServiceInterval", _serviceInterval.ToString());
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
                //SaveAppSetting("ServiceInterval", _serviceInterval.ToString());

            }
        }


        private double _maxDriveSpaceUsedPercent = 0.90;
        public double MaxDriveSpaceUsedPercent
        {
            get
            {
                string str = GetAppSetting("MaxDriveSpaceUsedPercent");
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
                    //SaveAppSetting("MaxDriveSpaceUsedPercent", _maxDriveSpaceUsedPercent.ToString());
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
                //SaveAppSetting("MaxDriveSpaceUsedPercent", _maxDriveSpaceUsedPercent.ToString());

            }
        }
        #endregion

        /// <summary>
        /// Event Log Class
        /// </summary>
        private static System.Diagnostics.EventLog _evt;

        /// <summary>
        /// dtSyncConfi initialization
        /// </summary>
        private void init_dtSyncConfig()
        {
            dtSyncConfig = SyncFolder.init_dtConfig();
        }

        /// <summary>
        /// dtRetentionConfig initialization
        /// </summary>
        private void init_dtRetentionConfig()
        {
            dtRetentionConfig = RetentionFolder.init_dtConfig();
        }

        /// <summary>
        /// dtCompressConfig initialization
        /// </summary>
        private void init_dtCompressConfig()
        {
            dtCompressConfig = CompressFolder.init_dtConfig();
        }

        /// <summary>
        /// dtRemoteConfig initialization
        /// </summary>
        private void init_dtRemoteConfig()
        {
            dtRemoteConfig = RemoteFolder.init_dtConfig();
        }

     


        ~BackupRetentionSystemTray()
        {
            try
            {
                if (trayIcon != null)
                {
                    trayIcon.Dispose();
                }
                sc.Dispose();
                dtSyncConfig.Dispose();
                dtRetentionConfig.Dispose();
                dtRemoteConfig.Dispose();
                dtCompressConfig.Dispose();
                bs.Dispose();
                FolderBrowserD.Dispose();
                dsEvents.Dispose();

            }
            catch (Exception)
            {


            }
        }

        


        /// <summary>
        /// BackupRetentionSystemTray Constructor
        /// </summary>
        public BackupRetentionSystemTray()
        {
            InitializeComponent();
            _evt = Common.GetEventLog;
            eventLogBackupRetention = Common.GetEventLog;
            
            //eventLogBackupRetention.EnableRaisingEvents = true;
            //eventLogBackupRetention.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.eventLogBackupRetention_EntryWritten);
            

            sc = new ServiceController("BackupRetentionService");
             // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Settings", OnShowForm);
            trayMenu.MenuItems.Add("Start BackupRetentionService", OnRestart);
            trayMenu.MenuItems.Add("Restart BackupRetentionService", OnRestart);
            trayMenu.MenuItems.Add("Stop BackupRetentionService", OnStop);
            trayMenu.MenuItems.Add("Close Tray", OnExit);
            

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon      = new NotifyIcon();
            trayIcon.Text = "BackupRetentionTray";
            try
            {
                trayIcon.Icon = Properties.Resources.IcoWoodDriveTime;//new Icon(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\wood_drive_time_machine.ico", 40, 40);
                this.Icon = Properties.Resources.IcoWoodDriveTime;
            }
            catch (Exception)
            {

                trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
            }
           
            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible     = true;

            //Initialize Tables and Read Contents
            init_dtSyncConfig();
           
            try
            {
                dtSyncConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SyncConfig.xml");
            }
            catch (Exception)
            {
                dtSyncConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SyncConfig.xml");
            }
            dgvSync.AutoGenerateColumns = false;
            dgvSync.DataSource = dtSyncConfig;
            //dgvSync.AutoGenerateColumns = false;
            /*
            DataGridViewComboBoxColumn myCombo = new DataGridViewComboBoxColumn();
            myCombo.HeaderText = "Enabled";
            myCombo.Name = "Enabled";
            dgvSync.Columns.Insert(0, myCombo); // n is index
             */


            init_dtRetentionConfig();
            try
            {
                dtRetentionConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RetentionConfig.xml");
            
            }
            catch (Exception)
            {
                dtRetentionConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RetentionConfig.xml");
            }
            
            dgvRetention.AutoGenerateColumns = false;
            dgvRetention.DataSource = dtRetentionConfig;

            init_dtCompressConfig();

            try
            {
                dtCompressConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompressConfig.xml");
            }
            catch (Exception)
            {
                dtCompressConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompressConfig.xml");
            }

            dgvCompress.AutoGenerateColumns = false;
            dgvCompress.DataSource = dtCompressConfig;


            init_dtRemoteConfig();

            try
            {
                dtRemoteConfig.ReadXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RemoteConfig.xml");
            }
            catch (Exception)
            {
                dtRemoteConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RemoteConfig.xml");
            }

            dgvRemote.AutoGenerateColumns = false;
            dgvRemote.DataSource = dtRemoteConfig;

            
            mtxtMaxDriveSpaceUsed.Text = (MaxDriveSpaceUsedPercent * 100).ToString().Trim();
            txtServiceInterval.Text = ServiceInterval.ToString().Trim();

            lblServiceIntervalMinutes.Text = lblServiceIntervalMinutes.Text = (Math.Round(((double) ServiceInterval) / 1000 / 60, 2)).ToString() + " Minutes";

            FolderBrowserD = new FolderBrowserDialog();

            
            GetServiceStatus();
        }
        
        


        /// <summary>
        /// Saves App.config application settings and refreshes.
        /// </summary>
        /// <param name="strProperty"></param>
        /// <param name="strValue"></param>
        public static void SaveAppSetting(string strProperty, string strValue)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetentionService.exe");

            //SAVE ALL OF THE SETTINGS
            config.AppSettings.Settings.Remove(strProperty);
            config.AppSettings.Settings.Add(strProperty, strValue);

            // Save the config file.
            config.Save(ConfigurationSaveMode.Modified);

            // Force a reload of a changed section. 
            ConfigurationManager.RefreshSection("appSettings");


        }

        /// <summary>
        /// Gets Application Setting from BackupRetentionService.exe.config
        /// </summary>
        /// <param name="strProperty"></param>
        /// <returns></returns>
        public string GetAppSetting(string strProperty)
        {
            try
            {
                string strValue = "";
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetentionService.exe");
                //System.Configuration.AppSettingsSection appsettings = config.AppSettings;
                //return appsettings.Settings[strProperty].Value.ToString();
                //return config.AppSettings[strProperty];
                strValue = config.AppSettings.Settings[strProperty].Value.ToString();
                return strValue;
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// On Form Load Event Handler
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible       = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        /// <summary>
        /// Close Tray Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExit(object sender, EventArgs e)
        {
            //CloseBackupRetentionSystemTray();

            trayMenu.Dispose();
            trayIcon.Dispose();
            trayIcon = null;
            System.Environment.Exit(0);
        }

        private delegate void d_showTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon);
        

        public void ShowBalloonTip(int timeout,string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            
            if (this.InvokeRequired)
            {
                d_showTip d = new d_showTip(ShowBalloonTip);

                this.Invoke(d,new object[] {timeout,tipTitle,tipText,tipIcon});
            }
            else
            {
                trayIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
                Application.DoEvents();
                
            }

        
        }

        private delegate void d_WriteEntry(string message);
        private void WriteEntry(string message)
        {
            if (this.InvokeRequired)
            {
                d_WriteEntry d = new d_WriteEntry(WriteEntry);

                this.Invoke(d, new object[] { message });
            }
            else
            {
                _evt.WriteEntry(message);
            }
        }

        /// <summary>
        /// Restarts or Starts BackupRetentionService
        /// </summary>
        /// <returns></returns>
        private bool Restart()
        {
            bool blSuccess = false;
            string strStatus = "";
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
            try
            {
                strStatus = sc.Status.ToString();
                
                //Restart
                if (strStatus == "Running")
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    GetServiceStatus();
                    Application.DoEvents();
                    if (sc.Status.ToString() == "Stopped")
                    {
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        if (sc.Status.ToString() == "Running")
                        {
                            ShowBalloonTip(5000, "Restart", "BackupRetentionService Restarted Successfully", ToolTipIcon.Info);
                            blSuccess = true;
                        }
                        else
                        {
                            ShowBalloonTip(5000, "Restart", "BackupRetentionService Restart Timed Out", ToolTipIcon.Error);
                        }
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Restart Failed", "BackupRetentionService did not stop in time to start back up", ToolTipIcon.Error);
                    }
                } //Already Stopped Just Start
                else if (strStatus == "Stopped")
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    if (sc.Status.ToString() == "Running")
                    {
                        ShowBalloonTip(5000, "Start", "BackupRetentionService Started Successfully", ToolTipIcon.Info);
                        blSuccess = true;
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Start", "BackupRetentionService Start Timed Out", ToolTipIcon.Error);
                    }
                }
                else
                {
                    ShowBalloonTip(5000, "Restart Failed", "BackupRetentionService is not in a proper state to restart", ToolTipIcon.Error);

                }
            }
            catch (Exception ex)
            {
                ShowBalloonTip(5000, "Restart Failed", "Restart of BackupRetentionService Failed!", ToolTipIcon.Error);
                WriteEntry(ex.Message);
            }
            ////GetServiceStatus();
            return blSuccess;
        }


        /// <summary>
        /// OnRestart Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRestart(object sender, EventArgs e)
        {
            //Restart();
            backgroundWorker1.RunWorkerAsync("Restart");
        }

        /// <summary>
        /// Stops BackupRetentionService
        /// </summary>
        /// <returns></returns>
        private bool Stop()
        {
            bool blSuccess = false;
            string strStatus = "";
            TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
            try
            {
                strStatus = sc.Status.ToString();

                if (strStatus == "Running")
                {
                    sc.Stop();

                    sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                    if (sc.Status.ToString() == "Stopped")
                    {
                        ShowBalloonTip(5000, "Stop", "BackupRetentionService Stopped Successfully", ToolTipIcon.Info);
                        blSuccess = true;
                    }
                    else
                    {
                        ShowBalloonTip(5000, "Stop Failed", "BackupRetentionService timed out stopping.", ToolTipIcon.Error);
                    }
                }
                else if (strStatus == "Stopped")
                {
                    ShowBalloonTip(5000, "Stop", "BackupRetentionService Already Stopped", ToolTipIcon.Info);
                    blSuccess = true;
                }
                else
                {
                    ShowBalloonTip(5000, "Stop Failed", "BackupRetentionService is not in a proper state to stop.", ToolTipIcon.Error);

                }
            }
            catch (Exception ex)
            {
                ShowBalloonTip(5000, "Stop Failed", "Stop of BackupRetentionService Failed.", ToolTipIcon.Error);
                WriteEntry(ex.Message);
            }
            //GetServiceStatus();
            return blSuccess;
        }

        /// <summary>
        /// OnStop Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStop(object sender, EventArgs e)
        {
            //Stop();
            backgroundWorker1.RunWorkerAsync("Stop");
        }


        public void RefreshEventsTab()
        {
            Application.DoEvents();
            //Refresh Events
            dsEvents = new DataSet("EventLog");
            DataTable dtEvents = new DataTable("Events");
            dtEvents.Columns.Add(new DataColumn("Type", typeof(String)));
            dtEvents.Columns.Add(new DataColumn("EventImage", typeof(System.Drawing.Image)));
            dtEvents.Columns.Add(new DataColumn("Time", typeof(System.DateTime)));
            dtEvents.Columns.Add(new DataColumn("Message", typeof(String)));
            dtEvents.Columns.Add(new DataColumn("Source", typeof(String)));
            dtEvents.Columns.Add(new DataColumn("Category", typeof(String)));
            dtEvents.Columns.Add(new DataColumn("EventID", typeof(long)));
            dsEvents.Tables.Add(dtEvents);

            foreach (System.Diagnostics.EventLogEntry entry in eventLogBackupRetention.Entries)
            {
                if (entry.Source == "BackupRetention")
                {
                    AddEventLogEntry(entry);
                }
            }
            bs = new BindingSource(dsEvents, "Events");
            string strFilter = "";
            if (!Common.FixNullbool(chkError.Checked))
            {
                strFilter = "Type<>'Error'";
            }
            if (!Common.FixNullbool(chkWarning.Checked))
            {
                if (strFilter.Length > 0)
                {
                    strFilter += " AND Type<>'Warning'";
                }
                else
                {
                    strFilter = "Type<>'Warning'";
                }
            }
            if (!Common.FixNullbool(chkInformation.Checked))
            {
                if (strFilter.Length > 0)
                {
                    strFilter += " AND Type<>'Information'";
                }
                else
                {
                    strFilter = "Type<>'Information'";
                }
            }

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                if (strFilter.Length > 0)
                {
                    strFilter += " AND Message LIKE '%" + txtSearch.Text +  "%'";
                }
                else
                {
                    strFilter = "Message LIKE '%" + txtSearch.Text + "%'";
                }
            }

            if (strFilter.Length > 0)
            {
                bs.Filter = strFilter; //+ " AND Source='BackupRetention'";
            }

            
            bs.Sort = "Time DESC";
            dgvEvents.DataSource = bs;
            Application.DoEvents();
            GetServiceStatus();
            Application.DoEvents();
        }

        /// <summary>
        /// Shows Settings Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowForm(object sender, EventArgs e)
        {
            GetServiceStatus();
            Visible = true;
            WindowState = FormWindowState.Normal;
            RefreshEventsTab();
        }

        /// <summary>
        /// Hides the settings form instead of closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupRetentionSystemTray_FormClosing(object sender, FormClosingEventArgs e)
        {
                this.Hide();
                e.Cancel = true; // this cancels the close event.
        }

        /// <summary>
        /// On Minimize Hides the settings form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupRetentionSystemTray_SizeChanged(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
            }
        }

        

        
        /// <summary>
        /// Validates Time in string format
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private bool ValidateTime(string strValue)
        {
            try
            {
                if (!string.IsNullOrEmpty(strValue))
                {
                    if (!(strValue.Replace(":", "").Trim() == ""))
                    {
                        TimeSpan timeStart;
                        timeStart = TimeSpan.Parse(strValue);
                        
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        


        /// <summary>
        /// Service Interval text box validation
        /// </summary>
        /// <returns></returns>
        private bool ValidateServiceInterval()
        {
            string strValue = txtServiceInterval.Text;
            try
            {
                long lValue = 0;
                lValue = long.Parse(strValue);
                if (lValue > 1209600000 || lValue < 1000)
                {
                    throw new Exception("Time in milliseconds too small or too large");
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Max Drive Space Used Percent masked text box validation
        /// </summary>
        /// <returns></returns>
        private bool ValidateMaxDriveSpaceUsed()
        {
            int intValue = 0;
            int.TryParse(mtxtMaxDriveSpaceUsed.Text, out intValue);

            //Value between 1-99
            if (intValue < 100 && intValue >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// txt Service Interval validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServiceInterval_Validating(object sender, CancelEventArgs e)
        {
            string strValue = txtServiceInterval.Text;
            try
            {
                long lValue = 0;
                lValue = long.Parse(strValue);
                if (lValue > 1209600000 || lValue < 1000)
                {
                    throw new Exception("Time in milliseconds too small or too large");

                }
                else
                {
                    lblServiceIntervalMinutes.Text = (Math.Round(((double) lValue) / 1000 / 60,2)).ToString() + " Minutes";
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                MessageBox.Show("Incorrect time in milliseconds greater than or equal 1 seconds and less than two weeks", "BackupRetentionSystemTray", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        /// <summary>
        /// txtService
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServiceInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Please enter number only..."); 
                e.Handled = true;
            }
        }

        /// <summary>
        /// btnSave Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Save Procedure
        /// </summary>
        private void Save()
        {
            try
            {
                if (SaveValidated())
                {
                    long lValue = 0;
                    double dblValue = 0;

                    long.TryParse(txtServiceInterval.Text, out lValue);
                    ServiceInterval = lValue;
                    if (string.IsNullOrEmpty(mtxtMaxDriveSpaceUsed.Text))
                    {
                        MaxDriveSpaceUsedPercent = 90.0 / 100.0;
                    }
                    else
                    {
                        double.TryParse(mtxtMaxDriveSpaceUsed.Text, out dblValue);
                        MaxDriveSpaceUsedPercent = dblValue / 100;
                    }

                    SaveAppSetting("ServiceInterval", _serviceInterval.ToString());
                    SaveAppSetting("MaxDriveSpaceUsedPercent", _maxDriveSpaceUsedPercent.ToString());


                    //Save Configuration XML Files
                    dtSyncConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\SyncConfig.xml");
                    dtRetentionConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RetentionConfig.xml");
                    dtCompressConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CompressConfig.xml");
                    dtRemoteConfig.WriteXml(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RemoteConfig.xml");


                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
            }
            
            
            
        }

        /// <summary>
        /// Save Validated Function
        /// </summary>
        /// <returns></returns>
        private bool SaveValidated()
        {
            bool blOkToSave = true;

            if (!ValidateServiceInterval())
            {
                blOkToSave = false;
            }
            if (!ValidateMaxDriveSpaceUsed())
            {
                blOkToSave = false;
            }

            return blOkToSave;

        }


        /// <summary>
        /// btnSaveApply Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveApply_Click(object sender, EventArgs e)
        {
            Save();
            //Restart();
            backgroundWorker1.RunWorkerAsync("Restart");
        }

        /// <summary>
        /// Form Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupRetentionSystemTray_Load(object sender, EventArgs e)
        {
            //Set Tool Tips
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnSaveApply, "Saves Settings and Restarts Backup Retention Service");

            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.txtServiceInterval, "Time in milliseconds for the service to execute repeatedly");
            ToolTip2.SetToolTip(this.lblServiceInterval, "Time in milliseconds for the service to execute repeatedly");

            System.Windows.Forms.ToolTip ToolTip3 = new System.Windows.Forms.ToolTip();
            ToolTip3.SetToolTip(this.mtxtMaxDriveSpaceUsed, "Max Drive Spaced Used Percent if reached will stop the syncing process.  \nCompressing a file will fail if double the amount of space for the file is not available.");
            ToolTip3.SetToolTip(this.lblMaxDriveSpaceUsed, "Max Drive Spaced Used Percent if reached will stop the syncing process.  \nCompressing a file will fail if double the amount of space for the file is not available.");
            //trayIcon.Visible = false;
        }

        /// <summary>
        /// Cell Event Argument IsNumeric validation
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CellEventArgIsNumeric(ref DataGridViewCellValidatingEventArgs e)
        {

            bool blIsNumeric = true;
            try
            {
                char[] chars = e.FormattedValue.ToString().ToCharArray();
                foreach (char c in chars)
                {
                    if (char.IsDigit(c) == false)
                    {
                        blIsNumeric = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                blIsNumeric = false;
            }
            return blIsNumeric;
        }

        /// <summary>
        /// Cell Event Argument IsNumeric validation
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CellEventArgIsNumeric2(ref DataGridViewCellValidatingEventArgs e)
        {

            bool blIsNumeric = true;
            int i = 0;
            try
            {
                char[] chars = e.FormattedValue.ToString().ToCharArray();
                foreach (char c in chars)
                {
                    i++;
                    if (char.IsDigit(c) == false && !(c =='-' && i == 1))
                    {
                        blIsNumeric = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                blIsNumeric = false;
            }
            return blIsNumeric;
        }


        /// <summary>
        /// Cell Event Argument IsTime validation
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool CellEventArgIsTime(ref DataGridViewCellValidatingEventArgs e)
        {

            bool blIsNumeric = true;
            try
            {
                char[] chars = e.FormattedValue.ToString().ToCharArray();
                foreach (char c in chars)
                {
                    if (char.IsDigit(c) == false && c != ':')
                    {
                        blIsNumeric = false;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                blIsNumeric = false;
            }
            return blIsNumeric;
        }

        /// <summary>
        /// dgvRetention cell validation event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRetention_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvRetention.Columns[e.ColumnIndex].HeaderText == "BackupFolder")
                {
                    DataGridViewTextBoxCell cell = dgvRetention[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (!Directory.Exists(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("BackupFolder Path does not exist or permission problem.");
                            e.Cancel = true;
                        }
                    }
                }
                else if (dgvRetention.Columns[e.ColumnIndex].HeaderText == "MinFileCount" || dgvRetention.Columns[e.ColumnIndex].HeaderText == "DailyMaxDaysOld" || dgvRetention.Columns[e.ColumnIndex].HeaderText == "WeeklyMaxDaysOld" || dgvRetention.Columns[e.ColumnIndex].HeaderText == "MonthlyMaxDaysOld")
                {
                    if (!CellEventArgIsNumeric(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
                else if (dgvRetention.Columns[e.ColumnIndex].HeaderText == "DayOfMonth")
                {
                    if (!CellEventArgIsNumeric2(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                
            }
            


        }



        /// <summary>
        /// dgvCompress Cell validation event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompress_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "SourceFolder" || dgvCompress.Columns[e.ColumnIndex].HeaderText == "DestinationFolder")
                {
                    DataGridViewTextBoxCell cell = dgvCompress[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (!Directory.Exists(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("Folder Path does not exist or permission problem.");
                            e.Cancel = true;
                        }
                    }
                }
                else if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "StartCompressingAfterDays")
                {
                    if (!CellEventArgIsNumeric(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
                else if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "DayOfMonth")
                {
                    if (!CellEventArgIsNumeric2(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {

                _evt.WriteEntry(ex.Message);
            }
            
           
            
        }

        /// <summary>
        /// dgvSync cell validation event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSync_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvSync.Columns[e.ColumnIndex].HeaderText == "SourceFolder" || dgvSync.Columns[e.ColumnIndex].HeaderText == "DestinationFolder" || dgvSync.Columns[e.ColumnIndex].HeaderText == "ArchiveFolder")
                {
                    DataGridViewTextBoxCell cell = dgvSync[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (!Directory.Exists(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show("BackupFolder Path does not exist or permission problem.");
                            e.Cancel = true;
                        }
                    }
                    else if (dgvSync.Columns[e.ColumnIndex].HeaderText == "DayOfMonth")
                    {
                        if (!CellEventArgIsNumeric2(ref e))
                        {
                            MessageBox.Show("You have to enter numbers only");
                            e.Cancel = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
            }
            

        }

        /// <summary>
        /// dgvRemote Cell Validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRemote_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "BackupFolder" || dgvRemote.Columns[e.ColumnIndex].HeaderText == "KeyFileDirectory")
                {
                    DataGridViewTextBoxCell cell = dgvRemote[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                    if (cell != null)
                    {
                        if (!Directory.Exists(e.FormattedValue.ToString()) || e.FormattedValue.ToString() == "")
                        {
                            if (!(dgvRemote.Columns[e.ColumnIndex].HeaderText == "KeyFileDirectory" && e.FormattedValue.ToString() == ""))
                            {
                                MessageBox.Show("Folder Path does not exist or permission problem.");
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "Port" || dgvRemote.Columns[e.ColumnIndex].HeaderText == "Timeout")
                {
                    //validates text boxes that are supposed to be numeric
                    if (!CellEventArgIsNumeric(ref e))
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
                else if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "Time")
                {
                    //validates text boxes that are supposed to be a military time 00:00
                    if (!CellEventArgIsTime(ref e))
                    {
                        MessageBox.Show("You have to enter numbers or a colon only");
                        e.Cancel = true;
                    }
                }
                else if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "DayOfMonth")
                {
                    if (!CellEventArgIsNumeric2(ref e) )
                    {
                        MessageBox.Show("You have to enter numbers only");
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                
            }
            
            
        }
        
        /// <summary>
        /// btnStop Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            //Stop();
            backgroundWorker1.RunWorkerAsync("Stop");
        }

        /// <summary>
        /// btnStart Click Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Restart();
            backgroundWorker1.RunWorkerAsync("Restart");
        }

        /// <summary>
        /// Gets BackupRetentionService Status and sets enabled on buttons and label text
        /// </summary>
        private void GetServiceStatus()
        {
            try
            {
                sc.Refresh();
                string strStatus = sc.Status.ToString();
                txtServiceStatus.Text = strStatus;
                if (strStatus == "Running" || strStatus == "StartPending")
                {

                    btnStart.Enabled = false;
                    trayMenu.MenuItems[1].Enabled = false;
                    trayMenu.MenuItems[2].Enabled = true;
                    trayMenu.MenuItems[3].Enabled = true;
                    
                    btnStop.Enabled = true;
                    
                    txtServiceStatus.BackColor = Color.DarkGreen;
                }
                else if (strStatus == "Stopped" || strStatus == "StopPending")
                {
                    trayMenu.MenuItems[1].Enabled = true;
                    trayMenu.MenuItems[2].Enabled = false;
                    trayMenu.MenuItems[3].Enabled = false;
                    
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    txtServiceStatus.BackColor = Color.DarkRed;
                }
                else
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = true;
                    trayMenu.MenuItems[1].Enabled = true;
                    trayMenu.MenuItems[2].Enabled = true;
                    trayMenu.MenuItems[3].Enabled = true;
                    txtServiceStatus.BackColor = Color.DarkRed;
                }
            }
            catch (Exception ex)
            {

                _evt.WriteEntry(ex.Message);
            }
            

        }

        /// <summary>
        /// Cell FolderBrowser Call 
        /// </summary>
        /// <param name="cell"></param>
        private void CellFolderBrowser(ref DataGridViewTextBoxCell cell)
        {
            try
            {
                if (cell != null)
                {
                    if (Directory.Exists(cell.Value.ToString()))
                    {
                        //FolderBrowserD.RootFolder =  Environment.SpecialFolder.MyComputer;
                        FolderBrowserD.SelectedPath = cell.Value.ToString();
                    }
                    if (FolderBrowserD.ShowDialog() == DialogResult.OK)
                    {
                        //cell.Value = FolderBrowserD.SelectedPath.Replace("\\", "\\\\");
                        cell.Value = FolderBrowserD.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                
            }
           
        }


        /// <summary>
        /// dgvRetention double click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRetention_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRetention.Columns[e.ColumnIndex].HeaderText == "BackupFolder")
            {
                DataGridViewTextBoxCell cell = dgvRetention[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                CellFolderBrowser(ref cell);
                dgvRetention.RefreshEdit();
            }
        }

        /// <summary>
        /// dgvCompress Double click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "SourceFolder" || dgvCompress.Columns[e.ColumnIndex].HeaderText == "DestinationFolder")
            {
                DataGridViewTextBoxCell cell = dgvCompress[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                CellFolderBrowser(ref cell);
                dgvCompress.RefreshEdit();
            }
        }

        /// <summary>
        /// dgvSync Folders Double Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSync_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSync.Columns[e.ColumnIndex].HeaderText == "SourceFolder" || dgvSync.Columns[e.ColumnIndex].HeaderText == "DestinationFolder" || dgvSync.Columns[e.ColumnIndex].HeaderText == "ArchiveFolder")
            {
                DataGridViewTextBoxCell cell = dgvSync[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                CellFolderBrowser(ref cell);
                dgvSync.RefreshEdit();
            }
        }

        /// <summary>
        /// dgvRemote Folders Double Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRemote_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "BackupFolder" || dgvRemote.Columns[e.ColumnIndex].HeaderText == "KeyFileDirectory")
            {
                DataGridViewTextBoxCell cell = dgvRemote[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                CellFolderBrowser(ref cell);
                dgvRemote.RefreshEdit();
            }
        }


        /// <summary>
        /// Help Menu About Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FrmAboutBox == null)
            {
                FrmAboutBox = new AboutBox();
                FrmAboutBox.Show();
            }
            else
            {
                FrmAboutBox.Dispose();
                FrmAboutBox = new AboutBox();
                FrmAboutBox.Show();
            }
        }

        /// <summary>
        /// File Menu Exit Event Handler - Hide instead of close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Adds event log entry into row for datatable
        /// </summary>
        /// <param name="entry"></param>
        private void AddEventLogEntry(System.Diagnostics.EventLogEntry entry)
        {

            try
            {
                DataRow row = dsEvents.Tables["Events"].NewRow();
                if (entry.EntryType == 0)
                {
                    row["Type"] = "Information";
                }
                else
                {
                    row["Type"] = Enum.GetName(typeof(System.Diagnostics.EventLogEntryType), entry.EntryType).ToString();
                }
                switch (Common.FixNullstring(row["Type"]))
                {
                    case "Error":
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgError;
                        break;
                    case "Warning":
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgWarning;
                        break;
                    case "Information":
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgInformation;
                        break;
                    default:
                        row["EventImage"] = (System.Drawing.Image)Properties.Resources.ImgInformation;
                        break;
                }
                row["Time"] = entry.TimeGenerated;
                row["Message"] = entry.Message;
                row["Source"] = entry.Source;
                row["Category"] = entry.Category;
                row["EventID"] = entry.InstanceId;
                dsEvents.Tables["Events"].Rows.Add(row);
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
            }
            
        }

        /// <summary>
        /// Event Log EntryWritten Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventLogBackupRetention_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            AddEventLogEntry(e.Entry);
        }

        

        /// <summary>
        /// dgvRemote Cell Formatting Event makes Password text box use asterisks for display purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRemote_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "Password")
            {
                if(e.Value != null)
                {
                    //Replace displayed text with asterisks
                    e.Value = new String('*', e.Value.ToString().Length);
                }
            }
        }

        /// <summary>
        /// dgvCompress Cell Formatting Event makes Password text box use asterisks for display purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompress_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "EncryptionPassword")
            {
                if (e.Value != null)
                {
                    //Replace displayed text with asterisks
                    e.Value = new String('*', e.Value.ToString().Length);
                }
            }
        }

        /// <summary>
        /// dgvRemote Cell End Edit Event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRemote_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "Password")
            {
                DataGridViewTextBoxCell cell = dgvRemote[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                AES256 aes = new AES256(ep);
                if (Common.FixNullstring(cell.Value).Length > 0)
                {
                    //Encrypt the Password
                    cell.Value = aes.Encrypt(cell.Value.ToString());
                }
            }
        }

        /// <summary>
        /// dgvCompress Cell End Edit Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompress_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "EncryptionPassword")
            {
                DataGridViewTextBoxCell cell = dgvCompress[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;
                AES256 aes = new AES256(ep);
                if (Common.FixNullstring(cell.Value).Length > 0)
                {
                    //Encrypt the password
                    cell.Value = aes.Encrypt(cell.Value.ToString());
                }
            }
        }

        /// <summary>
        /// dgvRemote Cell Begin Edit event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRemote_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvRemote.Columns[e.ColumnIndex].HeaderText == "Password")
            {
                //Clear value so that same password is not encrypted twice and so that encrypted password is not edited
                dgvRemote[e.ColumnIndex, e.RowIndex].Value = "";
                dgvRemote.RefreshEdit();
            }
        }

        /// <summary>
        /// dgvCompress Cell Begin Edit event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompress_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvCompress.Columns[e.ColumnIndex].HeaderText == "EncryptionPassword")
            {
                //Clear value so that same password is not encrypted twice and so that encrypted password is not edited
                dgvCompress[e.ColumnIndex, e.RowIndex].Value = "";
                dgvCompress.RefreshEdit();
            }
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            _evt.Clear();
            dsEvents.Tables["Events"].Rows.Clear();
            _evt.WriteEntry("BackupRe Event Log Cleared.");
        }

        private void btnRefreshEventLog_Click(object sender, EventArgs e)
        {
            RefreshEventsTab();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string strTask = Common.FixNullstring(e.Argument);
            switch (strTask)
            {
                case "Stop":
                    Stop();
                    break;
                case "Restart":
                    Restart();
                    break;

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.DoEvents();
            GetServiceStatus();
            Application.DoEvents();
        }

        

        

        

        

        

        
        

        
        

        
        
        
        

        
    }
}

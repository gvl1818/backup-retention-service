using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace BackupRetention
{
    /// <summary>
    /// Retention Plan folder Class
    /// </summary>
    public class RetentionFolder: IFolderConfig
    {
        #region "Variables"
        public System.Collections.Generic.List<System.IO.FileInfo> AllFiles;
        public System.Collections.Generic.List<System.IO.FileInfo> FilesDeleted;
        public System.Diagnostics.EventLog _evt;
        #endregion

        #region "Properties"

        private int _id = 1;
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }

        }

        private string _title = "";
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }

        }

        private string _time = "";
        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }

        }

        private string _endTime = "";
        public string EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
            }

        }

        private IntervalTypes _intervalType = IntervalTypes.Daily;
        public IntervalTypes IntervalType
        {
            get
            {
                return _intervalType;
            }

            set
            {
                _intervalType = value;
            }

        }

        private long _interval = 0;
        public long Interval
        {
            get
            {
                return _interval;
            }

            set
            {
                _interval = value;
            }

        }

        private bool _monday = false;
        public bool Monday
        {
            get
            {
                return _monday;
            }
            set
            {
                _monday = value;
            }
        }

        private bool _tuesday = false;
        public bool Tuesday
        {
            get
            {
                return _tuesday;
            }
            set
            {
                _tuesday = value;
            }
        }

        private bool _wednesday = false;
        public bool Wednesday
        {
            get
            {
                return _wednesday;
            }
            set
            {
                _wednesday = value;
            }
        }

        private bool _thursday = false;
        public bool Thursday
        {
            get
            {
                return _thursday;
            }
            set
            {
                _thursday = value;
            }
        }

        private bool _friday = false;
        public bool Friday
        {
            get
            {
                return _friday;
            }
            set
            {
                _friday = value;
            }
        }

        private bool _saturday = false;
        public bool Saturday
        {
            get
            {
                return _saturday;
            }
            set
            {
                _saturday = value;
            }
        }

        private bool _sunday = false;
        public bool Sunday
        {
            get
            {
                return _sunday;
            }
            set
            {
                _sunday = value;
            }
        }

        private bool _enabled = false;
        /// <summary>
        /// Retention Plan Enabled for this folder?
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
            }
        }

        private string _comment = "";
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
            }

        }

        public static string _backupFolder = "";
        /// <summary>
        /// Main Backup folder to delete files out of.
        /// </summary>
        public string BackupFolder
        {
            get
            {
                return _backupFolder;
            }
            set
            {
                _backupFolder = Common.WindowsPathClean(value);
            }
        }

        public static int _minFileCount = 10;
        /// <summary>
        /// Minimum file count must be exceeded before deletions of the Retention plan will occur.
        /// </summary>
        public int MinFileCount
        {
            get
            {
                return _minFileCount;
            }
            set
            {
                _minFileCount = value;
            }
        }

        public DayOfWeek _dayOfWeekToKeep = DayOfWeek.Monday;
        /// <summary>
        /// Day Of the Week to not delete for Weekly's and Monthly's
        /// </summary>
        public DayOfWeek DayOfWeekToKeep
        {
            get
            {
                //_dayOfWeekToKeep = (DayOfWeek)System.Enum.Parse(typeof(DayOfWeek), str);
                return _dayOfWeekToKeep;
            }
            set
            {
                _dayOfWeekToKeep = value;
            }
        }

        public static int _dailyMaxDaysOld = 31;
        public int DailyMaxDaysOld
        {
            get
            {
                return _dailyMaxDaysOld;
            }
            set
            {
                _dailyMaxDaysOld = value;
            }
        }


        public static int _weeklyMaxDaysOld = 31;
        public int WeeklyMaxDaysOld
        {
            get
            {
                return _weeklyMaxDaysOld;
            }
            set
            {
                _weeklyMaxDaysOld = value;
            }
        }


        public static int _monthlyMaxDaysOld = 31;
        public int MonthlyMaxDaysOld
        {
            get
            {
                return _monthlyMaxDaysOld;
            }
            set
            {
                _monthlyMaxDaysOld = value;
            }
        }

        public static string _fileNameFilter = "";
        public string FileNameFilter
        {
            get
            {
                return _fileNameFilter;
            }
            set
            {
                _fileNameFilter = value;
            }
        }


        public RetentionAlgorithmOptions _retentionAlgorithm = RetentionAlgorithmOptions.KeepDaily;
        /// <summary>
        /// Retention Algorithm to use for deleting old backups
        /// GFS Grandfather, Father, Son (Daily's, Weekly's, Monthly's kept until MaxDaysOld for each),KeepAll (No Deleting),KeepDaily (Keep Daily's up to DailyMaxDaysOld),KeepWeekly (Keep Weekly's up to WeeklyMaxDaysOld),KeepMonthly (Keep Monthly's up to MonthlyMaxDaysOld)
        /// </summary>
        public RetentionAlgorithmOptions RetentionAlgorithm
        {
            get
            {
                //_retentionAlgorithm = (RetentionAlgorithmOptions)System.Enum.Parse(typeof(RetentionAlgorithmOptions), str);
                /*
                    ,GFS=0
                    ,KeepAll=1
                    ,KeepDaily=2
                    ,KeepWeekly=3
                    ,KeepMonthly=4
                 */
                return _retentionAlgorithm;
            }
            set
            {
                _retentionAlgorithm = value;
            }
        }
        #endregion

        #region "Methods"

        /// <summary>
        /// RetentionFolder Class initializtion for contructors
        /// </summary>
        private void init_BackupFolder()
        {
            AllFiles = new System.Collections.Generic.List<System.IO.FileInfo>();
            FilesDeleted = new System.Collections.Generic.List<System.IO.FileInfo>();
            _evt = Common.GetEventLog;
        }

        /// <summary>
        /// RetentionFolder Class Contructor
        /// </summary>
        public RetentionFolder()
        {
            init_BackupFolder();
        }




        /// <summary>
        /// RetentionFolder Class Contructor
        /// </summary>
        /// <param name="row"></param>
        public RetentionFolder(DataRow row)
        {
            init_BackupFolder();
            string str = "";

            ID = Common.FixNullInt32(row["ID"]);
            Enabled = Common.FixNullbool(row["Enabled"]);
            Time = Common.FixNullstring(row["Time"]);
            EndTime = Common.FixNullstring(row["EndTime"]);
            try
            {
                IntervalType = (IntervalTypes)System.Enum.Parse(typeof(IntervalTypes), Common.FixNullstring(row["IntervalType"]));
            }
            catch (Exception)
            {
                IntervalType = IntervalTypes.Daily;
            }
            Interval = Common.FixNulllong(row["Interval"]);
            Monday = Common.FixNullbool(row["Monday"]);
            Tuesday = Common.FixNullbool(row["Tuesday"]);
            Wednesday = Common.FixNullbool(row["Wednesday"]);
            Thursday = Common.FixNullbool(row["Thursday"]);
            Friday = Common.FixNullbool(row["Friday"]);
            Saturday = Common.FixNullbool(row["Saturday"]);
            Sunday = Common.FixNullbool(row["Sunday"]);
            
            BackupFolder = Common.FixNullstring(row["BackupFolder"]);
            MinFileCount = Common.FixNullInt32(row["MinFileCount"]);
            str = Common.FixNullstring(row["DayOfWeekToKeep"]);
            try
            {
                DayOfWeekToKeep = (DayOfWeek)System.Enum.Parse(typeof(DayOfWeek), str);
            }
            catch (Exception)
            {
                DayOfWeekToKeep = (DayOfWeek)System.Enum.Parse(typeof(DayOfWeek), "Monday");
            }

            DailyMaxDaysOld = Common.FixNullInt32(row["DailyMaxDaysOld"]);
            WeeklyMaxDaysOld = Common.FixNullInt32(row["WeeklyMaxDaysOld"]);
            MonthlyMaxDaysOld = Common.FixNullInt32(row["MonthlyMaxDaysOld"]);
            FileNameFilter = Common.FixNullstring(row["FileNameFilter"]);
            //RetentionAlgorithm
            str = Common.FixNullstring(row["RetentionAlgorithm"]);
            try
            {
                RetentionAlgorithm = (RetentionAlgorithmOptions)System.Enum.Parse(typeof(RetentionAlgorithmOptions), str);
            }
            catch (Exception)
            {
                RetentionAlgorithm = (RetentionAlgorithmOptions)System.Enum.Parse(typeof(RetentionAlgorithmOptions), "GFS");
            }

            Title = Common.FixNullstring(row["Title"]);
            Comment = Common.FixNullstring(row["Comment"]);
        }

        /// <summary>
        /// RetentionFolder Class Destructor
        /// </summary>
        ~RetentionFolder()
        {
            AllFiles.Clear();
            FilesDeleted.Clear();
            AllFiles = null;
            FilesDeleted = null;
            _evt = null;
        }

        /// <summary>
        /// Retention Configuration Table initialization
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtRetentionConfig;
            dtRetentionConfig = new DataTable("RetentionConfig");

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtRetentionConfig.Columns.Add(dcID);
            columns[0] = dtRetentionConfig.Columns["ID"];
            dtRetentionConfig.PrimaryKey = columns;


            //Create Columns
            dtRetentionConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Title", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("BackupFolder", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("MinFileCount", typeof(Int32)));
            dtRetentionConfig.Columns.Add(new DataColumn("DayOfWeekToKeep", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("DailyMaxDaysOld", typeof(Int32)));
            dtRetentionConfig.Columns.Add(new DataColumn("WeeklyMaxDaysOld", typeof(Int32)));
            dtRetentionConfig.Columns.Add(new DataColumn("MonthlyMaxDaysOld", typeof(Int32)));
            dtRetentionConfig.Columns.Add(new DataColumn("RetentionAlgorithm", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("FileNameFilter", typeof(String)));
            dtRetentionConfig.Columns.Add(new DataColumn("Comment", typeof(String)));

            dtRetentionConfig.Columns["Enabled"].DefaultValue = "true";
            dtRetentionConfig.Columns["Time"].DefaultValue = "01:00";
            dtRetentionConfig.Columns["IntervalType"].DefaultValue = "Daily";
            dtRetentionConfig.Columns["Interval"].DefaultValue = "0";
            dtRetentionConfig.Columns["Monday"].DefaultValue = "true";
            dtRetentionConfig.Columns["Tuesday"].DefaultValue = "true";
            dtRetentionConfig.Columns["Wednesday"].DefaultValue = "true";
            dtRetentionConfig.Columns["Thursday"].DefaultValue = "true";
            dtRetentionConfig.Columns["Friday"].DefaultValue = "true";
            dtRetentionConfig.Columns["Saturday"].DefaultValue = "true";
            dtRetentionConfig.Columns["Sunday"].DefaultValue = "true";
            
            dtRetentionConfig.Columns["MinFileCount"].DefaultValue = 10;
            dtRetentionConfig.Columns["DayOfWeekToKeep"].DefaultValue = "Monday";
            dtRetentionConfig.Columns["DailyMaxDaysOld"].DefaultValue = 7;
            dtRetentionConfig.Columns["WeeklyMaxDaysOld"].DefaultValue = 31;
            dtRetentionConfig.Columns["MonthlyMaxDaysOld"].DefaultValue = 62;
            dtRetentionConfig.Columns["RetentionAlgorithm"].DefaultValue = "GFS";
            return dtRetentionConfig;
        }

        /// <summary>
        /// Determines which Retention algorithm is specified per RetentionAlgorithm
        /// </summary>
        public void Execute(ref bool blShuttingDown)
        {
            try
            {
                if (Enabled)
                {
                    switch (RetentionAlgorithm)
                    {
                        case RetentionAlgorithmOptions.GFS:
                            GFS(ref blShuttingDown);
                            break;
                        case RetentionAlgorithmOptions.KeepAll:
                            KeepAll(ref blShuttingDown);
                            break;
                        case RetentionAlgorithmOptions.KeepDaily:
                            KeepDaily(ref blShuttingDown);
                            break;
                        case RetentionAlgorithmOptions.KeepWeekly:
                            KeepWeekly(ref blShuttingDown);
                            break;
                        case RetentionAlgorithmOptions.KeepMonthly:
                            KeepMonthly(ref blShuttingDown);
                            break;
                        default:
                            KeepDaily(ref blShuttingDown);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                _evt.WriteEntry(ex.Message);
            }


        }

        /// <summary>
        /// "Grand Father, Father, Son" Retention Plan 
        /// </summary>
        public void GFS(ref bool blShuttingDown)
        {
            int intFilesNotToCount = 0;
            try
            {
                if (RetentionAlgorithm == RetentionAlgorithmOptions.KeepAll)
                {
                    _evt.WriteEntry("Retention Logging Only Mode: GFS Starting: ", System.Diagnostics.EventLogEntryType.Information,6001, 60);
                }
                else
                {
                    _evt.WriteEntry("Retention: GFS Starting: ", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                }
                AllFiles.Clear();
                FilesDeleted.Clear();
                


                AllFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);

                if (AllFiles != null)
                {

                    foreach (System.IO.FileInfo file1 in AllFiles)
                    {
                        if (blShuttingDown)
                        {
                            throw new Exception("Shutting Down");
                        }
                        try
                        {
                           
                            intFilesNotToCount = 0;

                            DateTime Today = DateTime.Now;
                            DateTime FileDate;

                            if (blShuttingDown)
                            {
                                Exception ex1 = new Exception("Retention: Shutting Down, about to GFS: " + file1.FullName);
                                _evt.WriteEntry("Retention: Shutting Down, about to GFS: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                                return;
                            }

                            try
                            {
                                FileDate = file1.LastWriteTime;
                                if (FileDate == DateTime.MinValue || FileDate == DateTime.MaxValue)
                                {
                                    Exception exc = new Exception("Retention: Failed to get Last Modified DateTime for file: " + file1.FullName);
                                    throw exc;
                                }
                            }
                            catch (Exception ex)
                            {
                                _evt.WriteEntry("Retention: GFS: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 6000, 60);

                                continue;
                            }

                            TimeSpan timespan = Today.Subtract(FileDate);
                            
                            if ((AllFiles.Count - (FilesDeleted.Count + intFilesNotToCount)) > MinFileCount || MinFileCount == 0)
                            {
                                //Monthly Retention Delete



                                if ((((timespan.Days * 24 + timespan.Hours) > (MonthlyMaxDaysOld * 24)) || (FileDate.DayOfWeek != DayOfWeekToKeep && FileDate.Day > 7)) && (timespan.Days > WeeklyMaxDaysOld && timespan.Days > DailyMaxDaysOld))
                                {
                                    if (RetentionAlgorithm == RetentionAlgorithmOptions.KeepAll)
                                    {
                                        FilesDeleted.Add(file1);
                                        _evt.WriteEntry("Retention Logging Only Mode: GFS Monthly File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6771, 60);

                                    }
                                    else
                                    {
                                        FilesDeleted.Add(file1);
                                        //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                        file1.IsReadOnly = false;
                                        File.Delete(file1.FullName);
                                        _evt.WriteEntry("Retention: GFS Monthly File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6770, 60);
                                    }

                                } //Weekly Retention Delete
                                else if ((((timespan.Days * 24 + timespan.Hours) > (WeeklyMaxDaysOld * 24)) || (FileDate.DayOfWeek != DayOfWeekToKeep)) && timespan.Days > DailyMaxDaysOld && FileDate.Day > 7)
                                {
                                    if (RetentionAlgorithm == RetentionAlgorithmOptions.KeepAll)
                                    {
                                        FilesDeleted.Add(file1);
                                        _evt.WriteEntry("Retention Logging Only Mode: GFS Weekly File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6671, 60);

                                    }
                                    else
                                    {
                                        FilesDeleted.Add(file1);
                                        //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                        file1.IsReadOnly = false;
                                        File.Delete(file1.FullName);
                                        
                                        _evt.WriteEntry("Retention: GFS Weekly File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6670, 60);
                                    }
                                }//Daily Retention Delete
                                else if (((timespan.Days * 24 + timespan.Hours) > (DailyMaxDaysOld * 24)) && (FileDate.DayOfWeek != DayOfWeekToKeep))
                                {
                                    if (RetentionAlgorithm == RetentionAlgorithmOptions.KeepAll)
                                    {
                                        FilesDeleted.Add(file1);
                                        _evt.WriteEntry("Retention Logging Only Mode: GFS Daily File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6571, 60);
                                    }
                                    else
                                    {
                                        FilesDeleted.Add(file1);
                                        //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                        file1.IsReadOnly = false;
                                        File.Delete(file1.FullName);
                                        _evt.WriteEntry("Retention: GFS Daily File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6570, 60);
                                    }
                                }


                            }

                        }
                        catch (Exception ex)
                        {
                            _evt.WriteEntry(ex.Message);
                        }

                    }
                }
                else
                {
                    _evt.WriteEntry("Retention: GFS Error: Path is incorrect: " + BackupFolder);
                }

            }
            catch (Exception ex)
            {
               
                _evt.WriteEntry("Retention: GFS Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 6000, 60);            
            
            }
            if (RetentionAlgorithm == RetentionAlgorithmOptions.KeepAll)
            {
                _evt.WriteEntry("Retention Logging Only Mode: GFS Complete Files Deleted: " + FilesDeleted.Count, System.Diagnostics.EventLogEntryType.Information,6000, 60);            
            }
            else
            {
                _evt.WriteEntry("Retention: GFS Complete Files Deleted: " + FilesDeleted.Count, System.Diagnostics.EventLogEntryType.Information, 6000, 60);
            }
        }

        /// <summary>
        /// No Deletion of any files
        /// </summary>
        public void KeepAll(ref bool blShuttingDown)
        {
            GFS(ref blShuttingDown);
        }

        /// <summary>
        /// Keep Daily Backups up to the DailyMaxDaysOld Retention Plan
        /// </summary>
        public void KeepDaily(ref bool blShuttingDown)
        {
            int intFilesNotToCount = 0;
            try
            {
                _evt.WriteEntry("Retention: Keep Daily Starting: ", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                
                AllFiles.Clear();
                FilesDeleted.Clear();
                AllFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);

                foreach (System.IO.FileInfo file1 in AllFiles)
                {
                    if (blShuttingDown)
                    {
                        throw new Exception("Shutting Down");
                    }
                    try
                    {
                       
                        intFilesNotToCount = 0;
                        

                        DateTime Today = DateTime.Now;
                        DateTime FileDate;

                        if (blShuttingDown)
                        {
                            _evt.WriteEntry("Retention: Shutting Down, about to KeepDaily: " + file1.FullName, System.Diagnostics.EventLogEntryType.Warning, 6000, 60);            
                            return;
                        }

                        try
                        {
                            FileDate = file1.LastWriteTime;
                            if (FileDate == DateTime.MinValue || FileDate == DateTime.MaxValue)
                            {
                                Exception exc = new Exception("Retention: Failed to get Last Modified DateTime for file: " + file1.FullName);
                                throw exc;
                            }
                        }
                        catch (Exception ex)
                        {
                            _evt.WriteEntry(ex.Message);
                            continue;
                        }



                        TimeSpan timespan = Today.Subtract(FileDate);
                        if ((AllFiles.Count - (FilesDeleted.Count + intFilesNotToCount)) > MinFileCount || MinFileCount == 0)
                        {
                            if ((timespan.Days * 24 + timespan.Hours) > (DailyMaxDaysOld * 24))
                            {
                                FilesDeleted.Add(file1);
                                //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                file1.IsReadOnly = false;
                                File.Delete(file1.FullName);
                                _evt.WriteEntry("Retention: Daily File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6570, 60);
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _evt.WriteEntry("Retention Error: " + ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Retention Error: " + ex.Message);
            }
            _evt.WriteEntry("Retention: Keep Daily Complete Files Deleted: " + FilesDeleted.Count, System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                
        }

        /// <summary>
        /// Keep Weekly Backups up to the WeeklyMaxDaysOld Retention Plan
        /// </summary>
        public void KeepWeekly(ref bool blShuttingDown)
        {
            int intFilesNotToCount = 0;
            try
            {
                _evt.WriteEntry("Retention: Keep Weekly Starting: ", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                
                AllFiles.Clear();
                FilesDeleted.Clear();
                AllFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);


                foreach (System.IO.FileInfo file1 in AllFiles)
                {
                    if (blShuttingDown)
                    {
                        throw new Exception("Shutting Down");
                    }
                    try
                    {
                        intFilesNotToCount = 0;
                        
                        DateTime Today = DateTime.Now;
                        DateTime FileDate;

                        if (blShuttingDown)
                        {
                            _evt.WriteEntry("Retention: Shutting Down, about to KeepWeekly: " + file1.FullName, System.Diagnostics.EventLogEntryType.Warning, 6000, 60);            
                            return;
                        }

                        //Try to get Date Last Modified or Created
                        try
                        {
                            FileDate = file1.LastWriteTime;
                            if (FileDate == DateTime.MinValue || FileDate == DateTime.MaxValue)
                            {
                                
                                Exception exc = new Exception("Retention: Failed to get Last Modified DateTime for file: " + file1.FullName);
                                throw exc;
                            }
                        }
                        catch (Exception ex)
                        {
                            _evt.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error, 6000, 60);            
                            continue;
                        }



                        TimeSpan timespan = Today.Subtract(FileDate);
                        if ((AllFiles.Count - (FilesDeleted.Count + intFilesNotToCount)) > MinFileCount || MinFileCount == 0)
                        {
                            if ((timespan.Days * 24 + timespan.Hours) > (WeeklyMaxDaysOld * 24) || FileDate.DayOfWeek != DayOfWeekToKeep)
                            {
                                FilesDeleted.Add(file1);
                                //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                file1.IsReadOnly = false;
                                File.Delete(file1.FullName);
                                _evt.WriteEntry("Retention: Weekly File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6670, 60);
                                
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        _evt.WriteEntry("Retention Error Weekly: " + ex.Message);

                    }

                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Retention Error Weekly: " + ex.Message);
            }
            _evt.WriteEntry("Retention: Keep Weekly Complete Files Deleted: " + FilesDeleted.Count, System.Diagnostics.EventLogEntryType.Information, 6000, 60);
            
        }

        /// <summary>
        /// Keep Monthly Backups up to the MonthlyMaxDaysOld Retention Plan
        /// </summary>
        public void KeepMonthly(ref bool blShuttingDown)
        {
            int intFilesNotToCount = 0;
            try
            {
                _evt.WriteEntry("Retention: Keep Monthly Starting: ", System.Diagnostics.EventLogEntryType.Information, 6000, 60);
                
                AllFiles.Clear();
                FilesDeleted.Clear();
                AllFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown, FileNameFilter);

                foreach (System.IO.FileInfo file1 in AllFiles)
                {
                    if (blShuttingDown)
                    {
                        throw new Exception("Shutting Down");
                    }
                    try
                    {
                        intFilesNotToCount = 0;
                        

                        DateTime Today = DateTime.Now;
                        DateTime FileDate;

                        if (blShuttingDown)
                        {
                            _evt.WriteEntry("Retention: Shutting Down, about to KeepMonthly: " + file1.FullName, System.Diagnostics.EventLogEntryType.Warning, 6000, 60);            
                            
                            return;
                        }


                        //Try to get Date Last Modified or Created
                        try
                        {
                            FileDate = file1.LastWriteTime;
                            if (FileDate == DateTime.MinValue || FileDate == DateTime.MaxValue)
                            {
                                Exception exc = new Exception("Retention: Failed to get Last Modified DateTime for file: " + file1.FullName);
                                throw exc;
                            }
                        }
                        catch (Exception ex)
                        {
                            _evt.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning, 6000, 60);            
                            
                            continue;
                        }



                        TimeSpan timespan = Today.Subtract(FileDate);
                        if ((AllFiles.Count - (FilesDeleted.Count + intFilesNotToCount)) > MinFileCount || MinFileCount == 0)
                        {
                            if (((timespan.Days * 24 + timespan.Hours) > (MonthlyMaxDaysOld * 24)) || (FileDate.DayOfWeek != DayOfWeekToKeep && FileDate.Day > 7))
                            {
                                FilesDeleted.Add(file1);
                                _evt.WriteEntry("Retention: Monthly File Date: " + FileDate.ToString() + "File Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 6770, 60);
                                
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        _evt.WriteEntry("Retention Error Monthly: " + ex.Message);

                    }

                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Retention Error: " + ex.Message);
            }
            _evt.WriteEntry("Retention: Keep Monthly Complete Files Deleted: " + FilesDeleted.Count, System.Diagnostics.EventLogEntryType.Information, 6000, 60);
            

        }
        #endregion


    }
}

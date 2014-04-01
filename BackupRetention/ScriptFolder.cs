using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Threading;


namespace BackupRetention
{
    /// <summary>
    /// ScriptFolder Class: executes commands on a schedule
    /// </summary>
    public class ScriptFolder: IFolderConfig
    {
        #region "Variables"
        //StringBuilder sbOutput;
        //StringBuilder sbError;
        public static System.Diagnostics.Process process=null;
        /// <summary>
        /// Event Log Class
        /// </summary>
        private static EventLog _evt;

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

        private Month _months = Month.January | Month.February | Month.March | Month.April | Month.May | Month.June | Month.July | Month.August | Month.September | Month.October | Month.November | Month.December;
        public Month Months
        {
            get
            {
                return _months;
            }

            set
            {
                _months = value;
            }
        }

        private bool _enabled = false;
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

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                _endDate = value;
            }
        }

        private string _workingDirectory = "";
        public string WorkingDirectory
        {
            get
            {
                return _workingDirectory;
            }

            set
            {
                _workingDirectory = value;
            }
        }

        private string _filename = "";
        public string FileName
        {
            get
            {
                return _filename;
            }

            set
            {
                _filename = value;
            }
        }

        private string _arguments = "";
        public string Arguments
        {
            get
            {
                return _arguments;
            }

            set
            {
                _arguments = value;
            }
        }

        public static string _sourceFolder = "";
        public string SourceFolder
        {
            get
            {
                return _sourceFolder;
            }
            set
            {
                _sourceFolder = Common.WindowsPathClean(value);
            }
        }

        public static string _destinationFolder = "";
        public string DestinationFolder
        {
            get
            {
                return _destinationFolder;
            }
            set
            {
                _destinationFolder = Common.WindowsPathClean(value);
            }
        }

        private int _timeout = 360;
        public int Timeout 
        { 
            get
            {
                return _timeout;
            }
            
            set
            {
                _timeout = value;
            }
        
        }


        

        #endregion


        #region "Methods"

        

        /// <summary>
        /// ScriptFolder Contructor
        /// </summary>
        /// <param name="row"></param>
        public ScriptFolder()
        {
            
        }




        /// <summary>
        /// ScriptFolder Contructor
        /// </summary>
        /// <param name="row"></param>
        public ScriptFolder(DataRow row)
        {
            

            ID = Common.FixNullInt32(row["ID"]);
            Title = Common.FixNullstring(row["Title"]);
            Enabled = Common.FixNullbool(row["Enabled"]);
            Time = Common.FixNullstring(row["Time"]);
            EndTime=Common.FixNullstring(row["EndTime"]);

            try
            {
                IntervalType = (IntervalTypes) System.Enum.Parse(typeof(IntervalTypes), Common.FixNullstring(row["IntervalType"]));
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
            Months = 0;
            if (Common.FixNullbool(row["January"]))
            {
                Months = Months | Month.January;
            }
            if (Common.FixNullbool(row["February"]))
            {
                Months = Months | Month.February;
            }
            if (Common.FixNullbool(row["March"]))
            {
                Months = Months | Month.March;
            }
            if (Common.FixNullbool(row["April"]))
            {
                Months = Months | Month.April;
            }
            if (Common.FixNullbool(row["May"]))
            {
                Months = Months | Month.May;
            }
            if (Common.FixNullbool(row["June"]))
            {
                Months = Months | Month.June;
            }
            if (Common.FixNullbool(row["July"]))
            {
                Months = Months | Month.July;
            }
            if (Common.FixNullbool(row["August"]))
            {
                Months = Months | Month.August;
            }
            if (Common.FixNullbool(row["September"]))
            {
                Months = Months | Month.September;
            }
            if (Common.FixNullbool(row["October"]))
            {
                Months = Months | Month.October;
            }
            if (Common.FixNullbool(row["November"]))
            {
                Months = Months | Month.November;
            }
            if (Common.FixNullbool(row["December"]))
            {
                Months = Months | Month.December;
            }
            DateTime.TryParse(Common.FixNullstring(row["StartDate"]), out _startDate);
            DateTime.TryParse(Common.FixNullstring(row["EndDate"]), out _endDate);
            FileName = Common.FixNullstring(row["FileName"]);
            Arguments = Common.FixNullstring(row["Arguments"]);
            WorkingDirectory = Common.FixNullstring(row["WorkingDirectory"]);
            SourceFolder = Common.FixNullstring(row["SourceFolder"]);
            DestinationFolder = Common.FixNullstring(row["DestinationFolder"]);
            Timeout = Common.FixNullInt32(row["Timeout"]);
            Comment = Common.FixNullstring(row["Comment"]);

        }

        /// <summary>
        /// ScriptFolder Class Destructor
        /// </summary>
        ~ScriptFolder()
        {
            try
            {
                if (process != null)
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                    process.Close();
                    process.Dispose();
                }
            }
            catch (Exception)
            {   
            }
            process = null;
            _evt = null;
        }


        /// <summary>
        /// Initializes the synchronization config table
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtScriptConfig;
            dtScriptConfig = new DataTable("ScriptConfig");
            _evt = Common.GetEventLog;

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtScriptConfig.Columns.Add(dcID);
            columns[0] = dtScriptConfig.Columns["ID"];
            dtScriptConfig.PrimaryKey = columns;


            //Create Columns
            dtScriptConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Title", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("January", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("February", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("March", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("April", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("May", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("June", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("July", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("August", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("September", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("October", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("November", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("December", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("DayOfMonth", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("WorkingDirectory", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("FileName", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Arguments", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("SourceFolder", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("DestinationFolder", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("StartDate", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("EndDate", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Timeout", typeof(String)));
            dtScriptConfig.Columns.Add(new DataColumn("Comment", typeof(String)));

            dtScriptConfig.Columns["Enabled"].DefaultValue = "true";
            dtScriptConfig.Columns["Time"].DefaultValue = "01:00";
            dtScriptConfig.Columns["IntervalType"].DefaultValue = "Daily";
            dtScriptConfig.Columns["Interval"].DefaultValue = "0";
            dtScriptConfig.Columns["Monday"].DefaultValue = "true";
            dtScriptConfig.Columns["Tuesday"].DefaultValue = "true";
            dtScriptConfig.Columns["Wednesday"].DefaultValue = "true";
            dtScriptConfig.Columns["Thursday"].DefaultValue = "true";
            dtScriptConfig.Columns["Friday"].DefaultValue = "true";
            dtScriptConfig.Columns["Saturday"].DefaultValue = "true";
            dtScriptConfig.Columns["Sunday"].DefaultValue = "true";
            dtScriptConfig.Columns["January"].DefaultValue = "true";
            dtScriptConfig.Columns["February"].DefaultValue = "true";
            dtScriptConfig.Columns["March"].DefaultValue = "true";
            dtScriptConfig.Columns["April"].DefaultValue = "true";
            dtScriptConfig.Columns["May"].DefaultValue = "true";
            dtScriptConfig.Columns["June"].DefaultValue = "true";
            dtScriptConfig.Columns["July"].DefaultValue = "true";
            dtScriptConfig.Columns["August"].DefaultValue = "true";
            dtScriptConfig.Columns["September"].DefaultValue = "true";
            dtScriptConfig.Columns["October"].DefaultValue = "true";
            dtScriptConfig.Columns["November"].DefaultValue = "true";
            dtScriptConfig.Columns["December"].DefaultValue = "true";
            dtScriptConfig.Columns["DayOfMonth"].DefaultValue = "0";
            dtScriptConfig.Columns["FileName"].DefaultValue = "";
            dtScriptConfig.Columns["Arguments"].DefaultValue = "";
            dtScriptConfig.Columns["WorkingDirectory"].DefaultValue = "";
            dtScriptConfig.Columns["Timeout"].DefaultValue = "360";
            return dtScriptConfig;

        }


        /// <summary>
        /// Executes Script
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void Execute(ref bool blShuttingDown)
        {
            try
            {
                if (Enabled)
                {
                    //sbOutput = new StringBuilder();
                    //sbError = new StringBuilder();
                    _evt.WriteEntry("Script: Starting Script: " + Common.FixNullstring(FileName) + " " + Common.FixNullstring(Arguments), EventLogEntryType.Information);
                    process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.RedirectStandardError = false;
                    startInfo.RedirectStandardOutput = false;
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Common.WindowsPathClean(Common.FixNullstring(WorkingDirectory));
                    startInfo.FileName = Common.WindowsPathClean(Common.FixNullstring(FileName));
                    if (Common.FixNullstring(Arguments).Trim().Length > 0)
                    {
                        startInfo.Arguments = Common.WindowsArgumentClean(Common.FixNullstring(Arguments));
                        //startInfo.Arguments = Arguments;
                    }
                    //left off debugging here
                    process.StartInfo = startInfo;
                    //process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                    process.Exited += new EventHandler(ProcessExitHandler);
                    //process.ErrorDataReceived += new DataReceivedEventHandler(ErrorOutputHandler);
                    process.Start();
                    //process.BeginOutputReadLine();
                    //process.BeginErrorReadLine();
                    
                    process.WaitForExit(1000 * 60 * Timeout);
                    process.Refresh();
                    
                }
            }
            catch (Exception e)
            {
                _evt.WriteEntry("Script: " + Common.FixNullstring(FileName) + " " + Common.FixNullstring(Arguments) + " Error: " + e.ToString());
            }
            finally
            {
                if (!process.HasExited)
                {
                    process.Kill();
                }
                process.Close();
                process = null;
               
            }
        }

        private void ProcessExitHandler(Object source, EventArgs ag)
        {
            /*if (sbError.Length > 0)
            {
                _evt.WriteEntry("Script: " + sbError.ToString(), EventLogEntryType.Error);
            }
            if (sbOutput.Length > 0)
            {
                _evt.WriteEntry("Script: " + sbOutput.ToString(), EventLogEntryType.Information);
            }*/
            _evt.WriteEntry("Script: Script Finished: " + Common.FixNullstring(FileName) + " " + Common.FixNullstring(Arguments), EventLogEntryType.Information);
        }

        /*
        private void ErrorOutputHandler(Object source, DataReceivedEventArgs drArgs)
        {
            // Collect the sort command output. 
            if (!String.IsNullOrEmpty(drArgs.Data))
            {
                sbError.AppendLine(drArgs.Data);
            }

        }


        private void OutputHandler(Object source, DataReceivedEventArgs drArgs)
        {
            // Collect the sort command output. 
            if (!String.IsNullOrEmpty(drArgs.Data))
            {
                sbOutput.AppendLine(drArgs.Data);
            }
        }
        */
       
        #endregion
    }
}

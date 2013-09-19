using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
//using Microsoft.Synchronization;
//using Microsoft.Synchronization.Files;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Data.SqlServerCe;


namespace BackupRetention
{
    /// <summary>
    /// Synchronization Folder Class
    /// </summary>
    public class SyncFolder: IFolderConfig
    {
        #region "Variables"

        public System.Collections.Generic.List<System.IO.FileInfo> AllFiles;
        private System.Object lockMirror = new System.Object();
        private System.Object lockMirrorDelete = new System.Object();

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

        private string _intervalType = "";
        public string IntervalType
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

        private int _interval = 0;
        public int EndTime
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

        private int _dayOfMonth = 0;
        public int DayOfMonth
        {
            get
            {
                return _dayOfMonth;
            }
            set
            {
                _dayOfMonth = value;
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

        public FileSyncReplicaOptions _fileSyncReplicaOption = FileSyncReplicaOptions.OneWayBackup;
        public FileSyncReplicaOptions FileSyncReplicaOption
        {
            get
            {
                return _fileSyncReplicaOption;
            }
            set
            {
                _fileSyncReplicaOption = value;
            }
        }

        private bool _fileSyncReset = false;
        public bool FileSyncReset
        {
            get
            {
                return _fileSyncReset;
            }

            set
            {
                _fileSyncReset = value;
            }
        }

        public SyncDirectionOrder _folderSyncDirectionOrder = SyncDirectionOrder.Upload;
        public SyncDirectionOrder FolderSyncDirectionOrder
        {
           
            get
            {
                return _folderSyncDirectionOrder;
            }
            set
            {
                _folderSyncDirectionOrder = value;
            }
        }

        public ConflictResolutionPolicy _defaultConflictResolutionPolicy = ConflictResolutionPolicy.SourceWins;
        public ConflictResolutionPolicy DefaultConflictResolutionPolicy
        {
            get
            {
                return _defaultConflictResolutionPolicy;
            }
            set
            {
                _defaultConflictResolutionPolicy = value;
            }
        }



        private bool _archiveDeleted = false;
        public bool ArchiveDeleted
        {
            get
            {
                return _archiveDeleted;
            }

            set
            {
                _archiveDeleted = value;
            }
        }

        public static string _archiveFolder = "";
        public string ArchiveFolder
        {
            get
            {
                return _archiveFolder;
            }
            set
            {
                _archiveFolder = Common.WindowsPathClean(value);
            }
        }


        #endregion


        #region "Methods"

        /// <summary>
        /// Sychronization Folder Contructor for reuse
        /// </summary>
        private void init_SyncFolder()
        {
            AllFiles = new System.Collections.Generic.List<System.IO.FileInfo>();
            //FilesSynced = new System.Collections.Generic.List<System.IO.FileInfo>();
            _evt = Common.GetEventLog;
        }

        /// <summary>
        /// Sychronization Folder Contructor
        /// </summary>
        /// <param name="row"></param>
        public SyncFolder()
        {
            init_SyncFolder();
        }




        /// <summary>
        /// Sychronization Folder Contructor
        /// </summary>
        /// <param name="row"></param>
        public SyncFolder(DataRow row)
        {
            init_SyncFolder();

            ID = Common.FixNullInt32(row["ID"]);
            Enabled = Common.FixNullbool(row["Enabled"]);
            Time = Common.FixNullstring(row["Time"]);
            //left off here!
            EndTime=Common.FixNullstring(row["EndTime"]);
            Monday = Common.FixNullbool(row["Monday"]);
            Tuesday = Common.FixNullbool(row["Tuesday"]);
            Wednesday = Common.FixNullbool(row["Wednesday"]);
            Thursday = Common.FixNullbool(row["Thursday"]);
            Friday = Common.FixNullbool(row["Friday"]);
            Saturday = Common.FixNullbool(row["Saturday"]);
            Sunday = Common.FixNullbool(row["Sunday"]);
            DayOfMonth = Common.FixNullInt32(row["DayOfMonth"]);
            SourceFolder = Common.FixNullstring(row["SourceFolder"]);
            DestinationFolder = Common.FixNullstring(row["DestinationFolder"]);
            try
            {
                FileSyncReplicaOption = (FileSyncReplicaOptions)System.Enum.Parse(typeof(FileSyncReplicaOptions), Common.FixNullstring(row["FileSyncReplicaOption"]));
            }
            catch (Exception)
            {
                FileSyncReplicaOption = FileSyncReplicaOptions.OneWayBackup;
            }
            FileSyncReset = Common.FixNullbool(row["FileSyncReset"]);
            try
            {
                FolderSyncDirectionOrder = (SyncDirectionOrder)System.Enum.Parse(typeof(SyncDirectionOrder), Common.FixNullstring(row["FolderSyncDirectionOrder"]));
            }
            catch (Exception)
            {
                FolderSyncDirectionOrder = SyncDirectionOrder.Upload;
            }
            try
            {
                DefaultConflictResolutionPolicy = (ConflictResolutionPolicy)System.Enum.Parse(typeof(ConflictResolutionPolicy), Common.FixNullstring(row["DefaultConflictResolutionPolicy"]));
            }
            catch (Exception)
            {

                DefaultConflictResolutionPolicy = ConflictResolutionPolicy.SourceWins;
            }
            ArchiveDeleted = Common.FixNullbool(row["ArchiveDeleted"]);
            ArchiveFolder = Common.FixNullstring(row["ArchiveFolder"]);

        }

        /// <summary>
        /// Synchronization Folder Class Destructor
        /// </summary>
        ~SyncFolder()
        {
            AllFiles.Clear();
            AllFiles = null;
        }


        /// <summary>
        /// Initializes the synchronization config table
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtSyncConfig;
            dtSyncConfig = new DataTable("SyncConfig");
            

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtSyncConfig.Columns.Add(dcID);
            columns[0] = dtSyncConfig.Columns["ID"];
            dtSyncConfig.PrimaryKey = columns;


            //Create Columns
            dtSyncConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("DayOfMonth", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("SourceFolder", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("DestinationFolder", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("FileSyncReplicaOption", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("FileSyncReset", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("FolderSyncDirectionOrder", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("DefaultConflictResolutionPolicy", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("ArchiveDeleted", typeof(String)));
            dtSyncConfig.Columns.Add(new DataColumn("ArchiveFolder", typeof(String)));


            dtSyncConfig.Columns["Enabled"].DefaultValue = "true";
            dtSyncConfig.Columns["Monday"].DefaultValue = "true";
            dtSyncConfig.Columns["Tuesday"].DefaultValue = "true";
            dtSyncConfig.Columns["Wednesday"].DefaultValue = "true";
            dtSyncConfig.Columns["Thursday"].DefaultValue = "true";
            dtSyncConfig.Columns["Friday"].DefaultValue = "true";
            dtSyncConfig.Columns["Saturday"].DefaultValue = "true";
            dtSyncConfig.Columns["Sunday"].DefaultValue = "true";
            dtSyncConfig.Columns["DayOfMonth"].DefaultValue = "0";
            dtSyncConfig.Columns["FileSyncReplicaOption"].DefaultValue = "OneWayBackup";
            dtSyncConfig.Columns["FileSyncReset"].DefaultValue = "false";
            dtSyncConfig.Columns["FolderSyncDirectionOrder"].DefaultValue = "Upload";
            dtSyncConfig.Columns["DefaultConflictResolutionPolicy"].DefaultValue = "SourceWins";
            dtSyncConfig.Columns["ArchiveDeleted"].DefaultValue = "false";
            return dtSyncConfig;

        }

        /*
        ,RunTime TEXT
	    ,FolderAction TEXT
	    ,Direction TEXT
	    ,SourceFolder TEXT
	    ,DestinationFolder TEXT
	    ,Host TEXT
	    ,Port INT
	    ,Protocol TEXT
	    ,Username TEXT
	    ,Comment TEXT
         */
        /// <summary>
        /// Saves Folder Files to SQL Server Compact for later comparison to find missing or modified files
        /// </summary>
        /// <param name="strComment"></param>
        /// <returns></returns>
        public long Save(string strComment)
        {

            SqlCEHelper db = null;
            List<SqlCeParameter> list = null;
            long lastid = 0;
            object o=null;
            SqlCeParameter sp;
            try
            {
                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                list = new List<SqlCeParameter>();
                
                sp = new SqlCeParameter("@SourceFolderMD5", SqlDbType.NVarChar, 32);
                sp.Value = Common.GetMD5HashFromString(Common.FixNullstring(SourceFolder));
                list.Add(sp);

                sp = new SqlCeParameter("@DestinationFolderMD5", SqlDbType.NVarChar, 32);
                sp.Value = Common.GetMD5HashFromString(Common.FixNullstring(DestinationFolder));
                list.Add(sp);

                sp = new SqlCeParameter("@FolderAction", SqlDbType.NVarChar, 100);
                sp.Value = FileSyncReplicaOption.ToString();
                list.Add(sp);

                sp = new SqlCeParameter("@Comment", SqlDbType.NVarChar, 3000);
                sp.Value = Common.FixNullstring(strComment);
                list.Add(sp);

                o = db.ExecuteScalar("SELECT ID FROM FolderAction WHERE SourceFolderMD5=@SourceFolderMD5 AND DestinationFolderMD5=@DestinationFolderMD5 AND FolderAction=@FolderAction AND Comment=@Comment",list);
                
                long.TryParse(Common.FixNullstring(o), out lastid);

                list.Clear();
                if (lastid == 0)
                {
                    list = new List<SqlCeParameter>();

                    sp = new SqlCeParameter("@RunTime", SqlDbType.DateTime);
                    sp.Value = DateTime.Now;
                    list.Add(sp);

                    sp = new SqlCeParameter("@FolderAction", SqlDbType.NVarChar, 100);
                    sp.Value = FileSyncReplicaOption.ToString();
                    list.Add(sp);

                    sp = new SqlCeParameter("@Direction", SqlDbType.NVarChar, 100);
                    sp.Value = FolderSyncDirectionOrder.ToString();
                    list.Add(sp);

                    sp = new SqlCeParameter("@SourceFolder", SqlDbType.NVarChar, 3000);
                    sp.Value = Common.FixNullstring(SourceFolder);
                    list.Add(sp);

                    sp = new SqlCeParameter("@DestinationFolder", SqlDbType.NVarChar, 3000);
                    sp.Value = Common.FixNullstring(DestinationFolder);
                    list.Add(sp);

                    sp = new SqlCeParameter("@Comment", SqlDbType.NVarChar, 3000);
                    sp.Value = Common.FixNullstring(strComment);
                    list.Add(sp);

                    sp = new SqlCeParameter("@SourceFolderMD5", SqlDbType.NVarChar, 32);
                    sp.Value = Common.GetMD5HashFromString(Common.FixNullstring(SourceFolder));
                    list.Add(sp);

                    sp = new SqlCeParameter("@DestinationFolderMD5", SqlDbType.NVarChar, 32);
                    sp.Value = Common.GetMD5HashFromString(Common.FixNullstring(DestinationFolder));
                    list.Add(sp);

                    
                    lastid = db.Insert("INSERT INTO FolderAction (RunTime,FolderAction,Direction,SourceFolder,DestinationFolder,Comment, SourceFolderMD5, DestinationFolderMD5) VALUES (@RunTime,@FolderAction,@Direction,@SourceFolder,@DestinationFolder,@Comment,@SourceFolderMD5,@DestinationFolderMD5)", list);
                    
                }
            }
            catch (Exception ex)
            {
                lastid = 0;
                _evt.WriteEntry("Sync Mirror: Save() Error: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
            }
            return lastid;
        }

        /*
        //Microsoft Sync Methods
        //http://msdn.microsoft.com/en-us/sync/bb887623
        //http://www.microsoft.com/en-us/download/details.aspx?id=23217


        public void MicrosoftFileSync(ref bool blShuttingDown)
        {
            // Set options for the synchronization session. In this case, options specify
            // that the application will explicitly call FileSyncProvider.DetectChanges, and
            // that items should be moved to the Recycle Bin instead of being permanently deleted.
            FileSyncOptions options = FileSyncOptions.ExplicitDetectChanges |
                     FileSyncOptions.RecycleDeletedFiles | FileSyncOptions.RecyclePreviousFileOnUpdates |
                     FileSyncOptions.RecycleConflictLoserFiles;

            // Create a filter that excludes all *.lnk files. The same filter should be used 
            // by both providers.
            FileSyncScopeFilter filter = new FileSyncScopeFilter();
            filter.FileNameExcludes.Add("*.lnk");
            filter.FileNameExcludes.Add("File.ID");
            //filter.FileNameExcludes.Add("*.7z");

            AllFiles.Clear();
            AllFiles = Common.WalkDirectory(DestinationFolder, ref blShuttingDown);
            //Exclude the compressed files on the destination without .7z extension on the source!!
            foreach (System.IO.FileInfo file1 in AllFiles)
            {
                //Skip over already compressed files
                if (file1.Extension.ToLower() == ".7z")
                {
                    //Compare LastModified of Source and Destination Files
                    string strSFile = file1.FullName.Replace(DestinationFolder, SourceFolder);
                    string strExclude = file1.Name.Substring(0, file1.Name.Length - 3);
                    FileInfo SFile = null;

                    strSFile = strSFile.Substring(0, strSFile.Length - 3);
                    if (File.Exists(strSFile))
                    {
                        SFile = new FileInfo(strSFile);
                    }

                    if (SFile != null)
                    {
                        if (SFile.LastWriteTimeUtc == file1.LastWriteTimeUtc)
                        {
                            filter.FileNameExcludes.Add(strExclude);
                        }
                        else
                        {
                            if (SFile.Extension != file1.Extension)
                            {
                                //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                file1.IsReadOnly = false;
                                File.Delete(file1.FullName);
                                _evt.WriteEntry("Sync:  7z Compressed file in destination but source file modified Deleted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 40);

                            }
                        }
                    }
                }
            }



            //Reset Synchronization so that files deleted in the destination are resynchronized
            if (FileSyncReset)
            {
                if (File.Exists(SourceFolder + "\\filesync.metadata"))
                {
                    File.SetAttributes(SourceFolder + "\\filesync.metadata", FileAttributes.Normal);
                    File.Delete(SourceFolder + "\\filesync.metadata");
                }
                if (File.Exists(SourceFolder + "\\File.ID"))
                {
                    File.SetAttributes(SourceFolder + "\\File.ID", FileAttributes.Normal);
                    File.Delete(SourceFolder + "\\File.ID");
                }
                if (File.Exists(DestinationFolder + "\\filesync.metadata"))
                {
                    File.SetAttributes(DestinationFolder + "\\filesync.metadata", FileAttributes.Normal);
                    File.Delete(DestinationFolder + "\\filesync.metadata");
                }
                if (File.Exists(DestinationFolder + "\\File.ID"))
                {
                    File.SetAttributes(DestinationFolder + "\\File.ID", FileAttributes.Normal);
                    File.Delete(DestinationFolder + "\\File.ID");
                }
            }

            // Explicitly detect changes on both replicas before syncyhronization occurs.
            // This avoids two change detection passes for the bidirectional synchronization 
            // that we will perform.
            DetectChangesOnFileSystemReplica(
                SourceFolder, filter, options);
            DetectChangesOnFileSystemReplica(
                DestinationFolder, filter, options);

            // Synchronize the replicas in one directions. In the first session replica 1 is
            // the source. The third parameter
            // (the filter value) is null because the filter is specified in DetectChangesOnFileSystemReplica().
            SyncFileSystemReplicasOneWay(SourceFolder, DestinationFolder, null, options, FolderSyncDirectionOrder, DefaultConflictResolutionPolicy);



        }

        */

        /// <summary>
        /// Synchronizes two folders with the specified options of this class
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void ExecuteSyncFolder(ref bool blShuttingDown)
        {
            try
            {
                if (Enabled)
                {
                    if (string.IsNullOrEmpty(SourceFolder) || string.IsNullOrEmpty(DestinationFolder) || !Directory.Exists(SourceFolder) || !Directory.Exists(DestinationFolder))
                    {
                        _evt.WriteEntry("Sync: invalid source directory path 1 or invalid destination directory path 2", EventLogEntryType.Error);
                        return;
                    }

                    switch (FileSyncReplicaOption)
                    {
                        case FileSyncReplicaOptions.OneWayMirror:
                             _evt.WriteEntry("Sync: Mirroring starting from: " + SourceFolder + " to: " + DestinationFolder, EventLogEntryType.Information, 4120, 45);
                            ExecuteMirror(ref blShuttingDown, true);
                            _evt.WriteEntry("Sync: Mirroring ended from: " + SourceFolder + " to: " + DestinationFolder, EventLogEntryType.Information, 4130, 45);
                            break;
                        default: 
                            //FileSyncReplicaOptions.OneWayBackup:
                            _evt.WriteEntry("Sync: OneWay Backup starting from: " + SourceFolder + " to: " + DestinationFolder, EventLogEntryType.Information, 4120, 45);
                            ExecuteMirror(ref blShuttingDown, false);
                            _evt.WriteEntry("Sync: OneWay Backup ended from: " + SourceFolder + " to: " + DestinationFolder, EventLogEntryType.Information, 4130, 45);
                            break;
                        //default:
                            //MicrosoftFileSync(ref blShuttingDown);
                        //    break;

                    }
                }
            }
            catch (Exception e)
            {
                _evt.WriteEntry("Sync: Exception from File Sync Provider:\n" + e.ToString());
            }
            finally
            {
                //Sync is finished clear all FileOperations in RFile table
                ClearAllFileOperations();
            }
        }



        


        public FileInfo GetRenamedFile(FileInfo file1, System.Collections.Generic.List<FileInfo> files,string strSourceFolder,string strDestinationFolder)
        {
            string strMD5 = "";
            string strPossibleMatchMD5 = "";

            FileInfo NewFile = null;
            try
            {
                strMD5 = Common.GetMD5HashFromFile(file1.FullName);
                foreach (FileInfo srcFile in files)
                {
                    try
                    {
                        
                        string strDestinationCheck = Common.WindowsPathClean(srcFile.FullName.Replace(strSourceFolder, strDestinationFolder));
                        if (file1.Length == srcFile.Length && !File.Exists(strDestinationCheck) && File.Exists(file1.FullName))
                        {  
                            strPossibleMatchMD5 = Common.GetMD5HashFromFile(srcFile.FullName);
                            if (strMD5 == strPossibleMatchMD5 && !string.IsNullOrEmpty(strMD5))
                            {
                                NewFile = srcFile;
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                }

            }
            catch (Exception)
            {

            }
            return NewFile;
        }

        
        


        public static void CompactDatabase()
        {
            DataSet ds=null;
            SqlCEHelper db=null;
            List<RemoteFile> RFiles=null;
            List<SqlCeParameter> listp = null;
            _evt = Common.GetEventLog;
            //Clear Database Old Records
            try
            {
                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));
                db.ExecuteNonQuery("DELETE FROM RFile");
                db.ExecuteNonQuery("DELETE FROM FolderAction");
                db.ExecuteNonQuery("ALTER TABLE FolderAction ALTER COLUMN [ID] IDENTITY (1,1)");
                db.ExecuteNonQuery("ALTER TABLE RFile ALTER COLUMN [ID] IDENTITY (1,1)");
                    
                _evt.WriteEntry("Sync Mirror: Compacted Database Successfully");
                
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Sync Mirror: Compact Database Error: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
                if (ds != null)
                {
                    ds.Clear();
                    ds.Dispose();
                    ds = null;
                }
                if (RFiles != null)
                {
                    RFiles.Clear();
                    RFiles = null;
                }
                if (listp != null)
                {
                    listp.Clear();
                    listp = null;
                }
            }
            
            
            

        }


        /// <summary>
        /// Returns Files not in the Destination Folder or Files Modified
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="DestinationID"></param>
        /// <returns></returns>
        public List<RemoteFile> GetMissingOrModifiedFiles(long SourceID, long DestinationID)
        {
            string strSQL = "";
            DataTable dtRenamedOrDeleted=null;
            SqlCEHelper db=null;
            List<RemoteFile> RFiles = new List<RemoteFile>();
            DataSet ds = null;
            List<SqlCeParameter> list = null;
            SqlCeParameter sp;
            try
            {
                if (this.FolderSyncDirectionOrder == SyncDirectionOrder.Download)
                {
                    long ltemp = SourceID;
                    SourceID = DestinationID;
                    DestinationID = ltemp;
                }
                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                //strSQL = "SELECT ID,Name,FullName,FileLength,FileParentDirectory,IsDirectory,LastWriteTime,LastWriteTimeUTC,NewFileName,MD5,FolderActionID FROM RFile WHERE FolderActionID=@SourceID AND replace(FullName,@strSourceFolder,@strDestinationFolder) NOT IN (SELECT FullName FROM RFile WHERE FolderActionID=@DestinationID)";

                //Missing or Modified Files
                //strSQL = "SELECT ID,Name,FullName,FileLength,FileParentDirectory,IsDirectory,LastWriteTime,LastWriteTimeUTC,NewFileName,MD5,FolderActionID,ShortFullName,ShortFullNameMD5,SourceFolderMD5 FROM RFile WHERE FolderActionID=@SourceID AND ShortFullNameMD5 NOT IN (SELECT ShortFullNameMD5 FROM RFile WHERE FolderActionID=@DestinationID)";
                
                //Find Files Not in Destination
                strSQL = "UPDATE RFile SET FileOperation='Created' WHERE FolderActionID=@SourceID AND FileOperation='None' AND ShortFullNameMD5 NOT IN (SELECT ShortFullNameMD5 FROM RFile WHERE FolderActionID=@DestinationID)";
                
                list = new List<SqlCeParameter>();

                

                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = SourceID;
                list.Add(sp);

                sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                sp.Value = DestinationID;
                list.Add(sp);
                db.ExecuteNonQuery(strSQL, list);

                strSQL = "UPDATE RFile SET FileOperation='Modified' WHERE RFile.ID IN (SELECT RFile.ID FROM RFile INNER JOIN RFile AS R2 ON R2.FolderActionID=@DestinationID AND R2.ShortFullNameMD5 = RFile.ShortFullNameMD5  WHERE RFile.FolderActionID=@SourceID AND RFile.FileOperation='None' AND (RFile.LastWriteTime <> R2.LastWriteTime OR RFile.FileLength <> R2.FileLength OR (LEN(R2.MD5)>0 AND LEN(RFile.MD5)>0 AND RFile.MD5 <> R2.MD5)))";


                list = new List<SqlCeParameter>();
                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = SourceID;
                list.Add(sp);

                sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                sp.Value = DestinationID;
                list.Add(sp);
                db.ExecuteNonQuery(strSQL, list);


                strSQL = "SELECT ID,Name,FullName,FileLength,FileParentDirectory,IsDirectory,LastWriteTime,LastWriteTimeUTC,NewFileName,MD5,FolderActionID,ShortFullName,ShortFullNameMD5,SourceFolderMD5 FROM RFile WHERE RFile.FileOperation <> 'None' AND RFile.FolderActionID=@SourceID";


                list.Clear();
                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = SourceID;
                list.Add(sp);

                ds = db.ExecuteDataSet(strSQL, list);

                dtRenamedOrDeleted = ds.Tables["DATA"];
                foreach (DataRow row in dtRenamedOrDeleted.Rows)
                {
                    RFiles.Add(new RemoteFile(row));
                }

                
            }
            catch (Exception ex)
            {
                RFiles.Clear();
                RFiles = null;
                _evt.WriteEntry("Sync Mirror: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();

                }
                if (dtRenamedOrDeleted != null)
                {
                    dtRenamedOrDeleted.Clear();
                    dtRenamedOrDeleted.Dispose();
                }
                if (ds != null)
                {
                    ds.Clear();
                    ds.Dispose();
                }
                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
            }
            return RFiles;

        }

        /// <summary>
        /// Deletes a file record from the database
        /// </summary>
        /// <param name="lRFileID"></param>
        public void DeleteFileFromDb(long lRFileID)
        {
            string strSQL = "";
            SqlCEHelper db = null;
            List<SqlCeParameter> list = null;
            int intRA = 0;
            if (lRFileID > 0)
            {
                try
                {

                    db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                    strSQL = "DELETE FROM RFile WHERE ID=@ID";
                    list = new List<SqlCeParameter>();

                    SqlCeParameter sp;

                    sp = new SqlCeParameter("@ID", SqlDbType.BigInt);
                    sp.Value = lRFileID;
                    list.Add(sp);

                    intRA = db.ExecuteNonQuery(strSQL, list);

                }
                catch (Exception ex)
                {
                    _evt.WriteEntry("Sync Mirror: Delete from db Failed ID: " + lRFileID + " Error: " + ex.Message);
                }
                finally
                {
                    if (db != null)
                    {
                        db.Dispose();

                    }

                    if (list != null)
                    {
                        list.Clear();
                        list = null;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a file record from the database
        /// </summary>
        /// <param name="ShortFullNameMD5"></param>
        public void DeleteFileFromDb(string ShortFullNameMD5)
        {
            string strSQL = "";
            SqlCEHelper db = null;
            List<SqlCeParameter> list = null;
            int intRA = 0;
            if (!string.IsNullOrEmpty(ShortFullNameMD5))
            {
                try
                {

                    db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                    strSQL = "DELETE FROM RFile WHERE ShortFullNameMD5=@ShortFullNameMD5";
                    list = new List<SqlCeParameter>();

                    SqlCeParameter sp;

                    sp = new SqlCeParameter("@ShortFullNameMD5", SqlDbType.NVarChar,32);
                    sp.Value = ShortFullNameMD5;
                    list.Add(sp);

                    intRA = db.ExecuteNonQuery(strSQL, list);

                }
                catch (Exception ex)
                {
                    _evt.WriteEntry("Sync Mirror: Delete from db Failed ShortFullNameMD5: " + ShortFullNameMD5 + " Error: " + ex.Message);
                }
                finally
                {
                    if (db != null)
                    {
                        db.Dispose();

                    }

                    if (list != null)
                    {
                        list.Clear();
                        list = null;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes all files for both specified FolderActionID's 
        /// </summary>
        /// <param name="lSourceFilesID"></param>
        /// <param name="lDestinationFilesID"></param>
        public void DeleteFromDb(long lSourceFilesID,long lDestinationFilesID)
        {
            object o;
            long lID = 0;
            List<SqlCeParameter> list = null;
            SqlCEHelper db = null;
            SqlCeParameter sp;
            //Clear Comparison Data from the Database
            try
            {
                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                list = new List<SqlCeParameter>();

                sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                sp.Value = lDestinationFilesID;
                list.Add(sp);

                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = lSourceFilesID;
                list.Add(sp);

                db.ExecuteNonQuery("DELETE FROM RFile WHERE FolderActionID=@DestinationID OR FolderActionID=@SourceID", list);
                ////db.ExecuteNonQuery("DELETE FROM RFile WHERE FolderActionID=@SourceID", list);

                list.Clear();
                list = new List<SqlCeParameter>();

                sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                sp.Value = lDestinationFilesID;
                list.Add(sp);

                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = lSourceFilesID;
                list.Add(sp);
                db.ExecuteNonQuery("DELETE FROM FolderAction WHERE ID=@DestinationID OR ID=@SourceID", list);
                ////db.ExecuteNonQuery("DELETE FROM FolderAction WHERE ID=@SourceID", list);

                //Reseed tables

                o = db.ExecuteScalar("SELECT Max(ID) + 1 AS Seed1 FROM RFile");
                lID = Common.FixNulllong(o);
                if (lID > 0)
                {
                    db.ExecuteNonQuery("ALTER TABLE RFile ALTER COLUMN [ID] IDENTITY (" + lID.ToString() + ",1)");
                }
                o = db.ExecuteScalar("SELECT Max(ID) + 1 AS Seed1 FROM FolderAction");
                lID = Common.FixNulllong(o);
                if (lID > 0)
                {
                    db.ExecuteNonQuery("ALTER TABLE FolderAction ALTER COLUMN [ID] IDENTITY (" + lID.ToString() + ",1)");
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Sync Mirror: Delete from db source and destination Error: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();

                }

                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
            }
            
        }

       

        /// <summary>
        /// Finds New Files in the Source Folder
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="DestinationID"></param>
        /// <returns></returns>
        public List<FileInfo> GetSourceNewFiles(long SourceID, long DestinationID)
        {
            string strSQL = "";
            DataTable dtNewFiles = null;
            SqlCEHelper db = null;
            List<FileInfo> RFiles = new List<FileInfo>();
            DataSet ds = null;
            List<SqlCeParameter> list = null;
            SqlCeParameter sp;
            try
            {

                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                //strSQL = "SELECT ID,Name,FullName,FileLength,FileParentDirectory,IsDirectory,LastWriteTime,LastWriteTimeUTC,NewFileName,MD5,FolderActionID FROM RFile WHERE FolderActionID=@SourceID AND replace(FullName,@strSourceFolder,@strDestinationFolder) NOT IN (SELECT FullName FROM RFile WHERE FolderActionID=@DestinationID)";

                //Missing or Modified Files
                strSQL = "SELECT RFile.ID, RFile.Name, RFile.FullName, RFile.FileLength, RFile.FileParentDirectory, RFile.IsDirectory, RFile.LastWriteTime, RFile.LastWriteTimeUTC, RFile.NewFileName, RFile.MD5, RFile.FolderActionID, RFile.ShortFullName, RFile.ShortFullNameMD5, RFile.SourceFolderMD5 FROM RFile LEFT JOIN FolderAction ON FolderAction.ID=RFile.FolderActionID WHERE FolderActionID=@SourceID AND ShortFullNameMD5 NOT IN (SELECT ShortFullNameMD5 FROM RFile WHERE FolderActionID=@DestinationID)";

                list = new List<SqlCeParameter>();

                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = SourceID;
                list.Add(sp);

                sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                sp.Value = DestinationID;
                list.Add(sp);

                ds = db.ExecuteDataSet(strSQL, list);

                dtNewFiles = ds.Tables["DATA"];
                foreach (DataRow row in dtNewFiles.Rows)
                {

                    FileInfo rfile = new FileInfo(Common.FixNullstring(row["FullName"]));
                    //rfile.FileOperation = FileOperations.Deleted;
                    RFiles.Add(rfile);
                }

            }
            catch (Exception ex)
            {
                RFiles.Clear();
                RFiles = null;
                _evt.WriteEntry("Sync Mirror: GetSourceNewFiles : " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();

                }
                if (dtNewFiles != null)
                {
                    dtNewFiles.Clear();
                    dtNewFiles.Dispose();
                }
                if (ds != null)
                {
                    ds.Clear();
                    ds.Dispose();
                }
                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
            }
            return RFiles;

        }

        /// <summary>
        /// Deletes files and/or Renames Files according to settings and if file was renamed
        /// </summary>
        /// <param name="blShuttingDown"></param>
        /// <param name="blDelete"></param>
        /// <param name="lSourceFilesID"></param>
        /// <param name="lDestinationFilesID"></param>
        /// <returns></returns>
        public List<RemoteFile> ExecuteMirrorDelete(ref bool blShuttingDown, bool blDelete/*, List<FileInfo> DestinationFiles, List<DirectoryInfo> SrcFolders, List<DirectoryInfo> DestFolders*/, long lSourceFilesID, long lDestinationFilesID)
        {
            List<RemoteFile> FilesDeleted = null;
            List<RemoteFile> FilesRenamed = null;
            List<FileInfo> NewFiles = null;
            lock (lockMirrorDelete)
            {
                string strSourceFolder = "";
                string strDestinationFolder = "";

                try
                {
                    FilesDeleted = new List<RemoteFile>();
                    if (this.FolderSyncDirectionOrder == SyncDirectionOrder.Download)
                    {
                        //Download is the reverse of upload
                        strSourceFolder = Common.WindowsPathClean(DestinationFolder);
                        strDestinationFolder = Common.WindowsPathClean(SourceFolder);
                    }
                    else
                    {
                        //Upload
                        strSourceFolder = Common.WindowsPathClean(SourceFolder);
                        strDestinationFolder = Common.WindowsPathClean(DestinationFolder);
                    }
                    Common.CreateDestinationFolders(strSourceFolder, strDestinationFolder);
                    if (ArchiveFolder.Length > 0 && ArchiveDeleted)
                    {
                        if (Directory.Exists(ArchiveFolder))
                        {
                            Common.CreateDestinationFolders(strDestinationFolder, ArchiveFolder);
                        }
                    }
                    NewFiles = GetSourceNewFiles(lSourceFilesID, lDestinationFilesID);

                    
                    

                        
                    //Fix Renamed Files 
                    FilesRenamed = GetDeletedOrRenamed(lSourceFilesID, lDestinationFilesID);

                    if (FilesRenamed != null)
                    {
                        foreach (RemoteFile rfile in FilesRenamed)
                        {
                            FileInfo destinationFile = new FileInfo(rfile.FullName);
                            string strSource = "";
                            strSource = Common.WindowsPathClean(destinationFile.FullName.Replace(strDestinationFolder, strSourceFolder));
                            //Find Renamed Files 
                            FileInfo FileRenamed = GetRenamedFile(destinationFile, NewFiles, strSourceFolder, strDestinationFolder);
                            if (FileRenamed != null && !FileSyncReset)
                            {
                                string strRenamedDest = Common.WindowsPathClean(FileRenamed.FullName.Replace(strSourceFolder, strDestinationFolder));

                                string strRenamedDir = Common.WindowsPathClean(FileRenamed.DirectoryName.Replace(strSourceFolder, strDestinationFolder));
                                if (!Directory.Exists(strRenamedDir))
                                {
                                    Directory.CreateDirectory(strRenamedDir);
                                }
                                //Rename Existing File
                                File.Move(destinationFile.FullName, strRenamedDest);
                                _evt.WriteEntry("Sync: File Renamed: " + FileRenamed.FullName + " from:" + destinationFile.FullName + " to: " + strRenamedDest, System.Diagnostics.EventLogEntryType.Information, 4080, 45);
                                RemoteFile rmoved = new RemoteFile(new FileInfo(strRenamedDest), strDestinationFolder);
                                rmoved.FolderActionID = rfile.FolderActionID;
                                rmoved.Comment = rfile.Comment;
                                rmoved.MD5 = rfile.MD5;
                                rmoved.FileOperation = FileOperations.Renamed;
                                DeleteFileFromDb(rfile.ID);
                                rmoved.Save();
                            }
                            else  //File has been deleted not renamed
                            {
                                FilesDeleted.Add(rfile);
                            }
                        }
                        FilesRenamed.Clear();
                        FilesRenamed = null;
                    }

                    if (this.FileSyncReplicaOption == FileSyncReplicaOptions.OneWayMirror)
                    {
                        //Delete or Archive the file according the settings
                        foreach (RemoteFile rfile in FilesDeleted)
                        {

                            FileInfo destinationFile = new FileInfo(rfile.FullName);
                            string strSource = "";
                            strSource = Common.WindowsPathClean(destinationFile.FullName.Replace(strDestinationFolder, strSourceFolder));



                            if (!rfile.IsDirectory)
                            {
                                if (File.Exists(Common.WindowsPathCombine(strSourceFolder, rfile.ShortFullName)) ^ File.Exists(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName)))
                                {
                                    if (File.Exists(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName)))
                                    {
                                        if (ArchiveDeleted && Directory.Exists(ArchiveFolder) && blDelete)
                                        {
                                            //Archive the File
                                            FileInfo afile = new FileInfo(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName));
                                            string strRenamedDest = Common.WindowsPathCombine(ArchiveFolder, rfile.ShortFullName) + "." + System.Guid.NewGuid().ToString() + afile.Extension;
                                            File.Move(afile.FullName, strRenamedDest);
                                            _evt.WriteEntry("Sync: Mirroring File Archived from:" + afile.FullName + " to: " + strRenamedDest, System.Diagnostics.EventLogEntryType.Information, 4080, 45);
                                        }
                                        else
                                        {
                                            if (blDelete)
                                            {
                                                //Delete the File
                                                _evt.WriteEntry("Sync: Mirroring File Deleted: " + Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName), System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                                File.Delete(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName));
                                            }
                                        }

                                    }
                                    DeleteFileFromDb(rfile.ShortFullNameMD5);
                                }
                            }
                            else
                            {
                                if (Directory.Exists(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName)))
                                {
                                    DirectoryInfo dirdel = new DirectoryInfo(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName));
                                    if (Common.GetFilesInDirectory(dirdel).Count == 0)
                                    {
                                        if (ArchiveDeleted && Directory.Exists(ArchiveFolder) && blDelete)
                                        {
                                            //Archive the File
                                            string strRenamedDest = Common.WindowsPathCombine(ArchiveFolder, rfile.ShortFullName) + "." + System.Guid.NewGuid().ToString();
                                            Directory.Move(dirdel.FullName, strRenamedDest);
                                            _evt.WriteEntry("Sync: Mirroring File Archived from:" + dirdel.FullName + " to: " + strRenamedDest, System.Diagnostics.EventLogEntryType.Information, 4080, 45);
                                        }
                                        else
                                        {
                                            //Delete the Directory
                                            if (blDelete)
                                            {
                                                _evt.WriteEntry("Sync: Mirroring files Directory Deleted: " + Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName), System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                                Directory.Delete(Common.WindowsPathCombine(strDestinationFolder, rfile.ShortFullName));
                                            }
                                        }
                                    }
                                }
                                DeleteFileFromDb(rfile.ShortFullNameMD5);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    if (FilesRenamed != null)
                    {
                        FilesRenamed.Clear();
                        FilesRenamed = null;
                    }
                }
            }
            return FilesDeleted;

        }

        /// <summary>
        /// Clears FileOperation field to "None" in RFile table
        /// </summary>
        public void ClearAllFileOperations()
        {
            SqlCEHelper db = null;
            string strSQL = "";
            try
            {
                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                strSQL = "UPDATE RFile SET FileOperation='None' WHERE FileOperation <> 'None'";
                db.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Sync Mirror: ClearFileOperation Error: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
           
        }

        /*public List<RemoteFile> GetDeletedOrRenamed(long lSourceID, long lDestinationID)
        {
            string strSQL = "";
            DataTable dtRenamedOrDeleted = null;
            SqlCEHelper db = null;
            List<RemoteFile> RFiles = new List<RemoteFile>();
            DataSet ds = null;
            List<SqlCeParameter> list=null;
            SqlCeParameter sp=null;
            
            try
            {

                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                //Find Deleted Files in Source Files

                if (lSourceID > 0 && lDestinationID > 0)
                {
                    strSQL = "SELECT ID,Name,FullName,FileLength,FileParentDirectory,IsDirectory,LastWriteTime,LastWriteTimeUTC,NewFileName,MD5,FolderActionID, ShortFullName, ShortFullNameMD5, SourceFolderMD5 FROM RFile WHERE FolderActionID=@DestinationID AND ShortFullNameMD5 NOT IN (SELECT ShortFullNameMD5 FROM RFile WHERE FolderActionID=@SourceID)";
                    
                    list = new List<SqlCeParameter>();

                    sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                    sp.Value = lSourceID;
                    list.Add(sp);

                    sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                    sp.Value = lDestinationID;
                    list.Add(sp);

                    ds = db.ExecuteDataSet(strSQL, list);

                    dtRenamedOrDeleted = ds.Tables["DATA"];
                    foreach (DataRow row in dtRenamedOrDeleted.Rows)
                    {
                        RemoteFile rfile = new RemoteFile(row);
                        rfile.FileOperation = FileOperations.Deleted;
                        rfile.Comment = "Destination";
                        RFiles.Add(rfile);
                    }
                }
                
               

            }
            catch (Exception ex)
            {
                RFiles.Clear();
                RFiles = null;
                _evt.WriteEntry("Sync Mirror: " + ex.Message);
            }
            finally
            {
                if (dtRenamedOrDeleted != null)
                {
                    dtRenamedOrDeleted.Clear();
                    dtRenamedOrDeleted.Dispose();
                }
                if (ds != null)
                {
                    ds.Clear();
                    ds.Dispose();
                }
                if (db != null)
                {
                    db.Dispose();
                }
                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
                sp = null;
            }
            return RFiles;

        }*/


        /// <summary>
        /// Returns list of Deleted or Renamed Files
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="DestinationID"></param>
        /// <returns></returns>
        public List<RemoteFile> GetDeletedOrRenamed(long SourceID, long DestinationID)
        {
            string strSQL = "";
            DataTable dtDelFiles = null;
            SqlCEHelper db = null;
            List<RemoteFile> RFiles = new List<RemoteFile>();
            DataSet ds = null;
            List<SqlCeParameter> list = null;
            SqlCeParameter sp;
            try
            {

                db = new SqlCEHelper("Data Source=" + Common.WindowsPathClean(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\BackupRetention.sdf;Max Database Size = 4000;Max Buffer Size = 1024"));

                //strSQL = "SELECT ID,Name,FullName,FileLength,FileParentDirectory,IsDirectory,LastWriteTime,LastWriteTimeUTC,NewFileName,MD5,FolderActionID FROM RFile WHERE FolderActionID=@SourceID AND replace(FullName,@strSourceFolder,@strDestinationFolder) NOT IN (SELECT FullName FROM RFile WHERE FolderActionID=@DestinationID)";

                //Missing or Modified Files
                strSQL = "SELECT RFile.ID, RFile.Name, RFile.FullName, RFile.FileLength, RFile.FileParentDirectory, RFile.IsDirectory, RFile.LastWriteTime, RFile.LastWriteTimeUTC, RFile.NewFileName, RFile.MD5, RFile.FolderActionID, RFile.ShortFullName, RFile.ShortFullNameMD5, RFile.SourceFolderMD5 FROM RFile WHERE FolderActionID=@DestinationID AND ShortFullNameMD5 NOT IN (SELECT ShortFullNameMD5 FROM RFile WHERE FolderActionID=@SourceID)";

                list = new List<SqlCeParameter>();

                sp = new SqlCeParameter("@SourceID", SqlDbType.BigInt);
                sp.Value = SourceID;
                list.Add(sp);

                sp = new SqlCeParameter("@DestinationID", SqlDbType.BigInt);
                sp.Value = DestinationID;
                list.Add(sp);

                ds = db.ExecuteDataSet(strSQL, list);

                dtDelFiles = ds.Tables["DATA"];
                foreach (DataRow row in dtDelFiles.Rows)
                {
                    RemoteFile rfile = new RemoteFile(row);
                    //FileInfo rfile = new FileInfo(Common.FixNullstring(row["FullName"]));
                    //rfile.FileOperation = FileOperations.Deleted;
                    RFiles.Add(rfile);
                }

            }
            catch (Exception ex)
            {
                RFiles.Clear();
                RFiles = null;
                _evt.WriteEntry("Sync Mirror: GetSourceNewFiles : " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();

                }
                if (dtDelFiles != null)
                {
                    dtDelFiles.Clear();
                    dtDelFiles.Dispose();
                }
                if (ds != null)
                {
                    ds.Clear();
                    ds.Dispose();
                }
                if (list != null)
                {
                    list.Clear();
                    list = null;
                }
            }
            return RFiles;

        }

        /// <summary>
        /// Custom Mirror Method if blDelete if false then Mirrors without deletion of files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        /// <param name="blDelete"></param>
        public void ExecuteMirror(ref bool blShuttingDown, bool blDelete)
        {
            
            lock (lockMirror)
            {
                string strSourceFolder="";
                string strDestinationFolder = "";
                long lSourceFilesID=0;
                long lDestinationFilesID=0;
                System.Collections.Generic.List<FileInfo> SourceFiles=null;
                System.Collections.Generic.List<FileInfo> DestinationFiles=null;
                System.Collections.Generic.List<DirectoryInfo> SrcFolders = null;
                System.Collections.Generic.List<DirectoryInfo> DestFolders = null;


                System.Collections.Generic.List<DirectoryInfo> DestinationFolders = null;

                List<RemoteFile> FilesDeleted = null;

                List<RemoteFile> FilesMissing = null;

                try
                {
                                       
                    if (this.FolderSyncDirectionOrder == SyncDirectionOrder.Download)
                    {
                        //Download is the reverse of upload
                        strSourceFolder = Common.WindowsPathClean(DestinationFolder);
                        strDestinationFolder = Common.WindowsPathClean(SourceFolder);

                    }
                    else
                    {
                        //Upload
                        strSourceFolder = Common.WindowsPathClean(SourceFolder);
                        strDestinationFolder = Common.WindowsPathClean(DestinationFolder);
                    }
                    
                    //Common.CreateDestinationFolders(strSourceFolder, strDestinationFolder);
                   
                    if (ArchiveFolder.Length > 0 && ArchiveDeleted)
                    {
                        if (Directory.Exists(ArchiveFolder))
                        {
                            Common.CreateDestinationFolders(strDestinationFolder, ArchiveFolder);
                        }
                    }
                    _evt.WriteEntry("Sync: Mirroring Started Crawling SourceFolder: " + strSourceFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);          
                    SourceFiles = Common.WalkDirectory(strSourceFolder, ref blShuttingDown);
                    SrcFolders = Common.GetAllDirectories(strSourceFolder);
                    _evt.WriteEntry("Sync: Mirroring Finished Crawling SourceFolder: " + strSourceFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                    _evt.WriteEntry("Sync: Mirroring Started Crawling DestinationFolder: " + strDestinationFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                    DestinationFiles = Common.WalkDirectory(strDestinationFolder, ref blShuttingDown);
                    DestFolders = Common.GetAllDirectories(strDestinationFolder);
                    _evt.WriteEntry("Sync: Mirroring Finished Crawling DestinationFolder: " + strDestinationFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);


                    //This will be used to compare files from the source and destination
                    lSourceFilesID=Save("MirrorSourceFiles");
                    lDestinationFilesID = Save("MirrorDestinationFiles");
                    _evt.WriteEntry("Sync: Mirroring Saving SourceFolder files to db: " + strSourceFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);          
                    Common.SaveFileInfoList(lSourceFilesID, SourceFiles, strSourceFolder);
                    Common.SaveFolderInfoList(lSourceFilesID, SrcFolders, strSourceFolder);
                    _evt.WriteEntry("Sync: Mirroring Finished Saving SourceFolder files to db: " + strSourceFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);          
                    _evt.WriteEntry("Sync: Mirroring Saving DestinationFolder files to db: " + strDestinationFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                    Common.SaveFileInfoList(lDestinationFilesID, DestinationFiles, strDestinationFolder);
                    Common.SaveFolderInfoList(lDestinationFilesID, DestFolders, strDestinationFolder);
                    _evt.WriteEntry("Sync: Mirroring Finished Saving DestinationFolder files to db: " + strDestinationFolder, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                   
                    //FilesDelOrRenamed=GetMirrorRenamedOrDeleted(lSourceFilesID, strSourceFolder,lDestinationFilesID, strDestinationFolder);


                    //Delete Files not in Source and Renamed Files in the Destination if renamed in the Source
                    FilesDeleted = ExecuteMirrorDelete(ref blShuttingDown, blDelete,lSourceFilesID,lDestinationFilesID);

                    if (blDelete)
                    {
                        //Delete Folders that no longer exist in the source
                        DestinationFolders = Common.GetAllDirectories(strDestinationFolder);
                        foreach (DirectoryInfo dir1 in DestinationFolders)
                        {

                            string strSource = "";
                            try
                            {
                                strSource = Common.WindowsPathClean(dir1.FullName.Replace(strDestinationFolder, strSourceFolder));
                                if (blShuttingDown)
                                {
                                    _evt.WriteEntry("Sync: Mirroring Shutting Down about to possibly delete mirror folder: " + dir1.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                    return;
                                }

                                if (!Directory.Exists(strSource))
                                {
                                    if (ArchiveDeleted && Directory.Exists(ArchiveFolder))
                                    {
                                        string strRenamedDest = Common.WindowsPathClean(dir1.FullName.Replace(strDestinationFolder, ArchiveFolder));
                                        if (!Directory.Exists(strRenamedDest))
                                        {
                                            try
                                            {
                                                Directory.CreateDirectory(strRenamedDest);
                                                _evt.WriteEntry("Sync: Mirroring Folder Archive Folder Created: " + strRenamedDest, System.Diagnostics.EventLogEntryType.Information, 4070, 45);

                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }
                                        //Directory.Move(dir1.FullName,strRenamedDest);
                                        
                                        
                                        Directory.Delete(dir1.FullName, true);
                                        _evt.WriteEntry("Sync: Mirroring Folder and sub folders and files Deleted: " + dir1.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                    }
                                    else
                                    {
                                        Directory.Delete(dir1.FullName, true);

                                        _evt.WriteEntry("Sync: Mirroring Folder and sub folders and files Deleted: " + dir1.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                    }
                                    RemoteFile dfile = new RemoteFile(dir1, strDestinationFolder);
                                    DeleteFileFromDb(dfile.ShortFullNameMD5);

                                }
                            }
                            catch (Exception exfd)
                            {
                                _evt.WriteEntry("Sync: Mirroring Folder and sub folders and files Delete failed for: " + dir1.FullName + " Error: " + exfd.Message, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                            }

                        }
                    }

                    //Missing or Modified Files after Deletions have been removed from the database
                    FilesMissing = GetMissingOrModifiedFiles(lSourceFilesID, lDestinationFilesID);


                    //Copy Files that are in the SourceFolder that are not in the DestinationFolder or if file is different
                    foreach (RemoteFile rsourceFile in FilesMissing)
                    {
                        FileInfo srcFile = new FileInfo(rsourceFile.FullName);
                        string strDestination = "";
                        try
                        {
                            strDestination = Common.WindowsPathClean(srcFile.FullName.Replace(strSourceFolder, strDestinationFolder));

                            if (blShuttingDown)
                            {
                                _evt.WriteEntry("Sync: Mirroring Shutting Down about to possibly mirror file: " + srcFile.FullName, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                return;
                            }

                            if (!rsourceFile.IsDirectory)
                            {
                                if (!File.Exists(strDestination))
                                {
                                    if (File.Exists(strDestination + ".7z"))
                                    {
                                        strDestination += ".7z";
                                    }
                                }

                                if (!Common.IsFileLocked(srcFile) && srcFile.Name.ToLower() != "file.id" && srcFile.Name.ToLower() != "filesync.metadata")
                                {
                                    if (File.Exists(strDestination))
                                    {
                                        FileInfo destFile = new FileInfo(strDestination);

                                        
                                        //Copy the Modified file
                                        switch (this.DefaultConflictResolutionPolicy)
                                        {
                                            case ConflictResolutionPolicy.SourceWins:
                                                //One Way Sync
                                                //Source Wins
                                                if ((srcFile.Length != destFile.Length && destFile.Extension == srcFile.Extension) || srcFile.LastWriteTimeUtc != destFile.LastWriteTimeUtc)
                                                {
                                                    strDestination = Common.WindowsPathClean(srcFile.FullName.Replace(strSourceFolder, strDestinationFolder));
                                                    if (ArchiveDeleted && Directory.Exists(ArchiveFolder))
                                                    {
                                                        string strRenamedDest = Common.WindowsPathClean(destFile.FullName.Replace(strDestinationFolder, ArchiveFolder)) + "." + System.Guid.NewGuid().ToString() + destFile.Extension;
                                                        File.Move(strDestination, strRenamedDest);
                                                        File.Copy(srcFile.FullName, strDestination, true);
                                                        _evt.WriteEntry("Sync: Mirroring File Modified Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                                    }
                                                    else
                                                    {
                                                        System.IO.File.Copy(srcFile.FullName, strDestination, true);
                                                        _evt.WriteEntry("Sync: Mirroring File Modified Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                                    }

                                                }
                                                break;

                                            case ConflictResolutionPolicy.DestinationWins:
                                                //One Way Sync
                                                //Copy the Modified file Destination Wins
                                                if ((srcFile.Length != destFile.Length && destFile.Extension == srcFile.Extension) || srcFile.LastWriteTimeUtc != destFile.LastWriteTimeUtc)
                                                {
                                                    strDestination = Common.WindowsPathClean(destFile.FullName.Replace(strDestinationFolder, strSourceFolder));
                                                    if (ArchiveDeleted && Directory.Exists(ArchiveFolder))
                                                    {
                                                        string strRenamedDest = Common.WindowsPathClean(srcFile.FullName.Replace(strDestinationFolder, ArchiveFolder)) + "." + System.Guid.NewGuid().ToString() + srcFile.Extension;
                                                        File.Move(strDestination, strRenamedDest);
                                                        File.Copy(srcFile.FullName, strDestination, true);
                                                        _evt.WriteEntry("Sync: Mirroring File Modified Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                                    }
                                                    else
                                                    {
                                                        System.IO.File.Copy(destFile.FullName, strDestination, true);
                                                        _evt.WriteEntry("Sync: Mirroring File Modified Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                                    }

                                                }
                                                break;

                                            }

                                        }
                                        else
                                        {
                                            //Copy the File
                                            System.IO.File.Copy(srcFile.FullName, strDestination, true);
                                            _evt.WriteEntry("Sync: Mirroring File Created Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);

                                        }

                                    }
                                    
                                }
                            }
                        
                        catch (Exception ex)
                        {
                            _evt.WriteEntry("Sync: Mirroring File Modified Copy failed from: " + srcFile.FullName + " to: " + strDestination + " " + ex.Message, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    _evt.WriteEntry("Sync: Mirroring Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 4000, 45);
                }
                finally
                {
                    if (lSourceFilesID > 0 && lDestinationFilesID > 0)
                    {
                        DeleteFromDb(lSourceFilesID, lDestinationFilesID);
                    }
                    if (SourceFiles != null)
                    {
                        SourceFiles.Clear();
                        
                    }
                    if (DestinationFiles != null)
                    {
                        DestinationFiles.Clear();
                    }

                    if (FilesMissing != null)
                    {
                        FilesMissing.Clear();
                    }
                    
                    if (FilesDeleted != null)
                    {
                        FilesDeleted.Clear();
                    }

                    if (SrcFolders != null)
                    {
                        SrcFolders.Clear();
                    }

                    if (DestFolders != null)
                    {
                        DestFolders.Clear();
                    }

                    if (DestinationFolders != null)
                    {
                        DestinationFolders.Clear();
                    }

                }
            }
        }

        /*
        /// <summary>
        /// Create a provider, and detect changes on the replica that the provider
        /// represents.
        /// </summary>
        /// <param name="replicaRootPath"></param>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        public static void DetectChangesOnFileSystemReplica(
                string replicaRootPath,
                FileSyncScopeFilter filter, FileSyncOptions options)
        {
            FileSyncProvider provider = null;

            try
            {
                //replicaRootPath
                SyncId ssyncId = GetSyncID(replicaRootPath + "\\File.ID");
                provider = new FileSyncProvider(ssyncId.GetGuidId(), replicaRootPath, filter, options);
                provider.DetectChanges();
            }
            finally
            {
                // Release resources.
                if (provider != null)
                    provider.Dispose();
            }
        }

        /// <summary>
        /// Creates a file in the path passed that stores a GUID the replicaid for synchronization if the file does not exist otherwise reads the guid from the file and returns the replicaid
        /// </summary>
        /// <param name="syncFilePath"></param>
        /// <returns></returns>
        private static SyncId GetSyncID(string syncFilePath)
        {
            Guid guid;
            SyncId replicaID = null;
            if (!File.Exists(syncFilePath)) //The ID file doesn't exist. 
            //Create the file and store the guid which is used to 
            //instantiate the instance of the SyncId.
            {
                guid = Guid.NewGuid();
                replicaID = new SyncId(guid);
                //FileStream fs = File.Open(syncFilePath, FileMode.Create);
                FileStream fs = new FileStream(syncFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(guid.ToString());
                sw.Close();
                fs.Close();
            }
            else
            {
                FileStream fs = new FileStream(syncFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fs);
                string guidString = sr.ReadLine();
                guid = new Guid(guidString);
                replicaID = new SyncId(guid);
                sr.Close();
                fs.Close();
            }
            return (replicaID);
        }

        /// <summary>
        /// One Way Folder Synchronization Method
        /// </summary>
        /// <param name="sourceReplicaRootPath"></param>
        /// <param name="destinationReplicaRootPath"></param>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="FolderSyncDirectionOrder"></param>
        /// <param name="DefaultConflictResolutionPolicy"></param>
        public static void SyncFileSystemReplicasOneWay(string sourceReplicaRootPath, string destinationReplicaRootPath
                , FileSyncScopeFilter filter, FileSyncOptions options, SyncDirectionOrder FolderSyncDirectionOrder, ConflictResolutionPolicy DefaultConflictResolutionPolicy)
        {
            FileSyncProvider sourceProvider = null;
            FileSyncProvider destinationProvider = null;


            try
            {
                SyncId sourceId = GetSyncID(sourceReplicaRootPath + "\\File.ID");
                SyncId destId = GetSyncID(destinationReplicaRootPath + "\\File.ID");

                // Instantiate source and destination providers, with a null filter (the filter
                // was specified in DetectChangesOnFileSystemReplica()), and options for both.
                sourceProvider = new FileSyncProvider(sourceId.GetGuidId(), sourceReplicaRootPath, filter, options);
                destinationProvider = new FileSyncProvider(destId.GetGuidId(), destinationReplicaRootPath, filter, options);



                // Register event handlers so that we can write information
                // to the console.

                //Additional Events
                //    provider.DestinationCallbacks.FullEnumerationNeeded += this.FullEnumerationNeededCallback;
                //    destinationProvider.ItemChangeSkipped += this.ItemChangeSkippedCallback;
                //    destinationProvider.ItemChanging += this.ItemChangingCallback;                    
                //    destinationProvider.ItemConstraint += this.ItemConstraintCallback;
                //    destinationProvider.ProgressChanged += this.ProgressChangedCallback;
                // 

                //Document Changes if needed
                destinationProvider.AppliedChange +=
                    new EventHandler<AppliedChangeEventArgs>(OnAppliedChange);


                destinationProvider.SkippedChange +=
                    new EventHandler<SkippedChangeEventArgs>(OnSkippedChange);
                destinationProvider.Configuration.ConflictResolutionPolicy = DefaultConflictResolutionPolicy;
                // Use SyncCallbacks for conflicting items.
                SyncCallbacks destinationCallbacks = destinationProvider.DestinationCallbacks;
                destinationCallbacks.ItemConflicting += new EventHandler<ItemConflictingEventArgs>(OnItemConflicting);

                SyncOrchestrator agent = new SyncOrchestrator();
                agent.LocalProvider = sourceProvider;
                agent.RemoteProvider = destinationProvider;

                agent.Direction = FolderSyncDirectionOrder; // Upload changes from the source to the destination.


                _evt.WriteEntry("Sync: Sync starting from: " + sourceReplicaRootPath + " to: " + destinationProvider.RootDirectoryPath, System.Diagnostics.EventLogEntryType.Information, 4120, 40);
                agent.Synchronize();
                _evt.WriteEntry("Sync: Sync complete from: " + sourceReplicaRootPath + " to: " + destinationProvider.RootDirectoryPath, System.Diagnostics.EventLogEntryType.Information, 4130, 40);
                
            }
            finally
            {
                // Release resources.
                if (sourceProvider != null) sourceProvider.Dispose();
                if (destinationProvider != null) destinationProvider.Dispose();
            }
        }


        /// <summary>
        /// Provide information about files that were affected by the synchronization session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void OnAppliedChange(object sender, AppliedChangeEventArgs args)
        {
            switch (args.ChangeType)
            {
                case ChangeType.Create:
                    _evt.WriteEntry("Sync: Created file: " + args.NewFilePath, System.Diagnostics.EventLogEntryType.Information, 4010, 40);
                    break;
                case ChangeType.Delete:
                    _evt.WriteEntry("Sync: Applied DELETE for file: " + args.OldFilePath, System.Diagnostics.EventLogEntryType.Information, 4070, 40);
                    break;
                case ChangeType.Update:
                    _evt.WriteEntry("Sync: Applied OVERWRITE for file " + args.OldFilePath, System.Diagnostics.EventLogEntryType.Information, 4030, 40);
                    break;
                case ChangeType.Rename:
                    _evt.WriteEntry("Sync: Applied RENAME for file " + args.OldFilePath + " as " + args.NewFilePath, System.Diagnostics.EventLogEntryType.Information, 4080, 40);
                    break;
            }
        }




        /// <summary>
        /// Provide error information for any changes that were skipped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void OnSkippedChange(object sender, SkippedChangeEventArgs args)
        {
            string strMessage = "";
            strMessage = "Sync: Skipped applying " + args.ChangeType.ToString().ToUpper()
                  + " for " + (!string.IsNullOrEmpty(args.CurrentFilePath) ?
                                args.CurrentFilePath : args.NewFilePath) + " due to error";

            if (args.Exception != null)
                strMessage += "   [" + args.Exception.Message + "]";
            _evt.WriteEntry(strMessage, System.Diagnostics.EventLogEntryType.Warning, 4100, 40);
        }

        /// <summary>
        /// By default, conflicts are resolved in favor of the last writer. In this example,
        /// the change from the source in the first session (replica 1), will always
        /// win the conflict.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void OnItemConflicting(object sender, ItemConflictingEventArgs args)
        {
            string strMessage = "";
            args.SetResolutionAction(ConflictResolutionAction.SourceWins);

            strMessage = "Sync: Concurrency conflict detected for item " + args.DestinationChange.ItemId.ToString();
            _evt.WriteEntry(strMessage, System.Diagnostics.EventLogEntryType.Warning, 4110, 40);
        }
        */
        #endregion
    }
}

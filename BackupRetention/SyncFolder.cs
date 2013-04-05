using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Files;
using System.IO;
using System.Data;
using System.Diagnostics;

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

        public FileSyncReplicaOptions _fileSyncReplicaOption = FileSyncReplicaOptions.OneWay;
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
                FileSyncReplicaOption = FileSyncReplicaOptions.OneWay;
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
            dtSyncConfig.Columns["FileSyncReplicaOption"].DefaultValue = "OneWay";
            dtSyncConfig.Columns["FileSyncReset"].DefaultValue = "false";
            dtSyncConfig.Columns["FolderSyncDirectionOrder"].DefaultValue = "Upload";
            dtSyncConfig.Columns["DefaultConflictResolutionPolicy"].DefaultValue = "SourceWins";
            dtSyncConfig.Columns["ArchiveDeleted"].DefaultValue = "false";
            return dtSyncConfig;

        }

        //Microsoft Sync Methods
        //http://msdn.microsoft.com/en-us/sync/bb887623
        //http://www.microsoft.com/en-us/download/details.aspx?id=23217



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
                        _evt.WriteEntry("Sync: invalid source directory path 1 or invalid destination directory path 2",EventLogEntryType.Error);
                        return;
                    }

                    if (FileSyncReplicaOption == FileSyncReplicaOptions.OneWayMirror)
                    {
                        _evt.WriteEntry("Sync: Mirroring starting from: " + SourceFolder + " to: " + DestinationFolder, EventLogEntryType.Information,4120, 45);
                        ExecuteMirror(ref blShuttingDown);
                        _evt.WriteEntry("Sync: Mirroring ended from: " + SourceFolder + " to: " + DestinationFolder, EventLogEntryType.Information, 4130, 45);
                    }
                    else
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
                                string strSFile = file1.FullName.Replace(DestinationFolder,SourceFolder);
                                string strExclude = file1.Name.Substring(0, file1.Name.Length - 3); 
                                FileInfo SFile=null;
                                
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
                                            file1.Delete();
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
                                File.Delete(SourceFolder + "\\filesync.metadata");
                            }
                            if (File.Exists(SourceFolder + "\\File.ID"))
                            {
                                File.Delete(SourceFolder + "\\File.ID");
                            }
                            if (File.Exists(DestinationFolder + "\\filesync.metadata"))
                            {
                                File.Delete(DestinationFolder + "\\filesync.metadata");
                            }
                            if (File.Exists(DestinationFolder + "\\File.ID"))
                            {
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

                        //If two way sync then reverse the source and destination
                        if (FileSyncReplicaOption == FileSyncReplicaOptions.TwoWay)
                        {
                            SyncFileSystemReplicasOneWay(DestinationFolder, SourceFolder, null, options, FolderSyncDirectionOrder, DefaultConflictResolutionPolicy);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                _evt.WriteEntry("Sync: Exception from File Sync Provider:\n" + e.ToString());
            }
        }



        public void CreateDestinationFolders(string strSourcePath, string strDestinationPath)
        {
            System.Collections.Generic.List<RemoteFile> Directories;
            Directories = Common.GetAllDirectoriesR(strSourcePath);
            Common.CreateLocalFolderStructure(Directories, strDestinationPath);

        }


        public FileInfo GetRenamedFile(FileInfo file1, System.Collections.Generic.List<FileInfo> files)
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
                        using (FileStream fs = new FileStream(srcFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            fs.ReadByte();
                            fs.Close();
                        }

                        using (FileStream dfs = new FileStream(file1.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            dfs.ReadByte();
                            dfs.Close();
                        }
                        srcFile.Refresh();
                        file1.Refresh();
                        string strDestinationCheck = Common.WindowsPathClean(srcFile.FullName.Replace(SourceFolder, DestinationFolder));
                        if (file1.Length == srcFile.Length && !File.Exists(strDestinationCheck))
                        {

                            strPossibleMatchMD5 = Common.GetMD5HashFromFile(srcFile.FullName);
                            if (strMD5 == strPossibleMatchMD5)
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


        public void ExecuteMirror(ref bool blShuttingDown)
        {
            
            lock (lockMirror)
            {
                string strSourceFolder="";
                string strDestinationFolder = "";
                System.Collections.Generic.List<FileInfo> SourceFiles=null;
                System.Collections.Generic.List<FileInfo> DestinationFiles=null;

                System.Collections.Generic.List<DirectoryInfo> DestinationFolders = null;

                try
                {



                    if (this.FolderSyncDirectionOrder == SyncDirectionOrder.Download || this.FolderSyncDirectionOrder == SyncDirectionOrder.DownloadAndUpload)
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
                    CreateDestinationFolders(strSourceFolder, strDestinationFolder);
                    if (ArchiveFolder.Length > 0 && ArchiveDeleted)
                    {
                        if (Directory.Exists(ArchiveFolder))
                        {
                            CreateDestinationFolders(strDestinationFolder, ArchiveFolder);
                        }
                    }

                    SourceFiles = Common.WalkDirectory(strSourceFolder, ref blShuttingDown);
                    DestinationFiles = Common.WalkDirectory(strDestinationFolder, ref blShuttingDown);

                    

                    //Delete Files in DestinationFolder that are not in the SourceFolder or Fix Renamed File
                    foreach (FileInfo destinationFile in DestinationFiles)
                    {
                        string strSource = "";
                        strSource = Common.WindowsPathClean(destinationFile.FullName.Replace(strDestinationFolder, strSourceFolder));
                        if (blShuttingDown)
                        {
                            _evt.WriteEntry("Sync: Mirroring Shutting Down about to possibly delete mirror file: " + destinationFile.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                            return;
                        }

                        if (!File.Exists(strSource))
                        {
                            if (!Common.IsFileLocked(destinationFile))
                            {
                                FileInfo FileRenamed = GetRenamedFile(destinationFile, SourceFiles);
                                if (FileRenamed != null && !FileSyncReset)
                                {
                                    string strRenamedDest = Common.WindowsPathClean(FileRenamed.FullName.Replace(strSourceFolder, strDestinationFolder));
                                    //Rename Existing File
                                    File.Move(destinationFile.FullName, strRenamedDest);
                                    _evt.WriteEntry("Sync: Mirroring File Renamed: " + FileRenamed.FullName + " from:" + destinationFile.FullName + " to: " + strRenamedDest, System.Diagnostics.EventLogEntryType.Information, 4080, 45);

                                }
                                else
                                {
                                    //File Not Found and Same File Uncompressed not found so Delete
                                    if (!(destinationFile.Extension == ".7z" && File.Exists(strSource.Replace(".7z", ""))))
                                    {
                                        if (ArchiveDeleted && Directory.Exists(ArchiveFolder))
                                        {
                                            string strRenamedDest = Common.WindowsPathClean(destinationFile.FullName.Replace(strDestinationFolder, ArchiveFolder)) + "." + System.Guid.NewGuid().ToString() + destinationFile.Extension;
                                            File.Move(destinationFile.FullName, strRenamedDest);
                                            _evt.WriteEntry("Sync: Mirroring File Archived from:" + destinationFile.FullName + " to: " + strRenamedDest, System.Diagnostics.EventLogEntryType.Information, 4080, 45);
                                        }
                                        else
                                        {
                                            File.Delete(destinationFile.FullName);
                                            _evt.WriteEntry("Sync: Mirroring File Deleted: " + destinationFile.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                        }
                                        
                                    }
                                }
                            }
                        }
                    }


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
                                    
                                    dir1.Delete(true);
                                    _evt.WriteEntry("Sync: Mirroring Folder and sub folders and files Deleted: " + dir1.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                }
                                else
                                {
                                    dir1.Delete(true);
                                    _evt.WriteEntry("Sync: Mirroring Folder and sub folders and files Deleted: " + dir1.FullName, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                                }

                            }
                        }
                        catch (Exception exfd)
                        {
                            _evt.WriteEntry("Sync: Mirroring Folder and sub folders and files Delete failed for: " + dir1.FullName + " Error: " + exfd.Message, System.Diagnostics.EventLogEntryType.Information, 4070, 45);
                        }
                        
                    }

                    //Copy Files that are in the SourceFolder that are not in the DestinationFolder or if file is different
                    foreach (FileInfo srcFile in SourceFiles)
                    {
                        string strDestination = "";
                        strDestination = Common.WindowsPathClean(srcFile.FullName.Replace(strSourceFolder, strDestinationFolder));

                        if (blShuttingDown)
                        {
                            _evt.WriteEntry("Sync: Mirroring Shutting Down about to possibly mirror file: " + srcFile.FullName, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                            return;
                        }

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

                                //Refresh Last Modified Dates and Length
                                using (FileStream fs = new FileStream(srcFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    fs.ReadByte();
                                    fs.Close();
                                }
                                srcFile.Refresh();
                                destFile = Common.RefreshFileInfo(destFile);


                                //Copy the Modified file
                                if ((srcFile.Length != destFile.Length && destFile.Extension == srcFile.Extension) || srcFile.LastWriteTimeUtc != destFile.LastWriteTimeUtc)
                                {
                                    strDestination = Common.WindowsPathClean(srcFile.FullName.Replace(strSourceFolder, strDestinationFolder));
                                    if (ArchiveDeleted && Directory.Exists(ArchiveFolder))
                                    {
                                        string strRenamedDest = Common.WindowsPathClean(destFile.FullName.Replace(strDestinationFolder, ArchiveFolder)) + "." + System.Guid.NewGuid().ToString() + destFile.Extension;
                                        System.IO.File.Move(strDestination,strRenamedDest);
                                        System.IO.File.Copy(srcFile.FullName, strDestination, true);
                                        _evt.WriteEntry("Sync: Mirroring File Modified Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                    }
                                    else
                                    {
                                        System.IO.File.Copy(srcFile.FullName, strDestination, true);
                                        _evt.WriteEntry("Sync: Mirroring File Modified Copied from: " + srcFile.FullName + " to: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 4090, 45);
                                    }

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
                    _evt.WriteEntry("Sync: Mirroring Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 4000, 45);
                }
                finally
                {
                    if (SourceFiles != null)
                    {
                        SourceFiles.Clear();
                        
                    }
                    if (DestinationFiles != null)
                    {
                        DestinationFiles.Clear();
                    }
                }
            }
        }


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

                /*//Additional Events
                    provider.DestinationCallbacks.FullEnumerationNeeded += this.FullEnumerationNeededCallback;
                    destinationProvider.ItemChangeSkipped += this.ItemChangeSkippedCallback;
                    destinationProvider.ItemChanging += this.ItemChangingCallback;                    
                    destinationProvider.ItemConstraint += this.ItemConstraintCallback;
                    destinationProvider.ProgressChanged += this.ProgressChangedCallback;
                 */

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
        #endregion
    }
}

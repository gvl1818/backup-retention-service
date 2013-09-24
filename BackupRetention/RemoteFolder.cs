using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using AlexPilotti.FTPS.Client;
using AlexPilotti.FTPS.Common;

namespace BackupRetention
{
    /// <summary>
    /// Remote Folder Class will upload or download an entire directory to/from SFTP,FTPS, or FTP
    /// </summary>
    public class RemoteFolder: IFolderConfig
    {
        #region "Variables"
        /// <summary>
        /// List of all Files to Upload 
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> UploadFiles=null;

        /// <summary>
        /// List of all Key Files
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> KeyFiles=null;

        /// <summary>
        /// List of all Files that were uploaded
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> FilesUploaded=null;

        /// <summary>
        /// List of all Files that were downloaded
        /// </summary>
        private System.Collections.Generic.List<System.IO.FileInfo> FilesDownloaded=null;

        private System.Diagnostics.EventLog _evt;
        private string ep = "ISincerely!HopeThisIsNotUsed@";
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
        /// <summary>
        /// Time to execute upload or download of entire directory
        /// </summary>
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

        private bool _enabled = true;
        /// <summary>
        /// Remote Upload or Download of entire directory enabled?
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

        private string _host = "";
        /// <summary>
        /// Remote Server IP address or hostname
        /// </summary>
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        private int _port = 22;
        /// <summary>
        /// Port for remote protocol
        /// </summary>
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        private ProtocolOptions _protocol = ProtocolOptions.SFTP;
        /// <summary>
        /// Remote Server protocol
        /// </summary>
        public ProtocolOptions Protocol
        {
            get
            {
                return _protocol;
            }
            set
            {
                _protocol = value;
            }
        }

        private bool _allowAnyCertificate = true;
        /// <summary>
        /// FTPS Allow Any Certificate or only valid
        /// </summary>
        public bool AllowAnyCertificate
        {
            get
            {
                return _allowAnyCertificate;
            }
            set
            {
                _allowAnyCertificate = value;
            }
        }

        private int _timeout = 120000;
        /// <summary>
        /// FTPS timeout
        /// </summary>
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


        private string _username = "";
        /// <summary>
        /// Username for remote connection
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        private string _keyFileDirectory = "";
        /// <summary>
        /// Directory path for keyfiles
        /// </summary>
        public string KeyFileDirectory
        {
            get
            {
                return _keyFileDirectory;
            }
            set
            {
                _keyFileDirectory = Common.WindowsPathClean(value);
            }
        }

        private bool _usePassPhrase = false;
        /// <summary>
        /// Use Password for Key file
        /// </summary>
        public bool UsePassPhrase
        {
            get
            {
                return _usePassPhrase;
            }
            set
            {
                _usePassPhrase = value;
            }
        }

        private string _password = "";
        /// <summary>
        /// Password for remote server connection or password for keyfile
        /// </summary>
        public string Password
        {
            set
            {
                _password = value;
            }
        }


        private string _remoteDirectory = "";
        /// <summary>
        /// Remote folder to upload to or download from
        /// </summary>
        public string RemoteDirectory
        {
            get
            {
                return _remoteDirectory;
            }
            set
            {
                _remoteDirectory = value;
            }
        }

        private string _backupFolder = "";
        /// <summary>
        /// Local Folder to upload or download to
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

        private TransferDirectionOptions _transferDirection = TransferDirectionOptions.Upload;
        /// <summary>
        /// Upload or Download entire directory
        /// </summary>
        public TransferDirectionOptions TransferDirection
        {
            get
            {
                return _transferDirection;
            }
            set
            {
                _transferDirection = value;
            }
        }

        private OverwriteOptions _overwrite = OverwriteOptions.LastModifiedChangeOverwrite;
        /// <summary>
        /// Whether to Overwrite files, not overwrite, or overwrite based on last modified.
        /// </summary>
        public OverwriteOptions Overwrite
        {
            get
            {
                return _overwrite;
            }
            set
            {
                _overwrite = value;
            }
        }
        #endregion
        #region "Methods"

        /// <summary>
        /// Initializes RemoteConfig datatable and returns it
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtRemote;
            dtRemote = new DataTable("RemoteConfig");

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtRemote.Columns.Add(dcID);
            columns[0] = dtRemote.Columns["ID"];
            dtRemote.PrimaryKey = columns;


            //Create Columns
            dtRemote.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Time", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Host", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Protocol", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Port", typeof(Int32)));
            dtRemote.Columns.Add(new DataColumn("Username", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Password", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("KeyFileDirectory", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("UsePassPhrase", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("BackupFolder", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("RemoteDirectory", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("TransferDirection", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Overwrite", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("AllowAnyCertificate", typeof(String)));
            dtRemote.Columns.Add(new DataColumn("Timeout", typeof(Int32)));

            dtRemote.Columns["Enabled"].DefaultValue = "true";
            dtRemote.Columns["IntervalType"].DefaultValue = "Daily";
            dtRemote.Columns["Interval"].DefaultValue = "0";
            dtRemote.Columns["Monday"].DefaultValue = "true";
            dtRemote.Columns["Tuesday"].DefaultValue = "true";
            dtRemote.Columns["Wednesday"].DefaultValue = "true";
            dtRemote.Columns["Thursday"].DefaultValue = "true";
            dtRemote.Columns["Friday"].DefaultValue = "true";
            dtRemote.Columns["Saturday"].DefaultValue = "true";
            dtRemote.Columns["Sunday"].DefaultValue = "true";
           
            dtRemote.Columns["Time"].DefaultValue = "00:00";
            dtRemote.Columns["Protocol"].DefaultValue = "SFTP";
            dtRemote.Columns["TransferDirection"].DefaultValue = "Upload";
            dtRemote.Columns["Overwrite"].DefaultValue = "ForceOverwrite";
            dtRemote.Columns["Port"].DefaultValue = 22;
            dtRemote.Columns["Timeout"].DefaultValue = 120000;
            dtRemote.Columns["AllowAnyCertificate"].DefaultValue = "true";

            return dtRemote;
        }

        private void init_RemoteFolder()
        {
            _evt = Common.GetEventLog;
        }

        /// <summary>
        /// Default contructor
        /// </summary>
        public RemoteFolder()
        {
            init_RemoteFolder();

        }

        /// <summary>
        /// Remote Folder Contructor for when passed a datarow
        /// </summary>
        /// <param name="row"></param>
        public RemoteFolder(DataRow row)
        {
            init_RemoteFolder();

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
            
            Host = Common.FixNullstring(row["Host"]);
            str = Common.FixNullstring(row["Protocol"]);
            try
            {
                Protocol = (ProtocolOptions)System.Enum.Parse(typeof(ProtocolOptions), str);
            }
            catch (Exception)
            {
                Protocol = ProtocolOptions.SFTP;
            }
            Port = Common.FixNullInt32(row["Port"]);
            Username = Common.FixNullstring(row["Username"]);
            Password = Common.FixNullstring(row["Password"]);
            KeyFileDirectory = Common.FixNullstring(row["KeyFileDirectory"]);
            UsePassPhrase = Common.FixNullbool(row["UsePassPhrase"]);
            BackupFolder = Common.FixNullstring(row["BackupFolder"]);
            RemoteDirectory = Common.FixNullstring(row["RemoteDirectory"]);
            AllowAnyCertificate = Common.FixNullbool(row["AllowAnyCertificate"]);
            Timeout = Common.FixNullInt32(row["Timeout"]);

            str = Common.FixNullstring(row["TransferDirection"]);
            try
            {
                TransferDirection = (TransferDirectionOptions)System.Enum.Parse(typeof(TransferDirectionOptions), str);
            }
            catch (Exception)
            {
                TransferDirection = TransferDirectionOptions.Upload;
            }

            str = Common.FixNullstring(row["Overwrite"]);
            try
            {
                Overwrite = (OverwriteOptions)System.Enum.Parse(typeof(OverwriteOptions), str);
            }
            catch (Exception)
            {
                Overwrite = OverwriteOptions.ForceOverwrite;
            }



        }

        /// <summary>
        /// Remote Folder Deconstructor
        /// </summary>
        ~RemoteFolder()
        {
            try
            {
                if (UploadFiles != null)
                {
                    UploadFiles.Clear();
                }
                if (FilesUploaded != null)
                {
                    FilesUploaded.Clear();
                }
                if (FilesDownloaded != null)
                {
                    FilesDownloaded.Clear();
                }
            }
            catch (Exception)
            {

            }

            UploadFiles = null;
            FilesUploaded = null;
            FilesDownloaded = null;
            _evt = null;
        }

        /// <summary>
        /// Executes Remote Configured Upload or Download of files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void ExecuteTransfer(ref bool blShuttingDown)
        {
            if (this.Enabled)
            {
                switch (Protocol)
                {
                    case ProtocolOptions.SFTP:
                        SFTP(ref blShuttingDown);
                        break;
                    case ProtocolOptions.FTPsExplicit:
                        FTPs(ref blShuttingDown);
                        break;
                    case ProtocolOptions.FTPsImplicit:
                        FTPs(ref blShuttingDown);
                        break;
                    case ProtocolOptions.FTP:
                        FTPs(ref blShuttingDown);
                        break;
                }
            }
        }



        /// <summary>
        /// SFTP procedure to upload or download files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void SFTP(ref bool blShuttingDown)
        {
            SftpClient sftp = null;
            try
            {
                string upassword = "";
                AES256 aes = new AES256(ep);
                if (_password.Length > 0)
                {
                    upassword = aes.Decrypt(_password);
                }
                if (string.IsNullOrEmpty(KeyFileDirectory))
                {
                    sftp = new SftpClient(Host, Port, Username, upassword);
                }
                else
                {
                    KeyFiles = Common.WalkDirectory(KeyFileDirectory, ref blShuttingDown);
                    List<PrivateKeyFile> PKeyFiles = new List<PrivateKeyFile>();
                    foreach (FileInfo kfile in KeyFiles)
                    {
                        PrivateKeyFile p;
                        if (UsePassPhrase)
                        {
                            p = new PrivateKeyFile(kfile.FullName, upassword);
                        }
                        else
                        {
                            p = new PrivateKeyFile(kfile.FullName);
                        }
                        PKeyFiles.Add(p);
                    }


                    sftp = new SftpClient(Host, Port, Username, PKeyFiles.ToArray());
                }
                
                BackupFolder = BackupFolder.Replace("\\\\", "\\");
                UploadFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown);
                sftp.Connect();
                upassword = "";
                _evt.WriteEntry("Remote Sync SFTP: Connected successfully to Host: " + Host, System.Diagnostics.EventLogEntryType.Information,1005, 10);
                                        
                switch (TransferDirection)
                {
                    case TransferDirectionOptions.Upload:
                        List<DirectoryInfo> Dirs = null;
                        try
                        {
                            string strRemotePath = "";
                            strRemotePath = Common.FixNullstring(RemoteDirectory);
                            if (RemoteDirectory != "")
                            {
                                strRemotePath = RemoteDirectory + "/";
                            }
                            strRemotePath = Common.RemotePathClean(Common.FixNullstring(strRemotePath));
                            BackupFolder = Common.WindowsPathClean(Common.FixNullstring(BackupFolder));

                            if (!string.IsNullOrEmpty(strRemotePath))
                            {
                                if (!sftp.Exists(strRemotePath))
                                {
                                    sftp.CreateDirectory(strRemotePath);
                                }
                                sftp.ChangeDirectory(strRemotePath);
                            }

                            //Create Directories
                            Dirs = Common.GetAllDirectories(BackupFolder);
                            foreach (DirectoryInfo dir in Dirs)
                            {
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    strRemotePath = dir.FullName;
                                    strRemotePath = Common.RemotePathCombine(RemoteDirectory, strRemotePath, BackupFolder);
                                    if (blShuttingDown)
                                    {
                                        _evt.WriteEntry("Remote Sync: Shutting Down, about to Create a Folder: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1040, 10);
                                        return;
                                    }
                                }
                                catch (Exception)
                                {
                                    _evt.WriteEntry("Remote Sync: Folder Path Text Formatting Error: " + strRemotePath, System.Diagnostics.EventLogEntryType.Error, 1040, 10);
                                }
                                try
                                {

                                    sftp.CreateDirectory(strRemotePath);
                                    _evt.WriteEntry("Remote Sync: Folder Created on " + Host + ": " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1040, 10);

                                }
                                catch (Exception)
                                {

                                }
                            }

                            //Upload every file found and place in the correct directory
                            UploadFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown);
                            foreach (FileInfo ufile in UploadFiles)
                            {

                                try
                                {
                                    bool blFileFound = false;
                                    bool blOverwriteFile = false;

                                    if (blShuttingDown)
                                    {
                                        throw new Exception("Shutting Down");
                                    }

                                    strRemotePath = ufile.DirectoryName;
                                    strRemotePath = Common.RemotePathCombine(Common.FixNullstring(RemoteDirectory),strRemotePath,Common.FixNullstring(BackupFolder));
                                    
                                    

                                    if (!string.IsNullOrEmpty(strRemotePath))
                                    {
                                        sftp.ChangeDirectory(strRemotePath);
                                    }
                                    
                                    strRemotePath = Common.RemotePathCombine(strRemotePath, ufile.Name);


                                    if (sftp.Exists(strRemotePath))
                                    {
                                        blFileFound = true;
                                        //if (!((ufile.LastWriteTimeUtc == sftp.GetLastWriteTimeUtc(strRemotePath))))
                                        SftpFile mupload = sftp.Get(strRemotePath);
                                        if (ufile.Length != mupload.Length)
                                        {
                                            blOverwriteFile = true;
                                        }
                                    }

                                    if (sftp.Exists(strRemotePath + ".7z"))
                                    {
                                        blFileFound = true;
                                        SftpFile mupload = sftp.Get(strRemotePath + ".7z");
                                        
                                        //File Size can't be used to compare and last write time cannot be modified so can't compare
                                        blOverwriteFile = true;  //Overwrite Options can be specified by the user

                                        /*if (!((ufile.LastWriteTimeUtc == mupload.LastWriteTimeUtc)))
                                        {
                                            blOverwriteFile = true;
                                        }*/
                                    }
                                    if (blShuttingDown)
                                    {

                                        _evt.WriteEntry("Remote Sync: Shutting Down, about to possibly upload a file: " + ufile.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1010, 10);
                                        return;
                                    }
                                    if (ufile.Name.ToLower() != "file.id" && ufile.Name.ToLower() != "filesync.metadata")
                                    {
                                        if ((blFileFound == false || blOverwriteFile || (Overwrite == OverwriteOptions.ForceOverwrite && blFileFound)) && !(Overwrite == OverwriteOptions.NoOverwrite && blFileFound == true))
                                        {
                                            /*if (sftp.Exists(ufile.FullName))
                                            {
                                                SftpFileAttributes fa=sftp.GetAttributes(ufile.FullName);
                                                fa.OwnerCanWrite = true;
                                                fa.OthersCanWrite = true;
                                                fa.GroupCanWrite = true;
                                                sftp.SetAttributes(ufile.FullName,fa);
                                                sftp.DeleteFile(ufile.FullName);
                                            }*/
                                            sftp.UploadFile(File.OpenRead(ufile.FullName), strRemotePath);
                                            _evt.WriteEntry("Remote Sync SFTP: File Uploaded: " + ufile.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 1010, 10);
                                        
                                            //sftp.SetLastWriteTime(ufile.Name, ufile.LastWriteTimeUtc); //not implemented yet 
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + "Upload of FileName: " + ufile.FullName + " Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                                }

                            }
                        }
                        catch (Exception exsftup)
                        {
                            _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + " Error: " + exsftup.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                                
                        }
                        finally
                        {
                            if (Dirs != null)
                            {
                                Dirs.Clear();
                            }
                            Dirs = null;
                            if (UploadFiles != null)
                            {
                                UploadFiles.Clear();
                            }
                            UploadFiles = null;
                        }
                       
                        break;
                    case TransferDirectionOptions.Download:
                        
                        List<RemoteFile> RemoteFiles=null;
                        try
                        {
                            if (!string.IsNullOrEmpty(RemoteDirectory))
                            {
                                sftp.ChangeDirectory(RemoteDirectory);

                            }

                            RemoteFiles = Common.GetRemoteDirectories(RemoteDirectory, sftp, "");
                            //Creates Folder Locally in the BackupFolder
                            Common.CreateLocalFolderStructure(RemoteFiles, BackupFolder);

                            //Downloads Files in each directory
                            foreach (RemoteFile ftpfile in RemoteFiles)
                            {
                                string DownloadPath = "";
                                string strLocalFilePath = "";
                                string strRemoteFilePath = "";
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    DownloadPath = Common.RemotePathCombine(RemoteDirectory,ftpfile.ParentDirectory);
                                    if (!ftpfile.IsDirectory)
                                    {
                                        strLocalFilePath = Common.WindowsPathCombine(BackupFolder, ftpfile.ParentDirectory);
                                        strLocalFilePath = Common.WindowsPathCombine(strLocalFilePath, ftpfile.Name);
                                        strRemoteFilePath = Common.RemotePathCombine(DownloadPath, ftpfile.Name);

                                        if (blShuttingDown)
                                        {
                                            if (sftp != null)
                                            {
                                                if (sftp.IsConnected)
                                                {
                                                    sftp.Disconnect();
                                                }
                                                sftp.Dispose();
                                            }
                                            _evt.WriteEntry("Remote Sync: Shutting Down, about to possibly download a Host: " + Host + " file: " + DownloadPath + "/" + ftpfile.Name + " From: " + strLocalFilePath, System.Diagnostics.EventLogEntryType.Information, 1020, 10);
                                            return;
                                        }
                                        if (ftpfile.Name.ToLower() != "file.id" && ftpfile.Name.ToLower() != "filesync.metadata")
                                        {
                                            if (Common.DownloadFile(strLocalFilePath, strRemoteFilePath, ftpfile, Overwrite))
                                            {
                                                if (File.Exists(strLocalFilePath))
                                                {
                                                    File.SetAttributes(strLocalFilePath, FileAttributes.Normal);
                                                    File.Delete(strLocalFilePath);
                                                }
                                                using (FileStream fs = new FileStream(strLocalFilePath, FileMode.Create))
                                                {

                                                    sftp.DownloadFile(strRemoteFilePath, fs);
                                                    _evt.WriteEntry("Remote Sync SFTP: File Downloaded: " + strRemoteFilePath + " Host: " + Host + " To: " + strLocalFilePath, System.Diagnostics.EventLogEntryType.Information, 1020, 10);

                                                }
                                            }
                                        }

                                    }
                                }
                                catch (Exception exintry)
                                {
                                    _evt.WriteEntry("Remote Sync SFTP: File Download Error: " + DownloadPath + "/" + ftpfile.Name + " Host: " + Host + " To: " + strLocalFilePath + " Error: " + exintry.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                                }

                            }

                            /*
                            
                            //Example Simple Usage
                            RemoteFiles = sftp.ListDirectory(".");
                            
                            foreach (Renci.SshNet.Sftp.SftpFile ftpfile in RemoteFiles)
                            {
                                using (FileStream fs = new FileStream(BackupFolder + "\\" + ftpfile.Name, FileMode.Create))
                                {
                                    sftp.DownloadFile(ftpfile.FullName, fs);
                                }
                                    
                            }
                            */
                        }
                        catch (Exception excon)
                        {
                            _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + " Error: " + excon.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);
                        }
                        finally
                        {
                            if (RemoteFiles != null)
                            {
                                RemoteFiles.Clear();
                            }
                            RemoteFiles = null;
                        }


                        break;
                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Remote Sync SFTP: Host: " + Host + " Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 1000, 10);     
            }
            finally
            {
                if (sftp != null)
                {
                    if (sftp.IsConnected)
                    {
                        sftp.Disconnect();
                    }
                    sftp.Dispose();
                }
            }
        }

        /// <summary>
        /// Validates any FTPS certificate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateTestServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Accept any certificate
            return true;
        }

        /// <summary>
        /// Validates only valid FTPS certificates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers. 
            return false;
        }

        /// <summary>
        /// FTPS procedure to upload or download files
        /// </summary>
        /// <param name="blShuttingDown"></param>
        public void FTPs(ref bool blShuttingDown)
        {
            FTPSClient FTPS = null;
            bool blFileFound = false;
            bool blOverwriteFile = false;
            try
            {
                FTPS = new FTPSClient();
                AES256 aes = new AES256(ep);
                string upassword = aes.Decrypt(_password);
                
                if (Protocol == ProtocolOptions.FTP)
                {
                    FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.ClearText, null, null, 0, 0, 0, Timeout);
                }
                else
                {
                    if (AllowAnyCertificate)
                    {
                        if (Protocol == ProtocolOptions.FTPsExplicit)
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.CredentialsRequired | ESSLSupportMode.DataChannelRequested, new RemoteCertificateValidationCallback(ValidateTestServerCertificate), null, 0, 0, 0, Timeout);

                        }
                        else
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.Implicit, new RemoteCertificateValidationCallback(ValidateTestServerCertificate), null, 0, 0, 0, Timeout);

                        }

                    }
                    else
                    {
                        if (Protocol == ProtocolOptions.FTPsExplicit)
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.CredentialsRequired | ESSLSupportMode.DataChannelRequested, new RemoteCertificateValidationCallback(ValidateServerCertificate), null, 0, 0, 0, Timeout);
                        }
                        else
                        {
                            FTPS.Connect(Host, Port, new NetworkCredential(Username, upassword), ESSLSupportMode.Implicit, new RemoteCertificateValidationCallback(ValidateServerCertificate), null, 0, 0, 0, Timeout);
                        }
                    }
                }

                _evt.WriteEntry("Remote Sync: FTPS Connected Successfully to: " + Host, System.Diagnostics.EventLogEntryType.Information, 2005, 20);
                                
                upassword = "";
                switch (TransferDirection)
                {
                    case TransferDirectionOptions.Upload:
                        BackupFolder = BackupFolder.Replace("\\\\", "\\");
                        IList<DirectoryListItem> RemoteFilesU=null;

                        try
                        {
                            FTPS.SetCurrentDirectory(RemoteDirectory);
                            RemoteFilesU = FTPS.GetDirectoryList();
                            UploadFiles = Common.WalkDirectory(BackupFolder, ref blShuttingDown);

                            //CreateRemote Directories
                            foreach (DirectoryInfo dir in Common.GetAllDirectories(BackupFolder))
                            {
                                string strRemotePath = "";
                                strRemotePath=Common.RemotePathCombine(RemoteDirectory, dir.FullName, BackupFolder);
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }

                                if (blShuttingDown)
                                {

                                    _evt.WriteEntry("Remote Sync: Shutting down about to possibly create a folder on Host: " + Host + " Folder: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2040, 20);
                                    return;
                                }

                                try
                                {
                                    FTPS.MakeDir(strRemotePath);
                                    _evt.WriteEntry("Remote Sync: Folder Created on " + Host + " : " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2040, 20);
                                }
                                catch (Exception)
                                {
                                }

                                //FTPS.SetCurrentDirectory(strRemotePath);
                            }

                            //Upload Each File
                            foreach (FileInfo fileU in UploadFiles)
                            {
                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    string strRemotePath = "";

                                    strRemotePath = Common.RemotePathCombine(RemoteDirectory, fileU.DirectoryName, BackupFolder);
                                    strRemotePath = Common.RemotePathCombine(strRemotePath, fileU.Name);

                                    blFileFound = false;
                                    blOverwriteFile = false;
                                    try
                                    {

                                        if (FTPS.GetFileTransferSize(strRemotePath) > 0)
                                        {
                                            blFileFound = true;
                                            if (!(/*(fileU.LastAccessTimeUtc == FTPS.GetFileModificationTime(strRemotePath)) &&*/ ((ulong)fileU.Length == FTPS.GetFileTransferSize(strRemotePath))))
                                            {
                                                blOverwriteFile = true;
                                            }

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //File Not found No Action Necessary on Error
                                    }

                                    try
                                    {
                                        if (FTPS.GetFileTransferSize(strRemotePath + ".7z") > 0)
                                        {
                                            blFileFound = true;
                                            //7z file exists but there is no current way to compare the 7zipped file vs the non compressed file so the user overwrite option will force the appropriate action
                                            blOverwriteFile = true;
                                            /*
                                            if (!((fileU.LastAccessTimeUtc == FTPS.GetFileModificationTime(strRemotePath + ".7z")) && ((ulong)fileU.Length == FTPS.GetFileTransferSize(strRemotePath + ".7z"))))
                                            {
                                                blOverwriteFile = true;
                                            }
                                            */

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        //File Not Found No Action Necessary on Error
                                    }
                                    if (blShuttingDown)
                                    {

                                        _evt.WriteEntry("Remote Sync FTPS: Shutting Down about to possible upload a file: " + fileU.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2010, 20);
                                        return;
                                    }
                                    if (fileU.Name.ToLower() != "file.id" && fileU.Name.ToLower() != "filesync.metadata")
                                    {
                                        if ((!blFileFound || blOverwriteFile || (Overwrite == OverwriteOptions.ForceOverwrite && blFileFound)) && !(Overwrite == OverwriteOptions.NoOverwrite && blFileFound))
                                        {
                                            //This Uploads the file
                                            FTPS.PutFile(fileU.FullName, strRemotePath);
                                            _evt.WriteEntry("Remote Sync FTPS: File Uploaded: " + fileU.FullName + " Host: " + Host + " To: " + strRemotePath, System.Diagnostics.EventLogEntryType.Information, 2010, 20);

                                        }
                                    }
                                }
                                catch (Exception exp)
                                {
                                    _evt.WriteEntry("Remote Sync FTPS: Host:" + Host + " Upload FileName: " + fileU.FullName + " Error: " + exp.Message, System.Diagnostics.EventLogEntryType.Error, 2010, 20);
                                }

                            }
                        }
                        catch (Exception exu)
                        {
                            _evt.WriteEntry("Remote Sync FTPS: Upload Error on Host:" + Host + " Error: " + exu.Message, System.Diagnostics.EventLogEntryType.Error, 2010, 20);
                        }
                        finally
                        {
                            if (RemoteFilesU != null)
                            {
                                RemoteFilesU.Clear();
                            }
                            if (UploadFiles != null)
                            {
                                UploadFiles.Clear();
                            }
                            RemoteFilesU = null;
                            UploadFiles = null;

                        }
                        break;
                    case TransferDirectionOptions.Download:
                        
                        List<RemoteFile> RemoteFilesD=null;

                        try
                        {
                            FTPS.SetCurrentDirectory(RemoteDirectory);
                            RemoteFilesD = Common.GetRemoteDirectories(RemoteDirectory, FTPS, "");
                            Common.CreateLocalFolderStructure(RemoteFilesD, BackupFolder);
                            foreach (RemoteFile FileD in RemoteFilesD)
                            {
                                string strLocalFile = "";
                                string strRemoteFile = "";

                                if (blShuttingDown)
                                {
                                    throw new Exception("Shutting Down");
                                }
                                try
                                {
                                    if (!FileD.IsDirectory)
                                    {


                                        strLocalFile = Common.WindowsPathCombine(BackupFolder, FileD.ParentDirectory);
                                        strLocalFile = Common.WindowsPathCombine(strLocalFile, FileD.Name);
                                        strRemoteFile = FileD.FullName;

                                        if (blShuttingDown)
                                        {
                                            FTPS.Close();
                                            FTPS.Dispose();
                                            _evt.WriteEntry("Remote Sync FTPS: Shutting Down about to possibly download file: " + strRemoteFile + " Host: " + Host + " To: " + strLocalFile, System.Diagnostics.EventLogEntryType.Information, 2020, 20);
                                            return;
                                        }
                                        if (FileD.Name.ToLower() != "file.id" && FileD.Name.ToLower() != "filesync.metadata")
                                        {
                                            if (Common.DownloadFile(strLocalFile, strRemoteFile, FileD, Overwrite))
                                            {
                                                FTPS.GetFile(strRemoteFile, strLocalFile);
                                                _evt.WriteEntry("Remote Sync FTPS: File Downloaded: " + strRemoteFile + " Host: " + Host + " To: " + strLocalFile, System.Diagnostics.EventLogEntryType.Information, 2020, 20);
                                            }
                                        }

                                    }
                                }
                                catch (Exception exdi)
                                {
                                    _evt.WriteEntry("Remote Sync FTPS: File Download Error: " + strRemoteFile + " Host: " + Host + " To: " + strLocalFile + " Error: " + exdi.Message, System.Diagnostics.EventLogEntryType.Error, 2020, 20);
                                }

                            }
                        }
                        catch (Exception exd)
                        {
                            _evt.WriteEntry("Remote Sync FTPS: Download Error: Host: " + Host + " Error: " + exd.Message, System.Diagnostics.EventLogEntryType.Error, 2020, 20);
                        }
                        finally
                        {
                            if (RemoteFilesD !=null)
                            {
                                RemoteFilesD.Clear();
                            }
                            RemoteFilesD = null;
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Remote Sync FTPS: Error: Host: " + Host + " Error: " + ex.Message, System.Diagnostics.EventLogEntryType.Error, 2000, 20);    
            }
            finally
            {
                if (FTPS != null)
                {
                    try
                    {
                        FTPS.Close();
                    }
                    catch (Exception)
                    {

                    }

                    FTPS.Dispose();
                    FTPS = null;
                }
            }

        }
        #endregion

    }

    
}

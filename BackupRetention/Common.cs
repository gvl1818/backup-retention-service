using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using AlexPilotti.FTPS.Client;
using AlexPilotti.FTPS.Common;
using System.Security.Cryptography;
using System.Management;
using System.Runtime.InteropServices;



namespace BackupRetention
{


    /// <summary>
    /// Enum for retention algorithm options GFS (Grandfather, father, son), KeepAll (No deleting), etc.
    /// </summary>
    public enum RetentionAlgorithmOptions
    {

        GFS
        ,KeepAll
        ,KeepDaily
        ,KeepWeekly
        ,KeepMonthly
    }

    public enum CompressSourceOptions
    {
        File
        ,Folder
    }


    public enum FileSyncReplicaOptions
    {
        OneWay
        ,TwoWay
        ,OneWayMirror
    }

    public enum ProtocolOptions
    {
        SFTP
        ,FTPsImplicit
        ,FTPsExplicit
        ,FTP

    }

    public enum TransferDirectionOptions
    {
        Upload
        ,Download
    }

    public enum OverwriteOptions
    {
        NoOverwrite
        ,ForceOverwrite
        ,LastModifiedChangeOverwrite
    }

    public enum CompressOption
    {
        Compress
        ,Extract
    }


    public enum FileOperations
    {
        Created   
        ,Modified
        ,Deleted
        ,Renamed
    }

    public interface IFolderConfig
    {
        int ID
        {
            get;
            set;
        }

        string Time
        {
            get;
            set;
        }

        bool Monday
        {
            get;
            set;
        }

        bool Tuesday
        {
            get;
            set;
        }

        bool Wednesday
        {
            get;
            set;
        }

        bool Thursday
        {
            get;
            set;
        }

        bool Friday
        {
            get;
            set;
        }

        bool Saturday
        {
            get;
            set;
        }

        bool Sunday
        {
            get;
            set;
        }

        int DayOfMonth
        {
            get;
            set;
        }

        bool Enabled
        {
            get;
            set;
        }


    }

    public static class Common
    {

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
        out ulong lpFreeBytesAvailable,
        out ulong lpTotalNumberOfBytes,
        out ulong lpTotalNumberOfFreeBytes);


        


        public static EventLog _evt = null;
        /// <summary>
        /// Gets Event Log Class instantiated.
        /// </summary>
        public static EventLog GetEventLog
        {

            get
            {
                if (_evt == null)
                {
                    //_evt = new Event_Log("BackupRetentionService", EventLogEntryType.Information,"BackupRe");
                    string strApplication_Name = "BackupRetention";
                    string strLog_Name = "BackupRe";
                    
                    try
                    {
                        EventSourceCreationData d = new EventSourceCreationData(strApplication_Name, strLog_Name);

                        if (!EventLog.SourceExists(strApplication_Name))
                        {
                            EventLog.CreateEventSource(d);
                        }
                        /*else
                        {
                            EventLog.DeleteEventSource(strApplication_Name, ".");
                            EventLog.CreateEventSource(d);
                        }*/
                    }
                    catch (Exception)
                    {
                        
                    }
                    _evt = new System.Diagnostics.EventLog(strLog_Name, ".", strApplication_Name);
                    

                }
                return _evt;
            }


        }


        /// <summary>
        /// Gets Drive Space Used Percent of Drive Letter passed
        /// </summary>
        /// <param name="strDrive"></param>
        /// <returns></returns>
        public static double DriveSpaceUsed(string strPath)
        {
            _evt = GetEventLog;
            double dblPercentFull = -1;
            try
            {
                /*
                
                DriveInfo drive = new DriveInfo(strDrive);
                long lfreeSpaceInBytes = drive.TotalFreeSpace;
                long lTotalSpaceInBytes = drive.TotalSize;
                double dblPercentFreeSpace = (double)lfreeSpaceInBytes / lTotalSpaceInBytes;
                double dblPercentFull = (double)1.0 - dblPercentFreeSpace;
                return Math.Round(dblPercentFull, 4);
                */
                
                ulong FreeBytesAvailable;
                ulong TotalNumberOfBytes;
                ulong TotalNumberOfFreeBytes;

                bool success = GetDiskFreeSpaceEx(strPath, out FreeBytesAvailable, out TotalNumberOfBytes,
                                    out TotalNumberOfFreeBytes);
                if (!success)
                    throw new System.ComponentModel.Win32Exception();
                double dblPercentFreeSpace = (double)TotalNumberOfFreeBytes / TotalNumberOfBytes;
                dblPercentFull = (double)1.0 - dblPercentFreeSpace;
                
               
                return dblPercentFull;

            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                return -1.0;
            }
        }

        /// <summary>
        /// Gets Drive Free Space of Drive letter passed
        /// </summary>
        /// <param name="strDrive"></param>
        /// <returns></returns>
        public static double DriveFreeSpace(string strPath)
        {
            double dblPercentFreeSpace = -1;
            _evt = GetEventLog;
            try
            {
                /*
                DriveInfo drive = new DriveInfo(strDrive);
                long lfreeSpaceInBytes = drive.TotalFreeSpace;
                long lTotalSpaceInBytes = drive.TotalSize;
                double dblPercentFreeSpace = (double)lfreeSpaceInBytes / lTotalSpaceInBytes;
                return Math.Round(dblPercentFreeSpace, 4);
                */

                ulong FreeBytesAvailable;
                ulong TotalNumberOfBytes;
                ulong TotalNumberOfFreeBytes;

                bool success = GetDiskFreeSpaceEx(strPath, out FreeBytesAvailable, out TotalNumberOfBytes,
                                    out TotalNumberOfFreeBytes);
                if (!success)
                    throw new System.ComponentModel.Win32Exception();
                dblPercentFreeSpace = (double)TotalNumberOfFreeBytes / TotalNumberOfBytes;

            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                return -1.0;
            }
            return dblPercentFreeSpace;
        }

        public static long DriveFreeSpaceBytes(string strPath)
        {
            _evt = GetEventLog;
            try
            {
                /*//This doesn't work with UNC paths
                DriveInfo drive = new DriveInfo(strDrive);
                long lfreeSpaceInBytes = drive.TotalFreeSpace;
                return lfreeSpaceInBytes;
                 */
                ulong FreeBytesAvailable;
                ulong TotalNumberOfBytes;
                ulong TotalNumberOfFreeBytes;

                bool success = GetDiskFreeSpaceEx(strPath, out FreeBytesAvailable, out TotalNumberOfBytes,
                                    out TotalNumberOfFreeBytes);
                if (!success)
                    throw new System.ComponentModel.Win32Exception();
                return (long) TotalNumberOfFreeBytes;
            }
            catch (Exception ex)
            {
                _evt.WriteEntry(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Calculates Folder Size by adding up all file.Length
        /// this works for UNC and Non UNC paths.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static float CalculateFolderSize(string folder)
        {
            float folderSize = 0.0f;
            try
            {
                //Checks if the path is valid or not
                if (!DirectoryExists(folder))
                    return folderSize;
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(folder))
                        {
                            if (File.Exists(file))
                            {
                                FileInfo finfo = new FileInfo(file);
                                folderSize += (float) finfo.Length;
                            }
                        }

                        foreach (string dir in Directory.GetDirectories(folder))
                        {
                            folderSize += CalculateFolderSize(dir);
                        }
                    }
                    catch (NotSupportedException)
                    {
                        //Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                //Console.WriteLine("Unable to calculate folder size: {0}", e.Message);
            }
            return folderSize;
        }


       

        /// <summary>Checks if the given path is on a network drive.</summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static bool isNetworkDrive(string pPath)
        {
            ManagementObject mo = new ManagementObject();

            if (pPath.StartsWith(@"\\")) { return true; }

            // Get just the drive letter for WMI call
            string driveletter = GetDriveLetter(pPath);

            mo.Path = new ManagementPath(string.Format("Win32_LogicalDisk='{0}'", driveletter));

            // Get the data we need
            uint DriveType = Convert.ToUInt32(mo["DriveType"]);
            mo = null;

            return DriveType == 4;
        }

        /// <summary>Given a path will extract just the drive letter with volume separator.</summary>
        /// <param name="pPath"></param>
        /// <returns>string</returns>
        public static string GetDriveLetter(string pPath)
        {
            if (pPath.StartsWith(@"\\")) { throw new ArgumentException("A UNC path was passed to GetDriveLetter"); }
            return Directory.GetDirectoryRoot(pPath).Replace(Path.DirectorySeparatorChar.ToString(), "");
        }


        /// <summary>
        ///  NthDayOfMonth
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dow"></param>
        /// <param name="n"></param>
        /// <returns>bool</returns>
        private static bool NthDayOfMonth(DateTime date, DayOfWeek dow, int n)
        {
            int d = date.Day;
            return date.DayOfWeek == dow && (d - 1) / 7 == (n - 1);
        }

        /// <summary>
        /// Checks whether file is locked by the operating system or another process
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                stream.Close();
                stream.Dispose();
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                try
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
                catch (Exception)
                {
                    
                }
                
            }

            //file is not locked
            return false;
        }

        public static FileInfo RefreshFileInfo(FileInfo file)
        {
            using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.ReadByte();
                fs.Close();
            }
            file.Refresh();
            return file;
        }


        public static string GetMD5HashFromFile(string strPath)
        {
            StringBuilder sb = new StringBuilder();
            FileStream fs=null;
            MD5 md5 = new MD5CryptoServiceProvider();
            try
            {
                fs = new FileStream(strPath, FileMode.Open);
                
                byte[] retVal = md5.ComputeHash(fs);
                


                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                md5.Clear();
            }
            
            return sb.ToString();
        }

        

        

        /// <summary>
        /// Fixes null strings and returns the string or ""
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static string FixNullstring(object objData)
        {
            string strValue = "";
            if (DBNull.Value == objData || objData == null)
            {
                return strValue;
            }
            else
            {
                return objData.ToString();
            }
        }

        public static bool DirectoryExists(string strPath)
        {
            bool blSuccess = false;
            try
            {
                strPath = WindowsPathClean(strPath);
                if (isNetworkDrive(strPath))
                {
                    strPath = WindowsPathClean(strPath);
                }
                blSuccess = Directory.Exists(strPath);
            }
            catch (Exception)
            {
                
                
            }
            return blSuccess;
        }

        /// <summary>
        /// Combines two Windows or Windows Share Paths
        /// </summary>
        /// <param name="strBasePath"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string WindowsPathCombine(string strBasePath, string strPath)
        {
            strBasePath = FixNullstring(strBasePath);
            strPath = FixNullstring(strPath);
            strBasePath = strBasePath.Replace("\\\\", "\\");
            if (strBasePath.Trim() != "\\" && strBasePath !="")
            {
                strBasePath += "\\" + strPath;
            }
            else
            {
                strBasePath = strPath;
            }
            strBasePath = WindowsPathClean(strBasePath);
                        
            return strBasePath;
        }


        /// <summary>
        /// Combines two Windows or Windows Share Paths and Removes a path
        /// </summary>
        /// <param name="strBasePath"></param>
        /// <param name="strPath"></param>
        /// <param name="strFolderToRemove"></param>
        /// <returns></returns>
        public static string WindowsPathCombine(string strBasePath, string strPath, string strFolderToRemove)
        {
            strBasePath = FixNullstring(strBasePath);
            strBasePath = strBasePath.Replace("\\\\", "\\");
            strPath = FixNullstring(strPath);
            strFolderToRemove = FixNullstring(strFolderToRemove);
            strFolderToRemove = strFolderToRemove.Replace("\\\\", "\\");
            //strPath = strPath.Trim();
            strPath = strPath.Replace("\\\\", "\\");

            strPath = strPath.Replace(strFolderToRemove, "").Trim();
            strPath = strPath.Replace("\\\\", "\\");
            if (strBasePath.Trim() != "" && strBasePath.Trim() != "\\")
            {
                strPath = strBasePath + "\\" + strPath;
            }

            strPath = WindowsPathClean(strPath);

            return strPath;
        }


        

        /// <summary>
        /// Cleans Windows or Windows Share Paths
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string WindowsPathClean(string strPath)
        {
            strPath = FixNullstring(strPath);
            strPath = strPath.Replace("/", "\\");
            strPath = strPath.Replace("\\\\", "\\");
            strPath = strPath.Replace("\\\\", "\\");
            
            //Fix Empty Paths
            if (strPath == "\\")
            {
                strPath = "";
            }

            if (strPath.Length > 2)
            {
                //Remove Ending BackSlashes
                if (strPath.Substring(strPath.Length - 2, 2) == "\\")
                {
                    strPath = strPath.Remove(strPath.Length - 2);
                }
                //Fix Share Paths to have 2 beginning backslashes
                if (strPath.Substring(0, 1) == "\\" && strPath.Substring(0, 2) != "\\\\")
                {
                    strPath = "\\" + strPath;
                }
                
            }

            return strPath;
        }

        /// <summary>
        /// Combines two Remote (FTP,FTPS,SFTP) Paths
        /// </summary>
        /// <param name="strBasePath"></param>
        /// <param name="strPath1"></param>
        /// <returns></returns>
        public static string RemotePathCombine(string strBasePath, string strPath1)
        {
            string strPath = "";
            strPath = FixNullstring(strBasePath);
            strPath = strPath.Replace("\\\\", "\\");
            strPath1 = FixNullstring(strPath1);
            strPath1 = strPath1.Trim();
            strPath = strPath.Replace("//", "/");
            if (strPath.Trim() != "" && strPath.Trim() != "/")
            {
                strPath += "/" + strPath1;
            }
            else
            {
                strPath = strPath1;
            }
            strPath = RemotePathClean(strPath);
            return strPath;
        }

        /// <summary>
        /// Combines two Remote (FTP,FTPS,SFTP) Paths and removes the third path passed
        /// </summary>
        /// <param name="strBaseDirectory"></param>
        /// <param name="strPath"></param>
        /// <param name="strFolderToRemove"></param>
        /// <returns></returns>
        public static string RemotePathCombine(string strBaseDirectory,string strPath, string strFolderToRemove)
        {
            
            strBaseDirectory = FixNullstring(strBaseDirectory);
            strBaseDirectory = strBaseDirectory.Replace("\\\\", "\\");
            strPath = FixNullstring(strPath);
            strFolderToRemove = FixNullstring(strFolderToRemove);
            strFolderToRemove = strFolderToRemove.Replace("\\\\", "\\");
            //strPath = strPath.Trim();
            strPath = strPath.Replace("\\\\", "\\");
            
            strPath = strPath.Replace(strFolderToRemove, "").Trim();
            strPath = strPath.Replace("\\", "/");
            strPath = strPath.Replace("//", "/");
            if (strBaseDirectory.Trim() !="" && strBaseDirectory.Trim() !="/")
            {
                strPath = strBaseDirectory + "/" + strPath;
            }

            strPath = RemotePathClean(strPath);
            
            return strPath;
        }

        /// <summary>
        /// Cleans the Remote Path (FTP,SFTP,FTPS)
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string RemotePathClean(string strPath)
        {
            strPath = FixNullstring(strPath);
            strPath = strPath.Replace("\\", "/");
            strPath = strPath.Replace("//", "/");
            strPath = strPath.Replace("//", "/");
            if (strPath.Trim() == "/")
            {
                strPath = "";
            }
            if (strPath.Length > 1 )
            {
                if (strPath.Substring(strPath.Length - 1, 1) == "/")
                {
                    strPath = strPath.Remove(strPath.Length - 1);
                }
                
            }
            return strPath;
        }

        /// <summary>
        /// Fixes null int32 and returns 0 if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static Int32 FixNullInt32(object objData)
        {
            Int32 intValue = 0;
            if (DBNull.Value == objData || objData == null)
            {
                return intValue;
            }
            else
            {
                Int32.TryParse(objData.ToString(), out intValue);
                return intValue;
            }
        }

        /// <summary>
        /// Fixes null int32 and returns 0 if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static long FixNulllong(object objData)
        {
            long lValue = 0;
            if (DBNull.Value == objData || objData == null)
            {
                return lValue;
            }
            else
            {
                long.TryParse(objData.ToString(), out lValue);
                return lValue;
            }
        }

        /// <summary>
        /// Fixes null bool and returns false if null
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static bool FixNullbool(object objData)
        {
            bool blValue = false;
            if (DBNull.Value == objData || objData == null)
            {
                return blValue;
            }
            else
            {
                bool.TryParse(objData.ToString(), out blValue);
                return blValue;
            }
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Whether to download the file or not
        /// </summary>
        /// <param name="strLocalFile"></param>
        /// <param name="strRemoteFile"></param>
        /// <param name="FileD"></param>
        /// <param name="Overwrite"></param>
        /// <returns></returns>
        public static bool DownloadFile(string strLocalFile, string strRemoteFile,RemoteFile FileD, OverwriteOptions Overwrite)
        {
            bool blOverwriteFile = false;
            bool blFileFound = false;

            
            if (File.Exists(strLocalFile) || File.Exists(strLocalFile + ".7z"))
            {
                blFileFound = true;
                FileInfo fileL;
                if (File.Exists(strLocalFile))
                {
                    fileL = new FileInfo(strLocalFile);
                }
                else
                {
                    fileL = new FileInfo(strLocalFile + ".7z");
                }

                if (!((fileL.LastAccessTimeUtc == FileD.LastWriteTimeUtc) && (fileL.Length == FileD.Length)))
                {
                    blOverwriteFile = true;
                }
            }

            return ((blFileFound == false || blOverwriteFile || (Overwrite == OverwriteOptions.ForceOverwrite && blFileFound)) && !(Overwrite == OverwriteOptions.NoOverwrite && blFileFound == true));
                                   
        }

       
        /// <summary>
        /// Walks Entire Path and returns Generic List of the files in the entire directory and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="blShuttingDown"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<System.IO.FileInfo> WalkDirectory(string strPath, ref bool blShuttingDown)
        {
            if (DirectoryExists(strPath))
            {
                System.Collections.Generic.List<System.IO.FileInfo> Files;
                Files = new System.Collections.Generic.List<System.IO.FileInfo>();
                System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(strPath);
                WalkDirectoryTree(rootDir, ref Files, ref blShuttingDown);
                return Files;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Walks Path and returns all Directories and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>System.Collections.Generic.List of DirectoryInfo</returns>
        public static System.Collections.Generic.List<DirectoryInfo> GetAllDirectories(string strPath)
        {
            System.Collections.Generic.List<DirectoryInfo> Directories = new List<DirectoryInfo>();
            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(strPath);
            DirectoryList(rootDir, ref Directories);
            return Directories;
        }

        /// <summary>
        /// Walks the Path and returns all Directories and sub directories
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns>System.Collections.Generic.List of RemoteFile</returns>
        public static System.Collections.Generic.List<RemoteFile> GetAllDirectoriesR(string strPath)
        {
            System.Collections.Generic.List<RemoteFile> Directories = new List<RemoteFile>();
            System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(strPath);
            DirectoryList(rootDir, ref Directories,strPath);
            return Directories;
        }

        /// <summary>
        /// Walks the path and add each directory and sub directory to the referenced list
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="folders"></param>
        private static void DirectoryList(DirectoryInfo dir, ref List<DirectoryInfo> folders)
        {

            try
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                        folders.Add(d);
                        DirectoryList(d, ref folders);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Walks the path and add each directory and sub directory to the referenced list
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="folders"></param>
        /// <param name="strParentPathRemove"></param>
        private static void DirectoryList(DirectoryInfo dir, ref List<RemoteFile> folders, string strParentPathRemove)
        {

            try
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    RemoteFile RD1 = new RemoteFile(d);
                    RD1.ParentDirectory = WindowsPathClean(d.Parent.FullName).Replace(WindowsPathClean(strParentPathRemove), "");
                    folders.Add(RD1);
                    DirectoryList(d, ref folders,strParentPathRemove);
                }
            }
            catch (Exception)
            {
            }
        }

        
        /// <summary>
        /// Walks the path and returns the files only in the first directory
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<FileInfo> GetFilesInDirectory(DirectoryInfo dir)
        {
            System.Collections.Generic.List<FileInfo> files = new System.Collections.Generic.List<FileInfo>();
            try
            {
                foreach (FileInfo f in dir.GetFiles())
                {
                    files.Add(f);
                }
            }
            catch (Exception)
            {
            }
            return files;
        }

        /// <summary>
        /// Walks the sftp site and returns all of the directories and sub directories
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="sftp"></param>
        /// <param name="ParentDirectory"></param>
        /// <returns>List of RemoteFile</returns>
        public static List<RemoteFile> GetRemoteDirectories(string Path, SftpClient sftp, string ParentDirectory = "")
        {
            List<RemoteFile> Files = new List<RemoteFile>();
            RemoteDirectories(ref Files, Path, sftp, ParentDirectory);
            return Files;
        }
        
        /// <summary>
        /// Walks the sftp site and passes by reference all of the directories and sub directories
        /// </summary>
        /// <param name="Files"></param>
        /// <param name="Path"></param>
        /// <param name="sftp"></param>
        /// <param name="ParentDirectory"></param>
        private static void RemoteDirectories(ref List<RemoteFile> Files, string Path, SftpClient sftp, string ParentDirectory = "")
        {
            foreach (SftpFile File in sftp.ListDirectory(Path))
            {
                if (File.Name != "." && File.Name != "..")
                {
                    RemoteFile Rfile = new RemoteFile(File.Name, File.FullName, File.Length, ParentDirectory,File.IsDirectory,File.LastWriteTime,File.LastWriteTimeUtc);

                    Files.Add(Rfile);
                    if (File.IsDirectory)
                    {
                        string Path2 = Path + "/" + File.Name;
                        RemoteDirectories(ref Files, Path2, sftp, ParentDirectory + "/" + File.Name);
                    }
                }
            }
           
        }


        public static List<RemoteFile> GetRemoteDirectories(string Path, AlexPilotti.FTPS.Client.FTPSClient ftps, string ParentDirectory = "")
        {
            List<RemoteFile> Files = new List<RemoteFile>();
            RemoteDirectories(ref Files, Path, ftps, ParentDirectory);
            return Files;
        }


        
        private static void RemoteDirectories(ref List<RemoteFile> Files, string Path, AlexPilotti.FTPS.Client.FTPSClient ftps, string ParentDirectory = "")
        {
            foreach (DirectoryListItem dli in ftps.GetDirectoryList(Path))
            {
                if (dli.Name != "." && dli.Name != "..")
                {
                    string Path2 = (Path + "/" + dli.Name).Replace("//","/");
                    RemoteFile Rfile;
                    try
                    {
                        if (!dli.IsDirectory)
                        {
                            Rfile = new RemoteFile(dli.Name, Path2, Common.FixNulllong(ftps.GetFileTransferSize(Path2)), ParentDirectory, dli.IsDirectory, (DateTime)ftps.GetFileModificationTime(Path2), (DateTime)ftps.GetFileModificationTime(Path2));
                        }
                        else
                        {
                            Rfile = new RemoteFile(dli.Name, Path2, 0, ParentDirectory, dli.IsDirectory, dli.CreationTime, dli.CreationTime);

                        }
                    }
                    catch (Exception)
                    {
                        Rfile = new RemoteFile(dli.Name, Path2, 0, ParentDirectory, dli.IsDirectory, dli.CreationTime, dli.CreationTime);

                    }
                    Files.Add(Rfile);
                    if (dli.IsDirectory)
                    {
                        
                        RemoteDirectories(ref Files, Path2, ftps, ParentDirectory + "/" + dli.Name);
                    }
                }
            }

        }


        public static void CreateLocalFolderStructure(List<RemoteFile> Files, string dlpath)
        {
            _evt = GetEventLog;
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].IsDirectory)
                {
                    //Create a new folder under the current active one
                    string PathToNewFolder = "";
                    PathToNewFolder = WindowsPathCombine(dlpath, Files[i].ParentDirectory);
                    PathToNewFolder = WindowsPathCombine(PathToNewFolder, Files[i].Name);
                    ////PathToNewFolder = Path.Combine(dlpath + Files[i].ParentDirectory, Files[i].Name).Replace("/", "\\").Replace("\\\\", "\\");
                    if (!DirectoryExists(PathToNewFolder))
                    {
                        Directory.CreateDirectory(PathToNewFolder);
                        _evt.WriteEntry("Local Directory Created: " + PathToNewFolder,EventLogEntryType.Information, 1, 1);
                    }
                }
            }
        }


        /// <summary>
        /// Recursive algorithm to get all files in a directory and sub directories
        /// </summary>
        /// <param name="root"></param>
        /// <param name="AllFiles"></param>
        /// <param name="blShuttingDown"></param>
        private static void WalkDirectoryTree(System.IO.DirectoryInfo root, ref System.Collections.Generic.List<System.IO.FileInfo> AllFiles, ref bool blShuttingDown)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            EventLog _evt = GetEventLog;

            // First, process all the files directly under this folder 
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater 
            // than the application provides. 
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse. 
                // You may decide to do something different here. For example, you 
                // can try to elevate your privileges and access the file again.
                _evt.WriteEntry(e.Message);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                _evt.WriteEntry(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    if (blShuttingDown)
                    {
                        _evt.WriteEntry("Shutting Down, about to walk: " + fi.FullName);
                        return;
                    }
                    // In this example, we only access the existing FileInfo object. If we 
                    // want to open, delete or modify the file, then 
                    // a try-catch block is required here to handle the case 
                    // where the file has been deleted since the call to TraverseTree().

                    //Console.WriteLine(fi.FullName);
                    AllFiles.Add(fi);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    if (blShuttingDown)
                    {
                        _evt.WriteEntry("Shutting Down, about to walk directory: " + dirInfo.FullName);
                        return;
                    }
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo, ref AllFiles, ref blShuttingDown);
                }
            }
        }
    }



    /// <summary>
    /// Remote File Class to help reuse code for ftp or sftp files
    /// </summary>
    public class RemoteFile
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long Length { get; set; }
        public string ParentDirectory { get; set; }
        public bool IsDirectory { get; set; }

        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }


        public FileOperations FileOperation { get; set; }

        public string NewFullName { get; set; }
       

        /// <summary>
        ///  A list of all the files/folder in a specific location.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fullName"></param>
        /// <param name="length"></param>
        /// <param name="parentDirectory"></param>
        /// <param name="isDirectory"></param>
        /// <param name="lastWriteTime"></param>
        /// <param name="lastWriteTimeUtc"></param>
        public RemoteFile(string name, string fullName, long length, string parentDirectory, bool isDirectory, DateTime lastWriteTime, DateTime lastWriteTimeUtc)
        {
            this.Name = name;
            this.FullName = fullName;
            this.Length = length;
            this.ParentDirectory = parentDirectory;
            this.IsDirectory = isDirectory;
            this.LastWriteTime = lastWriteTime;
            this.LastWriteTimeUtc = lastWriteTimeUtc;
        }

        public RemoteFile(FileInfo file1)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
        }

        public RemoteFile(FileInfo file1, FileOperations fileOp)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
        }

        public RemoteFile(FileInfo file1, FileOperations fileOp,string newFullName)
        {
            this.Name = file1.Name;
            this.FullName = file1.FullName;
            this.Length = file1.Length;
            this.ParentDirectory = file1.Directory.FullName;
            this.IsDirectory = false;
            this.LastWriteTime = file1.LastWriteTime;
            this.LastWriteTimeUtc = file1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
            this.NewFullName = newFullName;
        }

        public RemoteFile(DirectoryInfo dir1)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            //this.FileOperation = FileOperations.Modified;
        }

        public RemoteFile(DirectoryInfo dir1, FileOperations fileOp)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
        }

        public RemoteFile(DirectoryInfo dir1, FileOperations fileOp, string newFullName)
        {
            this.Name = dir1.Name;
            this.FullName = dir1.FullName;
            this.Length = 0;
            this.ParentDirectory = "";//dir1.Parent.FullName;
            this.IsDirectory = true;
            this.LastWriteTime = dir1.LastWriteTime;
            this.LastWriteTimeUtc = dir1.LastWriteTimeUtc;
            this.FileOperation = fileOp;
            this.NewFullName = newFullName;
        }
    }
    
}
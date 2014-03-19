using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SevenZip;
using System.IO;
using System.Data;

namespace BackupRetention
{
    /// <summary>
    /// Compress Folder Class
    /// </summary>
    public class CompressFolder: IFolderConfig
    {
        #region "Variables"
        /// <summary>
        /// List of all Files to Compress
        /// </summary>
        public System.Collections.Generic.List<System.IO.FileInfo> AllFiles;
        /// <summary>
        /// List of all Files that were compressed
        /// </summary>
        public System.Collections.Generic.List<System.IO.FileInfo> FilesCompressed;
        public System.Diagnostics.EventLog _evt;

        //ISincerely!HopeThisIsNotUsed@
        private string ep = "9BFD444C-8F19-4D3C-947C-03A8884502AE";

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

        private CompressOption _compress = CompressOption.Compress;
        public CompressOption Compress
        {
            get
            {
                return _compress;
            }
            set
            {
                _compress = value;
            }
        }

        private static CompressSourceOptions _sourceOption = CompressSourceOptions.File;
        public CompressSourceOptions SourceOption
        {
            get
            {
                return _sourceOption;
            }
            set
            {
                _sourceOption = value;
            }
        }

        private static string _sourceFolder = "";
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

        private static string _destinationFolder = "";
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


        private string _encryptionPassword = "";
        public string EncryptionPassword
        {
            set
            {
                _encryptionPassword = value;
            }
        }

        private bool _keepOriginalFile = false;
        public bool KeepOriginalFile
        {
            get
            {
                return _keepOriginalFile;
            }

            set
            {
                _keepOriginalFile = value;
            }
        }

        private SevenZip.CompressionLevel _compressionLvl = CompressionLevel.Normal;
        /// <summary>
        /// 7Zip Compression Level to use with the compression
        /// </summary>
        public SevenZip.CompressionLevel CompressionLvl
        {
            get
            {
                return _compressionLvl;
            }
            set
            {
                _compressionLvl = value;
            }

        }

        private int _startCompressingAfterDays = 0;
        public int StartCompressingAfterDays
        {
            get
            {
                return _startCompressingAfterDays;
            }
            set
            {
                _startCompressingAfterDays = value;
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

        #endregion



        #region "Methods"
        /// <summary>
        /// CompressFolder Class initialization method for contructors
        /// </summary>
        private void init_CompressFolder()
        {
            AllFiles = new System.Collections.Generic.List<System.IO.FileInfo>();
            FilesCompressed = new System.Collections.Generic.List<System.IO.FileInfo>();
            _evt = Common.GetEventLog;
        }

        /// <summary>
        /// CompressFolder Class Contructor
        /// </summary>
        public CompressFolder()
        {
            init_CompressFolder();
        }

        /// <summary>
        /// CompressFolder Class Contructor
        /// </summary>
        /// <param name="row"></param>
        public CompressFolder(DataRow row)
        {
            init_CompressFolder();

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
            
            str = Common.FixNullstring(row["Compress"]);
            try
            {
                Compress = (CompressOption)System.Enum.Parse(typeof(CompressOption), str);
            }
            catch (Exception)
            {
                Compress = CompressOption.Compress;
            }
            str = Common.FixNullstring(row["SourceOption"]);
            try
            {
                SourceOption = (CompressSourceOptions)System.Enum.Parse(typeof(CompressSourceOptions), str);
            }
            catch (Exception)
            {
                SourceOption = CompressSourceOptions.File;
            }
            SourceFolder = Common.FixNullstring(row["SourceFolder"]);
            DestinationFolder = Common.FixNullstring(row["DestinationFolder"]);
            EncryptionPassword = Common.FixNullstring(row["EncryptionPassword"]);
            KeepOriginalFile = Common.FixNullbool(row["KeepOriginalFile"]);

            str = Common.FixNullstring(row["CompressionLvl"]);
            try
            {
                CompressionLvl = (SevenZip.CompressionLevel)System.Enum.Parse(typeof(SevenZip.CompressionLevel), str);
            }
            catch (Exception)
            {
                CompressionLvl = SevenZip.CompressionLevel.Normal;
            }

            
            StartCompressingAfterDays = Common.FixNullInt32(row["StartCompressingAfterDays"]);
            FileNameFilter = Common.FixNullstring(row["FileNameFilter"]);
        }

        /// <summary>
        /// CompressFolder Class Destructor
        /// </summary>
        ~CompressFolder()
        {
            AllFiles.Clear();
            FilesCompressed.Clear();
            AllFiles = null;
            FilesCompressed = null;
        }

        /// <summary>
        /// Initialize CompressFolder Configuration Table
        /// </summary>
        /// <returns></returns>
        public static DataTable init_dtConfig()
        {
            DataTable dtCompressConfig;
            dtCompressConfig = new DataTable("CompressConfig");

            //Create Primary Key Column
            DataColumn dcID = new DataColumn("ID", typeof(Int32));
            dcID.AllowDBNull = false;
            dcID.Unique = true;
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;

            //Assign Primary Key
            DataColumn[] columns = new DataColumn[1];
            dtCompressConfig.Columns.Add(dcID);
            columns[0] = dtCompressConfig.Columns["ID"];
            dtCompressConfig.PrimaryKey = columns;


            //Create Columns
            dtCompressConfig.Columns.Add(new DataColumn("Enabled", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Time", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("EndTime", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("IntervalType", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Interval", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Compress", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("SourceOption", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("SourceFolder", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("DestinationFolder", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("EncryptionPassword", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("KeepOriginalFile", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("CompressionLvl", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("StartCompressingAfterDays", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("FileNameFilter", typeof(String)));

            dtCompressConfig.Columns["Enabled"].DefaultValue = "true";
            dtCompressConfig.Columns["IntervalType"].DefaultValue = "Daily";
            dtCompressConfig.Columns["Interval"].DefaultValue = "0";
            dtCompressConfig.Columns["Monday"].DefaultValue = "true";
            dtCompressConfig.Columns["Tuesday"].DefaultValue = "true";
            dtCompressConfig.Columns["Wednesday"].DefaultValue = "true";
            dtCompressConfig.Columns["Thursday"].DefaultValue = "true";
            dtCompressConfig.Columns["Friday"].DefaultValue = "true";
            dtCompressConfig.Columns["Saturday"].DefaultValue = "true";
            dtCompressConfig.Columns["Sunday"].DefaultValue = "true";
            
            dtCompressConfig.Columns["CompressionLvl"].DefaultValue = "Normal";
            dtCompressConfig.Columns["KeepOriginalFile"].DefaultValue = "true";
            dtCompressConfig.Columns["StartCompressingAfterDays"].DefaultValue = "2";
            dtCompressConfig.Columns["SourceOption"].DefaultValue = "File";
            return dtCompressConfig;

        }


        protected string Get7ZipFolder()
        {

            string str7zPath = "";
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\7-Zip"))
            {
                str7zPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\7-Zip\\7z.dll";
            }
            else if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\Program Files\\7-Zip"))
            {
                str7zPath = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\Program Files\\7-Zip\\7z.dll";
            }
            else if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\Program Files (x86)\\7-Zip"))
            {
                str7zPath = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\Program Files (x86)\\7-Zip\\7z.dll";
            }
            else
            {
                //_evt.WriteEntry("Compress: 7Zip Installation Not Found", System.Diagnostics.EventLogEntryType.Error, 5130, 50);
                throw new Exception("7zip Installation Not Found.");
            }
            return Common.WindowsPathClean(str7zPath);

        }


        /// <summary>
        /// Implements Archive Verify Settings
        /// </summary>
        /// <param name="extractor"></param>
        /// <param name="str7File"></param>
        /// <param name="strSourcePath"></param>
        /// <returns></returns>
        private void verifyArchive(bool blArchiveOk, string str7File,string strSourcePath)
        {
            
            FileInfo srcFile=null;
            DirectoryInfo srcDir = null;
            
            //Test 7zip archive
            if (blArchiveOk)
            {
                if (SourceOption == CompressSourceOptions.File)
                {
                    srcFile = new FileInfo(strSourcePath);
                    System.IO.File.SetCreationTime(str7File, srcFile.CreationTime); //Created
                    System.IO.File.SetLastWriteTime(str7File, srcFile.LastWriteTime);//Modified
                    //Move the compressed file if destination is specified and different than Source Folder
                    if (!string.IsNullOrEmpty(DestinationFolder))
                    {
                        if (SourceFolder.ToLower() != DestinationFolder.ToLower())
                        {
                            string strDestination = Common.WindowsPathCombine(DestinationFolder, str7File, SourceFolder);
                            if (!File.Exists(strDestination))
                            {
                                File.Move(str7File, strDestination);
                            }
                            
                        }
                    }
                    FilesCompressed.Add(srcFile);
                    //Delete Original Uncompressed File if KeepUncompressedFile == false
                    if (!KeepOriginalFile)
                    {
                        //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                        srcFile.IsReadOnly = false;
                        File.Delete(srcFile.FullName);
                        _evt.WriteEntry("Compress: Original File Deleted Per KeepUncompressedFile Setting: " + srcFile.FullName, System.Diagnostics.EventLogEntryType.Information, 5070, 50);

                    }
                    _evt.WriteEntry("Compress: File Compressed successfully: " + srcFile.FullName + "  To: " + srcFile.FullName + ".7z", System.Diagnostics.EventLogEntryType.Information, 5050, 50);
                }
                else //CompressSourceOptions.Folder
                {
                    srcDir = new DirectoryInfo(strSourcePath);

                    //7zip archive was created successfully
                    
                    //Set file attributes to match original file for retention purposes
                    System.IO.File.SetCreationTime(str7File, srcDir.CreationTime); //Created
                    System.IO.File.SetLastWriteTime(str7File, srcDir.LastWriteTime);//Modified

                    //Move the compressed file if destination is specified and different than Source Folder
                    if (!string.IsNullOrEmpty(DestinationFolder))
                    {
                        if (SourceFolder.ToLower() != DestinationFolder.ToLower())
                        {
                            string strDestination = Common.WindowsPathCombine(DestinationFolder, str7File, SourceFolder);
                            if (!File.Exists(strDestination))
                            {
                                File.Move(str7File, strDestination);
                            }
                        }
                    }

                    //FilesCompressed.Add(DirInfo1.FullName);
                    //Delete Original Uncompressed File if KeepUncompressedFile == false
                    if (!KeepOriginalFile)
                    {
                        Directory.Delete(srcDir.FullName, true);
                        _evt.WriteEntry("Compress: Original File Deleted Per KeepUncompressedFile Setting: " + srcDir.FullName, System.Diagnostics.EventLogEntryType.Information, 5070, 50);

                    }
                    _evt.WriteEntry("Compress: Folder Compressed successfully: " + srcDir.FullName + "  To: " + Common.WindowsPathCombine(DestinationFolder, str7File, SourceFolder), System.Diagnostics.EventLogEntryType.Information, 5050, 50);

                    
                }
                
            }
            else
            {
                FileInfo zFile = new FileInfo(str7File);
                //Delete corrupted file
                zFile.IsReadOnly = false;
                File.Delete(zFile.FullName);
                _evt.WriteEntry("Compress: 7zip Archive Corrupted and deleted: " + zFile.FullName, System.Diagnostics.EventLogEntryType.Error, 5160, 50);
            }

        }


        /// <summary>
        /// Whether StartCompressingAfterDays has been met
        /// </summary>
        /// <param name="dtLastWriteTime"></param>
        /// <returns></returns>
        private bool startCompressing(DateTime dtLastWriteTime)
        {
            bool blStartCompressing = false;
            //Start Compressing After Days specified
            try
            {
                DateTime Today = DateTime.Now;
                DateTime FileDate;
                FileDate = dtLastWriteTime;
                TimeSpan timespan = Today.Subtract(FileDate);
                if (!((timespan.Hours + (timespan.Days * 24)) <= (StartCompressingAfterDays * 24) && (StartCompressingAfterDays != 0)))
                {
                    blStartCompressing = true;
                    //continue;
                }

            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Compress: TimeSpan Error:" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 5130, 50);
                blStartCompressing = false;
            }

            return blStartCompressing;
        }

        /// <summary>
        /// Compress files individually with .7z added on the end of the filename
        /// </summary>
        /// <param name="blShuttingDown"></param>
        private void compressFile(ref bool blShuttingDown)
        {
            
            SevenZip.SevenZipCompressor compressor = null;
            SevenZip.SevenZipExtractor extractor = null;
            Stream exreader = null;
            Stream creader = null;
            Stream extestreader = null;
            string[] strfilearr = new string[1];
            AllFiles = Common.WalkDirectory(SourceFolder, ref blShuttingDown, FileNameFilter);

            try
            {
                SevenZip.SevenZipBase.SetLibraryPath(Get7ZipFolder());
                if (SourceFolder != DestinationFolder)
                {
                    Common.CreateDestinationFolders(SourceFolder, DestinationFolder);
                }
                if (blShuttingDown)
                {
                    throw new Exception("Shutting Down");
                }
                //Loop through every file
                foreach (System.IO.FileInfo file1 in AllFiles)
                {
                    string str7File = Common.WindowsPathClean(file1.FullName + ".7z");
                    string strDestination = Common.WindowsPathCombine(DestinationFolder, str7File, SourceFolder);
                            
                    bool blArchiveOk = false;
                   

                    if (blShuttingDown)
                    {
                        _evt.WriteEntry("Compress: Shutting Down, about to Compress: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 5130, 50);
                        throw new Exception("Shutting Down");
                    }

                    //Skip over already compressed files
                    if (file1.Extension.ToLower() == ".7z" || file1.Extension.ToLower() == ".zip" || file1.Extension.ToLower() == ".rar")
                    {
                        continue;
                    }

                    if (Common.IsFileLocked(file1))
                    {
                        _evt.WriteEntry("Compress: File is locked: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5130, 50);
                        continue;
                    }

                    if (!startCompressing(file1.LastWriteTime))
                    {
                        continue;
                    }

                    try
                    {

                        if (File.Exists(strDestination))
                        {
                            extestreader = new FileStream(strDestination, FileMode.Open);
                            extractor = new SevenZipExtractor(extestreader);

                            //If archive is not corrupted and KeepUncompressed is false then it is ok to delete the original
                            if (extractor.Check() && KeepOriginalFile == false)
                            {

                                FileInfo file2 = new FileInfo(strDestination);
                                //Same File compressed then ok to delete
                                if (file1.LastWriteTime == file2.LastWriteTime && file1.Length == extractor.UnpackedSize && extractor.FilesCount == 1)
                                {
                                    //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                    file1.IsReadOnly = false;
                                    File.Delete(file1.FullName);
                                }
                                file2 = null;
                            }

                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        _evt.WriteEntry("Compress: Failed to Delete Original File: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5140, 50);
                        continue;

                    }
                    finally
                    {
                        if (extestreader != null)
                        {
                            extestreader.Close();
                            extestreader.Dispose();
                            extestreader = null;
                        }
                        if (extractor != null)
                        {
                            extractor.Dispose();
                            extractor = null;
                        }
                    }
                    //If file already zipped and the last modified time are the same then delete


                    //Compression of individual files
                    strfilearr[0] = file1.FullName;

                    try
                    {
                        compressor = new SevenZip.SevenZipCompressor();
                        compressor.CompressionMethod = SevenZip.CompressionMethod.Lzma2;
                        compressor.CompressionLevel = CompressionLvl;
                        compressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                        if (!string.IsNullOrEmpty(_encryptionPassword))
                        {
                            compressor.ZipEncryptionMethod = ZipEncryptionMethod.Aes256;

                        }

                        long lFreeSpace = 0;

                        lFreeSpace = Common.DriveFreeSpaceBytes(DestinationFolder);

                        //Check for Enough Free Space to compress the file
                        if (((file1.Length * 2) > lFreeSpace) && (lFreeSpace != -1))
                        {
                            _evt.WriteEntry("Compress: Not enough available free space to compress this file: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5140, 50);
                            compressor = null;
                            continue;

                        }

                        if (lFreeSpace == -1)
                        {
                            _evt.WriteEntry("Compress: Only files local to this machine should be compressed.  Performance problem can occur with large files over the network. " + file1.FullName, System.Diagnostics.EventLogEntryType.Warning, 5150, 50);
                        }

                        //Compress or Compress and Encrypt Files
                        if (!string.IsNullOrEmpty(_encryptionPassword))
                        {

                            creader = new FileStream(str7File, FileMode.OpenOrCreate);

                            //Encrypt the file if password is specified
                            AES256 aes = new AES256(ep);
                            string upassword = aes.Decrypt(_encryptionPassword);
                            compressor.CompressFilesEncrypted(creader, upassword, strfilearr);
                            creader.Close();
                            creader.Dispose();
                            creader = null;
                            exreader = new FileStream(str7File, FileMode.Open);
                            extractor = new SevenZipExtractor(exreader, upassword);
                            upassword = "";
                        }
                        else
                        {
                            if (Common.IsFileLocked(file1))
                            {
                                _evt.WriteEntry("Compress: File is locked: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5070, 50);
                                continue;
                            }
                            creader = new FileStream(str7File, FileMode.OpenOrCreate);

                            compressor.CompressFiles(creader, strfilearr);
                            creader.Close();
                            creader.Dispose();
                            creader = null;
                            exreader = new FileStream(str7File, FileMode.Open);
                            extractor = new SevenZipExtractor(exreader);

                        }
                        
                        //7Zip file ok?
                        blArchiveOk = extractor.Check();
                        exreader.Close();
                        exreader.Dispose();
                        exreader = null;
                        verifyArchive(blArchiveOk, str7File, file1.FullName);

                    }
                    catch (Exception ex)
                    {
                        _evt.WriteEntry("Compress: " + ex.Message.ToString(), System.Diagnostics.EventLogEntryType.Error, 5000, 50);
                    }
                    finally
                    {
                        if (creader != null)
                        {
                            creader.Close();
                            creader.Dispose();
                            creader = null;
                        }
                        if (exreader != null)
                        {
                            exreader.Close();
                            exreader.Dispose();
                            exreader = null;
                        }
                        if (extractor != null)
                        {
                            extractor.Dispose();
                            extractor = null;
                        }
                        compressor = null;
                    }

                }// end foreach
                _evt.WriteEntry("Compress: Complete Files Compressed: " + FilesCompressed.Count, System.Diagnostics.EventLogEntryType.Information, 5000, 50);
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Compress: Compress Files Attempt Failed" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 5170, 50);
            }
            finally
            {
                if (creader != null)
                {
                    creader.Close();
                    creader.Dispose();
                    creader = null;
                }
                if (exreader != null)
                {
                    exreader.Close();
                    exreader.Dispose();
                    exreader = null;
                }
                if (extractor != null)
                {
                    extractor.Dispose();
                    extractor = null;
                }
                if (extestreader != null)
                {
                    extestreader.Close();
                    extestreader.Dispose();
                    extestreader = null;
                }
                compressor = null;
            }
        }

        /// <summary>
        /// Compress All Folders in a path each folder to a individual 7zip file
        /// </summary>
        /// <param name="blShuttingDown"></param>
        private void compressFolder(ref bool blShuttingDown)
        {
            
            
            SevenZip.SevenZipCompressor compressor = null;
            SevenZip.SevenZipExtractor extractor = null;
            Stream exreader = null;
            Stream creader = null;
            Stream extestreader = null;

            string[] strfilearr = new string[1];
            
            try
            {
                SevenZip.SevenZipBase.SetLibraryPath(Get7ZipFolder());
                string[] Directories = Directory.GetDirectories(SourceFolder);

                //Loop through every local directory
                foreach (string strDir in Directories)
                {
                    bool blArchiveOk = false;
                    
                    DirectoryInfo DirInfo1 = new DirectoryInfo(strDir);
                    string str7Dir = Common.WindowsPathClean(DirInfo1.FullName + ".7z");
                    string strDestination = Common.WindowsPathCombine(DestinationFolder, str7Dir, SourceFolder);
                    
                    if (blShuttingDown)
                    {
                        _evt.WriteEntry("Compress: Shutting Down, about to Compress: " + strDir, System.Diagnostics.EventLogEntryType.Information, 5130, 50);
                        return;
                    }

                    if (!startCompressing(DirInfo1.LastWriteTime))
                    {
                        continue;
                    }

                    //Check for Original Folder
                    try
                    {
                        if (File.Exists(strDestination))
                        {
                            FileInfo file7 = new FileInfo(strDestination);
                            extestreader = new FileStream(strDestination, FileMode.Open);
                            extractor = new SevenZipExtractor(extestreader);

                            //If archive is not corrupted and KeepUncompressed is false then it is ok to delete the original
                            if (extractor.Check() && KeepOriginalFile == false)
                            {
                                //Same File compressed then ok to delete
                                if (DirInfo1.LastWriteTime == file7.LastWriteTime && Common.CalculateFolderSize(DirInfo1.FullName) == extractor.UnpackedSize && Common.GetFolderFileCount(DirInfo1.FullName) == extractor.FilesCount)
                                {
                                    Directory.Delete(DirInfo1.FullName, true);
                                }

                            }

                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        _evt.WriteEntry("Compress: Failed to Delete Original File: " + DirInfo1.FullName, System.Diagnostics.EventLogEntryType.Error, 5140, 50);
                        continue;

                    }
                    finally
                    {
                        if (extestreader != null)
                        {
                            extestreader.Close();
                            extestreader.Dispose();
                            extestreader = null;
                        }
                        if (extractor != null)
                        {
                            extractor.Dispose();
                            extractor = null;
                        }
                    }

                    //Compression of Entire Folder
                    strfilearr[0] = DirInfo1.FullName;

                    try
                    {
                        compressor = new SevenZip.SevenZipCompressor();
                        compressor.CompressionMethod = SevenZip.CompressionMethod.Lzma2;
                        compressor.CompressionLevel = CompressionLvl;
                        compressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                        if (!string.IsNullOrEmpty(_encryptionPassword))
                        {
                            compressor.ZipEncryptionMethod = ZipEncryptionMethod.Aes256;

                        }

                        long lFreeSpace = 0;

                        lFreeSpace = Common.DriveFreeSpaceBytes(DestinationFolder);

                        //Check for Enough Free Space to compress the file
                        if (((Common.CalculateFolderSize(SourceFolder) * 2.0f) > (float)lFreeSpace) && (lFreeSpace != -1))
                        {
                            _evt.WriteEntry("Compress: Not enough available free space to compress this file: " + DirInfo1.FullName, System.Diagnostics.EventLogEntryType.Error, 5140, 50);
                            compressor = null;
                            continue;

                        }

                        if (lFreeSpace == -1)
                        {
                            _evt.WriteEntry("Compress: Only files local to this machine should be compressed.  Performance problem can occur with large files over the network. " + DirInfo1.FullName, System.Diagnostics.EventLogEntryType.Warning, 5150, 50);
                        }



                        //Compress or Compress and Encrypt Files
                        if (!string.IsNullOrEmpty(_encryptionPassword))
                        {
                            //Compress and Encrypt the Folder if password is specified
                            creader = new FileStream(str7Dir, FileMode.OpenOrCreate);


                            AES256 aes = new AES256(ep);
                            string upassword = aes.Decrypt(_encryptionPassword);
                            compressor.CompressDirectory(DirInfo1.FullName, creader, upassword);
                            creader.Close();
                            creader.Dispose();
                            creader = null;
                            exreader = new FileStream(str7Dir, FileMode.Open);
                            extractor = new SevenZipExtractor(exreader, upassword);
                            upassword = "";
                        }
                        else
                        {
                            //Compress the Folder Normally
                            creader = new FileStream(str7Dir, FileMode.OpenOrCreate);

                            compressor.CompressDirectory(DirInfo1.FullName, creader);
                            creader.Close();
                            creader.Dispose();
                            creader = null;
                            exreader = new FileStream(str7Dir, FileMode.Open);
                            extractor = new SevenZipExtractor(exreader);

                        }

                        //7Zip file ok?
                        blArchiveOk = extractor.Check();
                        //close the archive so it can be deleted later
                        exreader.Close();
                        exreader.Dispose();
                        exreader = null;
                        verifyArchive(blArchiveOk, str7Dir, DirInfo1.FullName);


                    }
                    catch (Exception ex)
                    {
                        _evt.WriteEntry("Compress: " + ex.Message.ToString(), System.Diagnostics.EventLogEntryType.Error, 5000, 50);
                    }
                    finally
                    {
                        if (creader != null)
                        {
                            creader.Close();
                            creader.Dispose();
                            creader = null;
                        }
                        if (exreader != null)
                        {
                            exreader.Close();
                            exreader.Dispose();
                            exreader = null;
                        }
                        if (extractor != null)
                        {
                            extractor.Dispose();
                            extractor = null;
                        }
                        compressor = null;
                    }

                }// end foreach
                _evt.WriteEntry("Compress: Compress Folders Completed", System.Diagnostics.EventLogEntryType.Information, 5000, 50);
            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Compress: Compress Folders Attempt Failed" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 5170, 50);
            }
            finally
            {
                if (creader != null)
                {
                    creader.Close();
                    creader.Dispose();
                    creader = null;
                }
                if (exreader != null)
                {
                    exreader.Close();
                    exreader.Dispose();
                    exreader = null;
                }
                if (extractor != null)
                {
                    extractor.Dispose();
                    extractor = null;
                }
                if (extestreader != null)
                {
                    extestreader.Close();
                    extestreader.Dispose();
                    extestreader = null;
                }
                compressor = null;
            }
        }



        /// <summary>
        /// Extracts 7zip file contents
        /// </summary>
        /// <param name="blShuttingDown"></param>
        private void extract(ref bool blShuttingDown)
        {
           
            SevenZip.SevenZipExtractor extractor = null;
            Stream exreader = null;
            
            //7Zip Extraction of 7zip contents
            string strDestination = "";

            try
            {
                SevenZip.SevenZipBase.SetLibraryPath(Get7ZipFolder());
                AllFiles = Common.WalkDirectory(SourceFolder, ref blShuttingDown, FileNameFilter);
                foreach (System.IO.FileInfo file1 in AllFiles)
                {
                    bool blArchiveOk = false;
                    try
                    {
                        if (blShuttingDown)
                        {
                            if (extractor != null)
                            {
                                extractor.Dispose();
                            }
                            _evt.WriteEntry("Compress: Shutting Down, about to Extract: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 5130, 50);
                            return;
                        }

                        //Skip over already compressed files
                        if (file1.Extension.ToLower() == ".7z" || file1.Extension.ToLower() == ".zip")
                        {
                            if (!startCompressing(file1.LastWriteTime))
                            {
                                continue;
                            }


                            //If file not already extracted and currently exists
                            if (File.Exists(file1.FullName) && !File.Exists(file1.FullName.Replace(file1.Extension, "")))
                            {
                                if (Common.IsFileLocked(file1))
                                {
                                    _evt.WriteEntry("Compress: File is locked: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5170, 50);
                                    continue;
                                }
                                exreader = new FileStream(file1.FullName, FileMode.Open);

                                if (string.IsNullOrEmpty(_encryptionPassword))
                                {

                                    extractor = new SevenZipExtractor(exreader);
                                }
                                else
                                {
                                    AES256 aes = new AES256(ep);
                                    string upassword = aes.Decrypt(_encryptionPassword);
                                    extractor = new SevenZipExtractor(exreader, upassword);
                                    upassword = "";
                                }

                                blArchiveOk = extractor.Check();

                                //FileInfo file2 = new FileInfo(file1.FullName.Replace(".7z",""));
                                //If archive is not corrupted and KeepUncompressed is false then it is ok to delete the original
                                if (blArchiveOk)
                                {

                                    long lFreeSpace = 0;

                                    lFreeSpace = Common.DriveFreeSpaceBytes(DestinationFolder);
                                    if (((file1.Length * 4) > lFreeSpace) && (lFreeSpace != -1))
                                    {


                                        _evt.WriteEntry("Compress: Not enough available free space to compress this file: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5140, 50);
                                        continue;

                                    }

                                    if (lFreeSpace == -1)
                                    {
                                        _evt.WriteEntry("Compress: Only files local to this machine should be compressed.  Performance problem can occur with large files over the network. " + file1.FullName, System.Diagnostics.EventLogEntryType.Warning, 5150, 50);
                                    }
                                    if (string.IsNullOrEmpty(DestinationFolder))
                                    {
                                        DestinationFolder = SourceFolder;
                                    }
                                    strDestination = DestinationFolder;
                                    strDestination = Common.WindowsPathCombine(strDestination, file1.DirectoryName, SourceFolder);

                                    if (SourceOption == CompressSourceOptions.Folder)
                                    {
                                        strDestination = Common.WindowsPathCombine(strDestination, Common.WindowsPathClean(Common.Strip7zExtension(file1.FullName)), SourceFolder);
                                        if (!Directory.Exists(strDestination))
                                        {
                                            Directory.CreateDirectory(strDestination);
                                        }
                                    }

                                    extractor.ExtractArchive(strDestination);
                                    FilesCompressed.Add(file1);
                                    _evt.WriteEntry("Compress: Successfully Extracted file: " + file1.FullName + " To: " + strDestination, System.Diagnostics.EventLogEntryType.Information, 5170, 50);

                                    //Unlock file so that it can be deleted
                                    extractor.Dispose();
                                    extractor = null;
                                    exreader.Close();
                                    exreader.Dispose();
                                    exreader = null;
                                    if (!KeepOriginalFile)
                                    {
                                        //File.SetAttributes(file1.FullName, FileAttributes.Normal);
                                        file1.IsReadOnly = false;
                                        File.Delete(file1.FullName);
                                        _evt.WriteEntry("Compress: Deleted Original Compressed File: " + file1.FullName + " Per Setting KeepOriginalFile", System.Diagnostics.EventLogEntryType.Information, 5170, 50);
                                    }
                                }
                                else
                                {
                                    _evt.WriteEntry("Compress: Extract Attempt 7zip Archive Encrypted or Corrupted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5170, 50);
                                }
                            }
                        }
                    }
                    catch (Exception exe)
                    {
                        _evt.WriteEntry("Compress: Extract Attempt Failed: " + file1.FullName + "Error: " + exe.Message, System.Diagnostics.EventLogEntryType.Error, 5170, 50);
                    }
                    finally
                    {
                        if (extractor != null)
                        {
                            extractor.Dispose();
                            extractor = null;
                        }
                        if (exreader != null)
                        {
                            exreader.Close();
                            exreader.Dispose();
                            exreader = null;
                        }

                        //extractor = null;
                    }

                } //extract foreach

            }
            catch (Exception ex)
            {
                _evt.WriteEntry("Compress: Extract Attempt Failed" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 5170, 50);

            }
            finally
            {
                if (extractor != null)
                {
                    extractor.Dispose();
                    extractor = null;
                }
                if (exreader != null)
                {
                    exreader.Close();
                    exreader.Dispose();
                    exreader = null;
                }
            }
        }



        /// <summary>
        /// Compresses All files encrypts the files with AES 256 7zip archive if password is passed.
        /// </summary>
        /// <param name="strPassword"></param>
        public void Execute(ref bool blShuttingDown)
        {
            if (Enabled)
            {
                AllFiles.Clear();
                FilesCompressed.Clear();
                _evt.WriteEntry("Compress: Starting", System.Diagnostics.EventLogEntryType.Information,5000, 50);

                if (Compress == CompressOption.Compress)
                {
                    

                    if (SourceOption == CompressSourceOptions.File)
                    {
                        compressFile(ref blShuttingDown);
                    }
                    else  //Compress Entire Folder
                    {
                        compressFolder(ref blShuttingDown);
                    }
                }
                else if (Compress == CompressOption.Extract)
                {
                    extract(ref blShuttingDown);
                }
                
            }  // end if enabled

        }
        #endregion

    }
}

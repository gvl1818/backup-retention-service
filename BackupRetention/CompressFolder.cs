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
            Monday = Common.FixNullbool(row["Monday"]);
            Tuesday = Common.FixNullbool(row["Tuesday"]);
            Wednesday = Common.FixNullbool(row["Wednesday"]);
            Thursday = Common.FixNullbool(row["Thursday"]);
            Friday = Common.FixNullbool(row["Friday"]);
            Saturday = Common.FixNullbool(row["Saturday"]);
            Sunday = Common.FixNullbool(row["Sunday"]);
            DayOfMonth = Common.FixNullInt32(row["DayOfMonth"]);
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
            dtCompressConfig.Columns.Add(new DataColumn("Monday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Tuesday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Wednesday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Thursday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Friday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Saturday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Sunday", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("DayOfMonth", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("Compress", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("SourceOption", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("SourceFolder", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("DestinationFolder", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("EncryptionPassword", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("KeepOriginalFile", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("CompressionLvl", typeof(String)));
            dtCompressConfig.Columns.Add(new DataColumn("StartCompressingAfterDays", typeof(String)));
            
            dtCompressConfig.Columns["Enabled"].DefaultValue = "true";
            dtCompressConfig.Columns["Monday"].DefaultValue = "true";
            dtCompressConfig.Columns["Tuesday"].DefaultValue = "true";
            dtCompressConfig.Columns["Wednesday"].DefaultValue = "true";
            dtCompressConfig.Columns["Thursday"].DefaultValue = "true";
            dtCompressConfig.Columns["Friday"].DefaultValue = "true";
            dtCompressConfig.Columns["Saturday"].DefaultValue = "true";
            dtCompressConfig.Columns["Sunday"].DefaultValue = "true";
            dtCompressConfig.Columns["DayOfMonth"].DefaultValue = "0";
            dtCompressConfig.Columns["CompressionLvl"].DefaultValue = "Normal";
            dtCompressConfig.Columns["KeepOriginalFile"].DefaultValue = "true";
            dtCompressConfig.Columns["StartCompressingAfterDays"].DefaultValue = "2";
            dtCompressConfig.Columns["SourceOption"].DefaultValue = "File";
            return dtCompressConfig;

        }



        /// <summary>
        /// Compresses All files encrypts the files with AES 256 7zip archive if password is passed.
        /// </summary>
        /// <param name="strPassword"></param>
        public void ExecuteCompressAll(ref bool blShuttingDown)
        {
            if (Enabled)
            {
                _evt.WriteEntry("Compress: Starting", System.Diagnostics.EventLogEntryType.Information,5000, 50);
                            
                SevenZip.SevenZipBase.SetLibraryPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\7-zip\\7z.dll");

                if (Compress == CompressOption.Compress)
                {
                    SevenZip.SevenZipCompressor compressor = null;
                    SevenZip.SevenZipExtractor extractor = null;
                    string[] strfilearr = new string[1];

                    
                                    
                    
                    AllFiles.Clear();
                    FilesCompressed.Clear();

                    if (SourceOption == CompressSourceOptions.File)
                    {
                        AllFiles = Common.WalkDirectory(SourceFolder, ref blShuttingDown);


                        foreach (System.IO.FileInfo file1 in AllFiles)
                        {
                            bool blArchiveOk = false;
                            Stream extestreader = null;

                            if (blShuttingDown)
                            {
                                _evt.WriteEntry("Compress: Shutting Down, about to Compress: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 5130, 50);
                                return;
                            }

                            //Skip over already compressed files
                            if (file1.Extension.ToLower() == ".7z" || file1.Extension.ToLower() == ".zip" || file1.Extension.ToLower() == ".rar" || file1.Extension.ToLower() == ".id" || file1.Extension.ToLower() == ".metadata")
                            {
                                continue;
                            }

                            if (Common.IsFileLocked(file1))
                            {
                                _evt.WriteEntry("Compress: File is locked: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error, 5130, 50);
                                continue;
                            }

                            //Start Compressing After Days specified
                            try
                            {
                                DateTime Today = DateTime.Now;
                                DateTime FileDate;
                                FileDate = file1.LastWriteTime;
                                TimeSpan timespan = Today.Subtract(FileDate);
                                if ((timespan.Hours + (timespan.Days * 24)) <= (StartCompressingAfterDays * 24) && (StartCompressingAfterDays != 0))
                                {
                                    continue;
                                }

                            }
                            catch (Exception ex)
                            {
                                _evt.WriteEntry("Compress: TimeSpan Error:" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 5130, 50);
                            }

                            try
                            {
                                if (File.Exists(file1.FullName + ".7z"))
                                {
                                    extestreader = new FileStream(file1.FullName + ".7z", FileMode.Open);
                                    extractor = new SevenZipExtractor(extestreader);

                                    //If archive is not corrupted and KeepUncompressed is false then it is ok to delete the original
                                    if (extractor.Check() && KeepOriginalFile == false)
                                    {
                                        FileInfo file2 = new FileInfo(file1.FullName + ".7z");
                                        //Same File compressed then ok to delete
                                        if (file1.LastWriteTime == file2.LastWriteTime)
                                        {
                                            file1.Delete();
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



                            strfilearr[0] = file1.FullName;
                            Stream exreader = null;
                            Stream creader = null;
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


                                //string strDrive = "";
                                long lFreeSpace = 0;
                                //strDrive = DestinationFolder.Substring(0, 1);
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

                                    creader = new FileStream(file1.FullName + ".7z", FileMode.OpenOrCreate);

                                    //Encrypt the file if password is specified
                                    AES256 aes = new AES256(ep);
                                    string upassword = aes.Decrypt(_encryptionPassword);
                                    compressor.CompressFilesEncrypted(creader, upassword, strfilearr);
                                    creader.Close();
                                    creader.Dispose();
                                    creader = null;
                                    exreader = new FileStream(file1.FullName + ".7z", FileMode.Open);
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
                                    creader = new FileStream(file1.FullName + ".7z", FileMode.OpenOrCreate);

                                    compressor.CompressFiles(creader, strfilearr);
                                    creader.Close();
                                    creader.Dispose();
                                    creader = null;
                                    exreader = new FileStream(file1.FullName + ".7z", FileMode.Open);
                                    extractor = new SevenZipExtractor(exreader);

                                }

                                //7Zip file ok?
                                blArchiveOk = extractor.Check();
                                exreader.Close();
                                exreader.Dispose();
                                exreader = null;

                                //Test 7zip archive
                                if (blArchiveOk)
                                {

                                    //Set file attributes to match original file for retention purposes
                                    System.IO.File.SetCreationTime(file1.FullName + ".7z", file1.CreationTime); //Created
                                    System.IO.File.SetLastWriteTime(file1.FullName + ".7z", file1.LastWriteTime);//Modified

                                    //Move the compressed file if destination is specified and different than Source Folder
                                    if (!string.IsNullOrEmpty(DestinationFolder))
                                    {
                                        if (SourceFolder.ToLower() != DestinationFolder.ToLower())
                                        {
                                            string strDestination = Common.WindowsPathCombine(DestinationFolder, file1.FullName + ".7z", SourceFolder);
                                            File.Move(file1.FullName + ".7z", strDestination);
                                        }
                                    }

                                    FilesCompressed.Add(file1);
                                    //Delete Original Uncompressed File if KeepUncompressedFile == false
                                    if (!KeepOriginalFile)
                                    {
                                        file1.Delete();
                                        _evt.WriteEntry("Compress: Original File Deleted Per KeepUncompressedFile Setting: " + file1.FullName, System.Diagnostics.EventLogEntryType.Information, 5070, 50);

                                    }
                                    _evt.WriteEntry("Compress: File Compressed successfully: " + file1.FullName + "  To: " + file1.FullName + ".7z", System.Diagnostics.EventLogEntryType.Information, 5050, 50);

                                }
                                else
                                {
                                    FileInfo zFile = new FileInfo(file1.FullName + ".7z");

                                    //Delete corrupted file
                                    zFile.Delete();
                                    _evt.WriteEntry("Compress: 7zip Archive Corrupted and deleted: " + zFile.FullName, System.Diagnostics.EventLogEntryType.Error, 5160, 50);
                                    zFile = null;

                                }

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
                                }
                                if (exreader != null)
                                {
                                    exreader.Close();
                                    exreader.Dispose();
                                }
                                if (extractor != null)
                                {
                                    extractor.Dispose();
                                }
                                compressor = null;
                            }

                        }// end foreach
                    }
                    else
                    {

                        //AllFiles = Common.WalkDirectory(SourceFolder, ref blShuttingDown);
                        string[] Directories = Directory.GetDirectories(SourceFolder);
                        

                        foreach (string strDir in Directories)
                        {
                            bool blArchiveOk = false;
                            Stream extestreader = null;
                            DirectoryInfo DirInfo1 = new DirectoryInfo(strDir);

                            if (blShuttingDown)
                            {
                                _evt.WriteEntry("Compress: Shutting Down, about to Compress: " + strDir, System.Diagnostics.EventLogEntryType.Information, 5130, 50);
                                return;
                            }

                            

                            

                            //Start Compressing After Days specified
                            try
                            {
                                DateTime Today = DateTime.Now;
                                DateTime FileDate;
                                FileDate = DirInfo1.LastWriteTime;
                                TimeSpan timespan = Today.Subtract(FileDate);
                                if ((timespan.Hours + (timespan.Days * 24)) <= (StartCompressingAfterDays * 24) && (StartCompressingAfterDays != 0))
                                {
                                    continue;
                                }

                            }
                            catch (Exception ex)
                            {
                                _evt.WriteEntry("Compress: TimeSpan Error:" + ex.Message, System.Diagnostics.EventLogEntryType.Error, 5130, 50);
                            }

                            try
                            {
                                if (File.Exists(DirInfo1.FullName + ".7z"))
                                {
                                    extestreader = new FileStream(DirInfo1.FullName + ".7z", FileMode.Open);
                                    extractor = new SevenZipExtractor(extestreader);

                                    //If archive is not corrupted and KeepUncompressed is false then it is ok to delete the original
                                    if (extractor.Check() && KeepOriginalFile == false)
                                    {
                                        DirectoryInfo dir2 = new DirectoryInfo(DirInfo1.FullName + ".7z");
                                        //Same File compressed then ok to delete
                                        if (DirInfo1.LastWriteTime == dir2.LastWriteTime)
                                        {
                                            DirInfo1.Delete();
                                        }
                                        dir2 = null;
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
                            //If file already zipped and the last modified time are the same then delete



                            strfilearr[0] = DirInfo1.FullName;
                            Stream exreader = null;
                            Stream creader = null;
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
                                if (((Common.CalculateFolderSize(SourceFolder) * 2.0f) > (float) lFreeSpace) && (lFreeSpace != -1))
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

                                    creader = new FileStream(DirInfo1.FullName + ".7z", FileMode.OpenOrCreate);

                                    //Encrypt the file if password is specified
                                    AES256 aes = new AES256(ep);
                                    string upassword = aes.Decrypt(_encryptionPassword);
                                    compressor.CompressDirectory(DirInfo1.FullName,creader, upassword);
                                    creader.Close();
                                    creader.Dispose();
                                    creader = null;
                                    exreader = new FileStream(DirInfo1.FullName + ".7z", FileMode.Open);
                                    extractor = new SevenZipExtractor(exreader, upassword);
                                    upassword = "";
                                }
                                else
                                {
                                    creader = new FileStream(DirInfo1.FullName + ".7z", FileMode.OpenOrCreate);

                                    compressor.CompressDirectory(DirInfo1.FullName,creader);
                                    creader.Close();
                                    creader.Dispose();
                                    creader = null;
                                    exreader = new FileStream(DirInfo1.FullName + ".7z", FileMode.Open);
                                    extractor = new SevenZipExtractor(exreader);

                                }

                                //7Zip file ok?
                                blArchiveOk = extractor.Check();
                                exreader.Close();
                                exreader.Dispose();
                                exreader = null;

                                //Test 7zip archive
                                if (blArchiveOk)
                                {

                                    //Set file attributes to match original file for retention purposes
                                    System.IO.File.SetCreationTime(DirInfo1.FullName + ".7z", DirInfo1.CreationTime); //Created
                                    System.IO.File.SetLastWriteTime(DirInfo1.FullName + ".7z", DirInfo1.LastWriteTime);//Modified

                                    //Move the compressed file if destination is specified and different than Source Folder
                                    if (!string.IsNullOrEmpty(DestinationFolder))
                                    {
                                        if (SourceFolder.ToLower() != DestinationFolder.ToLower())
                                        {
                                            string strDestination = Common.WindowsPathCombine(DestinationFolder, DirInfo1.FullName + ".7z", SourceFolder);
                                            
                                            File.Move(DirInfo1.FullName + ".7z", strDestination);
                                        }
                                    }

                                    //FilesCompressed.Add(DirInfo1.FullName);
                                    //Delete Original Uncompressed File if KeepUncompressedFile == false
                                    if (!KeepOriginalFile)
                                    {
                                        DirInfo1.Delete(true);
                                        _evt.WriteEntry("Compress: Original File Deleted Per KeepUncompressedFile Setting: " + DirInfo1.FullName, System.Diagnostics.EventLogEntryType.Information, 5070, 50);

                                    }
                                    _evt.WriteEntry("Compress: File Compressed successfully: " + DirInfo1.FullName + "  To: " + Common.WindowsPathCombine(DestinationFolder, DirInfo1.FullName + ".7z", SourceFolder), System.Diagnostics.EventLogEntryType.Information, 5050, 50);

                                }
                                else
                                {
                                    FileInfo zFile = new FileInfo(DirInfo1.FullName + ".7z");

                                    //Delete corrupted file
                                    zFile.Delete();
                                    _evt.WriteEntry("Compress: 7zip Archive Corrupted and deleted: " + zFile.FullName, System.Diagnostics.EventLogEntryType.Error, 5160, 50);
                                    zFile = null;

                                }

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
                                }
                                if (exreader != null)
                                {
                                    exreader.Close();
                                    exreader.Dispose();
                                }
                                if (extractor != null)
                                {
                                    extractor.Dispose();
                                }
                                compressor = null;
                            }

                        }// end foreach

                    }
                }
                else if (Compress == CompressOption.Extract)
                {
                    string strDestination = "";
                    SevenZip.SevenZipExtractor extractor = null;
                    

                    
                    AllFiles.Clear();
                    FilesCompressed.Clear();
                    AllFiles = Common.WalkDirectory(SourceFolder, ref blShuttingDown);


                    foreach (System.IO.FileInfo file1 in AllFiles)
                    {
                        Stream exreader=null;
                        bool blArchiveOk = false;
                        try
                        {
                            if (blShuttingDown)
                            {
                                if (extractor != null)
                                {
                                    extractor.Dispose();
                                }
                                _evt.WriteEntry("Compress: Shutting Down, about to Extract: " + file1.FullName,System.Diagnostics.EventLogEntryType.Information, 5130, 50);
                                return;
                            }

                            //Skip over already compressed files
                            if (file1.Extension.ToLower() == ".7z" || file1.Extension.ToLower() == ".zip")
                            {
                                //Start Compressing After Days specified
                                try
                                {
                                    DateTime Today = DateTime.Now;
                                    DateTime FileDate;
                                    FileDate = file1.LastWriteTime;
                                    TimeSpan timespan = Today.Subtract(FileDate);
                                    if ((timespan.Days <= this.StartCompressingAfterDays) && (StartCompressingAfterDays!=0))
                                    {
                                        continue;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    _evt.WriteEntry("Compress: LastModifiedDate Check Error with File: " + file1.FullName + " Error:" + ex.Message,System.Diagnostics.EventLogEntryType.Error, 5170, 50);      
                                }


                                //If file not already extracted and currently exists
                                if (File.Exists(file1.FullName) && !File.Exists(file1.FullName.Replace(file1.Extension,"")))
                                {
                                    if (Common.IsFileLocked(file1))
                                    {
                                        _evt.WriteEntry("Compress: File is locked: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error,5170, 50);
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
                                        string strDrive = "";
                                        long lFreeSpace = 0;
                                        strDrive = DestinationFolder.Substring(0, 1);
                                        lFreeSpace = Common.DriveFreeSpaceBytes(strDrive);
                                        if (((file1.Length * 4) > lFreeSpace) && (lFreeSpace != -1))
                                        {

                                            
                                            _evt.WriteEntry("Compress: Not enough available free space to compress this file: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error,5140, 50);
                                            continue;

                                        }

                                        if (lFreeSpace == -1)
                                        {
                                            _evt.WriteEntry("Compress: Only files local to this machine should be compressed.  Performance problem can occur with large files over the network. " + file1.FullName,System.Diagnostics.EventLogEntryType.Warning, 5150, 50);
                                        }
                                        if (string.IsNullOrEmpty(DestinationFolder))
                                        {
                                            DestinationFolder = SourceFolder;
                                        }
                                        strDestination = DestinationFolder;
                                        strDestination = Common.WindowsPathCombine(strDestination, file1.DirectoryName, SourceFolder);

                                        if (SourceOption == CompressSourceOptions.File)
                                        {
                                            strDestination = Common.WindowsPathCombine(strDestination, file1.DirectoryName, SourceFolder);
                                        }
                                        else
                                        {
                                            strDestination = Common.WindowsPathCombine(strDestination, file1.FullName.Replace(".7z", ""), SourceFolder);
                                            if (!Directory.Exists(strDestination))
                                            {
                                                Directory.CreateDirectory(strDestination);
                                            }
                                        }

                                        extractor.ExtractArchive(strDestination);
                                        FilesCompressed.Add(file1);
                                        _evt.WriteEntry("Compress: Successfully Extracted file: " + file1.FullName + " To: " + strDestination, System.Diagnostics.EventLogEntryType.Information,5170, 50);
                                        extractor.Dispose();
                                        extractor = null;
                                        exreader.Close();
                                        exreader.Dispose();
                                        exreader = null;
                                        if (!KeepOriginalFile)
                                        {
                                            file1.Delete();
                                            _evt.WriteEntry("Compress: Deleted Original Compressed File: " + file1.FullName + " Per Setting KeepOriginalFile", System.Diagnostics.EventLogEntryType.Information,5170, 50);
                                        }
                                    }
                                    else
                                    {
                                        extractor.Dispose();
                                        extractor = null;
                                        exreader.Close();
                                        exreader.Dispose();
                                        exreader = null;
                                        _evt.WriteEntry("Compress: Extract Attempt 7zip Archive Encrypted or Corrupted: " + file1.FullName, System.Diagnostics.EventLogEntryType.Error,5170, 50);
                                    }

                                   
                                    
                                    
                                }
                            }
                        }
                        catch (Exception exe)
                        {
                            _evt.WriteEntry("Compress: Extract Attempt Failed: " + file1.FullName + "Error: " + exe.Message, System.Diagnostics.EventLogEntryType.Error,5170, 50);
                        }
                        finally
                        {
                            if (extractor != null)
                            {
                                extractor.Dispose();
                            }
                            if (exreader != null)
                            {
                                exreader.Close();
                                exreader.Dispose();
                            }
                            
                            //extractor = null;
                        }
                        
                    } //extract foreach

                }
                _evt.WriteEntry("Compress: Complete Files Compressed: " + FilesCompressed.Count,System.Diagnostics.EventLogEntryType.Information, 5000, 50);
                
            }  // end if enabled

        }
        #endregion

    }
}

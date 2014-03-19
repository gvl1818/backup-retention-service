namespace BackupRetention
{
    partial class BackupRetentionSystemTray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            
            if (disposing && (components != null))
            {
                components.Dispose();
                trayIcon.Dispose();
                trayIcon = null;
            }
            base.Dispose(disposing);
        }
        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupRetentionSystemTray));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabRetention = new System.Windows.Forms.TabPage();
            this.dgvRetention = new System.Windows.Forms.DataGridView();
            this.dgvColRetentionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionIntervalType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRetentionInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRetentionBackupFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionMinFileCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionDayOfWeekToKeep = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRetentionDailyMaxDaysOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionWeeklyMaxDaysOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionMonthlyMaxDaysOld = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRetentionRetentionAlgorithm = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRetentionFileNameFilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabCompress = new System.Windows.Forms.TabPage();
            this.dgvCompress = new System.Windows.Forms.DataGridView();
            this.dgvColCompressID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressIntervalType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColCompressInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColCompressCompress = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColCompressSourceOption = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColCompressSourceFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressDestinationFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressEncryptionPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressKeepOriginalFile = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColCompressCompressionLvl = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColCompressStartCompressingAfterDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCompressFileNameFilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabSync = new System.Windows.Forms.TabPage();
            this.dgvSync = new System.Windows.Forms.DataGridView();
            this.dgvColSyncID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColSyncEnabled1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColSyncEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColSyncIntervalType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColSyncInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColSyncMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColSyncSourceFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColSyncDestinationFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColSyncFileSyncReplicaOption = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColSyncFileSyncReset = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColSyncFolderSyncDirectionOrder = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColSyncDefaultConflictResolutionPolicy = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColSyncArchiveDeleted = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColSyncArchiveFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabRemote = new System.Windows.Forms.TabPage();
            this.dgvRemote = new System.Windows.Forms.DataGridView();
            this.dgvColRemoteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteIntervalType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRemoteInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColRemoteHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteProtocol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRemotePort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemotePassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteKeyFileDirectory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteKeyFileUsePassPhrase = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRemoteRemoteDirectory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteBackupFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteTransferDirection = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRemoteAllowAnyCertificate = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRemoteTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColRemoteOverwrite = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColRemoteFileNameFilter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.chkInformation = new System.Windows.Forms.CheckBox();
            this.chkWarning = new System.Windows.Forms.CheckBox();
            this.chkError = new System.Windows.Forms.CheckBox();
            this.btnRefreshEventLog = new System.Windows.Forms.Button();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.dgvEvents = new System.Windows.Forms.DataGridView();
            this.dgvEventImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvEventType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColEventEventID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabTasks = new System.Windows.Forms.TabPage();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtServiceStatus = new System.Windows.Forms.TextBox();
            this.lblServiceIntervalMinutes = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.lblTimeInfo = new System.Windows.Forms.Label();
            this.btnSaveApply = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.mtxtMaxDriveSpaceUsed = new System.Windows.Forms.MaskedTextBox();
            this.lblMaxDriveSpaceUsed = new System.Windows.Forms.Label();
            this.lblServiceInterval = new System.Windows.Forms.Label();
            this.txtServiceInterval = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbLeftIcon = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dgvColScriptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptIntervalType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvColScriptInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptMonday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptTuesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptWednesday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptThursday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptFriday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptSaturday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptSunday = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvColScriptWorkingDirector = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptArguments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptSourceFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptDestinationFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColScriptTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabRetention.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetention)).BeginInit();
            this.tabCompress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompress)).BeginInit();
            this.tabSync.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSync)).BeginInit();
            this.tabRemote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRemote)).BeginInit();
            this.tabEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).BeginInit();
            this.TabTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabRetention);
            this.tabControl.Controls.Add(this.tabCompress);
            this.tabControl.Controls.Add(this.tabSync);
            this.tabControl.Controls.Add(this.tabRemote);
            this.tabControl.Controls.Add(this.TabTasks);
            this.tabControl.Controls.Add(this.tabEvents);
            this.tabControl.Location = new System.Drawing.Point(1, 247);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(617, 429);
            this.tabControl.TabIndex = 16;
            // 
            // tabRetention
            // 
            this.tabRetention.Controls.Add(this.dgvRetention);
            this.tabRetention.Location = new System.Drawing.Point(4, 22);
            this.tabRetention.Name = "tabRetention";
            this.tabRetention.Padding = new System.Windows.Forms.Padding(3);
            this.tabRetention.Size = new System.Drawing.Size(609, 403);
            this.tabRetention.TabIndex = 0;
            this.tabRetention.Text = "Retention";
            this.tabRetention.UseVisualStyleBackColor = true;
            // 
            // dgvRetention
            // 
            this.dgvRetention.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRetention.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRetention.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColRetentionID,
            this.dgvColRetentionEnabled,
            this.dgvColRetentionTime,
            this.dgvColRetentionEndTime,
            this.dgvColRetentionIntervalType,
            this.dgvColRetentionInterval,
            this.dgvColRetentionMonday,
            this.dgvColRetentionTuesday,
            this.dgvColRetentionWednesday,
            this.dgvColRetentionThursday,
            this.dgvColRetentionFriday,
            this.dgvColRetentionSaturday,
            this.dgvColRetentionSunday,
            this.dgvColRetentionBackupFolder,
            this.dgvColRetentionMinFileCount,
            this.dgvColRetentionDayOfWeekToKeep,
            this.dgvColRetentionDailyMaxDaysOld,
            this.dgvColRetentionWeeklyMaxDaysOld,
            this.dgvColRetentionMonthlyMaxDaysOld,
            this.dgvColRetentionRetentionAlgorithm,
            this.dgvColRetentionFileNameFilter});
            this.dgvRetention.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRetention.Location = new System.Drawing.Point(3, 3);
            this.dgvRetention.Name = "dgvRetention";
            this.dgvRetention.Size = new System.Drawing.Size(603, 397);
            this.dgvRetention.TabIndex = 9;
            this.dgvRetention.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRetention_CellDoubleClick);
            this.dgvRetention.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRetention_CellValidating);
            // 
            // dgvColRetentionID
            // 
            this.dgvColRetentionID.DataPropertyName = "ID";
            this.dgvColRetentionID.HeaderText = "ID";
            this.dgvColRetentionID.MaxInputLength = 10;
            this.dgvColRetentionID.Name = "dgvColRetentionID";
            this.dgvColRetentionID.ReadOnly = true;
            this.dgvColRetentionID.Width = 43;
            // 
            // dgvColRetentionEnabled
            // 
            this.dgvColRetentionEnabled.DataPropertyName = "Enabled";
            this.dgvColRetentionEnabled.FalseValue = "false";
            this.dgvColRetentionEnabled.HeaderText = "Enabled";
            this.dgvColRetentionEnabled.IndeterminateValue = "";
            this.dgvColRetentionEnabled.Name = "dgvColRetentionEnabled";
            this.dgvColRetentionEnabled.ToolTipText = "This configuration row enabled?";
            this.dgvColRetentionEnabled.TrueValue = "true";
            this.dgvColRetentionEnabled.Width = 52;
            // 
            // dgvColRetentionTime
            // 
            this.dgvColRetentionTime.DataPropertyName = "Time";
            this.dgvColRetentionTime.HeaderText = "StartTime";
            this.dgvColRetentionTime.MaxInputLength = 5;
            this.dgvColRetentionTime.Name = "dgvColRetentionTime";
            this.dgvColRetentionTime.Width = 77;
            // 
            // dgvColRetentionEndTime
            // 
            this.dgvColRetentionEndTime.DataPropertyName = "EndTime";
            this.dgvColRetentionEndTime.HeaderText = "EndTime";
            this.dgvColRetentionEndTime.MaxInputLength = 5;
            this.dgvColRetentionEndTime.Name = "dgvColRetentionEndTime";
            this.dgvColRetentionEndTime.Width = 74;
            // 
            // dgvColRetentionIntervalType
            // 
            this.dgvColRetentionIntervalType.DataPropertyName = "IntervalType";
            this.dgvColRetentionIntervalType.HeaderText = "IntervalType";
            this.dgvColRetentionIntervalType.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dgvColRetentionIntervalType.Name = "dgvColRetentionIntervalType";
            this.dgvColRetentionIntervalType.Width = 72;
            // 
            // dgvColRetentionInterval
            // 
            this.dgvColRetentionInterval.DataPropertyName = "Interval";
            this.dgvColRetentionInterval.HeaderText = "Interval";
            this.dgvColRetentionInterval.Name = "dgvColRetentionInterval";
            this.dgvColRetentionInterval.Width = 67;
            // 
            // dgvColRetentionMonday
            // 
            this.dgvColRetentionMonday.DataPropertyName = "Monday";
            this.dgvColRetentionMonday.FalseValue = "false";
            this.dgvColRetentionMonday.HeaderText = "Mon";
            this.dgvColRetentionMonday.IndeterminateValue = "";
            this.dgvColRetentionMonday.Name = "dgvColRetentionMonday";
            this.dgvColRetentionMonday.ToolTipText = "Monday - day to execute";
            this.dgvColRetentionMonday.TrueValue = "true";
            this.dgvColRetentionMonday.Width = 34;
            // 
            // dgvColRetentionTuesday
            // 
            this.dgvColRetentionTuesday.DataPropertyName = "Tuesday";
            this.dgvColRetentionTuesday.FalseValue = "false";
            this.dgvColRetentionTuesday.HeaderText = "Tue";
            this.dgvColRetentionTuesday.IndeterminateValue = "";
            this.dgvColRetentionTuesday.Name = "dgvColRetentionTuesday";
            this.dgvColRetentionTuesday.ToolTipText = "Tuesday - day to execute";
            this.dgvColRetentionTuesday.TrueValue = "true";
            this.dgvColRetentionTuesday.Width = 32;
            // 
            // dgvColRetentionWednesday
            // 
            this.dgvColRetentionWednesday.DataPropertyName = "Wednesday";
            this.dgvColRetentionWednesday.FalseValue = "false";
            this.dgvColRetentionWednesday.HeaderText = "Wed";
            this.dgvColRetentionWednesday.IndeterminateValue = "";
            this.dgvColRetentionWednesday.Name = "dgvColRetentionWednesday";
            this.dgvColRetentionWednesday.ToolTipText = "Wednesday - day to execute";
            this.dgvColRetentionWednesday.TrueValue = "true";
            this.dgvColRetentionWednesday.Width = 36;
            // 
            // dgvColRetentionThursday
            // 
            this.dgvColRetentionThursday.DataPropertyName = "Thursday";
            this.dgvColRetentionThursday.FalseValue = "false";
            this.dgvColRetentionThursday.HeaderText = "Thur";
            this.dgvColRetentionThursday.IndeterminateValue = "";
            this.dgvColRetentionThursday.Name = "dgvColRetentionThursday";
            this.dgvColRetentionThursday.ToolTipText = "Thursday - day to execute";
            this.dgvColRetentionThursday.TrueValue = "true";
            this.dgvColRetentionThursday.Width = 35;
            // 
            // dgvColRetentionFriday
            // 
            this.dgvColRetentionFriday.DataPropertyName = "Friday";
            this.dgvColRetentionFriday.FalseValue = "false";
            this.dgvColRetentionFriday.HeaderText = "Fri";
            this.dgvColRetentionFriday.IndeterminateValue = "";
            this.dgvColRetentionFriday.Name = "dgvColRetentionFriday";
            this.dgvColRetentionFriday.ToolTipText = "Friday - day to execute";
            this.dgvColRetentionFriday.TrueValue = "true";
            this.dgvColRetentionFriday.Width = 24;
            // 
            // dgvColRetentionSaturday
            // 
            this.dgvColRetentionSaturday.DataPropertyName = "Saturday";
            this.dgvColRetentionSaturday.FalseValue = "false";
            this.dgvColRetentionSaturday.HeaderText = "Sat";
            this.dgvColRetentionSaturday.IndeterminateValue = "";
            this.dgvColRetentionSaturday.Name = "dgvColRetentionSaturday";
            this.dgvColRetentionSaturday.ToolTipText = "Saturday - day to execute";
            this.dgvColRetentionSaturday.TrueValue = "true";
            this.dgvColRetentionSaturday.Width = 29;
            // 
            // dgvColRetentionSunday
            // 
            this.dgvColRetentionSunday.DataPropertyName = "Sunday";
            this.dgvColRetentionSunday.FalseValue = "false";
            this.dgvColRetentionSunday.HeaderText = "Sun";
            this.dgvColRetentionSunday.IndeterminateValue = "";
            this.dgvColRetentionSunday.Name = "dgvColRetentionSunday";
            this.dgvColRetentionSunday.ToolTipText = "Sunday - day to execute";
            this.dgvColRetentionSunday.TrueValue = "true";
            this.dgvColRetentionSunday.Width = 32;
            // 
            // dgvColRetentionBackupFolder
            // 
            this.dgvColRetentionBackupFolder.DataPropertyName = "BackupFolder";
            this.dgvColRetentionBackupFolder.HeaderText = "BackupFolder";
            this.dgvColRetentionBackupFolder.Name = "dgvColRetentionBackupFolder";
            this.dgvColRetentionBackupFolder.ToolTipText = "Path Must have double backslashes";
            this.dgvColRetentionBackupFolder.Width = 98;
            // 
            // dgvColRetentionMinFileCount
            // 
            this.dgvColRetentionMinFileCount.DataPropertyName = "MinFileCount";
            this.dgvColRetentionMinFileCount.HeaderText = "MinFileCount";
            this.dgvColRetentionMinFileCount.MaxInputLength = 10;
            this.dgvColRetentionMinFileCount.Name = "dgvColRetentionMinFileCount";
            this.dgvColRetentionMinFileCount.ToolTipText = "Minimum File Count -will stop retention from deleting all the files if backups st" +
    "op ";
            this.dgvColRetentionMinFileCount.Width = 93;
            // 
            // dgvColRetentionDayOfWeekToKeep
            // 
            this.dgvColRetentionDayOfWeekToKeep.DataPropertyName = "DayOfWeekToKeep";
            this.dgvColRetentionDayOfWeekToKeep.HeaderText = "DayOfWeekToKeep";
            this.dgvColRetentionDayOfWeekToKeep.Items.AddRange(new object[] {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"});
            this.dgvColRetentionDayOfWeekToKeep.Name = "dgvColRetentionDayOfWeekToKeep";
            this.dgvColRetentionDayOfWeekToKeep.ToolTipText = "Day of the Week to Keep for Weekly and Monthly Backups";
            this.dgvColRetentionDayOfWeekToKeep.Width = 110;
            // 
            // dgvColRetentionDailyMaxDaysOld
            // 
            this.dgvColRetentionDailyMaxDaysOld.DataPropertyName = "DailyMaxDaysOld";
            this.dgvColRetentionDailyMaxDaysOld.HeaderText = "DailyMaxDaysOld";
            this.dgvColRetentionDailyMaxDaysOld.MaxInputLength = 10;
            this.dgvColRetentionDailyMaxDaysOld.Name = "dgvColRetentionDailyMaxDaysOld";
            this.dgvColRetentionDailyMaxDaysOld.ToolTipText = "Max Days Old for Daily Backups before they are deleted";
            this.dgvColRetentionDailyMaxDaysOld.Width = 115;
            // 
            // dgvColRetentionWeeklyMaxDaysOld
            // 
            this.dgvColRetentionWeeklyMaxDaysOld.DataPropertyName = "WeeklyMaxDaysOld";
            this.dgvColRetentionWeeklyMaxDaysOld.HeaderText = "WeeklyMaxDaysOld";
            this.dgvColRetentionWeeklyMaxDaysOld.MaxInputLength = 10;
            this.dgvColRetentionWeeklyMaxDaysOld.Name = "dgvColRetentionWeeklyMaxDaysOld";
            this.dgvColRetentionWeeklyMaxDaysOld.ToolTipText = "Max Days old for Weekly before they start to be deleted";
            this.dgvColRetentionWeeklyMaxDaysOld.Width = 128;
            // 
            // dgvColRetentionMonthlyMaxDaysOld
            // 
            this.dgvColRetentionMonthlyMaxDaysOld.DataPropertyName = "MonthlyMaxDaysOld";
            this.dgvColRetentionMonthlyMaxDaysOld.HeaderText = "MonthlyMaxDaysOld";
            this.dgvColRetentionMonthlyMaxDaysOld.MaxInputLength = 10;
            this.dgvColRetentionMonthlyMaxDaysOld.Name = "dgvColRetentionMonthlyMaxDaysOld";
            this.dgvColRetentionMonthlyMaxDaysOld.ToolTipText = "Max Days old of Monthly backups before they are deleted";
            this.dgvColRetentionMonthlyMaxDaysOld.Width = 129;
            // 
            // dgvColRetentionRetentionAlgorithm
            // 
            this.dgvColRetentionRetentionAlgorithm.DataPropertyName = "RetentionAlgorithm";
            this.dgvColRetentionRetentionAlgorithm.HeaderText = "RetentionAlgorithm";
            this.dgvColRetentionRetentionAlgorithm.Items.AddRange(new object[] {
            "GFS",
            "KeepAll",
            "KeepDaily",
            "KeepWeekly",
            "KeepMonthly"});
            this.dgvColRetentionRetentionAlgorithm.Name = "dgvColRetentionRetentionAlgorithm";
            this.dgvColRetentionRetentionAlgorithm.ToolTipText = resources.GetString("dgvColRetentionRetentionAlgorithm.ToolTipText");
            this.dgvColRetentionRetentionAlgorithm.Width = 102;
            // 
            // dgvColRetentionFileNameFilter
            // 
            this.dgvColRetentionFileNameFilter.DataPropertyName = "FileNameFilter";
            this.dgvColRetentionFileNameFilter.HeaderText = "FileNameFilter";
            this.dgvColRetentionFileNameFilter.Name = "dgvColRetentionFileNameFilter";
            this.dgvColRetentionFileNameFilter.ToolTipText = resources.GetString("dgvColRetentionFileNameFilter.ToolTipText");
            this.dgvColRetentionFileNameFilter.Width = 98;
            // 
            // tabCompress
            // 
            this.tabCompress.Controls.Add(this.dgvCompress);
            this.tabCompress.Location = new System.Drawing.Point(4, 22);
            this.tabCompress.Name = "tabCompress";
            this.tabCompress.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompress.Size = new System.Drawing.Size(609, 403);
            this.tabCompress.TabIndex = 1;
            this.tabCompress.Text = "Compression";
            this.tabCompress.UseVisualStyleBackColor = true;
            // 
            // dgvCompress
            // 
            this.dgvCompress.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCompress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColCompressID,
            this.dgvColCompressEnabled,
            this.dgvColCompressTime,
            this.dgvColCompressEndTime,
            this.dgvColCompressIntervalType,
            this.dgvColCompressInterval,
            this.dgvColCompressMonday,
            this.dgvColCompressTuesday,
            this.dgvColCompressWednesday,
            this.dgvColCompressThursday,
            this.dgvColCompressFriday,
            this.dgvColCompressSaturday,
            this.dgvColCompressSunday,
            this.dgvColCompressCompress,
            this.dgvColCompressSourceOption,
            this.dgvColCompressSourceFolder,
            this.dgvColCompressDestinationFolder,
            this.dgvColCompressEncryptionPassword,
            this.dgvColCompressKeepOriginalFile,
            this.dgvColCompressCompressionLvl,
            this.dgvColCompressStartCompressingAfterDays,
            this.dgvColCompressFileNameFilter});
            this.dgvCompress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompress.Location = new System.Drawing.Point(3, 3);
            this.dgvCompress.Name = "dgvCompress";
            this.dgvCompress.Size = new System.Drawing.Size(603, 397);
            this.dgvCompress.TabIndex = 11;
            this.dgvCompress.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvCompress_CellBeginEdit);
            this.dgvCompress.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompress_CellDoubleClick);
            this.dgvCompress.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompress_CellEndEdit);
            this.dgvCompress.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCompress_CellFormatting);
            this.dgvCompress.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvCompress_CellValidating);
            // 
            // dgvColCompressID
            // 
            this.dgvColCompressID.DataPropertyName = "ID";
            this.dgvColCompressID.HeaderText = "ID";
            this.dgvColCompressID.Name = "dgvColCompressID";
            this.dgvColCompressID.ReadOnly = true;
            this.dgvColCompressID.Width = 43;
            // 
            // dgvColCompressEnabled
            // 
            this.dgvColCompressEnabled.DataPropertyName = "Enabled";
            this.dgvColCompressEnabled.FalseValue = "false";
            this.dgvColCompressEnabled.HeaderText = "Enabled";
            this.dgvColCompressEnabled.IndeterminateValue = "";
            this.dgvColCompressEnabled.Name = "dgvColCompressEnabled";
            this.dgvColCompressEnabled.ToolTipText = "This configuration row enabled?";
            this.dgvColCompressEnabled.TrueValue = "true";
            this.dgvColCompressEnabled.Width = 52;
            // 
            // dgvColCompressTime
            // 
            this.dgvColCompressTime.DataPropertyName = "Time";
            this.dgvColCompressTime.HeaderText = "StartTime";
            this.dgvColCompressTime.MaxInputLength = 5;
            this.dgvColCompressTime.Name = "dgvColCompressTime";
            this.dgvColCompressTime.Width = 77;
            // 
            // dgvColCompressEndTime
            // 
            this.dgvColCompressEndTime.DataPropertyName = "EndTime";
            this.dgvColCompressEndTime.HeaderText = "EndTime";
            this.dgvColCompressEndTime.MaxInputLength = 5;
            this.dgvColCompressEndTime.Name = "dgvColCompressEndTime";
            this.dgvColCompressEndTime.Width = 74;
            // 
            // dgvColCompressIntervalType
            // 
            this.dgvColCompressIntervalType.DataPropertyName = "IntervalType";
            this.dgvColCompressIntervalType.HeaderText = "IntervalType";
            this.dgvColCompressIntervalType.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dgvColCompressIntervalType.Name = "dgvColCompressIntervalType";
            this.dgvColCompressIntervalType.Width = 72;
            // 
            // dgvColCompressInterval
            // 
            this.dgvColCompressInterval.DataPropertyName = "Interval";
            this.dgvColCompressInterval.HeaderText = "Interval";
            this.dgvColCompressInterval.Name = "dgvColCompressInterval";
            this.dgvColCompressInterval.Width = 67;
            // 
            // dgvColCompressMonday
            // 
            this.dgvColCompressMonday.DataPropertyName = "Monday";
            this.dgvColCompressMonday.FalseValue = "false";
            this.dgvColCompressMonday.HeaderText = "Mon";
            this.dgvColCompressMonday.IndeterminateValue = "";
            this.dgvColCompressMonday.Name = "dgvColCompressMonday";
            this.dgvColCompressMonday.TrueValue = "true";
            this.dgvColCompressMonday.Width = 34;
            // 
            // dgvColCompressTuesday
            // 
            this.dgvColCompressTuesday.DataPropertyName = "Tuesday";
            this.dgvColCompressTuesday.FalseValue = "false";
            this.dgvColCompressTuesday.HeaderText = "Tue";
            this.dgvColCompressTuesday.IndeterminateValue = "";
            this.dgvColCompressTuesday.Name = "dgvColCompressTuesday";
            this.dgvColCompressTuesday.TrueValue = "true";
            this.dgvColCompressTuesday.Width = 32;
            // 
            // dgvColCompressWednesday
            // 
            this.dgvColCompressWednesday.DataPropertyName = "Wednesday";
            this.dgvColCompressWednesday.FalseValue = "false";
            this.dgvColCompressWednesday.HeaderText = "Wed";
            this.dgvColCompressWednesday.IndeterminateValue = "";
            this.dgvColCompressWednesday.Name = "dgvColCompressWednesday";
            this.dgvColCompressWednesday.TrueValue = "true";
            this.dgvColCompressWednesday.Width = 36;
            // 
            // dgvColCompressThursday
            // 
            this.dgvColCompressThursday.DataPropertyName = "Thursday";
            this.dgvColCompressThursday.FalseValue = "false";
            this.dgvColCompressThursday.HeaderText = "Thu";
            this.dgvColCompressThursday.IndeterminateValue = "";
            this.dgvColCompressThursday.Name = "dgvColCompressThursday";
            this.dgvColCompressThursday.TrueValue = "true";
            this.dgvColCompressThursday.Width = 32;
            // 
            // dgvColCompressFriday
            // 
            this.dgvColCompressFriday.DataPropertyName = "Friday";
            this.dgvColCompressFriday.FalseValue = "false";
            this.dgvColCompressFriday.HeaderText = "Fri";
            this.dgvColCompressFriday.IndeterminateValue = "";
            this.dgvColCompressFriday.Name = "dgvColCompressFriday";
            this.dgvColCompressFriday.TrueValue = "true";
            this.dgvColCompressFriday.Width = 24;
            // 
            // dgvColCompressSaturday
            // 
            this.dgvColCompressSaturday.DataPropertyName = "Saturday";
            this.dgvColCompressSaturday.FalseValue = "false";
            this.dgvColCompressSaturday.HeaderText = "Sat";
            this.dgvColCompressSaturday.IndeterminateValue = "";
            this.dgvColCompressSaturday.Name = "dgvColCompressSaturday";
            this.dgvColCompressSaturday.TrueValue = "true";
            this.dgvColCompressSaturday.Width = 29;
            // 
            // dgvColCompressSunday
            // 
            this.dgvColCompressSunday.DataPropertyName = "Sunday";
            this.dgvColCompressSunday.FalseValue = "false";
            this.dgvColCompressSunday.HeaderText = "Sun";
            this.dgvColCompressSunday.IndeterminateValue = "";
            this.dgvColCompressSunday.Name = "dgvColCompressSunday";
            this.dgvColCompressSunday.TrueValue = "true";
            this.dgvColCompressSunday.Width = 32;
            // 
            // dgvColCompressCompress
            // 
            this.dgvColCompressCompress.DataPropertyName = "Compress";
            this.dgvColCompressCompress.HeaderText = "Compress?";
            this.dgvColCompressCompress.Items.AddRange(new object[] {
            "Compress",
            "Extract"});
            this.dgvColCompressCompress.Name = "dgvColCompressCompress";
            this.dgvColCompressCompress.ToolTipText = "Compress or Extract All Files";
            this.dgvColCompressCompress.Width = 65;
            // 
            // dgvColCompressSourceOption
            // 
            this.dgvColCompressSourceOption.DataPropertyName = "SourceOption";
            this.dgvColCompressSourceOption.HeaderText = "SourceOption";
            this.dgvColCompressSourceOption.Items.AddRange(new object[] {
            "File",
            "Folder"});
            this.dgvColCompressSourceOption.Name = "dgvColCompressSourceOption";
            this.dgvColCompressSourceOption.ToolTipText = "Either to Compress all files individually or each folder to a single file";
            this.dgvColCompressSourceOption.Width = 78;
            // 
            // dgvColCompressSourceFolder
            // 
            this.dgvColCompressSourceFolder.DataPropertyName = "SourceFolder";
            this.dgvColCompressSourceFolder.HeaderText = "SourceFolder";
            this.dgvColCompressSourceFolder.Name = "dgvColCompressSourceFolder";
            this.dgvColCompressSourceFolder.Width = 95;
            // 
            // dgvColCompressDestinationFolder
            // 
            this.dgvColCompressDestinationFolder.DataPropertyName = "DestinationFolder";
            this.dgvColCompressDestinationFolder.HeaderText = "DestinationFolder";
            this.dgvColCompressDestinationFolder.Name = "dgvColCompressDestinationFolder";
            this.dgvColCompressDestinationFolder.Width = 114;
            // 
            // dgvColCompressEncryptionPassword
            // 
            this.dgvColCompressEncryptionPassword.DataPropertyName = "EncryptionPassword";
            this.dgvColCompressEncryptionPassword.HeaderText = "EncryptionPassword";
            this.dgvColCompressEncryptionPassword.Name = "dgvColCompressEncryptionPassword";
            this.dgvColCompressEncryptionPassword.Width = 128;
            // 
            // dgvColCompressKeepOriginalFile
            // 
            this.dgvColCompressKeepOriginalFile.DataPropertyName = "KeepOriginalFile";
            this.dgvColCompressKeepOriginalFile.HeaderText = "KeepOriginalFile";
            this.dgvColCompressKeepOriginalFile.Items.AddRange(new object[] {
            "true",
            "false"});
            this.dgvColCompressKeepOriginalFile.Name = "dgvColCompressKeepOriginalFile";
            this.dgvColCompressKeepOriginalFile.ToolTipText = "Whether to delete the original file after successful 7zip file is created";
            this.dgvColCompressKeepOriginalFile.Width = 89;
            // 
            // dgvColCompressCompressionLvl
            // 
            this.dgvColCompressCompressionLvl.DataPropertyName = "CompressionLvl";
            this.dgvColCompressCompressionLvl.HeaderText = "CompressionLvl";
            this.dgvColCompressCompressionLvl.Items.AddRange(new object[] {
            "None",
            "Fast",
            "Low",
            "Normal",
            "High",
            "Ultra"});
            this.dgvColCompressCompressionLvl.Name = "dgvColCompressCompressionLvl";
            this.dgvColCompressCompressionLvl.ToolTipText = "7zip compression level";
            this.dgvColCompressCompressionLvl.Width = 87;
            // 
            // dgvColCompressStartCompressingAfterDays
            // 
            this.dgvColCompressStartCompressingAfterDays.DataPropertyName = "StartCompressingAfterDays";
            this.dgvColCompressStartCompressingAfterDays.HeaderText = "StartCompressingAfterDays";
            this.dgvColCompressStartCompressingAfterDays.MaxInputLength = 10;
            this.dgvColCompressStartCompressingAfterDays.Name = "dgvColCompressStartCompressingAfterDays";
            this.dgvColCompressStartCompressingAfterDays.ToolTipText = "Only starts compressing files after days specified in this field.";
            this.dgvColCompressStartCompressingAfterDays.Width = 160;
            // 
            // dgvColCompressFileNameFilter
            // 
            this.dgvColCompressFileNameFilter.DataPropertyName = "FileNameFilter";
            this.dgvColCompressFileNameFilter.HeaderText = "FileNameFilter";
            this.dgvColCompressFileNameFilter.Name = "dgvColCompressFileNameFilter";
            this.dgvColCompressFileNameFilter.Width = 98;
            // 
            // tabSync
            // 
            this.tabSync.Controls.Add(this.dgvSync);
            this.tabSync.Location = new System.Drawing.Point(4, 22);
            this.tabSync.Name = "tabSync";
            this.tabSync.Padding = new System.Windows.Forms.Padding(3);
            this.tabSync.Size = new System.Drawing.Size(609, 403);
            this.tabSync.TabIndex = 2;
            this.tabSync.Text = "Synchronization";
            this.tabSync.UseVisualStyleBackColor = true;
            // 
            // dgvSync
            // 
            this.dgvSync.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSync.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSync.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColSyncID,
            this.dgvColSyncEnabled1,
            this.dgvColSyncTime,
            this.dgvColSyncEndTime,
            this.dgvColSyncIntervalType,
            this.dgvColSyncInterval,
            this.dgvColSyncMonday,
            this.dgvColSyncTuesday,
            this.dgvColSyncWednesday,
            this.dgvColSyncThursday,
            this.dgvColSyncFriday,
            this.dgvColSyncSaturday,
            this.dgvColSyncSunday,
            this.dgvColSyncSourceFolder,
            this.dgvColSyncDestinationFolder,
            this.dgvColSyncFileSyncReplicaOption,
            this.dgvColSyncFileSyncReset,
            this.dgvColSyncFolderSyncDirectionOrder,
            this.dgvColSyncDefaultConflictResolutionPolicy,
            this.dgvColSyncArchiveDeleted,
            this.dgvColSyncArchiveFolder});
            this.dgvSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSync.Location = new System.Drawing.Point(3, 3);
            this.dgvSync.Name = "dgvSync";
            this.dgvSync.Size = new System.Drawing.Size(603, 397);
            this.dgvSync.TabIndex = 13;
            this.dgvSync.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSync_CellDoubleClick);
            this.dgvSync.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvSync_CellValidating);
            // 
            // dgvColSyncID
            // 
            this.dgvColSyncID.DataPropertyName = "ID";
            this.dgvColSyncID.HeaderText = "ID";
            this.dgvColSyncID.Name = "dgvColSyncID";
            this.dgvColSyncID.ReadOnly = true;
            this.dgvColSyncID.Width = 43;
            // 
            // dgvColSyncEnabled1
            // 
            this.dgvColSyncEnabled1.DataPropertyName = "Enabled";
            this.dgvColSyncEnabled1.FalseValue = "false";
            this.dgvColSyncEnabled1.HeaderText = "Enabled";
            this.dgvColSyncEnabled1.IndeterminateValue = "";
            this.dgvColSyncEnabled1.Name = "dgvColSyncEnabled1";
            this.dgvColSyncEnabled1.TrueValue = "true";
            this.dgvColSyncEnabled1.Width = 52;
            // 
            // dgvColSyncTime
            // 
            this.dgvColSyncTime.DataPropertyName = "Time";
            this.dgvColSyncTime.HeaderText = "StartTime";
            this.dgvColSyncTime.MaxInputLength = 5;
            this.dgvColSyncTime.Name = "dgvColSyncTime";
            this.dgvColSyncTime.Width = 77;
            // 
            // dgvColSyncEndTime
            // 
            this.dgvColSyncEndTime.DataPropertyName = "EndTime";
            this.dgvColSyncEndTime.HeaderText = "EndTime";
            this.dgvColSyncEndTime.MaxInputLength = 5;
            this.dgvColSyncEndTime.Name = "dgvColSyncEndTime";
            this.dgvColSyncEndTime.Width = 74;
            // 
            // dgvColSyncIntervalType
            // 
            this.dgvColSyncIntervalType.DataPropertyName = "IntervalType";
            this.dgvColSyncIntervalType.HeaderText = "IntervalType";
            this.dgvColSyncIntervalType.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dgvColSyncIntervalType.Name = "dgvColSyncIntervalType";
            this.dgvColSyncIntervalType.Width = 72;
            // 
            // dgvColSyncInterval
            // 
            this.dgvColSyncInterval.DataPropertyName = "Interval";
            this.dgvColSyncInterval.HeaderText = "Interval";
            this.dgvColSyncInterval.Name = "dgvColSyncInterval";
            this.dgvColSyncInterval.Width = 67;
            // 
            // dgvColSyncMonday
            // 
            this.dgvColSyncMonday.DataPropertyName = "Monday";
            this.dgvColSyncMonday.FalseValue = "false";
            this.dgvColSyncMonday.HeaderText = "Mon";
            this.dgvColSyncMonday.IndeterminateValue = "";
            this.dgvColSyncMonday.Name = "dgvColSyncMonday";
            this.dgvColSyncMonday.TrueValue = "true";
            this.dgvColSyncMonday.Width = 34;
            // 
            // dgvColSyncTuesday
            // 
            this.dgvColSyncTuesday.DataPropertyName = "Tuesday";
            this.dgvColSyncTuesday.FalseValue = "false";
            this.dgvColSyncTuesday.HeaderText = "Tue";
            this.dgvColSyncTuesday.IndeterminateValue = "";
            this.dgvColSyncTuesday.Name = "dgvColSyncTuesday";
            this.dgvColSyncTuesday.TrueValue = "true";
            this.dgvColSyncTuesday.Width = 32;
            // 
            // dgvColSyncWednesday
            // 
            this.dgvColSyncWednesday.DataPropertyName = "Wednesday";
            this.dgvColSyncWednesday.FalseValue = "false";
            this.dgvColSyncWednesday.HeaderText = "Wed";
            this.dgvColSyncWednesday.IndeterminateValue = "";
            this.dgvColSyncWednesday.Name = "dgvColSyncWednesday";
            this.dgvColSyncWednesday.TrueValue = "true";
            this.dgvColSyncWednesday.Width = 36;
            // 
            // dgvColSyncThursday
            // 
            this.dgvColSyncThursday.DataPropertyName = "Thursday";
            this.dgvColSyncThursday.FalseValue = "false";
            this.dgvColSyncThursday.HeaderText = "Thu";
            this.dgvColSyncThursday.IndeterminateValue = "";
            this.dgvColSyncThursday.Name = "dgvColSyncThursday";
            this.dgvColSyncThursday.TrueValue = "true";
            this.dgvColSyncThursday.Width = 32;
            // 
            // dgvColSyncFriday
            // 
            this.dgvColSyncFriday.DataPropertyName = "Friday";
            this.dgvColSyncFriday.FalseValue = "false";
            this.dgvColSyncFriday.HeaderText = "Fri";
            this.dgvColSyncFriday.IndeterminateValue = "";
            this.dgvColSyncFriday.Name = "dgvColSyncFriday";
            this.dgvColSyncFriday.TrueValue = "true";
            this.dgvColSyncFriday.Width = 24;
            // 
            // dgvColSyncSaturday
            // 
            this.dgvColSyncSaturday.DataPropertyName = "Saturday";
            this.dgvColSyncSaturday.FalseValue = "false";
            this.dgvColSyncSaturday.HeaderText = "Sat";
            this.dgvColSyncSaturday.IndeterminateValue = "";
            this.dgvColSyncSaturday.Name = "dgvColSyncSaturday";
            this.dgvColSyncSaturday.TrueValue = "true";
            this.dgvColSyncSaturday.Width = 29;
            // 
            // dgvColSyncSunday
            // 
            this.dgvColSyncSunday.DataPropertyName = "Sunday";
            this.dgvColSyncSunday.FalseValue = "false";
            this.dgvColSyncSunday.HeaderText = "Sun";
            this.dgvColSyncSunday.IndeterminateValue = "";
            this.dgvColSyncSunday.Name = "dgvColSyncSunday";
            this.dgvColSyncSunday.TrueValue = "true";
            this.dgvColSyncSunday.Width = 32;
            // 
            // dgvColSyncSourceFolder
            // 
            this.dgvColSyncSourceFolder.DataPropertyName = "SourceFolder";
            this.dgvColSyncSourceFolder.HeaderText = "SourceFolder";
            this.dgvColSyncSourceFolder.Name = "dgvColSyncSourceFolder";
            this.dgvColSyncSourceFolder.Width = 95;
            // 
            // dgvColSyncDestinationFolder
            // 
            this.dgvColSyncDestinationFolder.DataPropertyName = "DestinationFolder";
            this.dgvColSyncDestinationFolder.HeaderText = "DestinationFolder";
            this.dgvColSyncDestinationFolder.Name = "dgvColSyncDestinationFolder";
            this.dgvColSyncDestinationFolder.Width = 114;
            // 
            // dgvColSyncFileSyncReplicaOption
            // 
            this.dgvColSyncFileSyncReplicaOption.DataPropertyName = "FileSyncReplicaOption";
            this.dgvColSyncFileSyncReplicaOption.HeaderText = "FileSyncReplicaOption";
            this.dgvColSyncFileSyncReplicaOption.Items.AddRange(new object[] {
            "OneWayMirror",
            "OneWayBackup"});
            this.dgvColSyncFileSyncReplicaOption.Name = "dgvColSyncFileSyncReplicaOption";
            this.dgvColSyncFileSyncReplicaOption.ToolTipText = "OneWayBackup or OneWay Mirror which will delete files in the destination folder t" +
    "hat no longer exist on the source folder.";
            this.dgvColSyncFileSyncReplicaOption.Width = 120;
            // 
            // dgvColSyncFileSyncReset
            // 
            this.dgvColSyncFileSyncReset.DataPropertyName = "FileSyncReset";
            this.dgvColSyncFileSyncReset.HeaderText = "FileSyncReset";
            this.dgvColSyncFileSyncReset.Items.AddRange(new object[] {
            "true",
            "false"});
            this.dgvColSyncFileSyncReset.Name = "dgvColSyncFileSyncReset";
            this.dgvColSyncFileSyncReset.ToolTipText = "Deletes .metadata file which forces files already copied if missing to be copied " +
    "over again";
            this.dgvColSyncFileSyncReset.Width = 81;
            // 
            // dgvColSyncFolderSyncDirectionOrder
            // 
            this.dgvColSyncFolderSyncDirectionOrder.DataPropertyName = "FolderSyncDirectionOrder";
            this.dgvColSyncFolderSyncDirectionOrder.HeaderText = "FolderSyncDirectionOrder";
            this.dgvColSyncFolderSyncDirectionOrder.Items.AddRange(new object[] {
            "Upload",
            "Download"});
            this.dgvColSyncFolderSyncDirectionOrder.Name = "dgvColSyncFolderSyncDirectionOrder";
            this.dgvColSyncFolderSyncDirectionOrder.ToolTipText = resources.GetString("dgvColSyncFolderSyncDirectionOrder.ToolTipText");
            this.dgvColSyncFolderSyncDirectionOrder.Width = 134;
            // 
            // dgvColSyncDefaultConflictResolutionPolicy
            // 
            this.dgvColSyncDefaultConflictResolutionPolicy.DataPropertyName = "DefaultConflictResolutionPolicy";
            this.dgvColSyncDefaultConflictResolutionPolicy.HeaderText = "DefaultConflictResolutionPolicy";
            this.dgvColSyncDefaultConflictResolutionPolicy.Items.AddRange(new object[] {
            "SourceWins",
            "DestinationWins"});
            this.dgvColSyncDefaultConflictResolutionPolicy.Name = "dgvColSyncDefaultConflictResolutionPolicy";
            this.dgvColSyncDefaultConflictResolutionPolicy.ToolTipText = "whether the source location will be the winner for conflicts or the destination.";
            this.dgvColSyncDefaultConflictResolutionPolicy.Width = 160;
            // 
            // dgvColSyncArchiveDeleted
            // 
            this.dgvColSyncArchiveDeleted.DataPropertyName = "ArchiveDeleted";
            this.dgvColSyncArchiveDeleted.HeaderText = "ArchiveDeleted";
            this.dgvColSyncArchiveDeleted.Items.AddRange(new object[] {
            "true",
            "false"});
            this.dgvColSyncArchiveDeleted.Name = "dgvColSyncArchiveDeleted";
            this.dgvColSyncArchiveDeleted.ToolTipText = "Whether to move deleted and modified files to ArchiveFolder before being overwrit" +
    "ten or deleted";
            this.dgvColSyncArchiveDeleted.Width = 86;
            // 
            // dgvColSyncArchiveFolder
            // 
            this.dgvColSyncArchiveFolder.DataPropertyName = "ArchiveFolder";
            this.dgvColSyncArchiveFolder.HeaderText = "ArchiveFolder";
            this.dgvColSyncArchiveFolder.Name = "dgvColSyncArchiveFolder";
            this.dgvColSyncArchiveFolder.ToolTipText = "Archive Destination Folder";
            this.dgvColSyncArchiveFolder.Width = 97;
            // 
            // tabRemote
            // 
            this.tabRemote.Controls.Add(this.dgvRemote);
            this.tabRemote.Location = new System.Drawing.Point(4, 22);
            this.tabRemote.Name = "tabRemote";
            this.tabRemote.Padding = new System.Windows.Forms.Padding(3);
            this.tabRemote.Size = new System.Drawing.Size(609, 403);
            this.tabRemote.TabIndex = 3;
            this.tabRemote.Text = "Remote Sync";
            this.tabRemote.UseVisualStyleBackColor = true;
            // 
            // dgvRemote
            // 
            this.dgvRemote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvRemote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRemote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColRemoteID,
            this.dgvColRemoteEnabled,
            this.dgvColRemoteTime,
            this.dgvColRemoteEndTime,
            this.dgvColRemoteIntervalType,
            this.dgvColRemoteInterval,
            this.dgvColRemoteMonday,
            this.dgvColRemoteTuesday,
            this.dgvColRemoteWednesday,
            this.dgvColRemoteThursday,
            this.dgvColRemoteFriday,
            this.dgvColRemoteSaturday,
            this.dgvColRemoteSunday,
            this.dgvColRemoteHost,
            this.dgvColRemoteProtocol,
            this.dgvColRemotePort,
            this.dgvColRemoteUsername,
            this.dgvColRemotePassword,
            this.dgvColRemoteKeyFileDirectory,
            this.dgvColRemoteKeyFileUsePassPhrase,
            this.dgvColRemoteRemoteDirectory,
            this.dgvColRemoteBackupFolder,
            this.dgvColRemoteTransferDirection,
            this.dgvColRemoteAllowAnyCertificate,
            this.dgvColRemoteTimeout,
            this.dgvColRemoteOverwrite,
            this.dgvColRemoteFileNameFilter});
            this.dgvRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRemote.Location = new System.Drawing.Point(3, 3);
            this.dgvRemote.Name = "dgvRemote";
            this.dgvRemote.Size = new System.Drawing.Size(603, 397);
            this.dgvRemote.TabIndex = 15;
            this.dgvRemote.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRemote_CellBeginEdit);
            this.dgvRemote.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRemote_CellDoubleClick);
            this.dgvRemote.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRemote_CellEndEdit);
            this.dgvRemote.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvRemote_CellFormatting);
            this.dgvRemote.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRemote_CellValidating);
            // 
            // dgvColRemoteID
            // 
            this.dgvColRemoteID.DataPropertyName = "ID";
            this.dgvColRemoteID.HeaderText = "ID";
            this.dgvColRemoteID.Name = "dgvColRemoteID";
            this.dgvColRemoteID.ReadOnly = true;
            this.dgvColRemoteID.Width = 43;
            // 
            // dgvColRemoteEnabled
            // 
            this.dgvColRemoteEnabled.DataPropertyName = "Enabled";
            this.dgvColRemoteEnabled.FalseValue = "false";
            this.dgvColRemoteEnabled.HeaderText = "Enabled";
            this.dgvColRemoteEnabled.IndeterminateValue = "";
            this.dgvColRemoteEnabled.Name = "dgvColRemoteEnabled";
            this.dgvColRemoteEnabled.TrueValue = "true";
            this.dgvColRemoteEnabled.Width = 52;
            // 
            // dgvColRemoteTime
            // 
            this.dgvColRemoteTime.DataPropertyName = "Time";
            this.dgvColRemoteTime.HeaderText = "StartTime";
            this.dgvColRemoteTime.MaxInputLength = 5;
            this.dgvColRemoteTime.Name = "dgvColRemoteTime";
            this.dgvColRemoteTime.Width = 77;
            // 
            // dgvColRemoteEndTime
            // 
            this.dgvColRemoteEndTime.DataPropertyName = "EndTime";
            this.dgvColRemoteEndTime.HeaderText = "EndTime";
            this.dgvColRemoteEndTime.MaxInputLength = 5;
            this.dgvColRemoteEndTime.Name = "dgvColRemoteEndTime";
            this.dgvColRemoteEndTime.Width = 74;
            // 
            // dgvColRemoteIntervalType
            // 
            this.dgvColRemoteIntervalType.DataPropertyName = "IntervalType";
            this.dgvColRemoteIntervalType.HeaderText = "IntervalType";
            this.dgvColRemoteIntervalType.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dgvColRemoteIntervalType.Name = "dgvColRemoteIntervalType";
            this.dgvColRemoteIntervalType.Width = 72;
            // 
            // dgvColRemoteInterval
            // 
            this.dgvColRemoteInterval.DataPropertyName = "Interval";
            this.dgvColRemoteInterval.HeaderText = "Interval";
            this.dgvColRemoteInterval.Name = "dgvColRemoteInterval";
            this.dgvColRemoteInterval.Width = 67;
            // 
            // dgvColRemoteMonday
            // 
            this.dgvColRemoteMonday.DataPropertyName = "Monday";
            this.dgvColRemoteMonday.FalseValue = "false";
            this.dgvColRemoteMonday.HeaderText = "Mon";
            this.dgvColRemoteMonday.Name = "dgvColRemoteMonday";
            this.dgvColRemoteMonday.TrueValue = "true";
            this.dgvColRemoteMonday.Width = 34;
            // 
            // dgvColRemoteTuesday
            // 
            this.dgvColRemoteTuesday.DataPropertyName = "Tuesday";
            this.dgvColRemoteTuesday.FalseValue = "false";
            this.dgvColRemoteTuesday.HeaderText = "Tue";
            this.dgvColRemoteTuesday.Name = "dgvColRemoteTuesday";
            this.dgvColRemoteTuesday.TrueValue = "true";
            this.dgvColRemoteTuesday.Width = 32;
            // 
            // dgvColRemoteWednesday
            // 
            this.dgvColRemoteWednesday.DataPropertyName = "Wednesday";
            this.dgvColRemoteWednesday.FalseValue = "false";
            this.dgvColRemoteWednesday.HeaderText = "Wed";
            this.dgvColRemoteWednesday.Name = "dgvColRemoteWednesday";
            this.dgvColRemoteWednesday.TrueValue = "true";
            this.dgvColRemoteWednesday.Width = 36;
            // 
            // dgvColRemoteThursday
            // 
            this.dgvColRemoteThursday.DataPropertyName = "Thursday";
            this.dgvColRemoteThursday.FalseValue = "false";
            this.dgvColRemoteThursday.HeaderText = "Thu";
            this.dgvColRemoteThursday.Name = "dgvColRemoteThursday";
            this.dgvColRemoteThursday.TrueValue = "true";
            this.dgvColRemoteThursday.Width = 32;
            // 
            // dgvColRemoteFriday
            // 
            this.dgvColRemoteFriday.DataPropertyName = "Friday";
            this.dgvColRemoteFriday.FalseValue = "false";
            this.dgvColRemoteFriday.HeaderText = "Fri";
            this.dgvColRemoteFriday.Name = "dgvColRemoteFriday";
            this.dgvColRemoteFriday.TrueValue = "true";
            this.dgvColRemoteFriday.Width = 24;
            // 
            // dgvColRemoteSaturday
            // 
            this.dgvColRemoteSaturday.DataPropertyName = "Saturday";
            this.dgvColRemoteSaturday.FalseValue = "false";
            this.dgvColRemoteSaturday.HeaderText = "Sat";
            this.dgvColRemoteSaturday.Name = "dgvColRemoteSaturday";
            this.dgvColRemoteSaturday.TrueValue = "true";
            this.dgvColRemoteSaturday.Width = 29;
            // 
            // dgvColRemoteSunday
            // 
            this.dgvColRemoteSunday.DataPropertyName = "Sunday";
            this.dgvColRemoteSunday.FalseValue = "false";
            this.dgvColRemoteSunday.HeaderText = "Sun";
            this.dgvColRemoteSunday.Name = "dgvColRemoteSunday";
            this.dgvColRemoteSunday.TrueValue = "true";
            this.dgvColRemoteSunday.Width = 32;
            // 
            // dgvColRemoteHost
            // 
            this.dgvColRemoteHost.DataPropertyName = "Host";
            this.dgvColRemoteHost.HeaderText = "Host";
            this.dgvColRemoteHost.Name = "dgvColRemoteHost";
            this.dgvColRemoteHost.Width = 54;
            // 
            // dgvColRemoteProtocol
            // 
            this.dgvColRemoteProtocol.DataPropertyName = "Protocol";
            this.dgvColRemoteProtocol.HeaderText = "Protocol";
            this.dgvColRemoteProtocol.Items.AddRange(new object[] {
            "SFTP",
            "FTPsImplicit",
            "FTPsExplicit",
            "FTP"});
            this.dgvColRemoteProtocol.Name = "dgvColRemoteProtocol";
            this.dgvColRemoteProtocol.Width = 52;
            // 
            // dgvColRemotePort
            // 
            this.dgvColRemotePort.DataPropertyName = "Port";
            this.dgvColRemotePort.HeaderText = "Port";
            this.dgvColRemotePort.MaxInputLength = 10;
            this.dgvColRemotePort.Name = "dgvColRemotePort";
            this.dgvColRemotePort.Width = 51;
            // 
            // dgvColRemoteUsername
            // 
            this.dgvColRemoteUsername.DataPropertyName = "Username";
            this.dgvColRemoteUsername.HeaderText = "Username";
            this.dgvColRemoteUsername.Name = "dgvColRemoteUsername";
            this.dgvColRemoteUsername.Width = 80;
            // 
            // dgvColRemotePassword
            // 
            this.dgvColRemotePassword.DataPropertyName = "Password";
            this.dgvColRemotePassword.HeaderText = "Password";
            this.dgvColRemotePassword.Name = "dgvColRemotePassword";
            this.dgvColRemotePassword.Width = 78;
            // 
            // dgvColRemoteKeyFileDirectory
            // 
            this.dgvColRemoteKeyFileDirectory.DataPropertyName = "KeyFileDirectory";
            this.dgvColRemoteKeyFileDirectory.HeaderText = "KeyFileDirectory";
            this.dgvColRemoteKeyFileDirectory.Name = "dgvColRemoteKeyFileDirectory";
            this.dgvColRemoteKeyFileDirectory.Width = 108;
            // 
            // dgvColRemoteKeyFileUsePassPhrase
            // 
            this.dgvColRemoteKeyFileUsePassPhrase.DataPropertyName = "UsePassPhrase";
            this.dgvColRemoteKeyFileUsePassPhrase.HeaderText = "KeyFileUsePassPhrase";
            this.dgvColRemoteKeyFileUsePassPhrase.Items.AddRange(new object[] {
            "true",
            "false"});
            this.dgvColRemoteKeyFileUsePassPhrase.Name = "dgvColRemoteKeyFileUsePassPhrase";
            this.dgvColRemoteKeyFileUsePassPhrase.Width = 122;
            // 
            // dgvColRemoteRemoteDirectory
            // 
            this.dgvColRemoteRemoteDirectory.DataPropertyName = "RemoteDirectory";
            this.dgvColRemoteRemoteDirectory.HeaderText = "RemoteDirectory";
            this.dgvColRemoteRemoteDirectory.Name = "dgvColRemoteRemoteDirectory";
            this.dgvColRemoteRemoteDirectory.Width = 111;
            // 
            // dgvColRemoteBackupFolder
            // 
            this.dgvColRemoteBackupFolder.DataPropertyName = "BackupFolder";
            this.dgvColRemoteBackupFolder.HeaderText = "BackupFolder";
            this.dgvColRemoteBackupFolder.Name = "dgvColRemoteBackupFolder";
            this.dgvColRemoteBackupFolder.Width = 98;
            // 
            // dgvColRemoteTransferDirection
            // 
            this.dgvColRemoteTransferDirection.DataPropertyName = "TransferDirection";
            this.dgvColRemoteTransferDirection.HeaderText = "TransferDirection";
            this.dgvColRemoteTransferDirection.Items.AddRange(new object[] {
            "Upload",
            "Download"});
            this.dgvColRemoteTransferDirection.Name = "dgvColRemoteTransferDirection";
            this.dgvColRemoteTransferDirection.Width = 94;
            // 
            // dgvColRemoteAllowAnyCertificate
            // 
            this.dgvColRemoteAllowAnyCertificate.DataPropertyName = "AllowAnyCertificate";
            this.dgvColRemoteAllowAnyCertificate.HeaderText = "AllowAnyCertificate";
            this.dgvColRemoteAllowAnyCertificate.Items.AddRange(new object[] {
            "true",
            "false"});
            this.dgvColRemoteAllowAnyCertificate.Name = "dgvColRemoteAllowAnyCertificate";
            this.dgvColRemoteAllowAnyCertificate.Width = 103;
            // 
            // dgvColRemoteTimeout
            // 
            this.dgvColRemoteTimeout.DataPropertyName = "Timeout";
            this.dgvColRemoteTimeout.HeaderText = "Timeout";
            this.dgvColRemoteTimeout.MaxInputLength = 15;
            this.dgvColRemoteTimeout.Name = "dgvColRemoteTimeout";
            this.dgvColRemoteTimeout.Width = 70;
            // 
            // dgvColRemoteOverwrite
            // 
            this.dgvColRemoteOverwrite.DataPropertyName = "Overwrite";
            this.dgvColRemoteOverwrite.HeaderText = "Overwrite";
            this.dgvColRemoteOverwrite.Items.AddRange(new object[] {
            "NoOverwrite",
            "ForceOverwrite",
            "FileSizeChangeOverwrite"});
            this.dgvColRemoteOverwrite.Name = "dgvColRemoteOverwrite";
            this.dgvColRemoteOverwrite.Width = 58;
            // 
            // dgvColRemoteFileNameFilter
            // 
            this.dgvColRemoteFileNameFilter.DataPropertyName = "FileNameFilter";
            this.dgvColRemoteFileNameFilter.HeaderText = "FileNameFilter";
            this.dgvColRemoteFileNameFilter.Name = "dgvColRemoteFileNameFilter";
            this.dgvColRemoteFileNameFilter.Width = 98;
            // 
            // tabEvents
            // 
            this.tabEvents.Controls.Add(this.lblSearch);
            this.tabEvents.Controls.Add(this.txtSearch);
            this.tabEvents.Controls.Add(this.chkInformation);
            this.tabEvents.Controls.Add(this.chkWarning);
            this.tabEvents.Controls.Add(this.chkError);
            this.tabEvents.Controls.Add(this.btnRefreshEventLog);
            this.tabEvents.Controls.Add(this.btnClearLogs);
            this.tabEvents.Controls.Add(this.dgvEvents);
            this.tabEvents.Location = new System.Drawing.Point(4, 22);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(609, 403);
            this.tabEvents.TabIndex = 4;
            this.tabEvents.Text = "Log";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(360, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(49, 15);
            this.lblSearch.TabIndex = 42;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(415, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(103, 21);
            this.txtSearch.TabIndex = 41;
            // 
            // chkInformation
            // 
            this.chkInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInformation.AutoSize = true;
            this.chkInformation.Checked = true;
            this.chkInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInformation.Location = new System.Drawing.Point(266, 11);
            this.chkInformation.Name = "chkInformation";
            this.chkInformation.Size = new System.Drawing.Size(88, 19);
            this.chkInformation.TabIndex = 40;
            this.chkInformation.Text = "Information";
            this.chkInformation.UseVisualStyleBackColor = true;
            // 
            // chkWarning
            // 
            this.chkWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWarning.AutoSize = true;
            this.chkWarning.Checked = true;
            this.chkWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWarning.Location = new System.Drawing.Point(188, 11);
            this.chkWarning.Name = "chkWarning";
            this.chkWarning.Size = new System.Drawing.Size(72, 19);
            this.chkWarning.TabIndex = 39;
            this.chkWarning.Text = "Warning";
            this.chkWarning.UseVisualStyleBackColor = true;
            // 
            // chkError
            // 
            this.chkError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkError.AutoSize = true;
            this.chkError.Checked = true;
            this.chkError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkError.Location = new System.Drawing.Point(129, 11);
            this.chkError.Name = "chkError";
            this.chkError.Size = new System.Drawing.Size(53, 19);
            this.chkError.TabIndex = 38;
            this.chkError.Text = "Error";
            this.chkError.UseVisualStyleBackColor = true;
            // 
            // btnRefreshEventLog
            // 
            this.btnRefreshEventLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshEventLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshEventLog.Location = new System.Drawing.Point(524, 6);
            this.btnRefreshEventLog.Name = "btnRefreshEventLog";
            this.btnRefreshEventLog.Size = new System.Drawing.Size(79, 27);
            this.btnRefreshEventLog.TabIndex = 37;
            this.btnRefreshEventLog.Text = "Search";
            this.btnRefreshEventLog.UseVisualStyleBackColor = true;
            this.btnRefreshEventLog.Click += new System.EventHandler(this.btnRefreshEventLog_Click);
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLogs.Location = new System.Drawing.Point(3, 5);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(79, 27);
            this.btnClearLogs.TabIndex = 36;
            this.btnClearLogs.Text = "Clear Log";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // dgvEvents
            // 
            this.dgvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvEventImage,
            this.dgvEventType,
            this.dgvColEventTime,
            this.dgvColEventMessage,
            this.dgvColEventSource,
            this.dgvColEventCategory,
            this.dgvColEventEventID});
            this.dgvEvents.Location = new System.Drawing.Point(0, 39);
            this.dgvEvents.Name = "dgvEvents";
            this.dgvEvents.Size = new System.Drawing.Size(609, 361);
            this.dgvEvents.TabIndex = 17;
            // 
            // dgvEventImage
            // 
            this.dgvEventImage.DataPropertyName = "EventImage";
            this.dgvEventImage.HeaderText = "EventImage";
            this.dgvEventImage.Name = "dgvEventImage";
            this.dgvEventImage.Width = 70;
            // 
            // dgvEventType
            // 
            this.dgvEventType.DataPropertyName = "Type";
            this.dgvEventType.HeaderText = "Type";
            this.dgvEventType.Name = "dgvEventType";
            this.dgvEventType.Visible = false;
            this.dgvEventType.Width = 56;
            // 
            // dgvColEventTime
            // 
            this.dgvColEventTime.DataPropertyName = "Time";
            this.dgvColEventTime.HeaderText = "Time";
            this.dgvColEventTime.Name = "dgvColEventTime";
            this.dgvColEventTime.Width = 55;
            // 
            // dgvColEventMessage
            // 
            this.dgvColEventMessage.DataPropertyName = "Message";
            this.dgvColEventMessage.HeaderText = "Message";
            this.dgvColEventMessage.Name = "dgvColEventMessage";
            this.dgvColEventMessage.Width = 75;
            // 
            // dgvColEventSource
            // 
            this.dgvColEventSource.DataPropertyName = "Source";
            this.dgvColEventSource.HeaderText = "Source";
            this.dgvColEventSource.Name = "dgvColEventSource";
            this.dgvColEventSource.Width = 66;
            // 
            // dgvColEventCategory
            // 
            this.dgvColEventCategory.DataPropertyName = "Category";
            this.dgvColEventCategory.HeaderText = "Category";
            this.dgvColEventCategory.Name = "dgvColEventCategory";
            this.dgvColEventCategory.Width = 74;
            // 
            // dgvColEventEventID
            // 
            this.dgvColEventEventID.DataPropertyName = "EventID";
            this.dgvColEventEventID.HeaderText = "EventID";
            this.dgvColEventEventID.Name = "dgvColEventEventID";
            this.dgvColEventEventID.Width = 71;
            // 
            // TabTasks
            // 
            this.TabTasks.Controls.Add(this.dgvTasks);
            this.TabTasks.Location = new System.Drawing.Point(4, 22);
            this.TabTasks.Name = "TabTasks";
            this.TabTasks.Padding = new System.Windows.Forms.Padding(3);
            this.TabTasks.Size = new System.Drawing.Size(609, 403);
            this.TabTasks.TabIndex = 5;
            this.TabTasks.Text = "Tasks";
            this.TabTasks.UseVisualStyleBackColor = true;
            // 
            // dgvTasks
            // 
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColScriptID,
            this.dgvColScriptEnabled,
            this.dgvColScriptStartTime,
            this.dgvColScriptEndTime,
            this.dgvColScriptIntervalType,
            this.dgvColScriptInterval,
            this.dgvColScriptMonday,
            this.dgvColScriptTuesday,
            this.dgvColScriptWednesday,
            this.dgvColScriptThursday,
            this.dgvColScriptFriday,
            this.dgvColScriptSaturday,
            this.dgvColScriptSunday,
            this.dgvColScriptWorkingDirector,
            this.dgvColScriptFileName,
            this.dgvColScriptArguments,
            this.dgvColScriptSourceFolder,
            this.dgvColScriptDestinationFolder,
            this.dgvColScriptTimeout});
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTasks.Location = new System.Drawing.Point(3, 3);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.Size = new System.Drawing.Size(603, 397);
            this.dgvTasks.TabIndex = 16;
            this.dgvTasks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTasks_CellDoubleClick);
            this.dgvTasks.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvTasks_CellValidating);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.helpToolStripMenuItem1,
            this.servicesConsoleToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.helpToolStripMenuItem1.Text = "&Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // servicesConsoleToolStripMenuItem
            // 
            this.servicesConsoleToolStripMenuItem.Name = "servicesConsoleToolStripMenuItem";
            this.servicesConsoleToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.servicesConsoleToolStripMenuItem.Text = "Services Console";
            this.servicesConsoleToolStripMenuItem.Click += new System.EventHandler(this.servicesConsoleToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtServiceStatus);
            this.panel1.Controls.Add(this.lblServiceIntervalMinutes);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.gbTime);
            this.panel1.Controls.Add(this.btnSaveApply);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.mtxtMaxDriveSpaceUsed);
            this.panel1.Controls.Add(this.lblMaxDriveSpaceUsed);
            this.panel1.Controls.Add(this.lblServiceInterval);
            this.panel1.Controls.Add(this.txtServiceInterval);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pbLeftIcon);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 211);
            this.panel1.TabIndex = 20;
            // 
            // txtServiceStatus
            // 
            this.txtServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceStatus.Location = new System.Drawing.Point(384, 174);
            this.txtServiceStatus.Name = "txtServiceStatus";
            this.txtServiceStatus.ReadOnly = true;
            this.txtServiceStatus.Size = new System.Drawing.Size(107, 24);
            this.txtServiceStatus.TabIndex = 33;
            // 
            // lblServiceIntervalMinutes
            // 
            this.lblServiceIntervalMinutes.AutoSize = true;
            this.lblServiceIntervalMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceIntervalMinutes.Location = new System.Drawing.Point(359, 54);
            this.lblServiceIntervalMinutes.Name = "lblServiceIntervalMinutes";
            this.lblServiceIntervalMinutes.Size = new System.Drawing.Size(72, 18);
            this.lblServiceIntervalMinutes.TabIndex = 32;
            this.lblServiceIntervalMinutes.Text = "5 Minutes";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(303, 170);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 30);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "S&tart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(221, 170);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 30);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Sto&p";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gbTime
            // 
            this.gbTime.Controls.Add(this.lblTimeInfo);
            this.gbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTime.Location = new System.Drawing.Point(9, 114);
            this.gbTime.Name = "gbTime";
            this.gbTime.Size = new System.Drawing.Size(495, 51);
            this.gbTime.TabIndex = 31;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "Times:";
            // 
            // lblTimeInfo
            // 
            this.lblTimeInfo.AutoSize = true;
            this.lblTimeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeInfo.Location = new System.Drawing.Point(6, 19);
            this.lblTimeInfo.Name = "lblTimeInfo";
            this.lblTimeInfo.Size = new System.Drawing.Size(476, 17);
            this.lblTimeInfo.TabIndex = 13;
            this.lblTimeInfo.Text = "Time can be blank. If blank then execution will occur every service interval.";
            // 
            // btnSaveApply
            // 
            this.btnSaveApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveApply.Location = new System.Drawing.Point(91, 171);
            this.btnSaveApply.Name = "btnSaveApply";
            this.btnSaveApply.Size = new System.Drawing.Size(124, 29);
            this.btnSaveApply.TabIndex = 4;
            this.btnSaveApply.Text = "Save and &Apply";
            this.btnSaveApply.UseVisualStyleBackColor = true;
            this.btnSaveApply.Click += new System.EventHandler(this.btnSaveApply_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(9, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mtxtMaxDriveSpaceUsed
            // 
            this.mtxtMaxDriveSpaceUsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtMaxDriveSpaceUsed.Location = new System.Drawing.Point(227, 84);
            this.mtxtMaxDriveSpaceUsed.Mask = "00";
            this.mtxtMaxDriveSpaceUsed.Name = "mtxtMaxDriveSpaceUsed";
            this.mtxtMaxDriveSpaceUsed.Size = new System.Drawing.Size(48, 24);
            this.mtxtMaxDriveSpaceUsed.TabIndex = 2;
            // 
            // lblMaxDriveSpaceUsed
            // 
            this.lblMaxDriveSpaceUsed.AutoSize = true;
            this.lblMaxDriveSpaceUsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxDriveSpaceUsed.Location = new System.Drawing.Point(6, 87);
            this.lblMaxDriveSpaceUsed.Name = "lblMaxDriveSpaceUsed";
            this.lblMaxDriveSpaceUsed.Size = new System.Drawing.Size(218, 18);
            this.lblMaxDriveSpaceUsed.TabIndex = 30;
            this.lblMaxDriveSpaceUsed.Text = "Max Drive Space Used Percent:";
            // 
            // lblServiceInterval
            // 
            this.lblServiceInterval.AutoSize = true;
            this.lblServiceInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceInterval.Location = new System.Drawing.Point(114, 54);
            this.lblServiceInterval.Name = "lblServiceInterval";
            this.lblServiceInterval.Size = new System.Drawing.Size(111, 18);
            this.lblServiceInterval.TabIndex = 25;
            this.lblServiceInterval.Text = "Service Interval:";
            // 
            // txtServiceInterval
            // 
            this.txtServiceInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceInterval.Location = new System.Drawing.Point(227, 54);
            this.txtServiceInterval.Name = "txtServiceInterval";
            this.txtServiceInterval.ReadOnly = true;
            this.txtServiceInterval.Size = new System.Drawing.Size(126, 24);
            this.txtServiceInterval.TabIndex = 1;
            this.txtServiceInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtServiceInterval_KeyPress);
            this.txtServiceInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtServiceInterval_Validating);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(86, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(381, 26);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = "Backup Retention Service Settings";
            // 
            // pbLeftIcon
            // 
            this.pbLeftIcon.Image = global::BackupRetention.Properties.Resources.ImgWoodDriveTime;
            this.pbLeftIcon.InitialImage = global::BackupRetention.Properties.Resources.ImgWoodDriveTime;
            this.pbLeftIcon.Location = new System.Drawing.Point(448, -6);
            this.pbLeftIcon.Name = "pbLeftIcon";
            this.pbLeftIcon.Size = new System.Drawing.Size(118, 127);
            this.pbLeftIcon.TabIndex = 34;
            this.pbLeftIcon.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // dgvColScriptID
            // 
            this.dgvColScriptID.DataPropertyName = "ID";
            this.dgvColScriptID.HeaderText = "ID";
            this.dgvColScriptID.Name = "dgvColScriptID";
            this.dgvColScriptID.ReadOnly = true;
            this.dgvColScriptID.Width = 43;
            // 
            // dgvColScriptEnabled
            // 
            this.dgvColScriptEnabled.DataPropertyName = "Enabled";
            this.dgvColScriptEnabled.FalseValue = "false";
            this.dgvColScriptEnabled.HeaderText = "Enabled";
            this.dgvColScriptEnabled.IndeterminateValue = "";
            this.dgvColScriptEnabled.Name = "dgvColScriptEnabled";
            this.dgvColScriptEnabled.TrueValue = "true";
            this.dgvColScriptEnabled.Width = 52;
            // 
            // dgvColScriptStartTime
            // 
            this.dgvColScriptStartTime.DataPropertyName = "Time";
            this.dgvColScriptStartTime.HeaderText = "StartTime";
            this.dgvColScriptStartTime.MaxInputLength = 5;
            this.dgvColScriptStartTime.Name = "dgvColScriptStartTime";
            this.dgvColScriptStartTime.Width = 77;
            // 
            // dgvColScriptEndTime
            // 
            this.dgvColScriptEndTime.DataPropertyName = "EndTime";
            this.dgvColScriptEndTime.HeaderText = "EndTime";
            this.dgvColScriptEndTime.MaxInputLength = 5;
            this.dgvColScriptEndTime.Name = "dgvColScriptEndTime";
            this.dgvColScriptEndTime.Width = 74;
            // 
            // dgvColScriptIntervalType
            // 
            this.dgvColScriptIntervalType.DataPropertyName = "IntervalType";
            this.dgvColScriptIntervalType.HeaderText = "IntervalType";
            this.dgvColScriptIntervalType.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Monthly"});
            this.dgvColScriptIntervalType.Name = "dgvColScriptIntervalType";
            this.dgvColScriptIntervalType.Width = 72;
            // 
            // dgvColScriptInterval
            // 
            this.dgvColScriptInterval.DataPropertyName = "Interval";
            this.dgvColScriptInterval.HeaderText = "Interval";
            this.dgvColScriptInterval.Name = "dgvColScriptInterval";
            this.dgvColScriptInterval.Width = 67;
            // 
            // dgvColScriptMonday
            // 
            this.dgvColScriptMonday.DataPropertyName = "Monday";
            this.dgvColScriptMonday.FalseValue = "false";
            this.dgvColScriptMonday.HeaderText = "Mon";
            this.dgvColScriptMonday.Name = "dgvColScriptMonday";
            this.dgvColScriptMonday.TrueValue = "true";
            this.dgvColScriptMonday.Width = 34;
            // 
            // dgvColScriptTuesday
            // 
            this.dgvColScriptTuesday.DataPropertyName = "Tuesday";
            this.dgvColScriptTuesday.FalseValue = "false";
            this.dgvColScriptTuesday.HeaderText = "Tue";
            this.dgvColScriptTuesday.Name = "dgvColScriptTuesday";
            this.dgvColScriptTuesday.TrueValue = "true";
            this.dgvColScriptTuesday.Width = 32;
            // 
            // dgvColScriptWednesday
            // 
            this.dgvColScriptWednesday.DataPropertyName = "Wednesday";
            this.dgvColScriptWednesday.FalseValue = "false";
            this.dgvColScriptWednesday.HeaderText = "Wed";
            this.dgvColScriptWednesday.Name = "dgvColScriptWednesday";
            this.dgvColScriptWednesday.TrueValue = "true";
            this.dgvColScriptWednesday.Width = 36;
            // 
            // dgvColScriptThursday
            // 
            this.dgvColScriptThursday.DataPropertyName = "Thursday";
            this.dgvColScriptThursday.FalseValue = "false";
            this.dgvColScriptThursday.HeaderText = "Thu";
            this.dgvColScriptThursday.Name = "dgvColScriptThursday";
            this.dgvColScriptThursday.TrueValue = "true";
            this.dgvColScriptThursday.Width = 32;
            // 
            // dgvColScriptFriday
            // 
            this.dgvColScriptFriday.DataPropertyName = "Friday";
            this.dgvColScriptFriday.FalseValue = "false";
            this.dgvColScriptFriday.HeaderText = "Fri";
            this.dgvColScriptFriday.Name = "dgvColScriptFriday";
            this.dgvColScriptFriday.TrueValue = "true";
            this.dgvColScriptFriday.Width = 24;
            // 
            // dgvColScriptSaturday
            // 
            this.dgvColScriptSaturday.DataPropertyName = "Saturday";
            this.dgvColScriptSaturday.FalseValue = "false";
            this.dgvColScriptSaturday.HeaderText = "Sat";
            this.dgvColScriptSaturday.Name = "dgvColScriptSaturday";
            this.dgvColScriptSaturday.TrueValue = "true";
            this.dgvColScriptSaturday.Width = 29;
            // 
            // dgvColScriptSunday
            // 
            this.dgvColScriptSunday.DataPropertyName = "Sunday";
            this.dgvColScriptSunday.FalseValue = "false";
            this.dgvColScriptSunday.HeaderText = "Sun";
            this.dgvColScriptSunday.Name = "dgvColScriptSunday";
            this.dgvColScriptSunday.TrueValue = "true";
            this.dgvColScriptSunday.Width = 32;
            // 
            // dgvColScriptWorkingDirector
            // 
            this.dgvColScriptWorkingDirector.DataPropertyName = "WorkingDirectory";
            this.dgvColScriptWorkingDirector.HeaderText = "WorkingDirectory";
            this.dgvColScriptWorkingDirector.Name = "dgvColScriptWorkingDirector";
            this.dgvColScriptWorkingDirector.ToolTipText = "The full path where the files most used by the executable are located.";
            this.dgvColScriptWorkingDirector.Width = 114;
            // 
            // dgvColScriptFileName
            // 
            this.dgvColScriptFileName.DataPropertyName = "FileName";
            this.dgvColScriptFileName.HeaderText = "FileName";
            this.dgvColScriptFileName.Name = "dgvColScriptFileName";
            this.dgvColScriptFileName.ToolTipText = "The full path to the executable program to run";
            this.dgvColScriptFileName.Width = 76;
            // 
            // dgvColScriptArguments
            // 
            this.dgvColScriptArguments.DataPropertyName = "Arguments";
            this.dgvColScriptArguments.HeaderText = "Arguments";
            this.dgvColScriptArguments.Name = "dgvColScriptArguments";
            this.dgvColScriptArguments.ToolTipText = "Arguments or additional switches or options for the executable.";
            this.dgvColScriptArguments.Width = 82;
            // 
            // dgvColScriptSourceFolder
            // 
            this.dgvColScriptSourceFolder.DataPropertyName = "SourceFolder";
            this.dgvColScriptSourceFolder.HeaderText = "SourceFolder";
            this.dgvColScriptSourceFolder.Name = "dgvColScriptSourceFolder";
            this.dgvColScriptSourceFolder.ToolTipText = "This will check the folder\'s free drive space before executing.";
            this.dgvColScriptSourceFolder.Width = 95;
            // 
            // dgvColScriptDestinationFolder
            // 
            this.dgvColScriptDestinationFolder.DataPropertyName = "DestinationFolder";
            this.dgvColScriptDestinationFolder.HeaderText = "DestinationFolder";
            this.dgvColScriptDestinationFolder.Name = "dgvColScriptDestinationFolder";
            this.dgvColScriptDestinationFolder.ToolTipText = "This will check the folder\'s free drive space before executing.";
            this.dgvColScriptDestinationFolder.Width = 114;
            // 
            // dgvColScriptTimeout
            // 
            this.dgvColScriptTimeout.DataPropertyName = "Timeout";
            this.dgvColScriptTimeout.HeaderText = "Timeout";
            this.dgvColScriptTimeout.Name = "dgvColScriptTimeout";
            this.dgvColScriptTimeout.ToolTipText = "Timeout for script in minutes";
            this.dgvColScriptTimeout.Width = 70;
            // 
            // BackupRetentionSystemTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 676);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BackupRetentionSystemTray";
            this.Text = "BackupRetentionSystemTray";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackupRetentionSystemTray_FormClosing);
            this.Load += new System.EventHandler(this.BackupRetentionSystemTray_Load);
            this.SizeChanged += new System.EventHandler(this.BackupRetentionSystemTray_SizeChanged);
            this.tabControl.ResumeLayout(false);
            this.tabRetention.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRetention)).EndInit();
            this.tabCompress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompress)).EndInit();
            this.tabSync.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSync)).EndInit();
            this.tabRemote.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRemote)).EndInit();
            this.tabEvents.ResumeLayout(false);
            this.tabEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvents)).EndInit();
            this.TabTasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbTime.ResumeLayout(false);
            this.gbTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabRetention;
        private System.Windows.Forms.TabPage tabCompress;
        private System.Windows.Forms.TabPage tabSync;
        private System.Windows.Forms.DataGridView dgvRetention;
        private System.Windows.Forms.DataGridView dgvCompress;
        private System.Windows.Forms.DataGridView dgvSync;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.TabPage tabRemote;
        private System.Windows.Forms.DataGridView dgvRemote;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.DataGridView dgvEvents;
        private System.Diagnostics.EventLog eventLogBackupRetention;
        private System.Windows.Forms.DataGridViewImageColumn dgvEventImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEventType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColEventEventID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtServiceStatus;
        private System.Windows.Forms.Label lblServiceIntervalMinutes;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Label lblTimeInfo;
        private System.Windows.Forms.Button btnSaveApply;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MaskedTextBox mtxtMaxDriveSpaceUsed;
        private System.Windows.Forms.Label lblMaxDriveSpaceUsed;
        private System.Windows.Forms.Label lblServiceInterval;
        private System.Windows.Forms.TextBox txtServiceInterval;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pbLeftIcon;
        private System.Windows.Forms.Button btnRefreshEventLog;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.CheckBox chkInformation;
        private System.Windows.Forms.CheckBox chkWarning;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        protected System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripMenuItem servicesConsoleToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncEnabled1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColSyncIntervalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColSyncSunday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncSourceFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncDestinationFolder;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColSyncFileSyncReplicaOption;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColSyncFileSyncReset;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColSyncFolderSyncDirectionOrder;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColSyncDefaultConflictResolutionPolicy;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColSyncArchiveDeleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColSyncArchiveFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRetentionIntervalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRetentionSunday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionBackupFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionMinFileCount;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRetentionDayOfWeekToKeep;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionDailyMaxDaysOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionWeeklyMaxDaysOld;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionMonthlyMaxDaysOld;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRetentionRetentionAlgorithm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRetentionFileNameFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColCompressIntervalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColCompressSunday;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColCompressCompress;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColCompressSourceOption;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressSourceFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressDestinationFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressEncryptionPassword;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColCompressKeepOriginalFile;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColCompressCompressionLvl;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressStartCompressingAfterDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCompressFileNameFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRemoteIntervalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColRemoteSunday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteHost;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRemoteProtocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemotePort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemotePassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteKeyFileDirectory;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRemoteKeyFileUsePassPhrase;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteRemoteDirectory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteBackupFolder;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRemoteTransferDirection;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRemoteAllowAnyCertificate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteTimeout;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColRemoteOverwrite;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColRemoteFileNameFilter;
        private System.Windows.Forms.TabPage TabTasks;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptEndTime;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvColScriptIntervalType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptInterval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptMonday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptTuesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptWednesday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptThursday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptFriday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptSaturday;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColScriptSunday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptWorkingDirector;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptArguments;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptSourceFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptDestinationFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColScriptTimeout;
    }
}


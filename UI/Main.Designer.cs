namespace MemCacheDManager.UI
{
	partial class Main
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.cmsServerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAddNewInstance = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRedeployUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
			this.generateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.enyimConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enyimConfigSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tvManager = new System.Windows.Forms.TreeView();
			this.scMain = new System.Windows.Forms.SplitContainer();
			this.pActionButtons = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnAddServer = new System.Windows.Forms.Button();
			this.pEdit = new System.Windows.Forms.Panel();
			this.cmsInstanceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiStopService = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiStartService = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRestartService = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteInstanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tcMain = new System.Windows.Forms.TabControl();
			this.tpConfigure = new System.Windows.Forms.TabPage();
			this.tpStatus = new System.Windows.Forms.TabPage();
			this.dgvStatus = new System.Windows.Forms.DataGridView();
			this.colStatusTooltip = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colStatusImage = new System.Windows.Forms.DataGridViewImageColumn();
			this.colServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colInstanceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUptime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colServerTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTotalItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colConnectionCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTotalConnections = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colConnectionStructures = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGetCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colSetCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGetHits = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colGetMisses = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colUsedBytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colBytesRead = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colBytesWritten = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMaxBytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pButtons = new System.Windows.Forms.Panel();
			this.tpOptions = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnApply = new System.Windows.Forms.Button();
			this.txtMemcachedVersion = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSelectMsvcrBinary = new System.Windows.Forms.Button();
			this.txtMsvcrBinaryPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSelectMemCacheDBinary = new System.Windows.Forms.Button();
			this.txtMemcachedBinaryPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pMain = new System.Windows.Forms.Panel();
			this.hpHelpProvider = new System.Windows.Forms.HelpProvider();
			this.tsmiNewConfiguration = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiLoadConfiguration = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsServerMenu.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.scMain.Panel1.SuspendLayout();
			this.scMain.Panel2.SuspendLayout();
			this.scMain.SuspendLayout();
			this.pActionButtons.SuspendLayout();
			this.cmsInstanceMenu.SuspendLayout();
			this.tcMain.SuspendLayout();
			this.tpConfigure.SuspendLayout();
			this.tpStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
			this.tpOptions.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.pMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsServerMenu
			// 
			this.cmsServerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddNewInstance,
            this.tsmiRedeployUpdate,
            this.refreshToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteServerToolStripMenuItem1});
			this.cmsServerMenu.Name = "cmsManagerMenu";
			this.cmsServerMenu.Size = new System.Drawing.Size(238, 98);
			// 
			// tsmiAddNewInstance
			// 
			this.tsmiAddNewInstance.Name = "tsmiAddNewInstance";
			this.tsmiAddNewInstance.Size = new System.Drawing.Size(237, 22);
			this.tsmiAddNewInstance.Text = "Add New Instance";
			this.tsmiAddNewInstance.Click += new System.EventHandler(this.tsmiAddNewInstance_Click);
			// 
			// tsmiRedeployUpdate
			// 
			this.tsmiRedeployUpdate.Name = "tsmiRedeployUpdate";
			this.tsmiRedeployUpdate.Size = new System.Drawing.Size(237, 22);
			this.tsmiRedeployUpdate.Text = "Redeploy / Update MemCacheD";
			this.tsmiRedeployUpdate.Click += new System.EventHandler(this.tsmiRedeployUpdate_Click);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(234, 6);
			// 
			// deleteServerToolStripMenuItem1
			// 
			this.deleteServerToolStripMenuItem1.Name = "deleteServerToolStripMenuItem1";
			this.deleteServerToolStripMenuItem1.Size = new System.Drawing.Size(237, 22);
			this.deleteServerToolStripMenuItem1.Text = "Delete Server";
			this.deleteServerToolStripMenuItem1.Click += new System.EventHandler(this.deleteServerToolStripMenuItem1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 451);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(792, 22);
			this.statusStrip1.TabIndex = 7;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsslStatus
			// 
			this.tsslStatus.Name = "tsslStatus";
			this.tsslStatus.Size = new System.Drawing.Size(42, 17);
			this.tsslStatus.Text = "Ready.";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.generateToolStripMenuItem1,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 22;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewConfiguration,
            this.tsmiLoadConfiguration,
            this.tsmiExit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(35, 20);
			this.tsmiFile.Text = "&File";
			// 
			// tsmiExit
			// 
			this.tsmiExit.Name = "tsmiExit";
			this.tsmiExit.Size = new System.Drawing.Size(176, 22);
			this.tsmiExit.Text = "E&xit";
			this.tsmiExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// generateToolStripMenuItem1
			// 
			this.generateToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enyimConfigurationToolStripMenuItem});
			this.generateToolStripMenuItem1.Name = "generateToolStripMenuItem1";
			this.generateToolStripMenuItem1.Size = new System.Drawing.Size(64, 20);
			this.generateToolStripMenuItem1.Text = "&Generate";
			// 
			// enyimConfigurationToolStripMenuItem
			// 
			this.enyimConfigurationToolStripMenuItem.Name = "enyimConfigurationToolStripMenuItem";
			this.enyimConfigurationToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.enyimConfigurationToolStripMenuItem.Text = "Enyim Configuration";
			this.enyimConfigurationToolStripMenuItem.Click += new System.EventHandler(this.enyimConfigSectionToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// showHelpToolStripMenuItem
			// 
			this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
			this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.showHelpToolStripMenuItem.Text = "&Show Help";
			this.showHelpToolStripMenuItem.Click += new System.EventHandler(this.showHelpToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// generateToolStripMenuItem
			// 
			this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
			this.generateToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.generateToolStripMenuItem.Text = "&Generate";
			// 
			// enyimConfigSectionToolStripMenuItem
			// 
			this.enyimConfigSectionToolStripMenuItem.Name = "enyimConfigSectionToolStripMenuItem";
			this.enyimConfigSectionToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.enyimConfigSectionToolStripMenuItem.Text = "&Enyim Config Section";
			this.enyimConfigSectionToolStripMenuItem.Click += new System.EventHandler(this.enyimConfigSectionToolStripMenuItem_Click);
			// 
			// tvManager
			// 
			this.tvManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvManager.FullRowSelect = true;
			this.tvManager.HideSelection = false;
			this.tvManager.Location = new System.Drawing.Point(0, 0);
			this.tvManager.MinimumSize = new System.Drawing.Size(180, 240);
			this.tvManager.Name = "tvManager";
			this.tvManager.Size = new System.Drawing.Size(180, 342);
			this.tvManager.TabIndex = 23;
			this.tvManager.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.tvManager_NodeMouseHover);
			this.tvManager.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvManager_AfterSelect);
			this.tvManager.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvManager_MouseMove);
			this.tvManager.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvManager_NodeMouseClick);
			this.tvManager.MouseLeave += new System.EventHandler(this.tvManager_MouseLeave);
			// 
			// scMain
			// 
			this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scMain.Location = new System.Drawing.Point(3, 3);
			this.scMain.Name = "scMain";
			// 
			// scMain.Panel1
			// 
			this.scMain.Panel1.Controls.Add(this.tvManager);
			this.scMain.Panel1.Controls.Add(this.pActionButtons);
			// 
			// scMain.Panel2
			// 
			this.scMain.Panel2.Controls.Add(this.pEdit);
			this.scMain.Size = new System.Drawing.Size(758, 375);
			this.scMain.SplitterDistance = 180;
			this.scMain.TabIndex = 24;
			// 
			// pActionButtons
			// 
			this.pActionButtons.Controls.Add(this.btnRefresh);
			this.pActionButtons.Controls.Add(this.btnAddServer);
			this.pActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pActionButtons.Location = new System.Drawing.Point(0, 342);
			this.pActionButtons.MinimumSize = new System.Drawing.Size(180, 0);
			this.pActionButtons.Name = "pActionButtons";
			this.pActionButtons.Size = new System.Drawing.Size(180, 33);
			this.pActionButtons.TabIndex = 24;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(85, 4);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 1;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			// 
			// btnAddServer
			// 
			this.btnAddServer.Location = new System.Drawing.Point(4, 4);
			this.btnAddServer.Name = "btnAddServer";
			this.btnAddServer.Size = new System.Drawing.Size(75, 23);
			this.btnAddServer.TabIndex = 0;
			this.btnAddServer.Text = "Add Server";
			this.btnAddServer.UseVisualStyleBackColor = true;
			this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
			// 
			// pEdit
			// 
			this.pEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pEdit.Location = new System.Drawing.Point(0, 0);
			this.pEdit.MinimumSize = new System.Drawing.Size(550, 360);
			this.pEdit.Name = "pEdit";
			this.pEdit.Size = new System.Drawing.Size(574, 375);
			this.pEdit.TabIndex = 0;
			// 
			// cmsInstanceMenu
			// 
			this.cmsInstanceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStopService,
            this.tsmiStartService,
            this.tsmiRestartService,
            this.toolStripSeparator2,
            this.deleteInstanceToolStripMenuItem});
			this.cmsInstanceMenu.Name = "cmsInstanceMenu";
			this.cmsInstanceMenu.Size = new System.Drawing.Size(162, 98);
			// 
			// tsmiStopService
			// 
			this.tsmiStopService.Name = "tsmiStopService";
			this.tsmiStopService.Size = new System.Drawing.Size(161, 22);
			this.tsmiStopService.Text = "Stop Service";
			this.tsmiStopService.Click += new System.EventHandler(this.tsmiStopService_Click);
			// 
			// tsmiStartService
			// 
			this.tsmiStartService.Name = "tsmiStartService";
			this.tsmiStartService.Size = new System.Drawing.Size(161, 22);
			this.tsmiStartService.Text = "Start Service";
			this.tsmiStartService.Click += new System.EventHandler(this.tsmiStartService_Click);
			// 
			// tsmiRestartService
			// 
			this.tsmiRestartService.Name = "tsmiRestartService";
			this.tsmiRestartService.Size = new System.Drawing.Size(161, 22);
			this.tsmiRestartService.Text = "Restart Service";
			this.tsmiRestartService.Click += new System.EventHandler(this.tsmiRestartService_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
			// 
			// deleteInstanceToolStripMenuItem
			// 
			this.deleteInstanceToolStripMenuItem.Name = "deleteInstanceToolStripMenuItem";
			this.deleteInstanceToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.deleteInstanceToolStripMenuItem.Text = "Delete Instance";
			this.deleteInstanceToolStripMenuItem.Click += new System.EventHandler(this.deleteInstanceToolStripMenuItem_Click);
			// 
			// tcMain
			// 
			this.tcMain.Controls.Add(this.tpConfigure);
			this.tcMain.Controls.Add(this.tpStatus);
			this.tcMain.Controls.Add(this.tpOptions);
			this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcMain.Location = new System.Drawing.Point(10, 10);
			this.tcMain.Margin = new System.Windows.Forms.Padding(0);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(772, 407);
			this.tcMain.TabIndex = 25;
			this.tcMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcMain_Selected);
			// 
			// tpConfigure
			// 
			this.tpConfigure.Controls.Add(this.scMain);
			this.tpConfigure.Location = new System.Drawing.Point(4, 22);
			this.tpConfigure.Name = "tpConfigure";
			this.tpConfigure.Padding = new System.Windows.Forms.Padding(3);
			this.tpConfigure.Size = new System.Drawing.Size(764, 381);
			this.tpConfigure.TabIndex = 0;
			this.tpConfigure.Text = "Configure";
			this.tpConfigure.UseVisualStyleBackColor = true;
			// 
			// tpStatus
			// 
			this.tpStatus.Controls.Add(this.dgvStatus);
			this.tpStatus.Controls.Add(this.pButtons);
			this.tpStatus.Location = new System.Drawing.Point(4, 22);
			this.tpStatus.Name = "tpStatus";
			this.tpStatus.Padding = new System.Windows.Forms.Padding(3);
			this.tpStatus.Size = new System.Drawing.Size(764, 374);
			this.tpStatus.TabIndex = 1;
			this.tpStatus.Text = "Status";
			this.tpStatus.UseVisualStyleBackColor = true;
			// 
			// dgvStatus
			// 
			this.dgvStatus.AllowUserToAddRows = false;
			this.dgvStatus.AllowUserToDeleteRows = false;
			this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatusTooltip,
            this.colStatusImage,
            this.colServerName,
            this.colInstanceName,
            this.colVersion,
            this.colUptime,
            this.colServerTime,
            this.colItemCount,
            this.colTotalItems,
            this.colConnectionCount,
            this.colTotalConnections,
            this.colConnectionStructures,
            this.colGetCount,
            this.colSetCount,
            this.colGetHits,
            this.colGetMisses,
            this.colUsedBytes,
            this.colBytesRead,
            this.colBytesWritten,
            this.colMaxBytes});
			this.dgvStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvStatus.Location = new System.Drawing.Point(3, 3);
			this.dgvStatus.Name = "dgvStatus";
			this.dgvStatus.ReadOnly = true;
			this.dgvStatus.Size = new System.Drawing.Size(758, 323);
			this.dgvStatus.TabIndex = 0;
			this.dgvStatus.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvStatus_MouseMove);
			// 
			// colStatusTooltip
			// 
			this.colStatusTooltip.DataPropertyName = "StatusTooltip";
			this.colStatusTooltip.HeaderText = "Status Tooltip";
			this.colStatusTooltip.Name = "colStatusTooltip";
			this.colStatusTooltip.ReadOnly = true;
			this.colStatusTooltip.Visible = false;
			// 
			// colStatusImage
			// 
			this.colStatusImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colStatusImage.DataPropertyName = "StatusIcon";
			this.colStatusImage.HeaderText = "";
			this.colStatusImage.MinimumWidth = 20;
			this.colStatusImage.Name = "colStatusImage";
			this.colStatusImage.ReadOnly = true;
			this.colStatusImage.Width = 20;
			// 
			// colServerName
			// 
			this.colServerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colServerName.DataPropertyName = "ServerName";
			this.colServerName.HeaderText = "Server Name";
			this.colServerName.Name = "colServerName";
			this.colServerName.ReadOnly = true;
			// 
			// colInstanceName
			// 
			this.colInstanceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.colInstanceName.DataPropertyName = "InstanceName";
			this.colInstanceName.HeaderText = "Instance";
			this.colInstanceName.Name = "colInstanceName";
			this.colInstanceName.ReadOnly = true;
			// 
			// colVersion
			// 
			this.colVersion.DataPropertyName = "Version";
			this.colVersion.HeaderText = "Version";
			this.colVersion.Name = "colVersion";
			this.colVersion.ReadOnly = true;
			// 
			// colUptime
			// 
			this.colUptime.DataPropertyName = "Uptime";
			this.colUptime.HeaderText = "Uptime";
			this.colUptime.Name = "colUptime";
			this.colUptime.ReadOnly = true;
			// 
			// colServerTime
			// 
			this.colServerTime.DataPropertyName = "ServerTime";
			this.colServerTime.HeaderText = "Server Time";
			this.colServerTime.Name = "colServerTime";
			this.colServerTime.ReadOnly = true;
			// 
			// colItemCount
			// 
			this.colItemCount.DataPropertyName = "ItemCount";
			this.colItemCount.HeaderText = "Item Count";
			this.colItemCount.Name = "colItemCount";
			this.colItemCount.ReadOnly = true;
			// 
			// colTotalItems
			// 
			this.colTotalItems.DataPropertyName = "TotalItems";
			this.colTotalItems.HeaderText = "Total Items";
			this.colTotalItems.Name = "colTotalItems";
			this.colTotalItems.ReadOnly = true;
			// 
			// colConnectionCount
			// 
			this.colConnectionCount.DataPropertyName = "ConnectionCount";
			this.colConnectionCount.HeaderText = "Connections";
			this.colConnectionCount.Name = "colConnectionCount";
			this.colConnectionCount.ReadOnly = true;
			// 
			// colTotalConnections
			// 
			this.colTotalConnections.DataPropertyName = "TotalConnections";
			this.colTotalConnections.HeaderText = "Total Connections";
			this.colTotalConnections.Name = "colTotalConnections";
			this.colTotalConnections.ReadOnly = true;
			// 
			// colConnectionStructures
			// 
			this.colConnectionStructures.DataPropertyName = "ConnectionStructures";
			this.colConnectionStructures.HeaderText = "Connection Structures";
			this.colConnectionStructures.Name = "colConnectionStructures";
			this.colConnectionStructures.ReadOnly = true;
			// 
			// colGetCount
			// 
			this.colGetCount.DataPropertyName = "GetCount";
			this.colGetCount.HeaderText = "Gets";
			this.colGetCount.Name = "colGetCount";
			this.colGetCount.ReadOnly = true;
			// 
			// colSetCount
			// 
			this.colSetCount.DataPropertyName = "SetCount";
			this.colSetCount.HeaderText = "Sets";
			this.colSetCount.Name = "colSetCount";
			this.colSetCount.ReadOnly = true;
			// 
			// colGetHits
			// 
			this.colGetHits.DataPropertyName = "GetHits";
			this.colGetHits.HeaderText = "Hits";
			this.colGetHits.Name = "colGetHits";
			this.colGetHits.ReadOnly = true;
			// 
			// colGetMisses
			// 
			this.colGetMisses.DataPropertyName = "GetMisses";
			this.colGetMisses.HeaderText = "Misses";
			this.colGetMisses.Name = "colGetMisses";
			this.colGetMisses.ReadOnly = true;
			// 
			// colUsedBytes
			// 
			this.colUsedBytes.DataPropertyName = "UsedBytes";
			this.colUsedBytes.HeaderText = "Used Bytes";
			this.colUsedBytes.Name = "colUsedBytes";
			this.colUsedBytes.ReadOnly = true;
			// 
			// colBytesRead
			// 
			this.colBytesRead.DataPropertyName = "BytesRead";
			this.colBytesRead.HeaderText = "Bytes Read";
			this.colBytesRead.Name = "colBytesRead";
			this.colBytesRead.ReadOnly = true;
			// 
			// colBytesWritten
			// 
			this.colBytesWritten.DataPropertyName = "BytesWritten";
			this.colBytesWritten.HeaderText = "Bytes Written";
			this.colBytesWritten.Name = "colBytesWritten";
			this.colBytesWritten.ReadOnly = true;
			// 
			// colMaxBytes
			// 
			this.colMaxBytes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colMaxBytes.DataPropertyName = "MaxBytes";
			this.colMaxBytes.HeaderText = "Max Bytes";
			this.colMaxBytes.Name = "colMaxBytes";
			this.colMaxBytes.ReadOnly = true;
			// 
			// pButtons
			// 
			this.pButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pButtons.Location = new System.Drawing.Point(3, 326);
			this.pButtons.Name = "pButtons";
			this.pButtons.Size = new System.Drawing.Size(758, 45);
			this.pButtons.TabIndex = 1;
			// 
			// tpOptions
			// 
			this.tpOptions.Controls.Add(this.panel1);
			this.tpOptions.Controls.Add(this.txtMemcachedVersion);
			this.tpOptions.Controls.Add(this.label3);
			this.tpOptions.Controls.Add(this.btnSelectMsvcrBinary);
			this.tpOptions.Controls.Add(this.txtMsvcrBinaryPath);
			this.tpOptions.Controls.Add(this.label2);
			this.tpOptions.Controls.Add(this.btnSelectMemCacheDBinary);
			this.tpOptions.Controls.Add(this.txtMemcachedBinaryPath);
			this.tpOptions.Controls.Add(this.label1);
			this.tpOptions.Location = new System.Drawing.Point(4, 22);
			this.tpOptions.Name = "tpOptions";
			this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tpOptions.Size = new System.Drawing.Size(764, 374);
			this.tpOptions.TabIndex = 2;
			this.tpOptions.Text = "Options";
			this.tpOptions.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(3, 287);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(758, 84);
			this.panel1.TabIndex = 8;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnApply);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(558, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(200, 84);
			this.panel2.TabIndex = 0;
			// 
			// btnApply
			// 
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(122, 58);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 0;
			this.btnApply.Text = "&Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Visible = false;
			// 
			// txtMemcachedVersion
			// 
			this.txtMemcachedVersion.Location = new System.Drawing.Point(496, 24);
			this.txtMemcachedVersion.Name = "txtMemcachedVersion";
			this.txtMemcachedVersion.ReadOnly = true;
			this.txtMemcachedVersion.Size = new System.Drawing.Size(100, 20);
			this.txtMemcachedVersion.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(493, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "MemCacheD Version";
			// 
			// btnSelectMsvcrBinary
			// 
			this.btnSelectMsvcrBinary.Location = new System.Drawing.Point(452, 77);
			this.btnSelectMsvcrBinary.Name = "btnSelectMsvcrBinary";
			this.btnSelectMsvcrBinary.Size = new System.Drawing.Size(24, 20);
			this.btnSelectMsvcrBinary.TabIndex = 5;
			this.btnSelectMsvcrBinary.Text = "...";
			this.btnSelectMsvcrBinary.UseVisualStyleBackColor = true;
			this.btnSelectMsvcrBinary.Click += new System.EventHandler(this.btnSelectMsvcrBinary_Click);
			// 
			// txtMsvcrBinaryPath
			// 
			this.txtMsvcrBinaryPath.Location = new System.Drawing.Point(7, 77);
			this.txtMsvcrBinaryPath.Name = "txtMsvcrBinaryPath";
			this.txtMsvcrBinaryPath.ReadOnly = true;
			this.txtMsvcrBinaryPath.Size = new System.Drawing.Size(439, 20);
			this.txtMsvcrBinaryPath.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(231, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "MSVCR71.DLL binary (copied to remote server)";
			// 
			// btnSelectMemCacheDBinary
			// 
			this.btnSelectMemCacheDBinary.Location = new System.Drawing.Point(452, 24);
			this.btnSelectMemCacheDBinary.Name = "btnSelectMemCacheDBinary";
			this.btnSelectMemCacheDBinary.Size = new System.Drawing.Size(24, 20);
			this.btnSelectMemCacheDBinary.TabIndex = 2;
			this.btnSelectMemCacheDBinary.Text = "...";
			this.btnSelectMemCacheDBinary.UseVisualStyleBackColor = true;
			this.btnSelectMemCacheDBinary.Click += new System.EventHandler(this.btnSelectMemCacheDBinary_Click);
			// 
			// txtMemcachedBinaryPath
			// 
			this.txtMemcachedBinaryPath.Location = new System.Drawing.Point(7, 24);
			this.txtMemcachedBinaryPath.Name = "txtMemcachedBinaryPath";
			this.txtMemcachedBinaryPath.ReadOnly = true;
			this.txtMemcachedBinaryPath.Size = new System.Drawing.Size(439, 20);
			this.txtMemcachedBinaryPath.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(225, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "MemCacheD binary (copied to remote servers)";
			// 
			// pMain
			// 
			this.pMain.Controls.Add(this.tcMain);
			this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pMain.Location = new System.Drawing.Point(0, 24);
			this.pMain.Name = "pMain";
			this.pMain.Padding = new System.Windows.Forms.Padding(10);
			this.pMain.Size = new System.Drawing.Size(792, 427);
			this.pMain.TabIndex = 26;
			// 
			// hpHelpProvider
			// 
			this.hpHelpProvider.HelpNamespace = "Help\\MemCacheDManager.chm";
			// 
			// tsmiNewConfiguration
			// 
			this.tsmiNewConfiguration.Name = "tsmiNewConfiguration";
			this.tsmiNewConfiguration.Size = new System.Drawing.Size(176, 22);
			this.tsmiNewConfiguration.Text = "&New Configuration";
			this.tsmiNewConfiguration.Click += new System.EventHandler(this.tsmiNewConfiguration_Click);
			// 
			// tsmiLoadConfiguration
			// 
			this.tsmiLoadConfiguration.Name = "tsmiLoadConfiguration";
			this.tsmiLoadConfiguration.Size = new System.Drawing.Size(176, 22);
			this.tsmiLoadConfiguration.Text = "&Load Configuration";
			this.tsmiLoadConfiguration.Click += new System.EventHandler(this.tsmiLoadConfiguration_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 473);
			this.Controls.Add(this.pMain);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "Main";
			this.hpHelpProvider.SetShowHelp(this, true);
			this.Text = "MemCacheD Manager";
			this.Load += new System.EventHandler(this.Main_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.cmsServerMenu.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.scMain.Panel1.ResumeLayout(false);
			this.scMain.Panel2.ResumeLayout(false);
			this.scMain.ResumeLayout(false);
			this.pActionButtons.ResumeLayout(false);
			this.cmsInstanceMenu.ResumeLayout(false);
			this.tcMain.ResumeLayout(false);
			this.tpConfigure.ResumeLayout(false);
			this.tpStatus.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
			this.tpOptions.ResumeLayout(false);
			this.tpOptions.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.pMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		//private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.TreeView tvManager;
		private System.Windows.Forms.ContextMenuStrip cmsServerMenu;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.SplitContainer scMain;
		private System.Windows.Forms.Panel pEdit;
		private System.Windows.Forms.Panel pActionButtons;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnAddServer;
		private System.Windows.Forms.ToolStripMenuItem deleteServerToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNewInstance;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ContextMenuStrip cmsInstanceMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteInstanceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem enyimConfigSectionToolStripMenuItem;
		private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tpConfigure;
		private System.Windows.Forms.TabPage tpStatus;
		private System.Windows.Forms.Panel pMain;
		private System.Windows.Forms.Panel pButtons;
		private System.Windows.Forms.DataGridView dgvStatus;
		private System.Windows.Forms.TabPage tpOptions;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSelectMemCacheDBinary;
		private System.Windows.Forms.TextBox txtMemcachedBinaryPath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSelectMsvcrBinary;
		private System.Windows.Forms.TextBox txtMsvcrBinaryPath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtMemcachedVersion;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
		private System.Windows.Forms.DataGridViewTextBoxColumn colStatusTooltip;
		private System.Windows.Forms.DataGridViewImageColumn colStatusImage;
		private System.Windows.Forms.DataGridViewTextBoxColumn colServerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colInstanceName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
		private System.Windows.Forms.DataGridViewTextBoxColumn colUptime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colServerTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colItemCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTotalItems;
		private System.Windows.Forms.DataGridViewTextBoxColumn colConnectionCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTotalConnections;
		private System.Windows.Forms.DataGridViewTextBoxColumn colConnectionStructures;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGetCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSetCount;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGetHits;
		private System.Windows.Forms.DataGridViewTextBoxColumn colGetMisses;
		private System.Windows.Forms.DataGridViewTextBoxColumn colUsedBytes;
		private System.Windows.Forms.DataGridViewTextBoxColumn colBytesRead;
		private System.Windows.Forms.DataGridViewTextBoxColumn colBytesWritten;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMaxBytes;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiExit;
		private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem enyimConfigurationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiRedeployUpdate;
		private System.Windows.Forms.ToolStripMenuItem tsmiStopService;
		private System.Windows.Forms.ToolStripMenuItem tsmiStartService;
		private System.Windows.Forms.ToolStripMenuItem tsmiRestartService;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.HelpProvider hpHelpProvider;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewConfiguration;
		private System.Windows.Forms.ToolStripMenuItem tsmiLoadConfiguration;
	}
}


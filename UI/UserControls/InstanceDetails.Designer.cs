namespace MemCacheDManager.UI.UserControls
{
	partial class InstanceDetails
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnApply = new System.Windows.Forms.Button();
			this.chkUseManagedInstance = new System.Windows.Forms.CheckBox();
			this.txtMaximumConnections = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtInstanceName = new System.Windows.Forms.TextBox();
			this.txtDefaultKeySize = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtChunkSizeGrowthFactor = new System.Windows.Forms.TextBox();
			this.chkMaximizeCoreFile = new System.Windows.Forms.CheckBox();
			this.txtMemoryLimit = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkUseUDP = new System.Windows.Forms.CheckBox();
			this.txtUdpPort = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTcpPort = new System.Windows.Forms.TextBox();
			this.gbSpecifyIPAddress = new System.Windows.Forms.GroupBox();
			this.chkUseSpecificIPAddress = new System.Windows.Forms.CheckBox();
			this.txtIpAddress = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.gbServiceAccount = new System.Windows.Forms.GroupBox();
			this.txtVerifyServicePassword = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtServicePassword = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtServiceUserName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.chkSpecifyServiceCredentials = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.epErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.groupBox1.SuspendLayout();
			this.gbSpecifyIPAddress.SuspendLayout();
			this.gbServiceAccount.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.epErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// btnApply
			// 
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(332, 323);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 16;
			this.btnApply.Text = "&Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// chkUseManagedInstance
			// 
			this.chkUseManagedInstance.AutoSize = true;
			this.chkUseManagedInstance.Location = new System.Drawing.Point(144, 200);
			this.chkUseManagedInstance.Name = "chkUseManagedInstance";
			this.chkUseManagedInstance.Size = new System.Drawing.Size(190, 17);
			this.chkUseManagedInstance.TabIndex = 10;
			this.chkUseManagedInstance.Text = "Use Managed Instances (Buckets)";
			this.chkUseManagedInstance.UseVisualStyleBackColor = true;
			this.chkUseManagedInstance.CheckedChanged += new System.EventHandler(this.chkUseManagedInstance_CheckedChanged);
			// 
			// txtMaximumConnections
			// 
			this.txtMaximumConnections.Location = new System.Drawing.Point(12, 200);
			this.txtMaximumConnections.Name = "txtMaximumConnections";
			this.txtMaximumConnections.Size = new System.Drawing.Size(100, 20);
			this.txtMaximumConnections.TabIndex = 9;
			this.txtMaximumConnections.Text = "1024";
			this.txtMaximumConnections.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtMaximumConnections.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 184);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(113, 13);
			this.label7.TabIndex = 40;
			this.label7.Text = "Maximum Connections";
			// 
			// txtInstanceName
			// 
			this.txtInstanceName.Location = new System.Drawing.Point(12, 24);
			this.txtInstanceName.Name = "txtInstanceName";
			this.txtInstanceName.Size = new System.Drawing.Size(331, 20);
			this.txtInstanceName.TabIndex = 0;
			this.txtInstanceName.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtInstanceName.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// txtDefaultKeySize
			// 
			this.txtDefaultKeySize.Location = new System.Drawing.Point(144, 148);
			this.txtDefaultKeySize.Name = "txtDefaultKeySize";
			this.txtDefaultKeySize.Size = new System.Drawing.Size(100, 20);
			this.txtDefaultKeySize.TabIndex = 7;
			this.txtDefaultKeySize.Text = "48";
			this.txtDefaultKeySize.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtDefaultKeySize.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(141, 132);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 13);
			this.label5.TabIndex = 37;
			this.label5.Text = "Default Key Size (Bytes)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(276, 132);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(131, 13);
			this.label4.TabIndex = 36;
			this.label4.Text = "Chunk Size Growth Factor";
			// 
			// txtChunkSizeGrowthFactor
			// 
			this.txtChunkSizeGrowthFactor.Location = new System.Drawing.Point(279, 148);
			this.txtChunkSizeGrowthFactor.Name = "txtChunkSizeGrowthFactor";
			this.txtChunkSizeGrowthFactor.Size = new System.Drawing.Size(100, 20);
			this.txtChunkSizeGrowthFactor.TabIndex = 8;
			this.txtChunkSizeGrowthFactor.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtChunkSizeGrowthFactor.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// chkMaximizeCoreFile
			// 
			this.chkMaximizeCoreFile.AutoSize = true;
			this.chkMaximizeCoreFile.Location = new System.Drawing.Point(348, 200);
			this.chkMaximizeCoreFile.Name = "chkMaximizeCoreFile";
			this.chkMaximizeCoreFile.Size = new System.Drawing.Size(137, 17);
			this.chkMaximizeCoreFile.TabIndex = 11;
			this.chkMaximizeCoreFile.Text = "Maximize Core File Limit";
			this.chkMaximizeCoreFile.UseVisualStyleBackColor = true;
			this.chkMaximizeCoreFile.CheckedChanged += new System.EventHandler(this.chkMaximizeCoreFile_CheckedChanged);
			// 
			// txtMemoryLimit
			// 
			this.txtMemoryLimit.Location = new System.Drawing.Point(12, 148);
			this.txtMemoryLimit.Name = "txtMemoryLimit";
			this.txtMemoryLimit.Size = new System.Drawing.Size(100, 20);
			this.txtMemoryLimit.TabIndex = 6;
			this.txtMemoryLimit.Text = "64";
			this.txtMemoryLimit.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtMemoryLimit.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 132);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(116, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "Maximum Memory (MB)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkUseUDP);
			this.groupBox1.Controls.Add(this.txtUdpPort);
			this.groupBox1.Location = new System.Drawing.Point(382, 59);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(125, 56);
			this.groupBox1.TabIndex = 31;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "                                                                                 " +
				"                ";
			// 
			// chkUseUDP
			// 
			this.chkUseUDP.AutoSize = true;
			this.chkUseUDP.CausesValidation = false;
			this.chkUseUDP.Location = new System.Drawing.Point(6, 0);
			this.chkUseUDP.Name = "chkUseUDP";
			this.chkUseUDP.Size = new System.Drawing.Size(93, 17);
			this.chkUseUDP.TabIndex = 4;
			this.chkUseUDP.Text = "Use UDP Port";
			this.chkUseUDP.UseVisualStyleBackColor = true;
			this.chkUseUDP.CheckedChanged += new System.EventHandler(this.chkUseUDP_CheckedChanged);
			// 
			// txtUdpPort
			// 
			this.txtUdpPort.Location = new System.Drawing.Point(7, 24);
			this.txtUdpPort.Name = "txtUdpPort";
			this.txtUdpPort.ReadOnly = true;
			this.txtUdpPort.Size = new System.Drawing.Size(92, 20);
			this.txtUdpPort.TabIndex = 5;
			this.txtUdpPort.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtUdpPort.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(240, 66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 30;
			this.label2.Text = "TCP Port";
			// 
			// txtTcpPort
			// 
			this.txtTcpPort.Location = new System.Drawing.Point(243, 82);
			this.txtTcpPort.Name = "txtTcpPort";
			this.txtTcpPort.Size = new System.Drawing.Size(100, 20);
			this.txtTcpPort.TabIndex = 3;
			this.txtTcpPort.Text = "11211";
			this.txtTcpPort.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtTcpPort.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// gbSpecifyIPAddress
			// 
			this.gbSpecifyIPAddress.Controls.Add(this.chkUseSpecificIPAddress);
			this.gbSpecifyIPAddress.Controls.Add(this.txtIpAddress);
			this.gbSpecifyIPAddress.Location = new System.Drawing.Point(12, 59);
			this.gbSpecifyIPAddress.Name = "gbSpecifyIPAddress";
			this.gbSpecifyIPAddress.Size = new System.Drawing.Size(222, 56);
			this.gbSpecifyIPAddress.TabIndex = 28;
			this.gbSpecifyIPAddress.TabStop = false;
			this.gbSpecifyIPAddress.Text = "                                            ";
			// 
			// chkUseSpecificIPAddress
			// 
			this.chkUseSpecificIPAddress.AutoSize = true;
			this.chkUseSpecificIPAddress.Location = new System.Drawing.Point(6, 0);
			this.chkUseSpecificIPAddress.Name = "chkUseSpecificIPAddress";
			this.chkUseSpecificIPAddress.Size = new System.Drawing.Size(140, 17);
			this.chkUseSpecificIPAddress.TabIndex = 1;
			this.chkUseSpecificIPAddress.Text = "Use Specific IP Address";
			this.chkUseSpecificIPAddress.UseVisualStyleBackColor = true;
			this.chkUseSpecificIPAddress.CheckedChanged += new System.EventHandler(this.chkUseSpecificIPAddress_CheckedChanged);
			// 
			// txtIpAddress
			// 
			this.txtIpAddress.Location = new System.Drawing.Point(7, 24);
			this.txtIpAddress.Name = "txtIpAddress";
			this.txtIpAddress.ReadOnly = true;
			this.txtIpAddress.Size = new System.Drawing.Size(187, 20);
			this.txtIpAddress.TabIndex = 2;
			this.txtIpAddress.TextChanged += new System.EventHandler(this.FormFieldChanged);
			this.txtIpAddress.Validating += new System.ComponentModel.CancelEventHandler(this.OnValidating);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 27;
			this.label1.Text = "Instance Name";
			// 
			// gbServiceAccount
			// 
			this.gbServiceAccount.Controls.Add(this.txtVerifyServicePassword);
			this.gbServiceAccount.Controls.Add(this.label9);
			this.gbServiceAccount.Controls.Add(this.txtServicePassword);
			this.gbServiceAccount.Controls.Add(this.label8);
			this.gbServiceAccount.Controls.Add(this.txtServiceUserName);
			this.gbServiceAccount.Controls.Add(this.label6);
			this.gbServiceAccount.Controls.Add(this.chkSpecifyServiceCredentials);
			this.gbServiceAccount.Location = new System.Drawing.Point(12, 242);
			this.gbServiceAccount.Name = "gbServiceAccount";
			this.gbServiceAccount.Size = new System.Drawing.Size(477, 76);
			this.gbServiceAccount.TabIndex = 44;
			this.gbServiceAccount.TabStop = false;
			this.gbServiceAccount.Text = "                                               ";
			this.gbServiceAccount.Visible = false;
			// 
			// txtVerifyServicePassword
			// 
			this.txtVerifyServicePassword.Location = new System.Drawing.Point(336, 41);
			this.txtVerifyServicePassword.Name = "txtVerifyServicePassword";
			this.txtVerifyServicePassword.PasswordChar = '*';
			this.txtVerifyServicePassword.ReadOnly = true;
			this.txtVerifyServicePassword.Size = new System.Drawing.Size(130, 20);
			this.txtVerifyServicePassword.TabIndex = 14;
			this.txtVerifyServicePassword.Visible = false;
			this.txtVerifyServicePassword.TextChanged += new System.EventHandler(this.FormFieldChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(333, 25);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(121, 13);
			this.label9.TabIndex = 50;
			this.label9.Text = "Verify Service Password";
			this.label9.Visible = false;
			// 
			// txtServicePassword
			// 
			this.txtServicePassword.Location = new System.Drawing.Point(172, 41);
			this.txtServicePassword.Name = "txtServicePassword";
			this.txtServicePassword.PasswordChar = '*';
			this.txtServicePassword.ReadOnly = true;
			this.txtServicePassword.Size = new System.Drawing.Size(130, 20);
			this.txtServicePassword.TabIndex = 14;
			this.txtServicePassword.Visible = false;
			this.txtServicePassword.TextChanged += new System.EventHandler(this.FormFieldChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(169, 25);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(92, 13);
			this.label8.TabIndex = 48;
			this.label8.Text = "Service Password";
			this.label8.Visible = false;
			// 
			// txtServiceUserName
			// 
			this.txtServiceUserName.Location = new System.Drawing.Point(10, 41);
			this.txtServiceUserName.Name = "txtServiceUserName";
			this.txtServiceUserName.ReadOnly = true;
			this.txtServiceUserName.Size = new System.Drawing.Size(130, 20);
			this.txtServiceUserName.TabIndex = 13;
			this.txtServiceUserName.Visible = false;
			this.txtServiceUserName.TextChanged += new System.EventHandler(this.FormFieldChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(99, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Service User Name";
			this.label6.Visible = false;
			// 
			// chkSpecifyServiceCredentials
			// 
			this.chkSpecifyServiceCredentials.AutoSize = true;
			this.chkSpecifyServiceCredentials.Location = new System.Drawing.Point(7, 0);
			this.chkSpecifyServiceCredentials.Name = "chkSpecifyServiceCredentials";
			this.chkSpecifyServiceCredentials.Size = new System.Drawing.Size(155, 17);
			this.chkSpecifyServiceCredentials.TabIndex = 12;
			this.chkSpecifyServiceCredentials.Text = "Specify Service Credentials";
			this.chkSpecifyServiceCredentials.UseVisualStyleBackColor = true;
			this.chkSpecifyServiceCredentials.Visible = false;
			this.chkSpecifyServiceCredentials.CheckedChanged += new System.EventHandler(this.chkSpecifyServiceCredentials_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.CausesValidation = false;
			this.btnCancel.Location = new System.Drawing.Point(413, 323);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 45;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// epErrorProvider
			// 
			this.epErrorProvider.ContainerControl = this;
			// 
			// InstanceDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.gbServiceAccount);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.chkUseManagedInstance);
			this.Controls.Add(this.txtMaximumConnections);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtInstanceName);
			this.Controls.Add(this.txtDefaultKeySize);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtChunkSizeGrowthFactor);
			this.Controls.Add(this.chkMaximizeCoreFile);
			this.Controls.Add(this.txtMemoryLimit);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtTcpPort);
			this.Controls.Add(this.gbSpecifyIPAddress);
			this.Controls.Add(this.label1);
			this.Name = "InstanceDetails";
			this.Size = new System.Drawing.Size(518, 360);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gbSpecifyIPAddress.ResumeLayout(false);
			this.gbSpecifyIPAddress.PerformLayout();
			this.gbServiceAccount.ResumeLayout(false);
			this.gbServiceAccount.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.epErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.CheckBox chkUseManagedInstance;
		private System.Windows.Forms.TextBox txtMaximumConnections;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtInstanceName;
		private System.Windows.Forms.TextBox txtDefaultKeySize;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtChunkSizeGrowthFactor;
		private System.Windows.Forms.CheckBox chkMaximizeCoreFile;
		private System.Windows.Forms.TextBox txtMemoryLimit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtUdpPort;
		private System.Windows.Forms.CheckBox chkUseUDP;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtTcpPort;
		private System.Windows.Forms.GroupBox gbSpecifyIPAddress;
		private System.Windows.Forms.TextBox txtIpAddress;
		private System.Windows.Forms.CheckBox chkUseSpecificIPAddress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbServiceAccount;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtServiceUserName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkSpecifyServiceCredentials;
		private System.Windows.Forms.TextBox txtServicePassword;
		private System.Windows.Forms.TextBox txtVerifyServicePassword;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ErrorProvider epErrorProvider;
	}
}

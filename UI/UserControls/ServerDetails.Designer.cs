namespace MemCacheDManager.UI.UserControls
{
	partial class ServerDetails
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gbUseImpersonation = new System.Windows.Forms.GroupBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.chkUseImpersonation = new System.Windows.Forms.CheckBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtBinaryPath = new System.Windows.Forms.TextBox();
			this.btnAddInstance = new System.Windows.Forms.Button();
			this.gbUseImpersonation.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Name";
			// 
			// txtServerName
			// 
			this.txtServerName.Location = new System.Drawing.Point(7, 21);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(297, 20);
			this.txtServerName.TabIndex = 1;
			this.txtServerName.TextChanged += new System.EventHandler(this.OnFieldValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "User Name";
			// 
			// gbUseImpersonation
			// 
			this.gbUseImpersonation.Controls.Add(this.txtPassword);
			this.gbUseImpersonation.Controls.Add(this.label3);
			this.gbUseImpersonation.Controls.Add(this.txtUserName);
			this.gbUseImpersonation.Controls.Add(this.chkUseImpersonation);
			this.gbUseImpersonation.Controls.Add(this.label2);
			this.gbUseImpersonation.Location = new System.Drawing.Point(7, 47);
			this.gbUseImpersonation.Name = "gbUseImpersonation";
			this.gbUseImpersonation.Size = new System.Drawing.Size(297, 134);
			this.gbUseImpersonation.TabIndex = 3;
			this.gbUseImpersonation.TabStop = false;
			this.gbUseImpersonation.Text = "                                       ";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(9, 90);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.ReadOnly = true;
			this.txtPassword.Size = new System.Drawing.Size(274, 20);
			this.txtPassword.TabIndex = 7;
			this.txtPassword.TextChanged += new System.EventHandler(this.OnFieldValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Password";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(9, 51);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.ReadOnly = true;
			this.txtUserName.Size = new System.Drawing.Size(277, 20);
			this.txtUserName.TabIndex = 5;
			this.txtUserName.TextChanged += new System.EventHandler(this.OnFieldValueChanged);
			// 
			// chkUseImpersonation
			// 
			this.chkUseImpersonation.AutoSize = true;
			this.chkUseImpersonation.Location = new System.Drawing.Point(6, 0);
			this.chkUseImpersonation.Name = "chkUseImpersonation";
			this.chkUseImpersonation.Size = new System.Drawing.Size(114, 17);
			this.chkUseImpersonation.TabIndex = 4;
			this.chkUseImpersonation.Text = "Use Impersonation";
			this.chkUseImpersonation.UseVisualStyleBackColor = true;
			this.chkUseImpersonation.CheckedChanged += new System.EventHandler(this.chkUseImpersonation_CheckedChanged);
			// 
			// btnApply
			// 
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(148, 231);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 4;
			this.btnApply.Text = "&Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(229, 231);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 188);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(205, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Default Installaton Path (on remote server)";
			// 
			// txtBinaryPath
			// 
			this.txtBinaryPath.Location = new System.Drawing.Point(10, 205);
			this.txtBinaryPath.Name = "txtBinaryPath";
			this.txtBinaryPath.Size = new System.Drawing.Size(294, 20);
			this.txtBinaryPath.TabIndex = 7;
			this.txtBinaryPath.Text = "C:\\Program Files\\MemCacheD\\memcached.exe";
			// 
			// btnAddInstance
			// 
			this.btnAddInstance.Location = new System.Drawing.Point(47, 231);
			this.btnAddInstance.Name = "btnAddInstance";
			this.btnAddInstance.Size = new System.Drawing.Size(95, 23);
			this.btnAddInstance.TabIndex = 8;
			this.btnAddInstance.Text = "Add &Instance";
			this.btnAddInstance.UseVisualStyleBackColor = true;
			this.btnAddInstance.Click += new System.EventHandler(this.btnAddInstance_Click);
			// 
			// ServerDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnAddInstance);
			this.Controls.Add(this.txtBinaryPath);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.gbUseImpersonation);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.label1);
			this.Name = "ServerDetails";
			this.Size = new System.Drawing.Size(469, 280);
			this.gbUseImpersonation.ResumeLayout(false);
			this.gbUseImpersonation.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox gbUseImpersonation;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.CheckBox chkUseImpersonation;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtBinaryPath;
		private System.Windows.Forms.Button btnAddInstance;
	}
}

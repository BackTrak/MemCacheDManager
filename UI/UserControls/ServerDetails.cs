using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MemCacheDManager.UI.UserControls
{
	public partial class ServerDetails : UserControlBase, ISupportEntityEditing
	{
		public delegate void SaveHandler(Business.Server server);
		public event SaveHandler Save;

		public delegate void AddInstanceHandler(Business.Server server);
		public event AddInstanceHandler AddInstance;

		private MemCacheDManager.Business.Server _server = null;
		public Business.EditableEntityBase EditableEntity
		{
			get { return _server; }
		}

		


		public ServerDetails()
		{
			InitializeComponent();
		}

		public void CreateNew(Business.EditableEntityBase parent)
		{
			_server = null;

			txtServerName.Text = "";
			txtUserName.Text = "";
			txtPassword.Text = "";
			chkUseImpersonation.Checked = false;
			txtBinaryPath.Text = @"C:\Program Files\MemCacheD\memcached.exe";

			btnApply.Enabled = false;
			btnAddInstance.Enabled = false;
		}

		public void SetFocusToDefaultControl()
		{
			if(txtServerName.Text.Length == 0)
				txtServerName.Focus();
		}

		public void Edit(Business.EditableEntityBase entityToEdit, Business.EditableEntityBase parentEntity)
		{
			Business.Server server = (Business.Server)entityToEdit;
				
			txtServerName.Text = server.ServerName;
			chkUseImpersonation.Checked = server.UseImpersonation;
			txtUserName.Text = server.UserName;

			if (server.BinaryPath != null && server.BinaryPath != String.Empty)
				txtBinaryPath.Text = server.BinaryPath;

			if (server.Password.Length > 0)
				txtPassword.Text = Encryption.Decrypt(server.Password);
			else
				txtPassword.Text = "";

			_server = server;

			btnApply.Enabled = false;

			if (_server == null)
			{
				btnCancel.Enabled = true;
				btnAddInstance.Enabled = false;
			}
			else
			{
				btnCancel.Enabled = false;
				btnAddInstance.Enabled = true;
			}

			
				
		}

		private void chkUseImpersonation_CheckedChanged(object sender, EventArgs e)
		{
			txtUserName.ReadOnly = chkUseImpersonation.Checked == false;
			txtPassword.ReadOnly = chkUseImpersonation.Checked == false;

			if (chkUseImpersonation.Checked == true && txtUserName.Text == String.Empty)
				txtUserName.Text = Configuration.Default.DefaultUsername;

			if (chkUseImpersonation.Checked == true && txtPassword.Text == String.Empty && Configuration.Default.DefaultPassword.Length > 0)
				txtPassword.Text = Encryption.Decrypt(Configuration.Default.DefaultPassword);

			OnFieldValueChanged(sender, e);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show("There was an error while saving the server infomation. Please check server connectivity and username or password.\n" + ex.Message);
			}
		}

		private void SaveChanges()
		{
			Cursor = Cursors.WaitCursor;

			try
			{
				SetStatusLabel("Checking server name.");

				if (IsUserAccountInfomationValid() == false)
					return;

				if (IsServerNameAcceptable() == false)
					return;

				if (DoesServerNameAlreadyExistInList() == true)
					return;

				string userName = null;
				string password = null;
				if (chkUseImpersonation.Checked == true)
				{
					userName = txtUserName.Text;
					password = txtPassword.Text;
				}

				SetStatusLabel("Checking connection and authorization.");

				// Test the credentials.
				try
				{
					WmiService.GetAllServices(txtServerName.Text, userName, password, "PathName LIKE '%memcached%'");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Couldn't connect to the server. Please check server connectivity and user name and password.\n" + ex.Message);
					return;
				}

				if (_server == null)
				{
					_server = new MemCacheDManager.Business.Server();

					ServerConfiguration.Servers.Add(_server);
				}

				_server.ServerName = txtServerName.Text;
				_server.UserName = txtUserName.Text;
				_server.Password = Encryption.Encrypt(txtPassword.Text);
				_server.UseImpersonation = chkUseImpersonation.Checked;
				_server.BinaryPath = txtBinaryPath.Text;

				SetStatusLabel("Checking for MemCacheD instances.");

				_server.ReadInstancesFromServer();

				SetStatusLabel("Saving.");

				ServerConfiguration.Save(Configuration.Default.LastConfigFile);

				btnApply.Enabled = false;
				btnCancel.Enabled = false;
				btnAddInstance.Enabled = true;

				SetStatusLabel("Updating UI.");

				if (Save != null)
					Save(this._server);
			}
			finally
			{
				Cursor = Cursors.Default;
				SetStatusLabel("Ready.");
			}
		}

		private bool DoesServerNameAlreadyExistInList()
		{
			if (ServerConfiguration.Servers != null)
			{
				foreach (Business.Server server in ServerConfiguration.Servers)
				{
					if (server.ServerName.Equals(txtServerName.Text, StringComparison.InvariantCultureIgnoreCase) && server != _server)
					{
						MessageBox.Show("The server name: " + txtServerName.Text + " is already in the servers list.");
						return true;
					}
				}
			}

			return false;
		}

		private bool IsServerNameAcceptable()
		{
			try
			{
				SetStatusLabel("Checking server name.");

				System.Net.Dns.GetHostEntry(txtServerName.Text);
			}
			catch (System.Net.Sockets.SocketException ex)
			{
				if (MessageBox.Show("There was an error resolving " + txtServerName.Text + ", do you want to save this server anyway?\n\n" + ex.Message, "Server Name Error", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
					return false;
			}
			finally
			{
				SetStatusLabel("Ready.");
			}

			return true;
		}

		private bool IsUserAccountInfomationValid()
		{
			ImpersonateUser impersonateUser = null;

			try
			{
				SetStatusLabel("Checking account credentials.");

				if (chkUseImpersonation.Checked == true)
				{
					try
					{
						impersonateUser = new ImpersonateUser(txtUserName.Text, txtPassword.Text);

						Configuration.Default.DefaultUsername = txtUserName.Text;
						Configuration.Default.DefaultPassword = Encryption.Encrypt(txtPassword.Text);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						return false;
					}
				}
			}
			finally
			{
				if (impersonateUser != null && impersonateUser.IsImpersonating == true)
					impersonateUser.Undo();

				SetStatusLabel("Ready.");
			}

			return true;
		}

		private void OnFieldValueChanged(object sender, EventArgs e)
		{
			bool areAllFieldsValid = true;

			if (txtServerName.Text == String.Empty)
				areAllFieldsValid = false;

			if (chkUseImpersonation.Checked == true)
			{
				if(txtUserName.Text == String.Empty)
					areAllFieldsValid = false;
			}

			if(txtBinaryPath.Text == String.Empty)
				areAllFieldsValid = false;

			btnApply.Enabled = areAllFieldsValid;

			btnCancel.Enabled = true;
		}

		public bool DoesControlHaveUnsavedChanges()
		{
			if (btnApply.Enabled == true)
			{
				switch(MessageBox.Show("You have unsaved changes, would you like to save now?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
				{
					case DialogResult.Yes:
						SaveChanges();
						return false;

					case DialogResult.No:
						return false;

					case DialogResult.Cancel:
						return true;
				}
			}

			return false;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you wish to cancel these changes?", "Unsaved Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				if (_server != null)
				{
					Edit(_server, null);
				}
				else
				{
					this.RaiseCancelEvent(sender, EventArgs.Empty);
				}
			}
		}

		private void btnAddInstance_Click(object sender, EventArgs e)
		{
			if (AddInstance != null)
				AddInstance(this._server);
		}

		
	}
}

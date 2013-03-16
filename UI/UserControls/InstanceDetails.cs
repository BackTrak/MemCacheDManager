using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;

namespace MemCacheDManager.UI.UserControls
{
	public partial class InstanceDetails : UserControlBase, ISupportEntityEditing
	{
		public event EventHandler Save;

		private bool _isLoadingUI = false;

		private Business.Server _server = null;

		private Business.Instance _instance = null;
		public MemCacheDManager.Business.EditableEntityBase EditableEntity
		{
			get { return _instance; }
		}

		

		public InstanceDetails()
		{
			InitializeComponent();

			// Setting this value here to accomodate international number styles.
			this.txtChunkSizeGrowthFactor.Text = string.Format("{0}", 1.25M);
		}

		private string GetNextMemcachedInstanceName()
		{
			List<WmiService> services;

			if (_server.UseImpersonation == true)
				services = WmiService.GetAllServices(_server.ServerName, _server.UserName, Encryption.Decrypt(_server.Password), "PathName LIKE '%memcached%'");
			else
				services = WmiService.GetAllServices(_server.ServerName, null, null, "PathName LIKE '%memcached%'");

			int highestInstanceNumber = 0;

			Regex instanceFinder = new Regex(
				  "\\w+_(?<instanceNumber>\\d+)",
				RegexOptions.Multiline
				| RegexOptions.Singleline
				| RegexOptions.CultureInvariant
				| RegexOptions.Compiled
				);

			foreach (WmiService service in services)
			{
				Match match = instanceFinder.Match(service.Name);
				if (match.Success == true)
				{
					int instanceNumber = 0;
					if (int.TryParse(match.Groups["instanceNumber"].Value, out instanceNumber) == true && instanceNumber > highestInstanceNumber)
					{
						highestInstanceNumber = instanceNumber;
					}
				}
			}

			int newInstanceNumber = highestInstanceNumber + 1;

			return "MemCacheD_" + newInstanceNumber;
		}

		#region ISupportEntityEditing Members

		public bool DoesControlHaveUnsavedChanges()
		{
			if (btnApply.Enabled == true)
			{
				switch (MessageBox.Show("You have unsaved changes, would you like to save now?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
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

		private void SaveChanges()
		{
			if (chkUseSpecificIPAddress.Checked == true && IsIpAddressValid() == false)
			{
				MessageBox.Show("IP address specified is not a legal IP address.");
				return;
			}

			//if (VerifyFieldValues() == false)
			//{
			//    MessageBox.Show("IP address specified is not a legal IP address.");
			//    return;
			//}

			if (AreIpAndTcpAndUdpPortUniqueForInstance() == false)
			{
				MessageBox.Show("You must specify a unique TCP/IP IP address and port combination.", "IP address or port duplication error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsInstanceNameUnique() == false)
			{
				MessageBox.Show("Instance name is not unique.");
				return;
			}

			Cursor = Cursors.WaitCursor;
			SetStatusLabel("Saving.");

			//ImpersonateUser impersonateUser = null;

			try
			{
				
				//if (_server.UseImpersonation == true)
				//{
				//    SetStatusLabel("Saving: Setting up impersonation.");
				//    impersonateUser = new ImpersonateUser(_server.UserName, Encryption.Decrypt(_server.Password));
				//}

				//
				//string networkPath = null;
				//try
				//{
				//    networkPath = "\\\\" + _server.ServerName + "\\c$";
				//    Directory.GetDirectories(networkPath);
				//}
				//catch (Exception ex)
				//{
				//    MessageBox.Show("Couldn't establish connection on network path: " + networkPath + "\n\n" + ex.Message);
				//    return;
				//}

				bool isNewInstance = false;

				try
				{
					if (_instance == null)
					{
						string nextInstanceServiceName = GetNextMemcachedInstanceName();

						_instance = new MemCacheDManager.Business.Instance();

						_instance.ServiceName = nextInstanceServiceName;
						_instance.ImageBasePath = _server.BinaryPath;

						_server.Instances.Add(_instance);

						isNewInstance = true;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Couldn't get a list of instances on this server. Please check server connectivity and user credentials and try again.\n" + ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				// TODO: Add rest of fields here.
				_instance.DisplayName = txtInstanceName.Text;

				decimal chunkSizeGrowthFactor = 0;
				string chunkSizeString = ConvertDecimalUserInputToLocalizedString(txtChunkSizeGrowthFactor.Text);
				if (Decimal.TryParse(chunkSizeString, out chunkSizeGrowthFactor) == true)
					_instance.ChunkSizeGrowthFactor = chunkSizeGrowthFactor;

				int defaultKeySize = 0;
				if (Int32.TryParse(txtDefaultKeySize.Text, out defaultKeySize) == true)
					_instance.DefaultKeySize = defaultKeySize;


				_instance.MaximizeCoreFile = chkMaximizeCoreFile.Checked;

				int maximumConnections = 0;
				if (Int32.TryParse(txtMaximumConnections.Text, out maximumConnections) == true)
					_instance.MaximumConnections = maximumConnections;

				int memoryLimit = 0;
				if (Int32.TryParse(txtMemoryLimit.Text, out memoryLimit) == true)
					_instance.MemoryLimit = memoryLimit;

				_instance.UseManagedInstance = chkUseManagedInstance.Checked;

				if (chkUseSpecificIPAddress.Checked == true)
					_instance.IpAddress = txtIpAddress.Text;
				else
					_instance.IpAddress = null;

				if (chkUseUDP.Checked == true)
				{
					int udpPort = 0;
					if (Int32.TryParse(txtUdpPort.Text, out udpPort) == true)
						_instance.UdpPort = udpPort;
				}
				else
				{
					_instance.UdpPort = 0;
				}

				int tcpPort;
				Int32.TryParse(txtTcpPort.Text, out tcpPort);
				_instance.TcpPort = tcpPort;

				_instance.UpdateInstanceOnServer(_server, this.SetStatusLabel);

				if (isNewInstance == true)
				{
					if (MessageBox.Show("Would you like to start the new instance now?", "Start Service?", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						string userName = null;
						string password = null;

						if (_server.UseImpersonation == true)
						{
							userName = _server.UserName;
							password = Encryption.Decrypt(_server.Password);
						}

						SetStatusLabel("Starting service.");
						Cursor.Current = Cursors.WaitCursor;
						try
						{
							WmiService.Start(_server.ServerName, userName, password, _instance.ServiceName);
						}
						finally
						{
							SetStatusLabel("Service started.");
							Cursor.Current = Cursors.Default;
						}
					}
				}

				btnApply.Enabled = false;

				SetStatusLabel("Updating cofiguration file.");

				ServerConfiguration.Save(Configuration.Default.LastConfigFile);

				SetStatusLabel("Refreshing UI.");

				if (Save != null)
					Save(this, EventArgs.Empty);
			}
			finally
			{
				//if (impersonateUser != null && impersonateUser.IsImpersonating == true)
				//    impersonateUser.Undo();

				Cursor = Cursors.Default;
				SetStatusLabel("Ready.");
			}
		}

		public void SetFocusToDefaultControl()
		{
			txtInstanceName.Focus();
		}
		
		public void Edit(MemCacheDManager.Business.EditableEntityBase entityToEdit, MemCacheDManager.Business.EditableEntityBase parentEntity)
		{
			_instance = (Business.Instance)entityToEdit;
			_server = (Business.Server)parentEntity;

			txtInstanceName.Text = _instance.DisplayName;
			txtChunkSizeGrowthFactor.Text = _instance.ChunkSizeGrowthFactor.ToString();
			txtDefaultKeySize.Text = _instance.DefaultKeySize.ToString();
			chkMaximizeCoreFile.Checked = _instance.MaximizeCoreFile;
			txtMaximumConnections.Text = _instance.MaximumConnections.ToString();
			txtMemoryLimit.Text = _instance.MemoryLimit.ToString();
			txtTcpPort.Text = _instance.TcpPort.ToString();

			if (_instance.IpAddress != null && _instance.IpAddress != String.Empty)
			{
				chkUseSpecificIPAddress.Checked = true;
				txtIpAddress.Text = _instance.IpAddress;
			}
			else
			{
				chkUseSpecificIPAddress.Checked = false;
				txtIpAddress.Text = "";
			}
			

			if (_instance.UdpPort > 0)
			{
				chkUseUDP.Checked = true;
				txtUdpPort.Text = _instance.UdpPort.ToString();
			}
			else
			{
				chkUseUDP.Checked = false;
				txtUdpPort.Text = String.Empty;
			}

			chkUseManagedInstance.Checked = _instance.UseManagedInstance;

			btnApply.Enabled = false;

			if(_instance != null)
				btnCancel.Enabled = false;
			else
				btnCancel.Enabled = true;
		}

		public void CreateNew(Business.EditableEntityBase parent)
		{
			_instance = null;
			_server = (Business.Server)parent;

			txtInstanceName.Text = "";

			int newTcpPort = 11211;
			foreach (Business.Instance instance in _server.Instances)
			{
				if (instance.TcpPort >= newTcpPort)
					newTcpPort = instance.TcpPort + 1;
			}

			txtTcpPort.Text = newTcpPort.ToString();

			btnApply.Enabled = false;
		}

		#endregion

		private bool IsInstanceNameUnique()
		{
			foreach (Business.Instance instance in _server.Instances)
			{
				if (instance == _instance)
					continue;

				if (instance.DisplayName.Trim().Equals(txtInstanceName.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) == true)
					return false;
			}

			return true;
		}

		private bool IsIpAddressValid()
		{
			Regex ipFinder = new Regex(
				  "(?<First>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.(?<Second>2[0-4]\\d" +
				  "|25[0-5]|[01]?\\d\\d?)\\.(?<Third>2[0-4]\\d|25[0-5]|[01]?\\d" +
				  "\\d?)\\.(?<Fourth>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)",
				RegexOptions.IgnoreCase
				| RegexOptions.CultureInvariant
				| RegexOptions.IgnorePatternWhitespace
				| RegexOptions.Compiled
				);

			return txtIpAddress.Text.Trim() == String.Empty || ipFinder.IsMatch(txtIpAddress.Text.Trim());
		}

		private bool AreIpAndTcpAndUdpPortUniqueForInstance()
		{
			int tcpPort;
			if(Int32.TryParse(txtTcpPort.Text, out tcpPort) == false)
				throw new Exception("Couldn't parse TCP Port.");

			int udpPort = -1;
			if (chkUseUDP.Checked == true && Int32.TryParse(txtUdpPort.Text, out udpPort) == false)
				udpPort = -1;

			foreach (Business.Instance instance in _server.Instances)
			{
				if (instance == _instance)
					continue;

				string instanceIpAddress = instance.IpAddress ?? String.Empty;

				if (instanceIpAddress.Trim().Equals(txtIpAddress.Text.Trim(), StringComparison.InvariantCultureIgnoreCase) == true
					&& (instance.TcpPort == tcpPort || (udpPort >= 0 && instance.UdpPort == udpPort)))
				{
					return false;
				}
			}

			return true;
		}

		
		private void chkUseSpecificIPAddress_CheckedChanged(object sender, EventArgs e)
		{
			FormFieldChanged(sender, e);

			txtIpAddress.ReadOnly = chkUseSpecificIPAddress.Checked == false;
			if (chkUseSpecificIPAddress.Checked == false)
			{
				txtIpAddress.Text = String.Empty;
			}
			else
			{
				txtIpAddress.Focus();
			}

			OnValidating(chkUseSpecificIPAddress, new CancelEventArgs());
		}

		private void chkUseUDP_CheckedChanged(object sender, EventArgs e)
		{
			FormFieldChanged(sender, e);

			txtUdpPort.ReadOnly = chkUseUDP.Checked == false;

			if (chkUseUDP.Checked == false)
			{
				txtUdpPort.Text = String.Empty;
			}
			else
			{
				txtUdpPort.Focus();
			}

			OnValidating(chkUseUDP, new CancelEventArgs());
		}

		private void chkUseManagedInstance_CheckedChanged(object sender, EventArgs e)
		{
			FormFieldChanged(sender, e);
		}

		private void chkMaximizeCoreFile_CheckedChanged(object sender, EventArgs e)
		{
			FormFieldChanged(sender, e);
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			SaveChanges();
		}

		private void FormFieldChanged(object sender, EventArgs e)
		{
			if (_isLoadingUI == false)
			{
				btnApply.Enabled = true;
				btnCancel.Enabled = true;
			}
			
		}

		private void chkSpecifyServiceCredentials_CheckedChanged(object sender, EventArgs e)
		{
			txtServicePassword.ReadOnly = chkSpecifyServiceCredentials.Checked == false;
			txtServiceUserName.ReadOnly = chkSpecifyServiceCredentials.Checked == false;
			txtVerifyServicePassword.ReadOnly = chkSpecifyServiceCredentials.Checked == false;

			FormFieldChanged(sender, e);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (btnApply.Enabled == true)
			{
				if (MessageBox.Show("Are you sure you want to cancel?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.No)
					return;
			}

			epErrorProvider.Clear();

			btnApply.Enabled = false;

			if (this._instance != null)
			{
				
				btnCancel.Enabled = false;

				this.Edit(this._instance, this._server);
			}
			else
			{
				this.RaiseCancelEvent(this, e);
			}
		}

		private void OnValidating(object sender, CancelEventArgs e)
		{
			int tcpPort;
			bool hadError = false;

			// Instance name validation.
			if (txtInstanceName.Text.Trim().Length == 0)
			{
				epErrorProvider.SetError(txtInstanceName, "Please specify an instance name.");
				hadError = true;
			}
			else if (IsInstanceNameUnique() == false)
			{
				epErrorProvider.SetError(txtInstanceName, "Please specify an unique instance name.");
				hadError = true;
			}
			else
			{
				epErrorProvider.SetError(txtInstanceName, "");
			}

			// Tcp Port validation.
			if (Int32.TryParse(txtTcpPort.Text, out tcpPort) == false)
			{
				epErrorProvider.SetError(txtTcpPort, "Please specify a numeric TCP Port value.");
				hadError = true;
			}
			else if (tcpPort < 1 || tcpPort > Int16.MaxValue)
			{
				epErrorProvider.SetError(txtTcpPort, "Please specify a numeric TCP Port value between 1 and " + Int16.MaxValue + ".");
				hadError = true;
			}
			else if (AreIpAndTcpAndUdpPortUniqueForInstance() == false)
			{
				epErrorProvider.SetError(txtTcpPort, "Please specify a unique IP Address or TCP Port for this instance.");

				if (chkUseSpecificIPAddress.Checked == true)
					epErrorProvider.SetError(txtIpAddress, "Please specify a unique IP Address or TCP Port for this instance.");

				hadError = true;
			}
			else
			{
				epErrorProvider.SetError(txtTcpPort, "");
			}

			// IP Address Validation.
			if (chkUseSpecificIPAddress.Checked == true && sender != chkUseSpecificIPAddress)
			{
				if (txtIpAddress.Text.Trim().Length == 0)
				{
					txtIpAddress.Focus();
					epErrorProvider.SetError(txtIpAddress, "Please specify an IP address.");
					
					hadError = true;
				}
				else if (IsIpAddressValid() == false)
				{
					txtIpAddress.Focus();
					epErrorProvider.SetError(txtIpAddress, "Please specify a valid IP address.");
					
					hadError = true;
				}
				else
				{
					epErrorProvider.SetError(txtIpAddress, "");
				}
			}
			else
			{
				epErrorProvider.SetError(txtIpAddress, "");
			}

			// UDP Port Verification
			if (chkUseUDP.Checked == true && sender != chkUseUDP)
			{
				int udpPort;
				if (Int32.TryParse(txtUdpPort.Text, out udpPort) == false)
				{
					epErrorProvider.SetError(txtUdpPort, "Please specify a UDP Port.");
					hadError = true;
				}
				else if (AreIpAndTcpAndUdpPortUniqueForInstance() == false)
				{
					epErrorProvider.SetError(txtUdpPort, "Please specify a unique IP Address and TCP/UDP port.");
					hadError = true;
				}
				else
				{
					epErrorProvider.SetError(txtUdpPort, "");
				}
			}
			else
			{
				epErrorProvider.SetError(txtUdpPort, "");
			}

			// Maximum Memory Verification
			int maxMemory;
			if (Int32.TryParse(txtMemoryLimit.Text, out maxMemory) == false)
			{
				epErrorProvider.SetError(txtMemoryLimit, "Please specify the maximum memory to use in megabytes (Default is 64).");
				hadError = true;
			}
			else if (maxMemory <= 0 || maxMemory > 2064)
			{
				epErrorProvider.SetError(txtMemoryLimit, "Please specify a maximum memory value between 1 and 2064 (Default is 64).");
				hadError = true;
			}
			else
			{
				epErrorProvider.SetError(txtMemoryLimit, "");
			}

			// Default Key Size
			int keySize;
			if (Int32.TryParse(txtDefaultKeySize.Text, out keySize) == false)
			{
				epErrorProvider.SetError(txtDefaultKeySize, "Please specify the key size to use in bytes (Default is 48).");
				hadError = true;
			}
			else if (keySize < 48 || keySize > 2064)
			{
				epErrorProvider.SetError(txtDefaultKeySize, "Please specify a key size value between 48 and 2064 (Default is 48).");
				hadError = true;
			}
			else
			{
				epErrorProvider.SetError(txtDefaultKeySize, "");
			}

			// Chunk size growth factor
			double chunkSize;
			string chunkSizeString = ConvertDecimalUserInputToLocalizedString(txtChunkSizeGrowthFactor.Text);

			if (Double.TryParse(chunkSizeString, out chunkSize) == false)
			{
				epErrorProvider.SetError(txtChunkSizeGrowthFactor, string.Format("Please specify a decimal value for the chunk size growth factor (Default is {0}).", 1.25));
				hadError = true;
			}
			else if (chunkSize < .01 || chunkSize > 100)
			{
				epErrorProvider.SetError(txtChunkSizeGrowthFactor, string.Format("Please specify a chunk size growth factor value between {0} and {1} (Default is {3}).", .01, 100, 1.25));
				hadError = true;
			}
			else
			{
				epErrorProvider.SetError(txtChunkSizeGrowthFactor, "");
			}


			// Max Connections
			int maxConnections;
			if (Int32.TryParse(txtMaximumConnections.Text, out maxConnections) == false)
			{
				epErrorProvider.SetError(txtMaximumConnections, "Please specify the maximum number of connections (Default is 1024).");
				hadError = true;
			}
			else if (maxConnections < 1 || maxConnections > 32768)
			{
				epErrorProvider.SetError(txtMaximumConnections, "Please specify the maximum number of connections between 1 and 32768 (Default is 1024).");
				hadError = true;
			}
			else
			{
				epErrorProvider.SetError(txtMaximumConnections, "");
			}

			e.Cancel = hadError;
		}

		private string ConvertDecimalUserInputToLocalizedString(string userInput)
		{
			System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();

			string chunkSizeString = userInput;
			if (chunkSizeString.Contains(".") == true && chunkSizeString.Contains(nfi.CurrencyDecimalSeparator) == false)
				chunkSizeString = chunkSizeString.Replace(".", nfi.CurrencyDecimalSeparator);

			return chunkSizeString;
		}

	}
}

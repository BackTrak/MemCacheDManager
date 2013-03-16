using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Globalization;

namespace MemCacheDManager.Business
{
	[Serializable]
	public class Instance : EditableEntityBase
	{
		public delegate void SetStatusLabelDelegate(string status);

		private string _displayName;
		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}

		private string _serviceName;
		public string ServiceName
		{
			get { return _serviceName; }
			set { _serviceName = value; }
		}

		private string _imageBasePath;
		public string ImageBasePath
		{
			get { return _imageBasePath; }
			set { _imageBasePath = value; }
		}

		private int _tcpPort = 11211;
		public int TcpPort
		{
			get { return _tcpPort; }
			set { _tcpPort = value; }
		}

		private int _udpPort = 0;
		public int UdpPort
		{
			get { return _udpPort; }
			set { _udpPort = value; }
		}

		private string _ipAddress = null;
		public string IpAddress
		{
			get { return _ipAddress; }
			set { _ipAddress = value; }
		}

		private bool _maximizeCoreFile = false;
		public bool MaximizeCoreFile
		{
			get { return _maximizeCoreFile; }
			set { _maximizeCoreFile = value; }
		}

		private int _memoryLimit = 64;
		public int MemoryLimit
		{
			get { return _memoryLimit; }
			set { _memoryLimit = value; }
		}

		private int _maximumConnections = 1024;
		public int MaximumConnections
		{
			get { return _maximumConnections; }
			set { _maximumConnections = value; }
		}

		private bool _useManagedInstance = false;
		public bool UseManagedInstance
		{
			get { return _useManagedInstance; }
			set { _useManagedInstance = value; }
		}

		private decimal _chunkSizeGrowthFactor = 1.25M;
		public decimal ChunkSizeGrowthFactor
		{
			get { return _chunkSizeGrowthFactor; }
			set { _chunkSizeGrowthFactor = value; }
		}

		private int _defaultKeySize = 48;
		public int DefaultKeySize
		{
			get { return _defaultKeySize; }
			set { _defaultKeySize = value; }
		}

		//private string _serviceUserName;
		//private string _servicePassword;

		internal void UpdateInstanceOnServer(Business.Server server, SetStatusLabelDelegate setStatusLabelDelegate)
		{
			//ImpersonateUser impersonateUser = null;

			try
			{
				//if (server.UseImpersonation == true)
				//{
				//    setStatusLabelDelegate("Impersonating user: " + server.UserName);

				//    impersonateUser = new ImpersonateUser(server.UserName, Encryption.Decrypt(server.Password));
				//}

				setStatusLabelDelegate("Checking MemCacheD.exe on remote server.");

				string binaryPath = server.EnsureExecutableIsAvailabeOnServer(false);

				string userName = null;
				string password = null;

				if (server.UseImpersonation == true)
				{
					userName = server.UserName;
					password = Encryption.Decrypt(server.Password);
				}

				setStatusLabelDelegate("Updating service on remote server.");

				WmiService.CreateOrUpdate(server.ServerName, userName, password, this.ServiceName, this.DisplayName, this.CreateImagePath(), StartMode.Auto, null, null);

			}
			finally
			{
				//if (impersonateUser != null && impersonateUser.IsImpersonating == true)
				//	impersonateUser.Undo();
			}

			/*
			string description = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + _serviceController.ServiceName).GetValue("Description").ToString();
			string imagePath = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + _serviceController.ServiceName).GetValue("ImagePath").ToString();
			string newImagePath = imagePath.Substring(0, imagePath.LastIndexOf("memcached.exe") + ((string)("memcached.exe")).Length);

			int tcpPort;
			if (int.TryParse(txtTcpPort.Text, out tcpPort) == true)
				newImagePath += " -p " + tcpPort;

			int udpPort;
			if (int.TryParse(txtUdpPort.Text, out udpPort) == true)
				newImagePath += " -U " + udpPort;

			if (chkMaximizeCoreFile.Checked == true)
				newImagePath += " -r";

			int maxMemory;
			if (int.TryParse(txtMemoryLimit.Text, out maxMemory) == true)
				newImagePath += " -m " + maxMemory;

			Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + _serviceController.ServiceName, true).SetValue("ImagePath", newImagePath);

			Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + _serviceController.ServiceName, true).SetValue("DisplayName", txtInstanceName.Text);

			bool requiresRestart = false;
			if (_serviceController.Status == ServiceControllerStatus.Running)
			{
				requiresRestart = true;
				lblStatus.Text = "Stopping " + txtInstanceName.Text;
				Refresh();

				_serviceController.Stop();
			}

			lblStatus.Text = "Removing " + _serviceController.DisplayName;
			Refresh();
			ServiceUtility.UninstallService(_serviceController.ServiceName);

			lblStatus.Text = "Reinstalling " + txtInstanceName.Text;
			Refresh();
			ServiceUtility.InstallService(_serviceController.ServiceName, txtInstanceName.Text, description, newImagePath);

			if (requiresRestart == true)
			{
				lblStatus.Text = "Starting " + txtInstanceName.Text;
				Refresh();
				_serviceController.Start();
			}

			lblStatus.Text = "Ready.";
			 
			 */
		}

		// TODO: Handle what to do for a version upgrade.
		


		internal void ParseImagePath(string imagePath)
		{
			Regex pathFinder = new Regex(
				  "((^\\\"(?<path>.*?)\\\")|(^(?<path>.*?)\\s+))",
				RegexOptions.Multiline
				| RegexOptions.Singleline
				| RegexOptions.CultureInvariant
				| RegexOptions.Compiled
				);

			Match pathMatch = pathFinder.Match(imagePath);
			if (pathMatch.Success == false)
				throw new Exception("Could not get file name from imagePath: " + imagePath);

			//string memcachedFilename = Path.GetFileName(pathMatch.Groups["path"].Value);

			//this.ImageBasePath = imagePath.Substring(0, imagePath.LastIndexOf("memcached.exe") + ((string)("memcached.exe")).Length);
			//this.ImageBasePath = imagePath.Substring(0, imagePath.LastIndexOf(memcachedFilename) + ((string)(memcachedFilename)).Length);
			this.ImageBasePath = pathMatch.Groups["path"].Value;


			Regex parameterFinder = new Regex(
					"\\\"?(?<parameterMarker>-\\w+)((\\s\"(?<parameterValue>.*?)\")" +
					"|(\\s(?<parameterValue>[^-].*?)))?(\\s|$|\\\")",
				RegexOptions.Multiline
				| RegexOptions.Singleline
				| RegexOptions.CultureInvariant
				| RegexOptions.Compiled
				);

			foreach (Match match in parameterFinder.Matches(imagePath))
			{
				string parameterMarker = match.Groups["parameterMarker"].Value;
				string parameterValue = match.Groups["parameterValue"].Value;

				switch (parameterMarker)
				{
					case "-p":
						TcpPort = Convert.ToInt32(parameterValue);
						break;

					case "-U":
						UdpPort = Convert.ToInt32(parameterValue);
						break;

					case "-l":
						IpAddress = parameterValue;
						break;

					case "-r":
						MaximizeCoreFile = true;
						break;

					case "-m":
						MemoryLimit = Convert.ToInt32(parameterValue);
						break;

					case "-c":
						MaximumConnections = Convert.ToInt32(parameterValue);
						break;

					case "-b":
						UseManagedInstance = true;
						break;

					case "-f":
						CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
						ChunkSizeGrowthFactor = Convert.ToDecimal(parameterValue, cultureInfo.NumberFormat);
						break;

					case "-n":
						DefaultKeySize = Convert.ToInt32(parameterValue);
						break;
				}
			}
		}

		public string CreateImagePath()
		{
			StringBuilder imagePath = new StringBuilder("\"" + this.ImageBasePath.Replace("\"", "") + "\"");

			imagePath.Append(" -d RunService");

			if (TcpPort > 0)
				imagePath.AppendFormat(" -p {0}", TcpPort);

			if(UdpPort > 0)
				imagePath.AppendFormat(" -U {0}", UdpPort);

			if(MaximizeCoreFile == true)
				imagePath.Append(" -r");

			if(MemoryLimit > 0)
				imagePath.AppendFormat(" -m {0}", MemoryLimit);

			if(IpAddress != null && IpAddress != String.Empty)
				imagePath.AppendFormat(" -l {0}", IpAddress);

			if(MaximumConnections > 0)
				imagePath.AppendFormat(" -c {0}", MaximumConnections);

			if(UseManagedInstance == true)
				imagePath.Append(" -b");

			if (ChunkSizeGrowthFactor > 0)
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
				imagePath.Append(String.Format(cultureInfo.NumberFormat, " -f {0}", ChunkSizeGrowthFactor));
				//imagePath.AppendFormat(" -f {0}", ChunkSizeGrowthFactor);
			}


			if(DefaultKeySize > 0)
				imagePath.AppendFormat(" -n {0}", DefaultKeySize);

			return imagePath.ToString();
		}

		internal void RemoveFromServer(Business.Server server, SetStatusLabelDelegate setStatusLabelDelegate)
		{
			string userName = null;
			string password = null;

			if (server.UseImpersonation == true)
			{
				userName = server.UserName;
				password = Encryption.Decrypt(server.Password);
			}

			List<WmiService> services = WmiService.GetAllServices(server.ServerName, userName, password, "Name = '" + this.ServiceName + "'");

			// Saftey in case we get too many services back, we don't want to delete them all...
			if(services.Count > 20)
			{
				StringBuilder servicesToRemove = new StringBuilder("This action will remove the following services, are you sure?\n");
				foreach (WmiService service in services)
					servicesToRemove.AppendFormat("\n{0}", service.DisplayName);

				if(MessageBox.Show(servicesToRemove.ToString(), "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
					return;
			}


			foreach (WmiService service in services)
			{
				setStatusLabelDelegate("Removing " + service.DisplayName);

				ReturnValue returnValue;

				if (service.State != State.Stopped)
				{
					if ((returnValue = WmiService.Stop(server.ServerName, userName, password, service.Name)) != ReturnValue.Success)
						throw new Exception("Couldn't stop service: " + service.DisplayName + ", the result was: " + returnValue);
				}

				if ((returnValue = WmiService.Delete(server.ServerName, userName, password, service.Name)) != ReturnValue.Success)
					throw new Exception("Couldn't remove service: " + service.DisplayName + ", the result was: " + returnValue);
				
			}

			setStatusLabelDelegate("Ready.");

		}

		internal void Stop(Business.Server server, SetStatusLabelDelegate setStatusLabelDelegate)
		{
			string userName = null;
			string password = null;

			if (server.UseImpersonation == true)
			{
				userName = server.UserName;
				password = Encryption.Decrypt(server.Password);
			}

			List<WmiService> services = WmiService.GetAllServices(server.ServerName, userName, password, "Name = '" + this.ServiceName + "'");

			foreach (WmiService service in services)
			{
				setStatusLabelDelegate("Stopping " + service.DisplayName);

				ReturnValue returnValue;

				if (service.State != State.Stopped)
				{
					if ((returnValue = WmiService.Stop(server.ServerName, userName, password, service.Name)) != ReturnValue.Success)
						throw new Exception("Couldn't stop service: " + service.DisplayName + ", the result was: " + returnValue);
				}
			}

			setStatusLabelDelegate("Ready.");

		}

		internal void Start(Business.Server server, SetStatusLabelDelegate setStatusLabelDelegate)
		{
			string userName = null;
			string password = null;

			if (server.UseImpersonation == true)
			{
				userName = server.UserName;
				password = Encryption.Decrypt(server.Password);
			}

			List<WmiService> services = WmiService.GetAllServices(server.ServerName, userName, password, "Name = '" + this.ServiceName + "'");

			foreach (WmiService service in services)
			{
				setStatusLabelDelegate("Starting " + service.DisplayName);

				ReturnValue returnValue;

				if (service.State != State.Running && service.State != State.StartPending)
				{
					if ((returnValue = WmiService.Start(server.ServerName, userName, password, service.Name)) != ReturnValue.Success)
						throw new Exception("Couldn't start service: " + service.DisplayName + ", the result was: " + returnValue);
				}
			}

			setStatusLabelDelegate("Ready.");

		}

		internal State GetServiceState(Server server)
		{
			string userName = null;
			string password = null;

			if (server.UseImpersonation == true)
			{
				userName = server.UserName;
				password = Encryption.Decrypt(server.Password);
			}

			List<WmiService> services = WmiService.GetAllServices(server.ServerName, userName, password, "Name = '" + this.ServiceName + "'");

			if (services.Count > 0)
			{
				return services[0].State;
			}

			return State.Unknown;
		}
	}
}

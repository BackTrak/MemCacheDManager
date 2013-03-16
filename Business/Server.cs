using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Xml.Serialization;

namespace MemCacheDManager.Business
{
	[Serializable]
	public class Server : EditableEntityBase
	{
		private string _serverName;
		public string ServerName
		{
			get { return _serverName; }
			set { _serverName = value; }
		}

		private bool _useImpersonation;
		public bool UseImpersonation
		{
			get { return _useImpersonation; }
			set { _useImpersonation = value; }
		}

		private string _userName;
		public string UserName
		{
			get { return _userName; }
			set { _userName = value; }
		}

		private string _password;
		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		private string _binaryPath;
		public string BinaryPath
		{
			get { return _binaryPath; }
			set { _binaryPath = value; }
		}


		private List<Instance> _instances = new List<Instance>();
		public List<Instance> Instances
		{
			get { return _instances; }
			set { _instances = value; }
		}

		public void ReadInstancesFromServer()
		{
			List<WmiService> services;
			if(this.UseImpersonation == true)
				services = WmiService.GetAllServices(this.ServerName, this.UserName, Encryption.Decrypt(this.Password), "PathName LIKE '%memcached%'");
			else
				services = WmiService.GetAllServices(this.ServerName, null, null, "PathName LIKE '%memcached%'");

			Instances.Clear();
			foreach (WmiService service in services)
			{
				Instance instance = new Instance();
				instance.DisplayName = service.DisplayName;
				instance.ServiceName = service.Name;
				instance.ParseImagePath(service.PathName);

				Instances.Add(instance);
			}

			//ImpersonateUser impersonateUser = null;

			//try
			//{
			//    if (this.UseImpersonation == true)
			//    {
			//        impersonateUser = new ImpersonateUser(UserName, Encryption.Decrypt(Password));
			//    }

			//    RegistryKey serviceBranchKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, this.ServerName).OpenSubKey(@"SYSTEM\CurrentControlSet\Services");


			//    Instances.Clear();

			//    foreach (string serviceName in serviceBranchKey.GetSubKeyNames())
			//    {
			//        RegistryKey serviceKey = serviceBranchKey.OpenSubKey(serviceName);
			//        string imagePath;
			//        if ((imagePath = (string)serviceKey.GetValue("ImagePath", String.Empty)) != String.Empty)
			//        {
			//            if (imagePath.ToLower().Contains("memcached.exe") == true)
			//            {
			//                Instance instance = new Instance();
			//                instance.DisplayName = (string)serviceKey.GetValue("DisplayName", String.Empty);

			//                Instances.Add(instance);
			//            }
			//        }
			//    }
			//}
			//finally
			//{
			//    if(impersonateUser != null && impersonateUser.IsImpersonating == true)
			//        impersonateUser.Undo();
			//}
		}

		public string GetNetworkPathToMemcachedBinary(string binaryPath)
		{
			string networkBinaryPath = null;

			if (binaryPath.StartsWith("\\") == false && Path.IsPathRooted(binaryPath))
			{
				string rootDrive = Path.GetPathRoot(binaryPath);
				string uncShare = rootDrive.Replace(":", "$");
				string rootPath = binaryPath.Substring(rootDrive.Length);
				networkBinaryPath = Path.Combine(Path.Combine(@"\\" + this.ServerName, uncShare), rootPath);
			}

			return networkBinaryPath;
		}

		public string EnsureExecutableIsAvailabeOnServer(bool forceUpdate)
		{
			return EnsureExecutableIsAvailabeOnServer(this.BinaryPath, forceUpdate);
		}

		public string EnsureExecutableIsAvailabeOnServer(string binaryPath, bool forceUpdate)
		{
			string applicationPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

			string networkBinaryPath;

			if (NetworkHelper.IsMachineNameLocal(this.ServerName) == false)
				networkBinaryPath = GetNetworkPathToMemcachedBinary(binaryPath);
			else
				networkBinaryPath = binaryPath;

			string mappedDriveLetter = null;

			// Map to the root drive to initialize the impersonalization.
			if (networkBinaryPath.Contains("\\") == true && IsNetworkRootAccessable(Path.GetPathRoot(networkBinaryPath)) == false)
				mappedDriveLetter = MapNetworkDrive(Path.GetPathRoot(networkBinaryPath), this.UserName, Encryption.Decrypt(this.Password));

			try
			{
				if (File.Exists(networkBinaryPath) == false || forceUpdate == true)
				{

					string sourceBinPath = Path.Combine(applicationPath, "BinaryFiles\\memcached.exe");

					if (Directory.Exists(Path.GetDirectoryName(networkBinaryPath)) == false)
						Directory.CreateDirectory(Path.GetDirectoryName(networkBinaryPath));

					// Sometimes, the service manager reports the service is stopped, but a handle might still be open on it.
					Exception lastException = null;
					for (int i = 0; i < 10; i++)
					{
						try
						{
							File.Copy(sourceBinPath, networkBinaryPath, true);
							lastException = null;
							break;
						}
						catch (System.IO.IOException ex)
						{
							lastException = ex;
							Thread.Sleep(1000);
						}
					}

					if (lastException != null)
						throw new Exception("Couldn't copy file after 10 tries.", lastException);
				}

				string vcrBinary = Path.Combine(Path.GetDirectoryName(networkBinaryPath), "msvcr71.dll");
				string vcrSource = Path.Combine(applicationPath, "BinaryFiles\\msvcr71.dll");

				if (File.Exists(vcrBinary) == false || forceUpdate == true)
					File.Copy(vcrSource, vcrBinary, true);
			}
			finally
			{
				if (mappedDriveLetter != null)
					UnmapNetworkDrive(mappedDriveLetter);
			}

			return networkBinaryPath;
		}

		private bool IsNetworkRootAccessable(string networkPath)
		{
			try
			{
				Directory.GetFiles(networkPath);
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		private string MapNetworkDrive(string networkPath, string userName, string password)
		{
			string nextDriveLetter = FindNextAvailableDriveLetter();

			NetworkDrive networkDrive = new NetworkDrive();
			networkDrive.LocalDrive = nextDriveLetter + ":";
			networkDrive.ShareName = networkPath;
			networkDrive.SaveCredentials = false;
			networkDrive.Persistent = false;
			networkDrive.MapDrive(userName, password);


			//ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", string.Format("/C NET USE {0}: \"{1}\" {2} /USER:{3}",
			//    nextDriveLetter,
			//    networkPath,
			//    password,
			//    userName));

			//processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			//processStartInfo.CreateNoWindow = true;
			//processStartInfo.UseShellExecute = true;

			//Process mapDriveProcess = Process.Start(processStartInfo);
			//mapDriveProcess.WaitForExit();

			return nextDriveLetter;
		}

		private void UnmapNetworkDrive(string mappedDriveLetter)
		{
			NetworkDrive networkDrive = new NetworkDrive();
			networkDrive.LocalDrive = mappedDriveLetter + ":";
			networkDrive.Force = true;
			networkDrive.UnMapDrive();

			//ProcessStartInfo processStartInfo = new ProcessStartInfo("CMD.EXE", string.Format("/C NET USE /D {0}:",
			//    mappedDriveLetter));

			//processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			//processStartInfo.CreateNoWindow = true;
			//processStartInfo.UseShellExecute = true;

			//Process unmapDriveProcess = Process.Start(processStartInfo);
			//unmapDriveProcess.WaitForExit();
		}

		private string FindNextAvailableDriveLetter()
		{
			// build a string collection representing the alphabet
			List<string> alphabet = new List<string>();

			int lowerBound = Convert.ToInt16('a');
			int upperBound = Convert.ToInt16('z');
			for (int i = lowerBound; i < upperBound; i++)
			{
				char driveLetter = (char)i;
				alphabet.Add(driveLetter.ToString());
			}

			// get all current drives
			DriveInfo[] drives = DriveInfo.GetDrives();
			foreach (DriveInfo drive in drives)
			{
				alphabet.Remove(drive.Name.Substring(0, 1).ToLower());
			}

			if (alphabet.Count > 0)
			{
				return alphabet[0];
			}
			else
			{
				throw (new ApplicationException("No drives available."));
			}
		}

	}
}

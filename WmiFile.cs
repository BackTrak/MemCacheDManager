using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Diagnostics;

namespace MemCacheDManager
{
	public class WmiFile
	{
		public static bool DoesFileExist(string machineName, string userName, string password, string fileName)
		{
			ManagementScope scope = WmiHelper.Connect(machineName, userName, password);

			SelectQuery selectQuery = new SelectQuery(string.Format(@"SELECT * FROM CIM_DataFile WHERE Name = '{0}'", fileName.Replace("\\", "\\\\")));

			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, selectQuery);

			if (searcher.Get().Count > 0)
				return true;
			else
				return false;
		}

		public static bool DoesDirectoryExist(string machineName, string userName, string password, string directory)
		{
			ManagementScope scope = WmiHelper.Connect(machineName, userName, password);

			SelectQuery selectQuery = new SelectQuery(string.Format(@"SELECT * FROM Win32_Directory WHERE Name = '{0}'", directory.Replace("\\", "\\\\")));

			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, selectQuery);

			if (searcher.Get().Count > 0)
				return true;
			else
				return false;
		}

		public static void CreateDirectory(string machineName, string userName, string password, string directory)
		{
			ManagementScope scope = WmiHelper.Connect(machineName, userName, password);
			string workingFolder = null;
			string startupConfig = null;

			IntPtr processID = IntPtr.Zero;

			WmiHelper.InvokeStaticMethod(machineName, userName, password, "Win32_Process", "Create", new object[] { "cmd.exe /c mkdir \"" + directory + "\"", workingFolder, startupConfig, processID });
		}

		public static void CopyFileToRemoteMachine(string machineName, string userName, string password, string localFileName, string remoteFileName)
		{
			//WmiHelper.InvokeInstanceMethod(machine, "CIM_DataFile", "
		}
	}
}

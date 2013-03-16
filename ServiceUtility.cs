using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MemCacheDManager
{
	public static class ServiceUtility
	{
		public static void UninstallService(string serviceName)
		{
			System.ServiceProcess.ServiceInstaller si = new System.ServiceProcess.ServiceInstaller();

			si.Context = new System.Configuration.Install.InstallContext();
			si.ServiceName = serviceName;

			si.Uninstall(null);
		}

		public static void InstallService(string serviceName, string displayName, string description, string assemblyPath)
		{
			System.ServiceProcess.ServiceProcessInstaller spi = new System.ServiceProcess.ServiceProcessInstaller();
			spi.Account = System.ServiceProcess.ServiceAccount.LocalSystem;

			System.ServiceProcess.ServiceInstaller si = new System.ServiceProcess.ServiceInstaller();
			si.Description = description;
			si.DisplayName = displayName;
			si.ServiceName = serviceName;
			si.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			si.Context = new System.Configuration.Install.InstallContext();
			si.Context.Parameters.Add("assemblyPath", assemblyPath);
			si.Parent = spi;

			Hashtable stateSaver = new Hashtable();
			si.Install(stateSaver);
		}

	}
}

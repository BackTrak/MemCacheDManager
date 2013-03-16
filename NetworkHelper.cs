using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace MemCacheDManager
{
	public class NetworkHelper
	{
		private static List<string> _resolvedRemotMachineNames = new List<string>();
		private static List<string> _resolvedLocalMachineNames = new List<string>();

		/// <summary>
		/// Tests the passed machine name or network address to see if it is the local machine.
		/// </summary>
		/// <param name="machineName"></param>
		/// <returns></returns>
		public static bool IsMachineNameLocal(string machineName)
		{
			machineName = machineName.Trim().ToLower();

			bool machineNameIsLocalMachine = false;
			
			if (_resolvedRemotMachineNames.Contains(machineName) == false && _resolvedLocalMachineNames.Contains(machineName) == false)
			{
				IPHostEntry ipHostEntryMachineName = Dns.GetHostEntry(machineName);
				IPHostEntry ipHostEntryLocal = Dns.GetHostEntry("localhost");

				foreach (IPAddress hostEntryIP in ipHostEntryMachineName.AddressList)
				{
					foreach (IPAddress localEntryIP in ipHostEntryLocal.AddressList)
					{
						if (hostEntryIP.Equals(localEntryIP) == true)
						{
							_resolvedLocalMachineNames.Add(machineName);
							machineNameIsLocalMachine = true;
							break;
						}
					}

					if (machineNameIsLocalMachine == true)
						break;
				}

				if (machineNameIsLocalMachine == false)
					_resolvedRemotMachineNames.Add(machineName);
			}
			else
			{
				if (_resolvedLocalMachineNames.Contains(machineName.ToLower()) == true)
					machineNameIsLocalMachine = true;
			}

			return machineNameIsLocalMachine;
		}

	}
}

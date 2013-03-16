#region --- Libraries ---
using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;
#endregion

//From: http://blog.jrboelens.com/?p=8
namespace MemCacheDManager
{
	public class WmiHelper
	{
		
		#region Connect
		public static ManagementScope Connect(string machineName)
		{
			return Connect(machineName, null, null);
		}

		public static ManagementScope Connect(string machineName, string userNameAndDomainName, string password)
		{
			ConnectionOptions options = new ConnectionOptions();

			if (userNameAndDomainName != null && password != null)
			{
				string userName;
				string domainName;

				if (userNameAndDomainName.Contains("\\") == true)
				{
					domainName = userNameAndDomainName.Split('\\')[0];
					userName = userNameAndDomainName.Split('\\')[1];
				}
				else
				{
					domainName = null;
					userName = userNameAndDomainName;
				}

				if (NetworkHelper.IsMachineNameLocal(machineName) == false)
				{
					options.Authentication = AuthenticationLevel.PacketPrivacy;
					options.Username = userName;
					options.Password = password;

					if(domainName != null)
						options.Authority = "ntlmdomain:" + domainName;
				}
			}

			string path = "\\\\{0}\\root\\cimv2";
			path = String.Format(path, machineName);
			ManagementScope scope = new ManagementScope(path, options);
			scope.Connect();
			return scope;
		}
		#endregion

		#region GetInstanceByName
		private static ManagementObject GetInstanceByName(string machineName, string className, string name)
		{
			return GetInstanceByName(machineName, null, null, className, name);
		}

		private static ManagementObject GetInstanceByName(string machineName, string userName, string password, string className, string name)
		{
			ManagementScope scope = Connect(machineName, userName, password);
			ObjectQuery query = new ObjectQuery("SELECT * FROM " + className + " WHERE Name = '" + name + "'");
			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
			ManagementObjectCollection results = searcher.Get();
			foreach (ManagementObject manObject in results)
				return manObject;

			return null;
		}
		#endregion

		#region GetStaticByName
		private static ManagementClass GetStaticByName(string machineName, string className)
		{
			return GetStaticByName(machineName, null, null, className);
		}

		private static ManagementClass GetStaticByName(string machineName, string userName, string password, string className)
		{
			ManagementScope scope = Connect(machineName, userName, password);
			ObjectGetOptions getOptions = new ObjectGetOptions();
			ManagementPath path = new ManagementPath(className);
			ManagementClass manClass = new ManagementClass(scope, path, getOptions);
			return manClass;
		}
		#endregion

		#region InvokeInstanceMethod
		public static int InvokeInstanceMethod(string machineName, string className, string name, string methodName)
		{
			return InvokeInstanceMethod(machineName, className, name, methodName, null);
		}

		public static int InvokeInstanceMethod(string machineName, string className, string name, string methodName, object[] parameters)
		{
			return InvokeInstanceMethod(machineName, null, null, className, name, methodName, parameters);
		}

		public static int InvokeInstanceMethod(string machineName, string userName, string password, string className, string name, string methodName, object[] parameters)
		{
			try
			{
				ManagementObject manObject = GetInstanceByName(machineName, userName, password, className, name);
				object result = manObject.InvokeMethod(methodName, parameters);
				return Convert.ToInt32(result);
			}
			catch
			{
				return -1;
			}
		}

		#endregion

		#region InvokeStaticMethod
		public static int InvokeStaticMethod(string machineName, string className, string methodName)
		{
			return InvokeStaticMethod(machineName, className, methodName, null);
		}
		public static int InvokeStaticMethod(string machineName, string className, string methodName, object[] parameters)
		{
			return InvokeStaticMethod(machineName, null, null, className, methodName, parameters);
		}

		public static int InvokeStaticMethod(string machineName, string userName, string password, string className, string methodName, object[] parameters)
		{
			try
			{
				ManagementClass manClass = GetStaticByName(machineName, userName, password, className);
				object result = manClass.InvokeMethod(methodName, parameters);
				return Convert.ToInt32(result);
			}
			catch
			{
				return -1;
			}
		}
		#endregion
	}
}

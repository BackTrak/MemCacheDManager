using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Threading;
using System.Data;

// From: http://blog.jrboelens.com/?p=8
namespace MemCacheDManager
{
	#region --- Enums ---
	//Reference: http://msdn2.microsoft.com/en-us/library/aa389390(VS.85).aspx

	public enum ServiceType
	{
		KernalDriver = 1,
		FileSystemDriver = 2,
		Adapter = 4,
		RecognizerDriver = 8,
		OwnProcess = 16,
		ShareProcess = 32,
		InteractiveProcess = 256,
	}

	public enum ErrorControl
	{
		/// <summary>
		/// Ignore. User is not notified.
		/// </summary>
		Ignore = 0,
		/// <summary>
		/// Normal. User is notified.
		/// </summary>
		Normal = 1,
		/// <summary>
		/// Severe. System is restarted with the last-known-good configuration.
		/// </summary>
		Severe = 2,
		/// <summary>
		/// Critical. System attempts to restart with a good configuration.
		/// </summary>
		Critical = 3
	}

	public enum StartMode
	{
		Boot,
		System,
		Auto,
		Manual,
		Disabled,
	}

	public enum ReturnValue
	{
		Success = 0,
		NotSupported = 1,
		AccessDenied = 2,
		DependentServicesRunning = 3,
		InvalidServiceControl = 4,
		ServiceCannotAcceptControl = 5,
		ServiceNotActive = 6,
		ServiceRequestTimeout = 7,
		UnknownFailure = 8,
		PathNotFound = 9,
		ServiceAlreadyRunning = 10,
		ServiceDatabaseLocked = 11,
		ServiceDependencyDeleted = 12,
		ServiceDependencyFailure = 13,
		ServiceDisabled = 14,
		ServiceLogonFailure = 15,
		ServiceMarkedForDeletion = 16,
		ServiceNoThread = 17,
		StatusCircularDependency = 18,
		StatusDuplicateName = 19,
		StatusInvalidName = 20,
		StatusInvalidParameter = 21,
		StatusInvalidServiceAccount = 22,
		StatusServiceExists = 23,
		ServiceAlreadyPaused = 24,
	}

	public enum State
	{
		Stopped,
		StartPending, 
		StopPending,
		Running,
		ContinuePending,
		PausePending,
		Paused,
		Unknown
	}
	#endregion 

	public class WmiService
	{
		public string _displayName;
		[Business.DataTableConverter.Conversion(AllowDbNull = false, DataTableConversion = true, KeyField = false)]
		public string DisplayName
		{
			get { return _displayName; }
			set { _displayName = value; }
		}

		public string _pathName;
		[Business.DataTableConverter.Conversion(AllowDbNull = false, DataTableConversion = true, KeyField = false)]
		public string PathName
		{
			get { return _pathName; }
			set { _pathName = value; }
		}

		public ServiceType ServiceType;
		public ErrorControl ErrorControl;
		public StartMode StartMode;
		public bool DesktopInteract;
		public string StartName;
		public string StartPassword;
		public string LoadOrderGroup;
		public string LoadOrderGroupDependencies;
		public string ServiceDependencies;
		public bool AcceptPause;
		public bool AcceptStop;
		public int CheckPoint;
		public string CreationClassName;
		public string Description;
		public int ExitCode;
		public DateTime InstallDate;

		public string _name;
		[Business.DataTableConverter.Conversion(AllowDbNull = false, DataTableConversion = true, KeyField = false)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int ProcessId;
		public int ServiceSpecificExitCode;
		public bool Started;
		public State State;
		public string Status;
		public string SystemCreationClassName;
		public string SystemName;
		public int TagId;
		public int WaitHint;

		public ManagementObject WmiServiceObject;



		#region --- Fields ---
		private const string CLASSNAME = "Win32_Service";
		#endregion

		#region --- Methods ---

		#region Create
		public static ReturnValue Create(string machineName, string serviceName, string displayName, string serviceLocation, StartMode startMode, string username, string password)
		{
			return Create(machineName, null, null, serviceName, displayName, serviceLocation, startMode, username, password);
		}

		public static ReturnValue Create(string machineName, string impersonationUsername, string impersonationPassword, string serviceName, string displayName, string serviceLocation, StartMode startMode, string username, string password)
		{
			try
			{
				string methodName = "Create";
				object[] parameters = new object[]
					{
						serviceName, // Name
						displayName, // Description 
						serviceLocation, // Location
						Convert.ToInt32(ServiceType.OwnProcess), // ServiceType
						Convert.ToInt32(ErrorControl.Normal), // Error Control
						GetStartModeString(startMode), // Start Mode
						false, // Desktop Interaction
						username, // Username
						password, // Password
						null, // Service Order Group
						null // Load Order Dependencies
					};
				return (ReturnValue)WmiHelper.InvokeStaticMethod(machineName, impersonationUsername, impersonationPassword, CLASSNAME, methodName, parameters);
			}
			catch
			{
				return ReturnValue.UnknownFailure;
			}
		}
		#endregion

		#region Delete
		public static ReturnValue Delete(string machineName, string serviceName)
		{
			return Delete(machineName, null, null, serviceName);
		}

		public static ReturnValue Delete(string machineName, string impersonationUsername, string impersonationPassword, string serviceName)
		{
			try
			{
				string methodName = "Delete";
				return (ReturnValue)WmiHelper.InvokeInstanceMethod(machineName, impersonationUsername, impersonationPassword, CLASSNAME, serviceName, methodName, null);
			}
			catch
			{
				return ReturnValue.UnknownFailure;
			}
		}
		#endregion

		#region Start
		public static ReturnValue Start(string machineName, string serviceName)
		{
			return Start(machineName, null, null, serviceName);
		}

		public static ReturnValue Start(string machineName, string impersonationUsername, string impersonationPassword, string serviceName)
		{
			try
			{
				string methodName = "StartService";
				return (ReturnValue)WmiHelper.InvokeInstanceMethod(machineName, impersonationUsername, impersonationPassword, CLASSNAME, serviceName, methodName, null);
			}
			catch
			{
				return ReturnValue.UnknownFailure;
			}
		}
		#endregion

		#region Stop

		public static ReturnValue Stop(string machineName, string serviceName)
		{
			return Stop(machineName, null, null, serviceName);
		}
		public static ReturnValue Stop(string machineName, string impersonationUsername, string impersonationPassword, string serviceName)
		{
			try
			{
				string methodName = "StopService";
				return (ReturnValue)WmiHelper.InvokeInstanceMethod(machineName, impersonationUsername, impersonationPassword, CLASSNAME, serviceName, methodName, null);
			}
			catch
			{
				return ReturnValue.UnknownFailure;
			}
		}
		#endregion

		#region Pause
		public static ReturnValue Pause(string machineName, string serviceName)
		{
			return Pause(machineName, null, null, serviceName);
		}

		public static ReturnValue Pause(string machineName, string impersonationUsername, string impersonationPassword, string serviceName)
		{
			try
			{
				string methodName = "PauseService";
				return (ReturnValue)WmiHelper.InvokeInstanceMethod(machineName, impersonationUsername, impersonationPassword, CLASSNAME, serviceName, methodName, null);
			}
			catch
			{
				return ReturnValue.UnknownFailure;
			}
		}
		#endregion

		#region Resume
		public static ReturnValue Resume(string machineName, string serviceName)
		{
			return Resume(machineName, null, null, serviceName);
		}

		public static ReturnValue Resume(string machineName, string impersonationUsername, string impersonationPassword, string serviceName)
		{
			try
			{
				string methodName = "ResumeService";
				return (ReturnValue)WmiHelper.InvokeInstanceMethod(machineName, impersonationUsername, impersonationPassword, CLASSNAME, serviceName, methodName, null);
			}
			catch
			{
				return ReturnValue.UnknownFailure;
			}
		}
		#endregion

		public static List<WmiService> GetAllServices(string machineName, string userName, string password)
		{
			return GetAllServices(machineName, userName, password, null);
		}

		public static List<WmiService> GetAllServices(string machineName, string userName, string password, string filterString)
		{
			List<WmiService> allServices = new List<WmiService>();

			ManagementScope scope = WmiHelper.Connect(machineName, userName, password);

			// Get all app pools.
			SelectQuery selectQuery;
			
			//if(filterString == null || filterString == String.Empty)
				selectQuery = new SelectQuery(@"SELECT * FROM Win32_Service");
			//else
			//	selectQuery = new SelectQuery(string.Format(@"SELECT * FROM Win32_Service WHERE {0}", filterString));

			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, selectQuery);

			foreach (ManagementObject managementObject in searcher.Get())
			{
				WmiService newService = new WmiService();
				newService.WmiServiceObject = managementObject;

				foreach(PropertyData propertyData in managementObject.Properties)
				{
					if (propertyData.Value == null)
						continue;


					switch(propertyData.Name)
					{
						case "DesktopInteract": 
							bool.TryParse(propertyData.Value.ToString(), out newService.DesktopInteract);
							break;

						case "DisplayName":
							newService.DisplayName = propertyData.Value.ToString();
							break;

						case "ErrorControl":
							newService.ErrorControl = (ErrorControl)Enum.Parse(typeof(ErrorControl), propertyData.Value.ToString(), true);
							break;

						case "LoadOrderGroup":
							newService.LoadOrderGroup = propertyData.Value.ToString();
							break;

						case "LoadOrderGroupDependencies":
							newService.LoadOrderGroupDependencies = propertyData.Value.ToString();
							break;

						case "PathName":
							newService.PathName = propertyData.Value.ToString();
							break;

						case "ServiceDependencies":
							newService.ServiceDependencies = propertyData.Value.ToString();
							break;

						case "ServiceType":
							newService.ServiceType = (ServiceType)Enum.Parse(typeof(ServiceType), propertyData.Value.ToString().Replace(" ", ""), true);
							break;

						case "StartMode":
							newService.StartMode = (StartMode)Enum.Parse(typeof(StartMode), propertyData.Value.ToString(), true);
							break;

						case "StartName":
							newService.StartName = propertyData.Value.ToString();
							break;

						case "StartPassword":
							newService.StartPassword = propertyData.Value.ToString();
							break;

						case "AcceptPause":
							bool.TryParse(propertyData.Value.ToString(), out newService.AcceptPause);
							break;

						case "AcceptStop":
							bool.TryParse(propertyData.Value.ToString(), out newService.AcceptStop);
							break;

						case "CheckPoint":
							int.TryParse(propertyData.Value.ToString(), out newService.CheckPoint);
							break;

						case "CreationClassName":
							newService.CreationClassName = propertyData.Value.ToString();
							break;

						case "Description":
							newService.Description = propertyData.Value.ToString();
							break;

						case "ExitCode":
							int.TryParse(propertyData.Value.ToString(), out newService.ExitCode);
							break;

						case "InstallDate":
							DateTime.TryParse(propertyData.Value.ToString(), out newService.InstallDate);
							break;

						case "Name":
							newService.Name = propertyData.Value.ToString();
							break;

						case "ProcessId":
							int.TryParse(propertyData.Value.ToString(), out newService.ProcessId);
							break;

						case "ServiceSpecificExitCode":
							int.TryParse(propertyData.Value.ToString(), out newService.ServiceSpecificExitCode);
							break;

						case "Started":
							bool.TryParse(propertyData.Value.ToString(), out newService.Started);
							break;

						case "State":
							newService.State = (State)Enum.Parse(typeof(State), propertyData.Value.ToString().Replace(" ", ""), true);
							break;

						case "Status":
							newService.Status = propertyData.Value.ToString();
							break;

						case "SystemCreationClassName":
							newService.SystemCreationClassName = propertyData.Value.ToString();
							break;

						case "SystemName":
							newService.SystemName = propertyData.Value.ToString();
							break;


						case "TagId":
							int.TryParse(propertyData.Value.ToString(), out newService.TagId);
							break;

						case "WaitHint":
							int.TryParse(propertyData.Value.ToString(), out newService.WaitHint);
							break;
					}
				}

                // ensure newService is a valid instance -CT
                if (!string.IsNullOrEmpty(newService.DisplayName))
                    allServices.Add(newService);
			}

			List<WmiService> filteredServices = new List<WmiService>();

			if(filterString != null && filterString != String.Empty)
			{
				DataTable allServicesDataTable = new Business.DataTableConverter.DataTableConverter<WmiService>().GetDataTable(allServices);
				DataView allServicesView = new DataView(allServicesDataTable);
				allServicesView.RowFilter = filterString;

				foreach (DataRowView allServicesRow in allServicesView)
					filteredServices.Add((WmiService) allServicesRow["OriginalSourceObject"]);
			}
			else
			{
				filteredServices = allServices;
			}

			return filteredServices;
		}


		public static ReturnValue CreateOrUpdate(string machineName, string impersonationUsername, string impersonationPassword, string serviceName, string displayName, string serviceLocation, StartMode startMode, string username, string password)
		{
			List<WmiService> servicesToChange = WmiService.GetAllServices(machineName, impersonationUsername, impersonationPassword, "Name = '" + serviceName + "'");

			if (servicesToChange.Count > 0)
			{
				ReturnValue result;

				WmiService serviceToChange = servicesToChange[0];

				bool requiresRestart = false;
				if (serviceToChange.State != State.Stopped)
				{
					requiresRestart = true;
					result = WmiService.Stop(machineName, impersonationUsername, impersonationPassword, serviceName);

					if (result != ReturnValue.Success)
						throw new Exception("Failed to stop " + displayName + " on: " + machineName + ", return code: " + result.ToString());
				}

				object[] parameters = new object[]
					{
						displayName, // DisplayName 
						serviceLocation, // Location
						Convert.ToInt32(ServiceType.OwnProcess), // ServiceType
						Convert.ToInt32(ErrorControl.Normal), // Error Control
						GetStartModeString(startMode), // Start Mode
						false, // Desktop Interaction
						username, // Username
						password, // Password
						null, // Service Order Group
						null, // Load Order Dependencies
						null // Service dependencies
					};

				result = (ReturnValue) Convert.ToInt32(serviceToChange.WmiServiceObject.InvokeMethod("Change", parameters));

				if (result != ReturnValue.Success)
					throw new Exception("Failed to update " + displayName + " on: " + machineName + ", return code: " + result.ToString());

				Thread.Sleep(3000);

				if (requiresRestart == true)
				{
					result = WmiService.Start(machineName, impersonationUsername, impersonationPassword, serviceName);

					if (result != ReturnValue.Success)
						throw new Exception("Failed to restart " + displayName + " on: " + machineName + ", return code: " + result.ToString());
				}

				return result;
			}
			else
			{
				return Create(machineName, impersonationUsername, impersonationPassword, serviceName, displayName, serviceLocation, startMode, username, password);
			}
		}

		#endregion

		public static string GetStartModeString(StartMode startMode)
		{
			switch(startMode)
			{
				case StartMode.Auto:
					return "Automatic";
			}

			return startMode.ToString();
		}
	}
}



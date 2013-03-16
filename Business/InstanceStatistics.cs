using System;
using System.Collections.Generic;
using System.Text;
using Enyim.Caching.Memcached;
using System.Drawing;
using MemCacheDManager.Properties;
using System.IO;
using System.Diagnostics;

namespace MemCacheDManager.Business
{
	public class InstanceStatistics
	{
		private long _bytesRead;
		public long BytesRead
		{
			get { return _bytesRead; }
		}

		private long _bytesWritten;
		public long BytesWritten
		{
			get { return _bytesWritten; }
		}


		private long _connectionCount;
		public long ConnectionCount
		{
			get { return _connectionCount; }
		}

		private long _connectionStructures;
		public long ConnectionStructures
		{
			get { return _connectionStructures; }
		}

		private long _getCount;
		public long GetCount
		{
			get { return _getCount; }
		}

		private long _getHits;
		public long GetHits
		{
			get { return _getHits; }
		}

		private long _getMisses;
		public long GetMisses
		{
			get { return _getMisses; }
		}

		private long _itemCount;
		public long ItemCount
		{
			get { return _itemCount; }
		}

		private long _maxBytes;
		public long MaxBytes
		{
			get { return _maxBytes; }
		}

		private string _serverTime;
		public string ServerTime
		{
			get { return _serverTime; }
		}

		private long _setCount;
		public long SetCount
		{
			get { return _setCount; }
		}

		private long _totalConnections;
		public long TotalConnections
		{
			get { return _totalConnections; }
		}

		private long _totalItems;
		public long TotalItems
		{
			get { return _totalItems; }
		}

		private string _uptime;
		public string Uptime
		{
			get { return _uptime; }
		}

		private long _usedBytes;
		public long UsedBytes
		{
			get { return _usedBytes; }
		}

		private string _version = null;
		public string Version
		{
			get { return _version; }
		}

		private string _serverName;
		public string ServerName
		{
			get { return _serverName; }
		}

		private string _instanceName;
		public string InstanceName
		{
			get { return _instanceName; }
		}

		private StatusIconIndex _statusIconIndex;
		public StatusIconIndex StatusIconIndex
		{
			get { return _statusIconIndex; }
		}

		private string _statusTooltip = "";
		public string StatusTooltip
		{
			get { return _statusTooltip; }
		}

		public Icon StatusIcon
		{
			get
			{
				switch (_statusIconIndex)
				{
					case StatusIconIndex.CommunicationError:
						return Resources.icnCommunicationError;

					case StatusIconIndex.Empty:
						return Resources.icnEmpty;

					case StatusIconIndex.NeedsUpdate:
						return Resources.icnNeedsUpdate;

					case StatusIconIndex.ServiceDown:
						return Resources.icnServiceDown;

					case StatusIconIndex.ServiceNonControllable:
						return Resources.icnDown;

					case StatusIconIndex.Up:
						return Resources.icnUp;

					default:
						throw new NotSupportedException("Icon for: " + _statusIconIndex.ToString() + " is not defined.");
				}

			}
		}

		private State _serviceState;
		public State ServiceState
		{
			get { return _serviceState; }
		}

		public readonly Instance Instance;
		
		public InstanceStatistics(System.Net.IPEndPoint serverEndPoint, Server server, Instance instance, ServerStats serverStats, State serviceState)
		{
			string memcachedFileVersion = null;

			if (File.Exists(Configuration.Default.MemcachedBinarySource) == true)
				memcachedFileVersion = FileVersionInfo.GetVersionInfo(Configuration.Default.MemcachedBinarySource).FileVersion;

			_serverName = server.ServerName;
			_instanceName = instance.DisplayName;
			_serviceState = serviceState;

			Instance = instance;

			_statusIconIndex = StatusIconIndex.CommunicationError;
			_statusTooltip = Constants.TooltipCommunicationError;

			if (serverStats != null && serverStats.GetRaw(serverEndPoint, StatItem.Version) != null)
			{

				foreach (StatItem statItem in Enum.GetValues(typeof(Enyim.Caching.Memcached.StatItem)))
				{
					switch (statItem)
					{
						case StatItem.BytesRead:
							_bytesRead = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.BytesWritten:
							_bytesWritten = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.ConnectionCount:
							_connectionCount = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.ConnectionStructures:
							_connectionStructures = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.GetCount:
							_getCount = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.GetHits:
							_getHits = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.GetMisses:
							_getMisses = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.ItemCount:
							_itemCount = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.MaxBytes:
							_maxBytes = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.ServerTime:
							string serverTimeRaw = serverStats.GetRaw(serverEndPoint, statItem);
							int serverTimeSeconds;
							if (serverTimeRaw != null && Int32.TryParse(serverTimeRaw, out serverTimeSeconds) == true)
								_serverTime = new DateTime(1970, 1, 1).AddSeconds(serverTimeSeconds).ToLocalTime().ToString();
							else
								_serverTime = "<unknown>";
							break;

						case StatItem.SetCount:
							_setCount = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.TotalConnections:
							_totalConnections = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.TotalItems:
							_totalItems = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.Uptime:
							string uptimeRaw = serverStats.GetRaw(serverEndPoint, statItem);
							int uptimeSeconds;
							if (uptimeRaw != null && Int32.TryParse(uptimeRaw, out uptimeSeconds) == true)
								_uptime = TimeSpan.FromSeconds(uptimeSeconds).ToString();
							else
								_uptime = "<unknown>";
							break;

						case StatItem.UsedBytes:
							_usedBytes = serverStats.GetValue(serverEndPoint, statItem);
							break;

						case StatItem.Version:
							_version = serverStats.GetRaw(serverEndPoint, statItem);
							break;
					}
				}
			}

			if (_serviceState != State.Running && _serviceState != State.Unknown)
			{
				_statusIconIndex = StatusIconIndex.ServiceDown;
				_statusTooltip = Constants.TooltipServiceDown;
			}
			else if (_serviceState == State.Unknown)
			{
				_statusIconIndex = StatusIconIndex.ServiceNonControllable;
				_statusTooltip = Constants.TooltipServiceNonControllable;
			}
			else if (_version == null || _version == String.Empty)
			{
				_statusIconIndex = StatusIconIndex.CommunicationError;
				_statusTooltip = Constants.TooltipCommunicationError;
			}
			else if (memcachedFileVersion != null && memcachedFileVersion != _version)
			{
				_statusIconIndex = StatusIconIndex.NeedsUpdate;
				_statusTooltip = Constants.TooltipNeedsUpdate;
			}
			else
			{
				_statusIconIndex = StatusIconIndex.Up;
				_statusTooltip = Constants.TooltipUp;
			}
			
		}

		
	}
}

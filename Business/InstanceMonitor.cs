using System;
using System.Collections.Generic;
using System.Text;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System.Threading;
using System.Diagnostics;

namespace MemCacheDManager.Business
{
	public class InstanceMonitor
	{
		private object _syncLock = new object();
		private volatile bool _isRefreshing = false;
		private volatile bool _stopRequested = false;

		private System.Timers.Timer _timer = null;

		private List<Server> _serversToMonitor = new List<Server>();

		private Dictionary<Instance, MemcachedClient> _memcachedConnections = new Dictionary<Instance, MemcachedClient>();

		public delegate void OnInstanceUpdated(Server server, Instance instance, InstanceStatistics instanceStatistics);
		public event OnInstanceUpdated InstanceUpdated;

		public InstanceMonitor(List<Server> servers)
		{
			_serversToMonitor = new List<Server>();

			if(servers != null && servers.Count > 0)
				_serversToMonitor.AddRange(servers);
			
			_timer = new System.Timers.Timer();
			_timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
			_timer.Interval = 5000;
			
		}

		public void Start()
		{
			_timer.Interval = 100;
			_timer.Start();
		}

		public void Stop()
		{
			//_timer.Stop();
			_stopRequested = true;

			_timer.Stop();

			for (int i = 0; i < 50 && _isRefreshing == true; i++)
				Thread.Sleep(100);

			//// this lock statement is intentionally to allow for a graceful shutdown of the monitor thread.
			//lock (_syncLock)
			//{
			//     //wait for shutdown to complete...
			//}

			_stopRequested = false;
			 
		}

		public void RefreshServerStatistics()
		{
			if (_isRefreshing == true)
				return;

			// It's not the end of the world if a call makes it through on a race condition. 
			// We're just going to try to stop it from spamming the cache servers if one of 
			// them takes longer than 5 seconds to respond.
			//lock (_syncLock)
			//{
				_isRefreshing = true;

				try
				{
					for(int serverIndex = 0; serverIndex < _serversToMonitor.Count; serverIndex++)
					{
						Server server = _serversToMonitor[serverIndex];

						if (_stopRequested == true)
							break;

						//foreach (Instance instance in server.Instances)
						for(int instanceIndex = 0; instanceIndex < server.Instances.Count; instanceIndex++)
						{
							Instance instance = server.Instances[instanceIndex];

							if (_stopRequested == true)
								break;

							MemcachedClient memcachedClient;
							System.Net.IPEndPoint serverEndPoint = new System.Net.IPEndPoint(System.Net.Dns.GetHostEntry(server.ServerName).AddressList[0], instance.TcpPort);

							if (_memcachedConnections.ContainsKey(instance) == false)
							{
								MemcachedClientConfiguration config = new MemcachedClientConfiguration();
								config.Servers.Add(serverEndPoint);
								config.SocketPool.ConnectionTimeout = new TimeSpan(0, 0, 3);

								//config.SocketPool.

								memcachedClient = new MemcachedClient(config);

								_memcachedConnections.Add(instance, memcachedClient);
							}
							else
							{
								memcachedClient = _memcachedConnections[instance];
							}
							
							State serviceState = State.Unknown;
							
							try
							{
								serviceState = instance.GetServiceState(server);
							}
							catch (Exception ex)
							{
								// Leave the state as unknown. 
								Debug.WriteLine(ex);
							}

							ServerStats serverStats = memcachedClient.Stats();
							InstanceStatistics instanceStatistics = new InstanceStatistics(serverEndPoint, server, instance, serverStats, serviceState);

							if (InstanceUpdated != null)
								InstanceUpdated(server, instance, instanceStatistics);
						}
					}
				}
				finally
				{
					_isRefreshing = false;
				}
			//}
		}

		private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			_timer.Interval = 5000;
			RefreshServerStatistics();
		}


	}
}

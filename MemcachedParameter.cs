using System;
using System.Collections.Generic;
using System.Text;

namespace MemCacheDManager
{
	public enum MemcachedParameter
	{
		TcpPort,
		UdpPort,
		MaximizeCoreFile,
		MemorySize,
		MaxConnections,
		ManagedInstanceBuckets,
		ChunkSize,
		KeySize
	}
}

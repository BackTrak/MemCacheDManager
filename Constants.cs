using System;
using System.Collections.Generic;
using System.Text;

namespace MemCacheDManager
{
	public static class Constants
	{
		public const string TooltipNeedsUpdate = "One or more MemCacheD instances are not running your version of MemCacheD.";
		public const string TooltipCommunicationError = "One or more MemCacheD instances are not reporting a version number, or are not reachable.";
		public const string TooltipUp = "Running normally.";
		public const string TooltipEmpty = "Please add instances of MemCacheD to this server. (Right click on the server name to add.)";
		public const string TooltipServiceDown = "One or more MemCacheD windows services are not running.";
		public const string TooltipServiceNonControllable = "Could not communicate with the server to get windows service status. Please check account credentials and server connectivity.";
	}
}

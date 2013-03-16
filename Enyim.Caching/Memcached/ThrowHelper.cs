﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Enyim.Caching.Memcached
{
	internal static class ThrowHelper
	{
		public static void ThrowSocketWriteError(IPEndPoint endpoint, SocketError error)
		{
			// move the string into resource file
			throw new System.IO.IOException(String.Format("Failed to write to the socket '{0}'. Error: {1}", endpoint, error));
		}
	}
}

#region [ License information          ]
/* ************************************************************
 *
 * Copyright (c) Attila Kiskó, enyim.com, 2007
 *
 * This source code is subject to terms and conditions of 
 * Microsoft Permissive License (Ms-PL).
 * 
 * A copy of the license can be found in the License.html
 * file at the root of this distribution. If you can not 
 * locate the License, please send an email to a@enyim.com
 * 
 * By using this source code in any fashion, you are 
 * agreeing to be bound by the terms of the Microsoft 
 * Permissive License.
 *
 * You must not remove this notice, or any other, from this
 * software.
 *
 * ************************************************************/
#endregion
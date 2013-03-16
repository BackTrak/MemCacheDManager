﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace Enyim.Caching.Memcached
{
	internal abstract class ItemOperation : Operation
	{
		private string key;
		private string hashedKey;

		private PooledSocket socket;

		protected ItemOperation(ServerPool pool, string key)
			: base(pool)
		{
			this.key = key;
		}

		protected string Key
		{
			get { return this.key; }
		}

		/// <summary>
		/// Gets the hashed bersion of the key which should be used as key in communication with memcached
		/// </summary>
		protected string HashedKey
		{
			get { return this.hashedKey ?? (this.hashedKey = this.ServerPool.KeyTransformer.Transform(this.key)); }
		}

		protected PooledSocket Socket
		{
			get
			{
				if (this.socket == null)
				{
					// get a connection to the server which belongs to "key"
					PooledSocket ps = this.ServerPool.Acquire(this.key);

					// null was returned, so our server is dead and no one could replace it
					// (probably all of our servers are down)
					if (ps == null)
					{
						return null;
					}

					this.socket = ps;
				}

				return this.socket;
			}
		}

		public override void Dispose()
		{
			GC.SuppressFinalize(this);

			if (this.socket != null)
			{
				((IDisposable)this.socket).Dispose();
				this.socket = null;
			}

			base.Dispose();
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
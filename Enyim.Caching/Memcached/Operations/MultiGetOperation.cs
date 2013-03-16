using System;
using System.Collections.Generic;
using System.Text;

namespace Enyim.Caching.Memcached
{
	internal class MultiGetOperation : Operation
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(MultiGetOperation));

		private IEnumerable<string> keys;
		private Dictionary<string, object> result;
		private Dictionary<string, ulong> casValues;

		public MultiGetOperation(ServerPool pool, IEnumerable<string> keys)
			: base(pool)
		{
			this.keys = keys;
		}

		protected override bool ExecuteAction()
		{
			// {hashed key -> normal key}: will be used when mapping the returned items back to the original keys
			Dictionary<string, string> hashedToReal = new Dictionary<string, string>(StringComparer.Ordinal);

			// {normal key -> hashed key}: we have to hash all keys anyway, so we better cache them to improve performance instead of doing the hashing later again
			Dictionary<string, string> realToHashed = new Dictionary<string, string>(StringComparer.Ordinal);

			IMemcachedKeyTransformer transformer = this.ServerPool.KeyTransformer;

			// and store them with the originals so we can map the returned items 
			// to the original keys
			foreach (string s in this.keys)
			{
				string hashed = transformer.Transform(s);

				hashedToReal[hashed] = s;
				realToHashed[s] = hashed;
			}

			// map each key to the appropriate server in the pool
			IDictionary<MemcachedNode, IList<string>> splitKeys = this.ServerPool.SplitKeys(this.keys);

			// we'll open 1 socket for each server
			List<PooledSocket> sockets = new List<PooledSocket>();
			
			try
			{
				string[] command;

				// send a 'gets' to each server
				foreach (KeyValuePair<MemcachedNode, IList<string>> kp in splitKeys)
				{
					// gets <keys>
					//
					// keys: key key key key
					command = new string[kp.Value.Count + 1];
					command[0] = "gets";
					kp.Value.CopyTo(command, 1);

					for (int i = 1; i < command.Length; i++)
						command[i] = realToHashed[command[i]];

					PooledSocket socket = kp.Key.Acquire();
					if (socket == null)
						continue;

					sockets.Add(socket);
					socket.SendCommand(String.Join(" ", command));
				}

				Dictionary<string, object> retval = new Dictionary<string, object>(StringComparer.Ordinal);
				Dictionary<string, ulong> cas = new Dictionary<string, ulong>(StringComparer.Ordinal);

				// process each response and build a dictionary from the results
				foreach (PooledSocket socket in sockets)
				{
					try
					{
						GetResponse r;

						while ((r = GetHelper.ReadItem(socket)) != null)
						{
							string originalKey = hashedToReal[r.Key];

							retval[originalKey] = this.ServerPool.Transcoder.Deserialize(r.Item);
							cas[originalKey] = r.CasValue;
						}
					}
					catch (NotSupportedException)
					{
						throw;
					}
					catch (Exception e)
					{
						log.Error(e);
					}
				}

				this.result = retval;
				this.casValues = cas;
			}
			finally
			{
				if (sockets != null)
					foreach (PooledSocket socket in sockets)
						((IDisposable)socket).Dispose();
			}

			return true;
		}

		public IDictionary<string, object> Result
		{
			get { return this.result; }
		}
	
		public IDictionary<string, ulong> CasValues
		{
			get { return this.casValues; }
		}
	}
}

#region [ License information          ]
/* ************************************************************
 *
 * Copyright (c) Attila Kisk�, enyim.com, 2007
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
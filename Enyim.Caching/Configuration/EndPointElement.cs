using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Enyim.Caching.Configuration
{
	/// <summary>
	/// Represents a configuration element that contains a Memcached node address. This class cannot be inherited. 
	/// </summary>
	public sealed class EndPointElement : ConfigurationElement
	{
		private System.Net.IPEndPoint endpoint;

		/// <summary>
		/// Gets or sets the ip address of the node.
		/// </summary>
		[ConfigurationProperty("address", IsRequired = true, IsKey = true), ConfigurationValidator(typeof(EndPointElement.IPAddressValidator))]
		public string Address
		{
			get { return (string)base["address"]; }
			set { base["address"] = value; }
		}

		/// <summary>
		/// Gets or sets the port of the node.
		/// </summary>
		[ConfigurationProperty("port", IsRequired = true, IsKey = true), IntegerValidator(MinValue = 0, MaxValue = 65535)]
		public int Port
		{
			get { return (int)base["port"]; }
			set { base["port"] = value; }
		}

		/// <summary>
		/// Gets the <see cref="T:IPEndPoint"/> representation of this instance.
		/// </summary>
		public System.Net.IPEndPoint EndPoint
		{
			get { return (this.endpoint ?? (this.endpoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(this.Address), this.Port))); }
		}

		#region [ T:IPAddressValidator         ]
		private class IPAddressValidator : ConfigurationValidatorBase
		{
			public override bool CanValidate(Type type)
			{
				return (type == typeof(string)) || base.CanValidate(type);
			}

			public override void Validate(object value)
			{
				string address = value as string;

				if (String.IsNullOrEmpty(address))
					return;

				System.Net.IPAddress tmp;

				if (!System.Net.IPAddress.TryParse(address, out tmp))
					throw new ConfigurationErrorsException("Invalid address specified: " + address);
			}
		}
		#endregion
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
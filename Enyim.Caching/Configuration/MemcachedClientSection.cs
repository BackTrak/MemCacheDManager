using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.ComponentModel;
using Enyim.Caching;
using System.Collections.ObjectModel;
using System.Net;

namespace Enyim.Caching.Configuration
{
	/// <summary>
	/// Configures the <see cref="T:MemcachedClient"/>. This class cannot be inherited.
	/// </summary>
	public sealed class MemcachedClientSection : ConfigurationSection, IMemcachedClientConfiguration
	{
		/// <summary>
		/// Returns a collection of Memcached servers which can be used by the client.
		/// </summary>
		[ConfigurationProperty("servers", IsRequired = true)]
		public EndPointElementCollection Servers
		{
			get { return (EndPointElementCollection)base["servers"]; }
		}

		/// <summary>
		/// Gets or sets the configuration of the socket pool.
		/// </summary>
		[ConfigurationProperty("socketPool", IsRequired = false)]
		public SocketPoolElement SocketPool
		{
			get { return (SocketPoolElement)base["socketPool"]; }
			set { base["socketPool"] = value; }
		}

		/// <summary>
		/// Gets or sets the type of the <see cref="T:Enyim.Caching.Memcached.IMemcachedKeyTransformer"/> which will be used to convert item keys for Memcached.
		/// </summary>
		[ConfigurationProperty("keyTransformer", IsRequired = false), TypeConverter(typeof(TypeNameConverter)), InterfaceValidator(typeof(Enyim.Caching.Memcached.IMemcachedKeyTransformer))]
		public Type KeyTransformer
		{
			get { return (Type)base["keyTransformer"]; }
			set { base["keyTransformer"] = value; }
		}

		/// <summary>
		/// Gets or sets the type of the <see cref="T:Enyim.Caching.Memcached.IMemcachedNodeLocator"/> which will be used to assign items to Memcached nodes.
		/// </summary>
		[ConfigurationProperty("nodeLocator", IsRequired = false), TypeConverter(typeof(TypeNameConverter)), InterfaceValidator(typeof(Enyim.Caching.Memcached.IMemcachedNodeLocator))]
		public Type NodeLocator
		{
			get { return (Type)base["nodeLocator"]; }
			set { base["nodeLocator"] = value; }
		}

		/// <summary>
		/// Gets or sets the type of the <see cref="T:Enyim.Caching.Memcached.ITranscoder"/> which will be used serialzie or deserialize items.
		/// </summary>
		[ConfigurationProperty("transcoder", IsRequired = false), TypeConverter(typeof(TypeNameConverter)), InterfaceValidator(typeof(Enyim.Caching.Memcached.ITranscoder))]
		public Type Transcoder
		{
			get { return (Type)base["transcoder"]; }
			set { base["transcoder"] = value; }
		}

		/// <summary>
		/// Called after deserialization.
		/// </summary>
		protected override void PostDeserialize()
		{
			WebContext hostingContext = base.EvaluationContext.HostingContext as WebContext;

			if (hostingContext != null && hostingContext.ApplicationLevel == WebApplicationLevel.BelowApplication)
			{
				throw new InvalidOperationException("The " + this.SectionInformation.SectionName + " section cannot be defined below the application level.");
			}
		}

		#region [ IMemcachedClientConfiguration]
		IList<IPEndPoint> IMemcachedClientConfiguration.Servers
		{
			get { return this.Servers.ToIPEndPointCollection(); }
		}

		ISocketPoolConfiguration IMemcachedClientConfiguration.SocketPool
		{
			get { return this.SocketPool; }
		}

		Type IMemcachedClientConfiguration.KeyTransformer
		{
			get { return this.KeyTransformer; }
			set { this.KeyTransformer = value; }
		}

		Type IMemcachedClientConfiguration.NodeLocator
		{
			get { return this.NodeLocator; }
			set { this.NodeLocator = value; }
		}

		Type IMemcachedClientConfiguration.Transcoder
		{
			get { return this.Transcoder; }
			set { this.Transcoder = value; }
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace MemCacheDManager
{
	public class ServerConfiguration
	{
		public void Load(string filename)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);

			XmlNode versionNode = xmlDocument.SelectSingleNode("/root/version");
			Version version = new Version(versionNode.InnerText);

			// Add logic to handle version mismatch and data file upgrading here. 

			XmlNode serversNode = xmlDocument.SelectSingleNode("/root/servers");

			if (serversNode != null)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(Business.ServerCollection));
				StringReader stringReader = new StringReader(serversNode.InnerXml);

				XmlReader xmlReader = XmlReader.Create(stringReader);
				this._servers = (Business.ServerCollection)xmlSerializer.Deserialize(xmlReader);
			}
		}

		public void Save(string filename)
		{
			XmlDocument xmlDocument = new XmlDocument();

			XmlNode rootNode = xmlDocument.CreateElement("root");

			xmlDocument.AppendChild(rootNode);

			XmlNode versionNode = xmlDocument.CreateElement("version");
			versionNode.InnerText = this.Version.ToString();

			rootNode.AppendChild(versionNode);

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Business.ServerCollection));
			StringWriter stringWriter = new StringWriter();
			
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;
			xmlWriterSettings.OmitXmlDeclaration = true;

			XmlWriter xmlWriter = XmlTextWriter.Create(stringWriter, xmlWriterSettings);

			xmlSerializer.Serialize(xmlWriter, this._servers);

			XmlNode serversNode = xmlDocument.CreateElement("servers");
			serversNode.InnerXml = stringWriter.GetStringBuilder().ToString();

			rootNode.AppendChild(serversNode);

			XmlTextWriter xmlDocumentWriter = new XmlTextWriter(filename, Encoding.Default);
			xmlDocumentWriter.Formatting = Formatting.Indented;

			xmlDocument.Save(xmlDocumentWriter);
			xmlDocumentWriter.Close();
		}

		private Business.ServerCollection _servers = new MemCacheDManager.Business.ServerCollection();
		public Business.ServerCollection Servers
		{
			get { return _servers; }
		}

		private string _version = Assembly.GetEntryAssembly().GetName().Version.ToString();
		public string Version
		{
			get { return _version; }
		}

	}
}
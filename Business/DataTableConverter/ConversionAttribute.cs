// Code from: http://www.codeproject.com/KB/cs/coreweb01.aspx

using System;

namespace MemCacheDManager.Business.DataTableConverter
{
   [AttributeUsage(AttributeTargets.Property)]
   public class ConversionAttribute : Attribute
   {
      private bool m_dataTableConversion;
      private bool m_allowDbNull;
      private bool m_keyField;

      public ConversionAttribute() { }

      public bool DataTableConversion
      {
         get { return m_dataTableConversion; }
         set { m_dataTableConversion = value; }
      }

      public bool AllowDbNull
      {
         get { return m_allowDbNull; }
         set { m_allowDbNull = value; }
      }

      public bool KeyField
      {
         get { return m_keyField; }
         set { m_keyField = value; }
      }
   }
}


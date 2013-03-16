// Code from: http://www.codeproject.com/KB/cs/coreweb01.aspx

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MemCacheDManager.Business.DataTableConverter
{
   public class DataTableConverter<T> : IDataTableConverter<T>
   {
      private bool m_enforceKeys;

      public DataTableConverter() { }

      public DataTableConverter(bool enforceKeys)
      {
         m_enforceKeys = enforceKeys;
      }

      public DataTable GetDataTable(List<T> items)
      {
         DataTable dt;

         try
         {
            // Build a table schema from the first element in the collection
            dt = this.ConstructDataTableSchema(items[0]);
         }
         catch (IndexOutOfRangeException ex)
         {
            throw (new ApplicationException("Cannot convert List of zero length to a DataTable", ex));
         }

         // If the container is not convertable than throw an ApplicationException.
         if (dt != null)
         {
            // Create a new row for every item in the collection and fill it.
            for (int i = 0; i < items.Count; i++)
            {
               DataRow dr = dt.NewRow();

               Type type = items[i].GetType();
               MemberInfo[] members = type.GetProperties();

               foreach (MemberInfo member in members)
               {
                  object[] attributes = member.GetCustomAttributes(true);

                  if (attributes.Length != 0)
                  {
                     foreach (object attribute in attributes)
                     {
                        ConversionAttribute ca = attribute as ConversionAttribute;
                        if (ca != null)
                        {
                           if (ca.DataTableConversion)
                           {
                              string[] nameArray = member.Name.ToString().Split(Convert.ToChar(" "));
                              PropertyInfo prop = type.GetProperty(nameArray[0]);
                              Type valueType = prop.GetValue(items[i], null).GetType();

                              dr[nameArray[0]] = prop.GetValue(items[i], null);
                           }
                        }
                     }
                  }
               }

			   dr["OriginalSourceObject"] = items[i];

               dt.Rows.Add(dr);
            }

            return dt;
         }
         else
         {
            throw (new ApplicationException("List items are not convertable."));
         }
      }

      // This method reads the attributes of your container class via reflection in order to
      // build a schema for the DataTable that you will explicitly convert to.
      private DataTable ConstructDataTableSchema(T item)
      {
         string tableName = string.Empty;
         List<DTConverterContainer> schemaContainers = new List<DTConverterContainer>();

         Type type = item.GetType();
         MemberInfo[] members = type.GetProperties();

         foreach (MemberInfo member in members)
         {
            object[] attributes = member.GetCustomAttributes(true);

            if (attributes.Length != 0)
            {
               foreach (object attribute in attributes)
               {
                  ConversionAttribute ca = attribute as ConversionAttribute;
                  if (ca != null)
                  {
                     if (ca.DataTableConversion)
                     {
                        // The name of the container class is used to name your DataTable
                        string[] classNameArray = member.ReflectedType.ToString().Split(Convert.ToChar("."));
                        tableName = classNameArray[classNameArray.Length - 1];

                        string name = member.Name.ToString();
                        PropertyInfo prop = type.GetProperty(name);
                        Type valueType = prop.GetValue(item, null).GetType();

                        // Each property that is  will be a column in our DataTable.
                        schemaContainers.Add(new DTConverterContainer(name,
                           valueType, ca.AllowDbNull, ca.KeyField));
                     }
                  }
               }
            }
         }

         if (schemaContainers.Count > 0)
         {
            DataTable dataTable = new DataTable(tableName);
            DataColumn[] dataColumn = new DataColumn[schemaContainers.Count];

            // Counts the number of keys that will need to be created
            int totalNumberofKeys = 0;
            foreach (DTConverterContainer container in schemaContainers)
            {
               if (container.IsKey == true && m_enforceKeys == true)
               {
                  totalNumberofKeys = totalNumberofKeys + 1;
               }
            }

            // Builds the DataColumns for our DataTable
            DataColumn[] keyColumnArray = new DataColumn[totalNumberofKeys];
            int keyColumnIndex = 0;
            for (int i = 0; i < schemaContainers.Count; i++)
            {
               dataColumn[i] = new DataColumn();
               dataColumn[i].DataType = schemaContainers[i].PropertyType;
               dataColumn[i].ColumnName = schemaContainers[i].PropertyName;
               dataColumn[i].AllowDBNull = schemaContainers[i].AllowDbNull;
               dataTable.Columns.Add(dataColumn[i]);

               if (schemaContainers[i].IsKey == true && m_enforceKeys == true)
               {
                  keyColumnArray[keyColumnIndex] = dataColumn[i];
                  keyColumnIndex = keyColumnIndex + 1;
               }
            }

			DataColumn objectColumn = new DataColumn();
			objectColumn.DataType = typeof(T);
			objectColumn.ColumnName = "OriginalSourceObject";
			objectColumn.AllowDBNull = false;
			dataTable.Columns.Add(objectColumn);

            if (m_enforceKeys)
            {
               dataTable.PrimaryKey = keyColumnArray;
            }
            return dataTable;
         }

         return null;
      }

      #region Internal Classes
      private class DTConverterContainer
      {
         private string m_propertyName;
         private Type m_propertyType;
         private bool m_allowDbNull;
         private bool m_isKey;

         internal DTConverterContainer(string propertyName, Type propertyType,
            bool allowDbNull, bool isKey)
         {
            m_propertyName = propertyName;
            m_propertyType = propertyType;
            m_allowDbNull = allowDbNull;
            m_isKey = isKey;
         }

         public string PropertyName
         {
            get { return m_propertyName; }
            set { m_propertyName = value; }
         }

         public Type PropertyType
         {
            get { return m_propertyType; }
            set { m_propertyType = value; }
         }

         public bool AllowDbNull
         {
            get { return m_allowDbNull; }
            set { m_allowDbNull = value; }
         }

         public bool IsKey
         {
            get { return m_isKey; }
            set { m_isKey = value; }
         }
      }
      #endregion
   }
}

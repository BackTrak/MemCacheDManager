// Code from: http://www.codeproject.com/KB/cs/coreweb01.aspx

using System;
using System.Collections.Generic;
using System.Data;

namespace MemCacheDManager.Business.DataTableConverter
{
   public interface IDataTableConverter<T>
   {
      DataTable GetDataTable(List<T> items);
   }
}

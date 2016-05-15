using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DAL.Base
{
    public abstract class DataAccessBase<T>:IPrimaryBase 
    {
        public string AppName { get; set; }
        public string ClassName { get; set; }       
        public abstract int Save(T item, string sOption);
        public abstract T GetObjList(T item);
        public abstract List<T> GetObjList(T item, int startRowIndex, int maxRow);
      //  public abstract List<T> LoadItemList(T item, DataLoadOption lOption, int startRowIndex, int maxRow);       
        public abstract T GetObjById(Int32 Id);
      
                       
    }
}

using System;
using System.Collections.Generic;
using DAL.Base;

namespace DAQMS.Service
{
    public abstract class BusinessBase<T> : IPrimaryBase
    {

        public string AppName { get; set; }
        public string ClassName { get; set; }

        public abstract int InsertData(T item);
        public abstract int UpdateData(T item);
        public abstract int DeleteData(int Id);
        public abstract List<T> GetAll();
        public abstract List<T> GetByItem(T item);
        public abstract List<T> GetItemByPaging(T item, int startRowIndex, int maxRow);
        public abstract T GetIById(int id);

    }
}

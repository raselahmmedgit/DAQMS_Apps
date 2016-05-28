using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Configuration;
using System;
using DAQMS.DAL;
using DAQMS.Domain.Models;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public  class DropDownService
    {
        public static DropDownService GetInstance()
        {
            return new  DropDownService();
        }
        #region Load Data

        public List<CommonViewModel> GetByItem(CommonViewModel item)
        {
            return CommonDAL.GetInstance().GetObjList(item.TableName, item.Id, item.Name, item.RefId);
        }

        public List<CommonViewModel> GetItemByTableName(string TableName, int RefId=0)
        {
            return CommonDAL.GetInstance().GetObjList(TableName, 0, "", RefId);
        }

        public CommonViewModel GetIById(string TableName, int Id, int RefId = 0)
        {
            return CommonDAL.GetInstance().GetObjList(TableName, Id, "", RefId).FirstOrDefault();
        }

        #endregion
    }
}

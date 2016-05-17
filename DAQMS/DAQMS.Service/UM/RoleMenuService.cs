using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Configuration;
using System;
using DAQMS.DAL;
using DAL.Base;
using DAQMS.Domain.Models;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class RoleMenuService : BusinessBase<RoleMenuViewModel>
    {

        public static RoleMenuService GetInstance()
        {
            return new RoleMenuService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(RoleMenuViewModel item)
        {
            try
            {
                return RoleMenuDAL.GetInstance().Save(item, "I");
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public override int UpdateData(RoleMenuViewModel item)
        {
            try
            {
                return RoleMenuDAL.GetInstance().Save(item, "U");
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override int DeleteData(int Id)
        {
            RoleMenuViewModel item = new RoleMenuViewModel();
            item.Id = Id;
            return RoleMenuDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<RoleMenuViewModel> GetByItem(RoleMenuViewModel item)
        {
            return RoleMenuDAL.GetInstance().GetObjList(item, 0, 10000000);
        }

        public override List<RoleMenuViewModel> GetItemByPaging(RoleMenuViewModel item, int startRowIndex, int maxRow)
        {
            return RoleMenuDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override RoleMenuViewModel GetIById(int Id)
        {
            return RoleMenuDAL.GetInstance().GetObjById(Id);
        }

        public override List<RoleMenuViewModel> GetAll()
        {
           RoleMenuDAL obj = new RoleMenuDAL();
           return obj.GetObjList(new RoleMenuViewModel(), 0, 100000000);
        }

        #endregion

        
    }
}

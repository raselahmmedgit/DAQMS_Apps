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
    public class RoleService : BusinessBase<RoleViewModel>
    {

        public static RoleService GetInstance()
        {
            return new RoleService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(RoleViewModel item)
        {
            try
            {
                return RoleDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(RoleViewModel item)
        {
            try
            {
                return RoleDAL.GetInstance().Save(item, "U");
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
            RoleViewModel item = new RoleViewModel();
            item.Id = Id;
            return RoleDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<RoleViewModel> GetByItem(RoleViewModel item)
        {
            return RoleDAL.GetInstance().GetObjList(item, 0, 10000000);
        }

        public override List<RoleViewModel> GetItemByPaging(RoleViewModel item, int startRowIndex, int maxRow)
        {
            return RoleDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override RoleViewModel GetIById(int Id)
        {
            return RoleDAL.GetInstance().GetObjById(Id);
        }

        public override List<RoleViewModel> GetAll()
        {
            RoleDAL obj = new RoleDAL();
            return obj.GetObjList(new RoleViewModel(), 0, 100000000);
        }

        #endregion

        
    }
}

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
    public class UserRoleService : BusinessBase<UserRoleViewModel>
    {

        public static UserRoleService GetInstance()
        {
            return new UserRoleService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(UserRoleViewModel item)
        {
            try
            {
                return UserRoleDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(UserRoleViewModel item)
        {
            try
            {
                return UserRoleDAL.GetInstance().Save(item, "U");
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
            UserRoleViewModel item = new UserRoleViewModel();
            item.Id = Id;
            return UserRoleDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<UserRoleViewModel> GetByItem(UserRoleViewModel item)
        {
            return UserRoleDAL.GetInstance().GetObjList(item, 0, 10000000);
        }

        public override List<UserRoleViewModel> GetItemByPaging(UserRoleViewModel item, int startRowIndex, int maxRow)
        {
            return UserRoleDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override UserRoleViewModel GetIById(int Id)
        {
            return UserRoleDAL.GetInstance().GetObjById(Id);
        }

        public override List<UserRoleViewModel> GetAll()
        {
           UserRoleDAL obj = new UserRoleDAL();
           return obj.GetObjList(new UserRoleViewModel(), 0, 100000000);
        }

        #endregion

        
    }
}


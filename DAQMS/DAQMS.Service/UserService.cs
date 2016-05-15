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

//namespace DAQMS.Service

namespace DAQMS.Service
{
    public class UserService : BusinessBase<UserViewModel>
    {

        public static UserService GetInstance()
        {
            return new UserService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(UserViewModel item)
        {
            try
            {
                return UserDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(UserViewModel item)
        {
            try
            {
                return UserDAL.GetInstance().Save(item, "U");
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
            UserViewModel item = new UserViewModel();
            item.UserId = Id;
            return UserDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<UserViewModel> GetByItem(UserViewModel item)
        {
            return UserDAL.GetInstance().GetObjList(item, 0, 10000000);
        }

        public override List<UserViewModel> GetItemByPaging(UserViewModel item, int startRowIndex, int maxRow)
        {
            return UserDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override UserViewModel GetIById(int Id)
        {
            return UserDAL.GetInstance().GetObjById(Id);
        }

        public UserViewModel LoadByLoginId(string loginId)
        {

            UserViewModel user = new UserViewModel();
            user.LoginID = loginId;
            return UserDAL.GetInstance().GetObjList(user);
        }

        public override List<UserViewModel> GetAll()
        {
            UserDAL UserDal = new UserDAL();
            return UserDal.GetObjList(new UserViewModel(), 0, 100000000);
        }

        #endregion

        
    }
}

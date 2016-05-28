using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class LoginHistoryService : BusinessBase<LoginHistoryViewModel>
    {

        public static LoginHistoryService GetInstance()
        {
            return new LoginHistoryService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(LoginHistoryViewModel item)
        {
            try
            {
                return LoginHistoryDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(LoginHistoryViewModel item)
        {
            try
            {
                return LoginHistoryDAL.GetInstance().Save(item, "U");
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
            LoginHistoryViewModel item = new LoginHistoryViewModel();
            item.Id = Id;
            return LoginHistoryDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<LoginHistoryViewModel> GetByItem(LoginHistoryViewModel item)
        {
            return LoginHistoryDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<LoginHistoryViewModel> GetItemByPaging(LoginHistoryViewModel item, int startRowIndex, int maxRow)
        {
            return LoginHistoryDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override LoginHistoryViewModel GetIById(int Id)
        {
            return LoginHistoryDAL.GetInstance().GetObjById(Id);
        }

        public override List<LoginHistoryViewModel> GetAll()
        {
           LoginHistoryDAL obj = new LoginHistoryDAL();
           return obj.GetObjList(new LoginHistoryViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}


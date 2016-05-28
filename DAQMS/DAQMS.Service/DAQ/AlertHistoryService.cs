using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class AlertHistoryService : BusinessBase<AlertHistoryViewModel>
    {

        public static AlertHistoryService GetInstance()
        {
            return new AlertHistoryService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(AlertHistoryViewModel item)
        {
            try
            {
                return AlertHistoryDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(AlertHistoryViewModel item)
        {
            try
            {
                return AlertHistoryDAL.GetInstance().Save(item, "U");
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
            AlertHistoryViewModel item = new AlertHistoryViewModel();
            item.Id = Id;
            return AlertHistoryDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<AlertHistoryViewModel> GetByItem(AlertHistoryViewModel item)
        {
            return AlertHistoryDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<AlertHistoryViewModel> GetItemByPaging(AlertHistoryViewModel item, int startRowIndex, int maxRow)
        {
            return AlertHistoryDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override AlertHistoryViewModel GetIById(int Id)
        {
            return AlertHistoryDAL.GetInstance().GetObjById(Id);
        }

        public override List<AlertHistoryViewModel> GetAll()
        {
            AlertHistoryDAL obj = new AlertHistoryDAL();
            return obj.GetObjList(new AlertHistoryViewModel(), 1, 100000000);
        }

        #endregion

    }
}

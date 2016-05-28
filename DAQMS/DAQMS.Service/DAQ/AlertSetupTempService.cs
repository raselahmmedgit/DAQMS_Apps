using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class AlertSetupTempService : BusinessBase<AlertSetupTempViewModel>
    {

        public static AlertSetupTempService GetInstance()
        {
            return new AlertSetupTempService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(AlertSetupTempViewModel item)
        {
            try
            {
                return AlertSetupTempDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(AlertSetupTempViewModel item)
        {
            try
            {
                return AlertSetupTempDAL.GetInstance().Save(item, "U");
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
            AlertSetupTempViewModel item = new AlertSetupTempViewModel();
            item.Id = Id;
            return AlertSetupTempDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<AlertSetupTempViewModel> GetByItem(AlertSetupTempViewModel item)
        {
            return AlertSetupTempDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<AlertSetupTempViewModel> GetItemByPaging(AlertSetupTempViewModel item, int startRowIndex, int maxRow)
        {
            return AlertSetupTempDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override AlertSetupTempViewModel GetIById(int Id)
        {
            return AlertSetupTempDAL.GetInstance().GetObjById(Id);
        }

        public override List<AlertSetupTempViewModel> GetAll()
        {
            AlertSetupTempDAL obj = new AlertSetupTempDAL();
            return obj.GetObjList(new AlertSetupTempViewModel(), 1, 100000000);
        }

        #endregion
    }
}


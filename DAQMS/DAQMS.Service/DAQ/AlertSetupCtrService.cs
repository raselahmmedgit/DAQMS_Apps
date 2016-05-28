using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class AlertSetupCTRService : BusinessBase<AlertSetupCTRViewModel>
    {

        public static AlertSetupCTRService GetInstance()
        {
            return new AlertSetupCTRService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(AlertSetupCTRViewModel item)
        {
            try
            {
                return AlertSetupCtrDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(AlertSetupCTRViewModel item)
        {
            try
            {
                return AlertSetupCtrDAL.GetInstance().Save(item, "U");
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
            AlertSetupCTRViewModel item = new AlertSetupCTRViewModel();
            item.Id = Id;
            return AlertSetupCtrDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<AlertSetupCTRViewModel> GetByItem(AlertSetupCTRViewModel item)
        {
            return AlertSetupCtrDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<AlertSetupCTRViewModel> GetItemByPaging(AlertSetupCTRViewModel item, int startRowIndex, int maxRow)
        {
            return AlertSetupCtrDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override AlertSetupCTRViewModel GetIById(int Id)
        {
            return AlertSetupCtrDAL.GetInstance().GetObjById(Id);
        }

        public override List<AlertSetupCTRViewModel> GetAll()
        {
            AlertSetupCtrDAL obj = new AlertSetupCtrDAL();
            return obj.GetObjList(new AlertSetupCTRViewModel(), 1, 100000000);
        }

        #endregion
    }
}


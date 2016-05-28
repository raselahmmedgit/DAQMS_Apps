using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class AlertSetupMasterService : BusinessBase<AlertSetupMasterViewModel>
    {

        public static AlertSetupMasterService GetInstance()
        {
            return new AlertSetupMasterService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(AlertSetupMasterViewModel item)
        {
            try
            {
                return AlertSetupMasterDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(AlertSetupMasterViewModel item)
        {
            try
            {
                return AlertSetupMasterDAL.GetInstance().Save(item, "U");
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
            AlertSetupMasterViewModel item = new AlertSetupMasterViewModel();
            item.Id = Id;
            return AlertSetupMasterDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<AlertSetupMasterViewModel> GetByItem(AlertSetupMasterViewModel item)
        {
            return AlertSetupMasterDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<AlertSetupMasterViewModel> GetItemByPaging(AlertSetupMasterViewModel item, int startRowIndex, int maxRow)
        {
            return AlertSetupMasterDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override AlertSetupMasterViewModel GetIById(int Id)
        {
            return AlertSetupMasterDAL.GetInstance().GetObjById(Id);
        }

        public override List<AlertSetupMasterViewModel> GetAll()
        {
            AlertSetupMasterDAL obj = new AlertSetupMasterDAL();
            return obj.GetObjList(new AlertSetupMasterViewModel(), 1, 100000000);
        }

        #endregion
    }
}


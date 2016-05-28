using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class DeviceService : BusinessBase<DeviceViewModel>
    {

        public static DeviceService GetInstance()
        {
            return new DeviceService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(DeviceViewModel item)
        {
            try
            {
                return DeviceDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(DeviceViewModel item)
        {
            try
            {
                return DeviceDAL.GetInstance().Save(item, "U");
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
            DeviceViewModel item = new DeviceViewModel();
            item.Id = Id;
            return DeviceDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<DeviceViewModel> GetByItem(DeviceViewModel item)
        {
            return DeviceDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<DeviceViewModel> GetItemByPaging(DeviceViewModel item, int startRowIndex, int maxRow)
        {
            return DeviceDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override DeviceViewModel GetIById(int Id)
        {
            return DeviceDAL.GetInstance().GetObjById(Id);
        }

        public override List<DeviceViewModel> GetAll()
        {
            DeviceDAL obj = new DeviceDAL();
            return obj.GetObjList(new DeviceViewModel(), 1, 100000000);
        }

        #endregion
    }
}

using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class SensorService : BusinessBase<SensorViewModel>
    {

        public static SensorService GetInstance()
        {
            return new SensorService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(SensorViewModel item)
        {
            try
            {
                return SensorDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(SensorViewModel item)
        {
            try
            {
                return SensorDAL.GetInstance().Save(item, "U");
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
            SensorViewModel item = new SensorViewModel();
            item.Id = Id;
            return SensorDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<SensorViewModel> GetByItem(SensorViewModel item)
        {
            return SensorDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<SensorViewModel> GetItemByPaging(SensorViewModel item, int startRowIndex, int maxRow)
        {
            return SensorDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override SensorViewModel GetIById(int Id)
        {
            return SensorDAL.GetInstance().GetObjById(Id);
        }

        public override List<SensorViewModel> GetAll()
        {
            SensorDAL obj = new SensorDAL();
            return obj.GetObjList(new SensorViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}

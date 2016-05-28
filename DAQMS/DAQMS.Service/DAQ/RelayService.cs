using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class RelayService : BusinessBase<RelayViewModel>
    {
        public static RelayService GetInstance()
        {
            return new RelayService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(RelayViewModel item)
        {
            try
            {
                return RelayDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(RelayViewModel item)
        {
            try
            {
                return RelayDAL.GetInstance().Save(item, "U");
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
            RelayViewModel item = new RelayViewModel();
            item.Id = Id;
            return RelayDAL.GetInstance().Save(item, "D");
        }
        #endregion

        #region Load Data

        public override List<RelayViewModel> GetByItem(RelayViewModel item)
        {
            return RelayDAL.GetInstance().GetObjList(item, 1, 10000000);
        }
        public override List<RelayViewModel> GetItemByPaging(RelayViewModel item, int startRowIndex, int maxRow)
        {
            return RelayDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }
        public override RelayViewModel GetIById(int Id)
        {
            return RelayDAL.GetInstance().GetObjById(Id);
        }
        public override List<RelayViewModel> GetAll()
        {
            RelayDAL obj = new RelayDAL();
            return obj.GetObjList(new RelayViewModel(), 1, 100000000);
        }

        #endregion
    }
}

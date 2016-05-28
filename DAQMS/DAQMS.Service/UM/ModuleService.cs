using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class ModuleService : BusinessBase<ModuleViewModel>
    {

        public static ModuleService GetInstance()
        {
            return new ModuleService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(ModuleViewModel item)
        {
            try
            {
                return ModuleDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(ModuleViewModel item)
        {
            try
            {
                return ModuleDAL.GetInstance().Save(item, "U");
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
            ModuleViewModel item = new ModuleViewModel();
            item.Id = Id;
            return ModuleDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<ModuleViewModel> GetByItem(ModuleViewModel item)
        {
            return ModuleDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<ModuleViewModel> GetItemByPaging(ModuleViewModel item, int startRowIndex, int maxRow)
        {
            return ModuleDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override ModuleViewModel GetIById(int Id)
        {
            return ModuleDAL.GetInstance().GetObjById(Id);
        }

        public override List<ModuleViewModel> GetAll()
        {
            ModuleDAL obj = new ModuleDAL();
            return obj.GetObjList(new ModuleViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}

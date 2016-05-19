using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class MenuService : BusinessBase<MenuViewModel>
    {

        public static MenuService GetInstance()
        {
            return new MenuService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(MenuViewModel item)
        {
            try
            {
                return MenuDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(MenuViewModel item)
        {
            try
            {
                return MenuDAL.GetInstance().Save(item, "U");
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
            MenuViewModel item = new MenuViewModel();
            item.Id = Id;
            return MenuDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<MenuViewModel> GetByItem(MenuViewModel item)
        {
            return MenuDAL.GetInstance().GetObjList(item, 0, 10000000);
        }

        public override List<MenuViewModel> GetItemByPaging(MenuViewModel item, int startRowIndex, int maxRow)
        {
            return MenuDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override MenuViewModel GetIById(int Id)
        {
            return MenuDAL.GetInstance().GetObjById(Id);
        }

        public override List<MenuViewModel> GetAll()
        {
           MenuDAL obj = new MenuDAL();
           return obj.GetObjList(new MenuViewModel(), 0, 100000000);
        }

        #endregion

        
    }
}

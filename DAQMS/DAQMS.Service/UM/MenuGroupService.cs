using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class MenuGroupService : BusinessBase<MenuGroupViewModel>
    {

        public static MenuGroupService GetInstance()
        {
            return new MenuGroupService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(MenuGroupViewModel item)
        {
            try
            {
                return MenuGroupDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(MenuGroupViewModel item)
        {
            try
            {
                return MenuGroupDAL.GetInstance().Save(item, "U");
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
            MenuGroupViewModel item = new MenuGroupViewModel();
            item.Id = Id;
            return MenuGroupDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<MenuGroupViewModel> GetByItem(MenuGroupViewModel item)
        {
            return MenuGroupDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<MenuGroupViewModel> GetItemByPaging(MenuGroupViewModel item, int startRowIndex, int maxRow)
        {
            return MenuGroupDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override MenuGroupViewModel GetIById(int Id)
        {
            return MenuGroupDAL.GetInstance().GetObjById(Id);
        }

        public override List<MenuGroupViewModel> GetAll()
        {
            MenuGroupDAL UserDal = new MenuGroupDAL();
            return UserDal.GetObjList(new MenuGroupViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}

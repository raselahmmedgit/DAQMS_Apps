using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class CompanyService : BusinessBase<CompanyViewModel>
    {

        public static CompanyService GetInstance()
        {
            return new CompanyService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(CompanyViewModel item)
        {
            try
            {
                return CompanyDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(CompanyViewModel item)
        {
            try
            {
                return CompanyDAL.GetInstance().Save(item, "U");
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
            CompanyViewModel item = new CompanyViewModel();
            item.Id = Id;
            return CompanyDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<CompanyViewModel> GetByItem(CompanyViewModel item)
        {
            return CompanyDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<CompanyViewModel> GetItemByPaging(CompanyViewModel item, int startRowIndex, int maxRow)
        {
            return CompanyDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override CompanyViewModel GetIById(int Id)
        {
            return CompanyDAL.GetInstance().GetObjById(Id);
        }

        public override List<CompanyViewModel> GetAll()
        {
            CompanyDAL obj = new CompanyDAL();
            return obj.GetObjList(new CompanyViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}

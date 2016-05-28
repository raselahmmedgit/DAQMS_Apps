using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class ContactService : BusinessBase<ContactViewModel>
    {

        public static ContactService GetInstance()
        {
            return new ContactService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(ContactViewModel item)
        {
            try
            {
                return ContactDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(ContactViewModel item)
        {
            try
            {
                return ContactDAL.GetInstance().Save(item, "U");
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
            ContactViewModel item = new ContactViewModel();
            item.Id = Id;
            return ContactDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<ContactViewModel> GetByItem(ContactViewModel item)
        {
            return ContactDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<ContactViewModel> GetItemByPaging(ContactViewModel item, int startRowIndex, int maxRow)
        {
            return ContactDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override ContactViewModel GetIById(int Id)
        {
            return ContactDAL.GetInstance().GetObjById(Id);
        }

        public override List<ContactViewModel> GetAll()
        {
            ContactDAL obj = new ContactDAL();
            return obj.GetObjList(new ContactViewModel(), 1, 100000000);
        }

        public List<ProjectContactViewModel> GetProjectContactList(int ContactId)
        {
            ProjectContactDAL obj = new ProjectContactDAL();
            return obj.GetObjList(0, 0, 0, ContactId, "", 1, 100000);
        }

        #endregion
    }
}

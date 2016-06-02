using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class ProjectContactService : BusinessBase<ProjectContactViewModel>
    {

        public static ProjectContactService GetInstance()
        {
            return new ProjectContactService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(ProjectContactViewModel item)
        {
            try
            {
                return ProjectContactDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(ProjectContactViewModel item)
        {
            try
            {
                return ProjectContactDAL.GetInstance().Save(item, "U");
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
            ProjectContactViewModel item = new ProjectContactViewModel();
            item.Id = Id;
            return ProjectContactDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<ProjectContactViewModel> GetByItem(ProjectContactViewModel item)
        {
            return ProjectContactDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<ProjectContactViewModel> GetItemByPaging(ProjectContactViewModel item, int startRowIndex, int maxRow)
        {
            return ProjectContactDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override ProjectContactViewModel GetIById(int Id)
        {
            return ProjectContactDAL.GetInstance().GetObjById(Id);
        }

        public override List<ProjectContactViewModel> GetAll()
        {
            ProjectContactDAL obj = new ProjectContactDAL();
            return obj.GetObjList(new ProjectContactViewModel(), 1, 100000000);
        }

        public List<ProjectContactViewModel> GetProjectContactList(int ContactId)
        {
            ProjectContactDAL obj = new ProjectContactDAL();
            return obj.GetObjList(0, 0, 0, ContactId, "", 1, 100000);
        }

        #endregion
    }
}

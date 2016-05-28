using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

namespace DAQMS.Service
{
    public class ProjectService : BusinessBase<ProjectViewModel>
    {

        public static ProjectService GetInstance()
        {
            return new ProjectService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(ProjectViewModel item)
        {
            try
            {
                return ProjectDAL.GetInstance().Save(item, "I");
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

        public override int UpdateData(ProjectViewModel item)
        {
            try
            {
                return ProjectDAL.GetInstance().Save(item, "U");
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
            ProjectViewModel item = new ProjectViewModel();
            item.Id = Id;
            return ProjectDAL.GetInstance().Save(item, "D");
        }

        #endregion

        #region Load Data

        public override List<ProjectViewModel> GetByItem(ProjectViewModel item)
        {
            return ProjectDAL.GetInstance().GetObjList(item,1, 10000000);
        }

        public override List<ProjectViewModel> GetItemByPaging(ProjectViewModel item, int startRowIndex, int maxRow)
        {
            return ProjectDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public override ProjectViewModel GetIById(int Id)
        {
            return ProjectDAL.GetInstance().GetObjById(Id);
        }

        public override List<ProjectViewModel> GetAll()
        {
            ProjectDAL obj = new ProjectDAL();
            return obj.GetObjList(new ProjectViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}

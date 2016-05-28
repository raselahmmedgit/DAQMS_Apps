using System.Collections.Generic;
using System;
using DAQMS.DAL;
using DAQMS.DAL.Base;
using DAQMS.DomainViewModel;

//namespace DAQMS.Service

namespace DAQMS.Service
{
    public class UserRightService : BusinessBase<UserRightViewModel>
    {

        public static UserRightService GetInstance()
        {
            return new UserRightService();
        }

        #region Operation
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override int InsertData(UserRightViewModel item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public override int UpdateData(UserRightViewModel item)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override int DeleteData(int Id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Load Data

        public override List<UserRightViewModel> GetByItem(UserRightViewModel item)
        {
            return UserRightDAL.GetInstance().GetObjList(item, 1, 10000000);
        }

        public override List<UserRightViewModel> GetItemByPaging(UserRightViewModel item, int startRowIndex, int maxRow)
        {
            return UserRightDAL.GetInstance().GetObjList(item, startRowIndex, maxRow);
        }

        public  List<UserRightViewModel> GetItemByUser(UserViewModel user) 
        {
            return UserRightDAL.GetInstance().GetObjList(0, user.Id, user.LoginID, 1, 10000000);
        }

        public override UserRightViewModel GetIById(int Id)
        {
            return UserRightDAL.GetInstance().GetObjById(Id);
        }

        public override List<UserRightViewModel> GetAll()
        {
            UserRightDAL obj = new UserRightDAL();
            return obj.GetObjList(new UserRightViewModel(), 1, 100000000);
        }

        #endregion

        
    }
}


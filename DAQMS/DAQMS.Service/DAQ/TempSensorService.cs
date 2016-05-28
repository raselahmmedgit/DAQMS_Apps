using DAQMS.DAL;
using DAQMS.DomainViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAQMS.Service
{
    public class TempSensorService
    {
        public static TempSensorService GetInstance()
        {
            return new TempSensorService();
        }
        #region Load Data
        public List<TempSensorViewModel> GetByItem(TempSensorViewModel item)
        {
            return TempSensorDAL.GetInstance().GetObjList(item.CompanyId, item.ProjectId, item.DeviceId, item.DateRangeFrom, item.DateRangeTo, item.ValueType, item.LoginUserID, 1, 999999999);
        }

        public List<TempSensorViewModel> GetItemByPaging(TempSensorViewModel item, int startRowIndex, int maxRow)
        {
            return TempSensorDAL.GetInstance().GetObjList(item.CompanyId, item.ProjectId, item.DeviceId, item.DateRangeFrom, item.DateRangeTo, item.ValueType, item.LoginUserID, startRowIndex, maxRow);
        }

        #endregion
    }
}

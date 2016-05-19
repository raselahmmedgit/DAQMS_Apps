using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAQMS.DAL.Base
{
   
        public enum DataLoadOption
        {
            LoadAll =1,
            LoadByCraiteria =2,
            LoadForTotalCount = 3,
        }
        public enum DataSaveOption
        {
            Insert =1,
            Update = 2,
            Delete=3,
        }
    
}

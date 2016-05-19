using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAQMS.DAL.Base
{
    public interface IPrimaryBase
    {
        string AppName
        {
            get;
            set;
        }          
    }
}

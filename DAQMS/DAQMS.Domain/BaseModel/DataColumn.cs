using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAQMS.Domain
{
    public class DataColumn : System.Attribute
    {
        private bool bolIsDataColumn = false;

        public DataColumn(bool status)
        {
            this.bolIsDataColumn = status;
        }

        public bool IsDataColumn()
        {
            return bolIsDataColumn;
        }
    }
}

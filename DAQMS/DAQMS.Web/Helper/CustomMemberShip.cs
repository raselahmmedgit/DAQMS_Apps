using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  Code
{
    public class CustomMemberShip
    {
        public static CustomMembershipProvider Provider
        {
            get { return new CustomMembershipProvider(); }
        }
    }
}

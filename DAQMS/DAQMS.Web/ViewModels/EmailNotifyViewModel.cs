using DAQMS.DomainViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAQMS.Web
{
    public class EmailNotifyViewModel
    {
        public UserViewModel UserViewModel { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        public DateTime EmailDate { get; set; }
    }
}
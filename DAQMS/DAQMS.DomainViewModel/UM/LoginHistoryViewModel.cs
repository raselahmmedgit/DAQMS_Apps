using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace DAQMS.DomainViewModel
{
    public class LoginHistoryViewModel : LoginHisory
    {
        [Display(Name = "Login ID")]
        [DataMember, DataColumn(true)]
        public string LoginID { get; set; }

        [Display(Name = "User Name")]
        [DataMember, DataColumn(true)]
        public string UserName { get; set; }

        public System.DateTime? LoginDate { get; set; }
    }
}

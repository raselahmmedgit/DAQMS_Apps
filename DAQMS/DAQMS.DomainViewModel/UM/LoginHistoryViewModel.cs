using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace DAQMS.DomainViewModel
{
    public class LoginHisoryViewModel : LoginHisory
    {
        [Display(Name = "Menu Group")]
        [DataMember, DataColumn(true)]
        public string LoginID { get; set; }

    }
}

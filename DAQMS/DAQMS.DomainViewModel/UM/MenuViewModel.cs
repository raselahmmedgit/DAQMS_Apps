using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace DAQMS.DomainViewModel
{
    public class MenuViewModel : Menu
    {
        [Display(Name = "Module Name")]
        [DataMember, DataColumn(true)]
        public string ModuleName { get; set; }

       [Display(Name = "Group Name")]
       [DataMember, DataColumn(true)]
        public string GroupName { get; set; }

       [Display(Name = "Parent Menu Name")]
       [DataMember, DataColumn(true)]
       public string ParentMenuName { get; set; }

       [Display(Name = "Parent Menu Caption")]
       [DataMember, DataColumn(true)]
       public string ParentMenuCaption { get; set; }
        
       
    }
}

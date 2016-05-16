
using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace DAQMS.DomainViewModel
{
    public class RoleMenuViewModel : RoleMenu
    {
        [Display(Name = "Menu Group")]
        [DataMember, DataColumn(true)]
        public string MenuGroup { get; set; }

        [Display(Name = "Menu Name")]
        [DataMember, DataColumn(true)]
        public string MenuName { get; set; }

        [Display(Name = "Menu Caption")]
        [DataMember, DataColumn(true)]
        public string MenuCaption { get; set; }

        [Display(Name = "Role Name")]
        [DataMember, DataColumn(true)]
        public string RoleName { get; set; }

        [Display(Name = "Module Name")]
        [DataMember, DataColumn(true)]
        public int ModuleId { get; set; }

    }
}

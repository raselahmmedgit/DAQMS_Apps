using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DAQMS.DomainViewModel
{
    public class MenuViewModel : Menu
    {
        public MenuViewModel()
        {
            this.ModuleList = new List<SelectListItem>();
            this.MenuGroupList = new List<SelectListItem>();
        }

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


       public IList<SelectListItem> ModuleList { get; set; }

       public IList<SelectListItem> MenuGroupList { get; set; }
       
    }
}

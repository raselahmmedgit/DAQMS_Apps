using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class RoleViewModel : Role
    {
        public RoleViewModel()
        {
            this.RoleMenuList = new List<RoleMenuViewModel>();
            this.ModuleList = new List<SelectListItem>();           
        }

        [Display(Name = "Module Name")]
        [DataMember, DataColumn(true)]
        public string ModuleName { get; set; }

        public IList<SelectListItem> ModuleList { get; set; }

        public virtual IList<RoleMenuViewModel> RoleMenuList { get; set; }

    }
}

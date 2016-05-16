using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DAQMS.DomainViewModel
{
    public class MenuGroupViewModel : MenuGroup
    {

        public MenuGroupViewModel()
        {
            this.ModuleList = new List<SelectListItem>();
        }


        [DataMember, DataColumn(true)]
        public string ModuleName { get; set; }

        public IList<SelectListItem> ModuleList { get; set; }
    }
}

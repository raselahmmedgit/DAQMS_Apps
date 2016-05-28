using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class ProjectViewModel : Project
    {
        public ProjectViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectStatusList = new List<SelectListItem>();     
        }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [Display(Name = "Project Status")]
        [DataMember, DataColumn(true)]
        public string ProjectStatus { get; set; }

        [Display(Name = "Device List")]
        [DataMember, DataColumn(true)]
        public string ProjectDevices { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectStatusList { get; set; }


    }
}


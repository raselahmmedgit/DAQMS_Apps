using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class DeviceViewModel : Device
    {
        public DeviceViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectList = new List<SelectListItem>();
            this.DeviceStatusList = new List<SelectListItem>(); 
        }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public int CompanyId { get; set; }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public string ProjectName { get; set; }

        [Display(Name = "Device Status")]
        [DataMember, DataColumn(true)]
        public string DeviceStatus { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectList { get; set; }
        public IList<SelectListItem> DeviceStatusList { get; set; }


    }
}



using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class AlertHistoryViewModel : AlertHistory
    {
        public AlertHistoryViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectList = new List<SelectListItem>();
            this.AlertTypeList = new List<SelectListItem>(); 
        }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public int CompanyId { get; set; }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public string ProjectName { get; set; }

        [Display(Name = "Device ID")]
        [DataMember, DataColumn(true)]
        public string DeviceID { get; set; }

        [Display(Name = "Contact Name")]
        [DataMember, DataColumn(true)]
        public string ContactName { get; set; }

        [Display(Name = "Alert Type")]
        [DataMember, DataColumn(true)]
        public string AlertType { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectList { get; set; }
        public IList<SelectListItem> AlertTypeList { get; set; }
        public IList<SelectListItem> AlertStatusList { get; set; }
    }
}

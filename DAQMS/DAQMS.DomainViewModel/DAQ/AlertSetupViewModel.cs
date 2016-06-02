using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class AlertSetupMasterViewModel : AlertSetupMaster
    {
        public AlertSetupMasterViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectList = new List<SelectListItem>();
            this.ContactNameList = new List<SelectListItem>();

            this.AlertSetupTempList = new List<AlertSetupTempViewModel>();
            this.AlertSetupCTRList = new List<AlertSetupCTRViewModel>();
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

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public string ContactName { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectList { get; set; }
        public IList<SelectListItem> ContactNameList { get; set; }

        public virtual IList<AlertSetupTempViewModel> AlertSetupTempList { get; set; }

        public virtual IList<AlertSetupCTRViewModel> AlertSetupCTRList { get; set; }
    }


    public class AlertSetupTempViewModel : AlertSetupTemp
    {
        [Display(Name = "Sensor")]
        [MaxLength(20)]
        [DataMember, DataColumn(true)]
        public string SensorName { get; set; }
    }

    public class AlertSetupCTRViewModel : AlertSetupCTR
    {
        [Display(Name = "CTR")]
        [MaxLength(20)]
        [DataMember, DataColumn(true)]
        public string SensorName { get; set; }
    }
}



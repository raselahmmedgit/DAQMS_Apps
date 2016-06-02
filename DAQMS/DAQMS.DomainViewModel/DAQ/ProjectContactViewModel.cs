using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace DAQMS.DomainViewModel
{
    public class ProjectContactViewModel : ProjectContact
    {
        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public int CompanyId { get; set; }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public string ProjectName { get; set; }

        [Display(Name = "Contact Name")]
        [DataMember, DataColumn(true)]
        public string ContactName { get; set; }

        [Display(Name = "Device List")]
        [DataMember, DataColumn(true)]
        public string DeviceList { get; set; }

        [Display(Name = "Select")]
        [DataMember, DataColumn(true)]
        public Boolean IsSelected { get; set; }


    }
}




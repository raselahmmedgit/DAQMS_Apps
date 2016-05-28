﻿using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class RelayViewModel : Relay
    {
        public RelayViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectList = new List<SelectListItem>();
            this.DeviceStateList = new List<SelectListItem>();
            this.DeviceIDList = new List<SelectListItem>();
            this.RelayStateList = new List<SelectListItem>(); 
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

        [Display(Name = "Device Status")]
        [DataMember, DataColumn(true)]
        public string DeviceStatus { get; set; }

        [Display(Name = "Relay State")]
        [DataMember, DataColumn(true)]
        public string RelayState { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectList { get; set; }
        public IList<SelectListItem> DeviceStateList { get; set; }
        public IList<SelectListItem> DeviceIDList { get; set; }
        public IList<SelectListItem> RelayStateList { get; set; }
    }
}




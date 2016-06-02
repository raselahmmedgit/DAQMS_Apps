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
    public class TempSensorViewModel : TempSensor
    {
        public TempSensorViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectList = new List<SelectListItem>();
            this.DeviceList = new List<SelectListItem>();
            this.ValueTypeList = new List<SelectListItem>();
            this.SensorList = new List<SelectListItem>();
        }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public int CompanyId { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public int ProjectId { get; set; }

        [Display(Name = "Device Name")]
        [DataMember, DataColumn(true)]
        public int DeviceId { get; set; }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public string ProjectName { get; set; }

        [Display(Name = "Device Name")]
        [DataMember, DataColumn(true)]
        public string DeviceName { get; set; }

        [Display(Name = "From Date")]
        [DataMember, DataColumn(true)]
        public string DateRangeFrom { get; set; }

        [Display(Name = "To Date")]
        [DataMember, DataColumn(true)]
        public string DateRangeTo { get; set; }

        [Display(Name = "Temp Sensor Value")]
        [DataMember, DataColumn(true)]
        public string ValueType { get; set; }

        [Display(Name = "Temp Sensor")]
        [DataMember, DataColumn(true)]
        public string Sensor { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectList { get; set; }
        public IList<SelectListItem> DeviceList { get; set; }
        public IList<SelectListItem> ValueTypeList { get; set; }
        public IList<SelectListItem> SensorList { get; set; }

    }
}



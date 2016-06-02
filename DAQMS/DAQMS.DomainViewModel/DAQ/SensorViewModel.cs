using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class SensorViewModel : Sensor
    {
        [Display(Name = "Table Name")]
        [DataMember, DataColumn(true)]
        public string TableName { get; set; }
    }
}


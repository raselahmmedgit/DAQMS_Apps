using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace DAQMS.Domain.Models
{
    [Table("daqdatats", Schema = "App")]
    public class TempSensor : BaseModel
    {

        [Display(Name = "Record Date")]
        [DataMember, DataColumn(true)]
        public DateTime RecordDate { get; set; }

        [Display(Name = "Device ID")]
        [DataMember, DataColumn(true)]
        public string strDeviceID { get; set; }

        [Display(Name = "T1")]
        [DataMember, DataColumn(true)]
        public Int32 T1 { get; set; }

        [Display(Name = "T2")]
        [DataMember, DataColumn(true)]
        public Int32 T2 { get; set; }

        [Display(Name = "T3")]
        [DataMember, DataColumn(true)]
        public Int32 T3 { get; set; }

        [Display(Name = "T4")]
        [DataMember, DataColumn(true)]
        public Int32 T4 { get; set; }

        [Display(Name = "T5")]
        [DataMember, DataColumn(true)]
        public Int32 T5 { get; set; }

        [Display(Name = "T6")]
        [DataMember, DataColumn(true)]
        public Int32 T6 { get; set; }

        [Display(Name = "T7")]
        [DataMember, DataColumn(true)]
        public Int32 T7 { get; set; }

        [Display(Name = "T8")]
        [DataMember, DataColumn(true)]
        public Int32 T8 { get; set; }

    }

}

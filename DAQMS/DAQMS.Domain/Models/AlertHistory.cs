using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAQMS.Domain.Models
{
    [Table("AlertHistory", Schema = "App")]
    public class AlertHistory : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ContactId { get; set; }

        [Display(Name = "Device ID")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 DeviceId { get; set; }

        [Display(Name = "Alert Type")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 AlertTypeId { get; set; }

        [Display(Name = "Alert Message")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String AlertMsg { get; set; }

        [Display(Name = "Is Unead?")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsNew { get; set; }

        [Display(Name = "Alert Time")]
        [Required]
        [DataMember, DataColumn(true)]
        public DateTime GenerateTimestamp { get; set; }

        [Display(Name = "Alert Time")]
        [Required]
        [DataMember, DataColumn(true)]
        public DateTime ReadTimestamp { get; set; }

    }


}


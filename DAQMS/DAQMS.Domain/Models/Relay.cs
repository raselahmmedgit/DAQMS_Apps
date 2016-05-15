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
    [Table("Relay", Schema = "App")]
    public class Relay : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Device ID")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 DeviceId { get; set; }

        [Display(Name = "Desired State")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String DesiredState { get; set; }

        [Display(Name = "Status")]
        [Required]
        [Range(0,1)]
        [DataMember, DataColumn(true)]
        public Int32 Status { get; set; }

        // 1= ON, 0=OFF

        [Display(Name = "Setup Time")]
        [DataMember, DataColumn(true)]
        public DateTime SetupTime { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public string Notes { get; set; }

    }


}


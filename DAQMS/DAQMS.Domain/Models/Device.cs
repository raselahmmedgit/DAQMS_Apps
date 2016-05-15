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
    [Table("Device", Schema = "App")]
    public class Device : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Project Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ProjectId { get; set; }

        [Display(Name = "Device ID")]
        [MaxLength(10)]
        [Required]
        [DataMember, DataColumn(true)]
        public string DevicetID { get; set; }

        [Display(Name = "Site Name")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String SiteName { get; set; }

        [Display(Name = "Farmeware Version")]
        [Required]
        [MaxLength(10)]
        [DataMember, DataColumn(true)]
        public String FarmwareVersion { get; set; }

        [Display(Name = "DAQ Location")]
        [MaxLength(30)]
        [DataMember, DataColumn(true)]
        public String DaqLocation { get; set; }

        [Display(Name = "Device Status")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 DeviceStatusId { get; set; }

    }

    [Table("DeviceStatus", Schema = "App")]
    public class DeviceStatus : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Device Status")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String StatusName { get; set; }
    }


}


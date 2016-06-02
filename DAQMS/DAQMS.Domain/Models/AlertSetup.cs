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
    [Table("AlertSetupMaster", Schema = "App")]
    public class AlertSetupMaster : BaseModel
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

        [Display(Name = "Project Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ProjectId { get; set; }

        [Display(Name = "Enable Email Notification")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsEmailNotification { get; set; }

        [Display(Name = "Other Email Address")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public string AdditionalEmail { get; set; }

        [Display(Name = "Enable Tempurature Sensor Alert")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsActiveTemp { get; set; }

        [Display(Name = "Enable CTR Sensor Alert")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsActiveCTR { get; set; }

        [Display(Name = "Enable NO_DATA_FOUND Alert")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsActiveNoDataFound { get; set; }

        [Display(Name = "Length of Duration (Minutes)")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 NoDataFoundDuration { get; set; }

    }

    [Table("AlertType", Schema = "App")]
    public class AlertType : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Alert Type")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String AlertTypeName { get; set; }
    }

    [Table("AlertSetupTemp", Schema = "App")]
    public class AlertSetupTemp : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Alert Master Id")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 AlertSetupId { get; set; }

        [Display(Name = "Sensor")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 SensorId { get; set; }

        [Display(Name = "Is Active?")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsActive { get; set; }

        [Display(Name = "Low Temp.)")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 LowTemp { get; set; }

        [Display(Name = "High Temp.)")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 HighTemp { get; set; }

    }


    [Table("AlertSetupCTR", Schema = "App")]
    public class AlertSetupCTR : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Alert Master Id")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 AlertSetupId { get; set; }

        [Display(Name = "Sensor")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 SensorId { get; set; }

        [Display(Name = "Is Active?")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean IsActive { get; set; }

        [Display(Name = "Maximum Value)")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 MaxVal { get; set; }

        [Display(Name = "Length of Duration (Minutes)")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 LengthOfTime { get; set; }

    }


}


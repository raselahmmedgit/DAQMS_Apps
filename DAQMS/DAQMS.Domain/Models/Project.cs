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
    [Table("Project", Schema = "App")]
    public class Project : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 CompanyId { get; set; }

        [Display(Name = "Project Name")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String ProjectName { get; set; }

        [Display(Name = "Project Address")]
        [Required]
        [MaxLength(30)]
        [DataMember, DataColumn(true)]
        public String Address { get; set; }

        [Display(Name = "Project City")]
        [MaxLength(20)]
        [DataMember, DataColumn(true)]
        public String City { get; set; }

        [Display(Name = "Project State")]
        [Required]
        [MaxLength(20)]
        [DataMember, DataColumn(true)]
        public String State { get; set; }

        [Display(Name = "Zip Code")]
        [Required]
        [MaxLength(10)]
        [DataMember, DataColumn(true)]
        public String Zip { get; set; }

        [Display(Name = "Number of Unit")]
        [Range(1, 50)]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 NumberOfUnit { get; set; }

        [Display(Name = "Monitoring Rate")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [Range(1, 5000)]
        [Required]
        [DataMember, DataColumn(true)]
        public Decimal MonitoringRate { get; set; }

        [Display(Name = "Project Status")]       
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ProjectStatusId { get; set; }
    }

    [Table("ProjectStatus", Schema = "App")]
    public class ProjectStatus : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Project Status")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String Status { get; set; }
    }
}


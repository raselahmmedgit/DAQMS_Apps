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
    [Table("ProjectContact", Schema = "App")]
    public class ProjectContact : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Project")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ProjectId { get; set; }

        [Display(Name = "Contact")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ContactId { get; set; }

    }


}


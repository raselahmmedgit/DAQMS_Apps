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
    [Table("Role", Schema = "App")]
    public class Role : BaseModel
    {

        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Role Name")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String RoleName { get; set; }

        [Display(Name = "Module Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ModuleId { get; set; }

    }
}

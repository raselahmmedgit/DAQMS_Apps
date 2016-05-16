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
    [Table("UserRole", Schema = "App")]
    public class UserRole : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "User Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 UserId { get; set; }

        [Display(Name = "Role Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 RoleId { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAQMS.Domain.Models
{
    [Table("UserRole", Schema = "App")]
    public class UserRole : BaseNotMapModel
    {
        [Key]
        public Int32 UserRoleId { get; set; }

        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}

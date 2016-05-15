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
    [Table("LoginHisory", Schema = "App")]
    public class LoginHisory : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Login ID")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 UserId { get; set; }

        [Display(Name = "Login Time")]
        [Required]
        [DataMember, DataColumn(true)]
        public DateTime LoginTimestamp { get; set; }

        [Display(Name = "Logout Time")]
        [DataMember, DataColumn(true)]
        public DateTime? LogoutTimestamp { get; set; }

    }
}


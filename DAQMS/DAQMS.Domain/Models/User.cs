using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace DAQMS.Domain.Models
{
   // [Table("Users", Schema = "App")]
     [DataContract]
    public class User : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 UserId { get; set; }

        [Required(ErrorMessage = "Login ID is required")]
        [Display(Name = "Login ID")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String LoginID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String UserPass { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String UserName { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        [Display(Name = "User Email")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "Contact ID is required")]
        [Display(Name = "Contact ID")]
        [MaxLength(10)]
        [DataMember, DataColumn(true)]
        public String ContactID { get; set; }

        [Required]
        [Display(Name = "Is Admin User?")]
        [DataMember, DataColumn(true)]
        public Boolean IsAdmin { get; set; }

        [Required]
        [Display(Name = "Is Active User?")]
        [DataMember, DataColumn(true)]
        public Boolean IsActive { get; set; }

        [Required]
        [Display(Name = "Is Locked User?")]
        [DataMember, DataColumn(true)]
        public Boolean IsLocked { get; set; }

        [Required]
        [Display(Name = "User must change password next login")]
        [DataMember, DataColumn(true)]
        public Boolean IsChangePassword { get; set; }

        [Display(Name = "Last Locked")]
        [MaxLength(10)]
        [DataMember, DataColumn(true)]
        public DateTime? LastLockedDateTime { get; set; }

     
    }
}

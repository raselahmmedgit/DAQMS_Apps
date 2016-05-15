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
    [Table("RoleMenu", Schema = "App")]
    public class RoleMenu : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Role Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 RoleId { get; set; }

        [Display(Name = "Menu Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 MenuId { get; set; }

        [Display(Name = "Add New")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean OptAdd { get; set; }

        [Display(Name = "Update")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean OptUpdate { get; set; }

        [Display(Name = "Delete")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean OptDelete { get; set; }

        [Display(Name = "View")]
        [Required]
        [DataMember, DataColumn(true)]
        public Boolean OptView { get; set; }

    }
}

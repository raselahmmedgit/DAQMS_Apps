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
    [Table("MenuGroup", Schema = "App")]
    public class MenuGroup : BaseModel
    {

        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Module Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ModuleId { get; set; }

        [Display(Name = "Group Name")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String GroupName  { get; set; }

        [Display(Name = "Serial Number")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 SerialNo { get; set; }


    }
}

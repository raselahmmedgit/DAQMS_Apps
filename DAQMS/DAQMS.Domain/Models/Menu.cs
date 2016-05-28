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
    [Table("Menu", Schema = "App")]
    public class Menu : BaseModel
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

        [Display(Name = "Menu Group Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 MenuGroupId { get; set; }

        [Display(Name = "Menu Name")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String MenuName { get; set; }

        [Display(Name = "Menu Caption")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String MenuCaption { get; set; }

        [Display(Name = "Parent Menu Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32? ParentMenuId { get; set; }

        [Display(Name = "Serial Number")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 SerialNo { get; set; }

        [Display(Name = "Page URL")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String PageUrl { get; set; }

       

    }
}

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
    [Table("Contact", Schema = "App")]
    public class Contact : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Contact ID")]
        [MaxLength(100)]
        [Required]
        [DataMember, DataColumn(true)]
        public string ContactID { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String ContactName { get; set; }

        [Display(Name = "Contact Type")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ContactTypeId { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 CompanyId { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String Notes { get; set; }

        [Display(Name = "Work Phone")]
        [Required]
        [MaxLength(15)]
        [DataMember, DataColumn(true)]
        public String WorkPhone { get; set; }

        [Display(Name = "Cell Phone")]
        [MaxLength(15)]
        [DataMember, DataColumn(true)]
        public String CellPhone { get; set; }

        [Display(Name = "Email")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String Email { get; set; }

        [Display(Name = "Is Create User Automatically?")]
        [DataMember, DataColumn(true)]
        public Boolean IsCreateUser { get; set; }
    }

    [Table("ProjectStatus", Schema = "App")]
    public class ContactType : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Contact Type")]
        [Required]
        [MaxLength(50)]
        [DataMember, DataColumn(true)]
        public String TypeName { get; set; }
    }

    [Table("ProjectContact", Schema = "App")]
    public class ProjectContact : BaseModel
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
        [DataMember, DataColumn(true)]
        public Int32 ProjectId { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        [DataMember, DataColumn(true)]
        public Int32 ContactId { get; set; }
    }

}


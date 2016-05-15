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
    [Table("Company", Schema = "App")]
    public class Company : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String CompanyName { get; set; }

        [Display(Name = "Company Phone")]
        [Required]
        [MaxLength(15)]
        [DataMember, DataColumn(true)]
        public String Phone { get; set; }

        [Display(Name = "Company Fax")]
        [MaxLength(15)]
        [DataMember, DataColumn(true)]
        public String Fax { get; set; }

        [Display(Name = "Company Email")]
        [Required]
        [MaxLength(100)]
        [DataMember, DataColumn(true)]
        public String Email { get; set; }


    }
}

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
    [Table("Sensor", Schema = "App")]
    public class Sensor : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        [DataMember, DataColumn(true)]
        public Int32 Id { get; set; }

        [Display(Name = "Sensor Name")]
        [MaxLength(10)]
        [Required]
        [DataMember, DataColumn(true)]
        public string SensorName { get; set; }
 
    }
}

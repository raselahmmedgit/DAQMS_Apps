using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;

namespace DAQMS.DomainViewModel
{
    public class MenuGroupViewModel : MenuGroup
    {
        [DataMember, DataColumn(true)]
        public string ModuleName { get; set; }
       
    }
}

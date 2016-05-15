using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;

namespace DAQMS.DomainViewModel
{
    [NotMapped]
    public class UserViewModel : User
    {
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [DataMember, DataColumn(true)]
        public string ContactName { get; set; }

        public string UserPhotoPath { get; set; }

        public string FullName { get; set; }
    }
}

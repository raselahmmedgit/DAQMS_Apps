using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;

namespace DAQMS.DomainViewModel
{
    [NotMapped]
    public class UserViewModel : User
    {
        public string UserPhotoPath { get; set; }

        public string FullName { get; set; }
    }
}

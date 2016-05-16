using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    [NotMapped]
    public class UserViewModel : User
    {

        public UserViewModel()
        {
            this.UserRoleList = new List<UserRoleViewModel>();
        }

        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [DataMember, DataColumn(true)]
        public string ContactName { get; set; }

        public string UserPhotoPath { get; set; }

        public string FullName { get; set; }

        public virtual IList<UserRoleViewModel> UserRoleList { get; set; }
    }
}

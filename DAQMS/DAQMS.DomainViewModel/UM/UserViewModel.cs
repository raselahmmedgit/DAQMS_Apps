using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


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

        [Display(Name = "Confirm Password")]
        [MaxLength(100)]
        [Compare("UserPass", ErrorMessage = "Confirm password doesn't match, Please type again !")]
        public string UserConfirmPassword { get; set; }

        public virtual IList<UserRoleViewModel> UserRoleList { get; set; }
    }
}

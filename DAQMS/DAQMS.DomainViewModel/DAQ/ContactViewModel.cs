using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DAQMS.DomainViewModel
{
    public class ContactViewModel : Contact
    {
        public ContactViewModel()
        {
            this.CompanyList = new List<SelectListItem>();
            this.ProjectList = new List<SelectListItem>();
            this.ContactTypeList = new List<SelectListItem>();

            this.ProjectContactList = new List<ProjectContactViewModel>();
        }

        [Display(Name = "Company Name")]
        [DataMember, DataColumn(true)]
        public string CompanyName { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        [DataMember, DataColumn(true)]
        public string ProjectName { get; set; }

        [Display(Name = "Contact Type")]
        [DataMember, DataColumn(true)]
        public string ContactType { get; set; }

        public IList<SelectListItem> CompanyList { get; set; }
        public IList<SelectListItem> ProjectList { get; set; }
        public IList<SelectListItem> ContactTypeList { get; set; }

        public virtual IList<ProjectContactViewModel> ProjectContactList { get; set; }
    }
}



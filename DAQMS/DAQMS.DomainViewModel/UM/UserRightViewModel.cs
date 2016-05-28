using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace DAQMS.DomainViewModel
{
    public class UserRightViewModel : Menu
    {
       public UserRightViewModel()
        {
            this.ApplicationId = 1;
            this.ApplicationName = "DAQ Managment System";
        }

       [DataMember, DataColumn(true)]
       public int UserId { set; get; }

       public int ApplicationId { set; get; }
      
       [Display(Name = "IconClass")]
       [MaxLength(100)]
       [DataMember, DataColumn(true)]
       public String IconClass { get; set; }

       [DataMember, DataColumn(true)]
       public string ApplicationName { set; get; }

       [DataMember, DataColumn(true)]
       public string ModuleName { set; get; }

       [DataMember, DataColumn(true)]
       public string MenuGroupName { set; get; }

       [Display(Name = "Controller Name")]
       [DataMember, DataColumn(true)]
       public string ControllerName { get; set; }

       [Display(Name = "Action Name")]
       [DataMember, DataColumn(true)]
       public string ActionName { get; set; }

       [Display(Name = "Action Params")]
       [DataMember, DataColumn(true)]
       public string ActionParam { get; set; }

       [Display(Name = "Parent Menu Name")]
       [DataMember, DataColumn(true)]
       public string ParentMenuName { get; set; }

       [Display(Name = "Parent Menu Caption")]
       [DataMember, DataColumn(true)]
       public string ParentMenuCaption { get; set; }

       [Display(Name = "Add New")]
       [Required]
       [DataMember, DataColumn(true)]
       public Boolean OptAdd { get; set; }

       [Display(Name = "Update")]
       [Required]
       [DataMember, DataColumn(true)]
       public Boolean OptUpdate { get; set; }

       [Display(Name = "Delete")]
       [Required]
       [DataMember, DataColumn(true)]
       public Boolean OptDelete { get; set; }

       [Display(Name = "View")]
       [Required]
       [DataMember, DataColumn(true)]
       public Boolean OptView { get; set; }

       [DataMember, DataColumn(true)]
       public int HasChild { get; set; }
       
    }
}


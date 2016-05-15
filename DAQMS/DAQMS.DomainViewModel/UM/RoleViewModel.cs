﻿using System.ComponentModel.DataAnnotations.Schema;
using DAQMS.Domain.Models;
using System.Runtime.Serialization;
using DAQMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace DAQMS.DomainViewModel
{
    public class RoleViewModel : Role
    {
        [Display(Name = "Module Name")]
        [DataMember, DataColumn(true)]
        public string ModuleName { get; set; }

    }
}
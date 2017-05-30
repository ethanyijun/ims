using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InterventionManagementSystem_MVC.Models
{
    public class InterventionViewModel
    {

        [Key]
        public Guid Id { get; set; }
        [Display (Name = "Intervention Type")]
        public string InterventionTypeName { get; set; }
        [Display(Name = "Client")]
        public string ClientName { get; set; }
        [Display (Name ="Create Date")]
        public Nullable<System.DateTime> DateCreate { get; set; }
        [Display(Name = "Perform Date")]
        public Nullable<System.DateTime> DateFinish { get; set; }
        public string InterventionState { get; set; }

        public string DistrictName { get; set; }

        public Nullable<decimal> Costs { get; set; }

        public Nullable<decimal> Hours { get; set; }

        //public Nullable<System.DateTime> DateFinish { get; set; }

        public string Comments { get; set; }
    }

}
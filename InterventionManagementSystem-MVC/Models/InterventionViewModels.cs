using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Models
{
    public class InterventionViewModel
    {
<<<<<<< HEAD
        public string Id { get; set; }
=======

        [Key]
        public Guid Id { get; set; }
        [Display (Name = "Intervention Type")]
>>>>>>> G
        public string InterventionTypeName { get; set; }
        [Display(Name = "Client")]
        public string ClientName { get; set; }
        public Guid ClientId { get; set; }
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

        public DateTime RecentiVisit { get; set; }
        public int LifeRemaining { get; set; }
        public IEnumerable<SelectListItem> InterventionStates { get; set; }

    }

}
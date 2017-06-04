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
        [Key]
        public Guid Id { get; set; }

        [Display (Name = "Intervention Type")]
        public string InterventionTypeName { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Display (Name ="Create Date")]
        public Nullable<System.DateTime> DateCreate { get; set; }

        [Display(Name = "Perform Date")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        public Nullable<System.DateTime> DateFinish { get; set; }

        public string InterventionState { get; set; }

        public string DistrictName { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid costs")]
        public Nullable<decimal> Costs { get; set; }

        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid hours")]
        public Nullable<decimal> Hours { get; set; }

        //public Nullable<System.DateTime> DateFinish { get; set; }

        public string Comments { get; set; }

        public DateTime RecentiVisit { get; set; }

        public int LifeRemaining { get; set; }

        public IEnumerable<SelectListItem> InterventionStates { get; set; }

    }

}
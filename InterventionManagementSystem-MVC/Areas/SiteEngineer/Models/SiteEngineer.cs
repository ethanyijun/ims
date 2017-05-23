using InterventionManagementSystem_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer.Models
{

    public class SiteEngineer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public Nullable<decimal> AuthorisedHours { get; set; }
        public Nullable<decimal> AuthorisedCosts { get; set; }
    }

    //public class SiteEngineerViewClientModel
    //{
    //    public int SelectedType { get; set; }
    //    public IEnumerable<InterventionViewModel> Interventions { get; set; }

    //    public IEnumerable<SelectListItem> ViewList { get; set; }
    //}


    //public class SiteEngineerViewInterventionModel
    //{
    //    public int SelectedType { get; set; }
    //    public IEnumerable<InterventionViewModel> Interventions { get; set; }

    //    public IEnumerable<SelectListItem> ViewList { get; set; }
    //}
}
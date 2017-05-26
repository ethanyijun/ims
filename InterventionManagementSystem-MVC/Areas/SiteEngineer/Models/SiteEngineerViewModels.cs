using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMSLogicLayer.Models;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer.Models
{
    public class SiteEngineerViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public Nullable<decimal> AuthorisedHours { get; set; }
        public Nullable<decimal> AuthorisedCosts { get; set; }
    }

    public class SiteEngineerViewInterventionModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }
        public int SelectedType { get; set; }
        public IEnumerable<InterventionViewModel> Interventions { get; set; }
        public InterventionViewModel Intervention { get; set; }
        public IEnumerable<SelectListItem> ViewInterventionTypeList { get; set; }
        public IEnumerable<SelectListItem> ViewClientsList { get; set; }

        //public IEnumerable<SelectListItem> ViewList { get; set; }
    }

    public class SiteEngineerViewClientModel
    {
        public IEnumerable<ClientViewModel> Clients { get; set; }

        //public int SelectedType { get; set; }
        //public IEnumerable<ClientViewModel> Clients { get; set; }

        //public IEnumerable<SelectListItem> ViewList { get; set; }
    }

}

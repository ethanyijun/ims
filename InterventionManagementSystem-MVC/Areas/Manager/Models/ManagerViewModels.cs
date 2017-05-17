using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMSLogicLayer.Models;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Models
{
    public class ManagerViewModel
    {
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public Nullable<decimal> AuthorisedHours { get; set; }
        public Nullable<decimal> AuthorisedCosts { get; set; }
    }

    public class ManagerViewInterventionModel
    {
        public int SelectedType { get; set; }
        public IEnumerable<InterventionViewModel> Interventions { get; set; }

        public IEnumerable<SelectListItem> ViewList { get; set; }
    }

}
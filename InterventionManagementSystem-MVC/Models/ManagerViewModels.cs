using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterventionManagementSystem_MVC.Models
{
    public class ManagerViewModels
    {
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public Nullable<decimal> AuthorisedHours { get; set; }
        public Nullable<decimal> AuthorisedCosts { get; set; }
    }
}
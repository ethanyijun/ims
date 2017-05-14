using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterventionManagementSystem_MVC.Models
{
    public class InterventionViewModel
    {
        public string InterventionTypeName { get; set; }
        public string ClientName { get; set; }

        public Nullable<System.DateTime> DateCreate { get; set; }
        public string InterventionState { get; set; }

        public string DistrictName { get; set; }

        public Nullable<decimal> Costs { get; set; }

        public Nullable<decimal> Hours { get; set; }

        public Nullable<System.DateTime> DateFinish { get; set; }

        public string Comments { get; set; }
    }

}
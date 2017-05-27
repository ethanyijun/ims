using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMSLogicLayer.Enums;
using IMSLogicLayer.Models;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Areas.Accountant.Models
{
    public class AccountantViewModel
    {
        public string Name { get; set; }

    }

    public class SiteEngineerViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class ManagerViewModel

    {
        public string Id { get; set; }
        public string Name { get; set; }


    }

    public class AccountListViewModel
    {
        public IEnumerable<SiteEngineerViewModel> SiteEngineers { get; set; }
        public IEnumerable<ManagerViewModel> Managers { get; set; }
    }

    public class EditDistrictViewModel
    {
        public string SelectedDistrict { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }

        public string CurrentDistrict { get; set; }

        public IEnumerable<SelectListItem> DistrictList { get; set; }

    }

    public class ReportViewModel
    {
        public string Name { get; set; }
    }

    public class ReportsViewModel
    {
        public IEnumerable<ReportViewModel> ReportList { get; set; }
    }

}
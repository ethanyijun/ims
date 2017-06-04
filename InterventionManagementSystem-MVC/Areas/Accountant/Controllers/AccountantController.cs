using IMSDBLayer.DataAccessObjects;
using IMSLogicLayer.ServiceInterfaces;
using IMSLogicLayer.Services;
using InterventionManagementSystem_MVC.Areas.Accountant.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMSLogicLayer.Enums;

namespace InterventionManagementSystem_MVC.Areas.Accountant.Controllers
{
    [AccountantAuthorize]
    [HandleError]
    public class AccountantController : Controller
    {
        private IAccountantService accountant;

        // GET: Accountant/Accountants
        public AccountantController() {

            //var identityId = User.Identity.GetUserId();

            accountant = new AccountantService("f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea");
            
        }

        public AccountantController(IAccountantService accountant) {
            this.accountant = accountant;
        }

        public ActionResult Index()
        {
            var user = accountant.getDetail();
            var model = new AccountantViewModel()
            {
                Name = user.Name,
            };
            return View(model);
         
        }
        
        public ActionResult AccountListView()
        {
           
            var siteEngineerList = accountant.getAllSiteEngineer();
            var siteEnigeerVMList = new List<SiteEngineerViewModel>();
            foreach (var siteEngineer in siteEngineerList)
            {
                siteEnigeerVMList.Add(new SiteEngineerViewModel()
                {
                    Id = siteEngineer.Id.ToString(),
                    Name = siteEngineer.Name
                });

            }

            var managerList = accountant.getAllManger();
            var managerVMList = new List<ManagerViewModel>();
            foreach (var manager in managerList)
            {
                managerVMList.Add(new ManagerViewModel()
                {
                    Id = manager.Id.ToString(),
                    Name = manager.Name
                });

            }

            var model = new AccountListViewModel()
            {
                SiteEngineers = siteEnigeerVMList,
                Managers = managerVMList
            };


            return View(model);
        }


        //GET Default information of an User
        public ActionResult EditDistrict(string id)
        {
         

            var user = accountant.getUserById(new Guid(id));
            user.District = accountant.getDistrictForUser(user.Id);

            var districts = accountant.getDistricts().Select(d=> new SelectListItem {Value = d.Id.ToString() ,Text=d.Name }).ToList();
         
            var model = new EditDistrictViewModel()
            {
                Name = user.Name,
                Id= id,
                CurrentDistrict = user.District.Name,
                DistrictList= districts
            };


            return View(model);
        }

   

        [HttpPost]
        public ActionResult EditDistrict(EditDistrictViewModel model)
        {
        
            if(accountant.changeDistrict(new Guid(model.Id), new Guid(model.SelectedDistrict)))
            {
                return RedirectToAction("EditDistrict","Accountant",model.Id);
            }

            return View("Error");
        }

        public ActionResult ReportList()
        {
            

            var reportList = Enum.GetValues(typeof(ReportType))
                                .Cast<ReportType>()
                                .Select(v => v.ToString())
                                .ToList();
         
            var model = new ReportListViewModel()
            {
                ReportList = reportList
            };


            return View(model);
        }


        public ActionResult PrintReport(string name)
        {
           
            ReportType reportType = (ReportType)Enum.Parse(typeof(ReportType), name);
            var report = new List<IMSLogicLayer.Models.ReportRow>();
            if (reportType == ReportType.AverageCostByEngineer)
            {
                report = accountant.printAverageCostByEngineer().ToList();
                foreach (var reportrow in report)
                {
                    reportrow.Hours = decimal.Round(reportrow.Hours.Value, 2, MidpointRounding.AwayFromZero);
                    reportrow.Costs = decimal.Round(reportrow.Costs.Value, 2, MidpointRounding.AwayFromZero);
                }
            }
            //if report is monthly cost by district redirect to monthly report page
            else if (reportType == ReportType.MonthlyCostByDistrict)
            {

                return PrintMonthlyReport();
            }
            else if (reportType == ReportType.TotalCostByDistrict)
            {
                report = accountant.printTotalCostByDistrict().ToList();
                var m = new DistrictReportViewModel()
                {
                    Report = report,
                    Total = report.Sum(r => r.Costs).ToString()
                };
                return View("TotalDistrictReport", m);

            }
            else if (reportType == ReportType.TotalCostByEngineer)
            {
                report = accountant.printTotalCostByEngineer().ToList();
            }
            var model = new ReportViewModel() {
                Report = report

            };

            return View("Report", model);
        }



        public ActionResult PrintMonthlyReport()
        {
            
            var report = new List<IMSLogicLayer.Models.ReportRow>();
           

            var districts = accountant.getDistricts().Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

            var model = new MonthlyDistrictReportViewModel()
            {
                DistrictList = districts,
            };
            
            return View("MonthlyDistrictReport",model);
        }


        [HttpPost]
        public ActionResult PrintMonthlyReport(MonthlyDistrictReportViewModel district)
        {
            if (district.SelectedDistrict ==null)
            {
                return PrintMonthlyReport();
            }

    
            var report = new List<IMSLogicLayer.Models.ReportRow>();
            var districtId = new Guid(district.SelectedDistrict);
            report = accountant.printMonthlyCostByDistrict(districtId).ToList();

            var districts = accountant.getDistricts().Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

            var model = new MonthlyDistrictReportViewModel()
            {
                DistrictList = districts,
                Report= report
            };


            return View("MonthlyDistrictReport", model);
        }
        private IAccountantService GetAccountantService()
        {
            //var identityId = User.Identity.GetUserId();

            IAccountantService accountant = new AccountantService("f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea");
            
            return accountant;
        }

    }
}
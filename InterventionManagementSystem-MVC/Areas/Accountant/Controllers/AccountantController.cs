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

        private IAccountantService Accountant
        {
            get
            {
                if (accountant == null)
                {
                    accountant = new AccountantService(System.Web.HttpContext.Current.User.Identity.GetUserId());
                }
                return accountant;
            }
        }

        // GET: Accountant/Accountants
        public AccountantController() { }

        public AccountantController(IAccountantService accountant)
        {
            this.accountant = accountant;
        }
        /// <summary>
        /// Return the accountant index page
        /// GET: ~/Accountant/Accountant/Index
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            var user = Accountant.getDetail();
            var model = new AccountantViewModel()
            {
                Name = user.Name,
            };
            return View(model);
        }
        /// <summary>
        /// Get a list of managers and site engineer in the system
        /// GET: ~/Accountant/Accountant/AccountListView
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountListView()
        {
            var siteEngineerList = Accountant.getAllSiteEngineer();
            var siteEnigeerVMList = new List<SiteEngineerViewModel>();
            foreach (var siteEngineer in siteEngineerList)
            {
                siteEnigeerVMList.Add(new SiteEngineerViewModel()
                {
                    Id = siteEngineer.Id.ToString(),
                    Name = siteEngineer.Name
                });
            }

            var managerList = Accountant.getAllManger();
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



        /// <summary>
        /// Edit District Form with current information of the User
        /// GET: ~/Accountant/Accountant/EditDistrict/Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditDistrict(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error");
            }

            var user = Accountant.getUserById(new Guid(id));
            user.District = Accountant.getDistrictForUser(user.Id);

            var districts = Accountant.getDistricts().Select(d=> new SelectListItem {Value = d.Id.ToString() ,Text=d.Name }).ToList();
         
            var model = new EditDistrictViewModel()
            {
                Name = user.Name,
                Id= id,
                CurrentDistrict = user.District.Name,
                DistrictList= districts
            };
            
            return View(model);
        }
        
        /// <summary>
        /// Post the updated district information to the server
        /// POST: ~/Accountant/Accountant/EditDistrict/Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDistrict(EditDistrictViewModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                return View("Error");
            }

            var user = Accountant.getUserById(new Guid(model.Id));
            
            if (user.DistrictId.ToString().Equals(model.SelectedDistrict))
            {
                return RedirectToAction("EditDistrict", "Accountant", model.Id);
            }
            if(Accountant.changeDistrict(new Guid(model.Id), new Guid(model.SelectedDistrict)))
            {
                return RedirectToAction("EditDistrict","Accountant",model.Id);
            }

            return View("Error");
        }
        /// <summary>
        /// View a list of report can be printed
        /// GET: ~/Accountant/Accountant/ReportList
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportList()
        {
            var reportList = Enum.GetValues(typeof(ReportType)).Cast<ReportType>().Select(v => v.ToString()).ToList();
         
            var model = new ReportListViewModel()
            {
                ReportList = reportList
            };
            
            return View(model);
        }
        /// <summary>
        /// Display the report details
        /// GET: ~/Accountant/Accountant/PrintReport?Name=reportName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult PrintReport(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View("Error");
            }
            ReportType reportType = (ReportType)Enum.Parse(typeof(ReportType), name);
            var report = new List<IMSLogicLayer.Models.ReportRow>();
            if (reportType == ReportType.AverageCostByEngineer)
            {
                report = Accountant.printAverageCostByEngineer().ToList();
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
                report = Accountant.printTotalCostByDistrict().ToList();
                var m = new DistrictReportViewModel()
                {
                    Report = report,
                    Total = report.Sum(r => r.Costs).ToString()
                };
                return View("TotalDistrictReport", m);

            }
            else if (reportType == ReportType.TotalCostByEngineer)
            {
                report = Accountant.printTotalCostByEngineer().ToList();
            }
            var model = new ReportViewModel() {
                Report = report

            };

            return View("Report", model);
        }
        /// <summary>
        /// Display a form to select which district to print
        /// GET: ~/Accountant/Accountant/PrintMonthlyReport
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintMonthlyReport()
        {
            var report = new List<IMSLogicLayer.Models.ReportRow>();
            var districts = Accountant.getDistricts().Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

            var model = new MonthlyDistrictReportViewModel()
            {
                DistrictList = districts,
            };
            
            return View("MonthlyDistrictReport",model);
        }

        /// <summary>
        /// Display the monthly report for district details
        /// POST: ~/Accountant/Accountant/PrintMonthlyReport
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PrintMonthlyReport(MonthlyDistrictReportViewModel district)
        {
            if (district ==null||district.SelectedDistrict ==null)
            {
                return RedirectToAction("PrintMonthlyReport","Accountant");
            }
            
            var report = new List<IMSLogicLayer.Models.ReportRow>();
            var districtId = new Guid(district.SelectedDistrict);
            report = Accountant.printMonthlyCostByDistrict(districtId).ToList();

            var districts = Accountant.getDistricts().Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

            var model = new MonthlyDistrictReportViewModel()
            {
                DistrictList = districts,
                Report= report
            };
            
            return View("MonthlyDistrictReport", model);
        }
    }
}
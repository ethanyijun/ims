using IMSLogicLayer.ServiceInterfaces;
using IMSLogicLayer.Services;
using InterventionManagementSystem_MVC.Areas.Accountant.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Areas.Accountant.Controllers
{
    public class AccountantController : Controller
    {
        // GET: Accountant/Accountants
        public ActionResult Index()
        {
            
            IAccountantService accountant = GetAccountantService();
            var user = accountant.getDetail();
            var model = new AccountantViewModel()
            {
                Name = user.Name,
            };
            return View(model);
         
        }
        
        public ActionResult AccountListView()
        {
            IAccountantService accountant = GetAccountantService();
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
            var accountant = GetAccountantService();

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

   


        public ActionResult ChangeDistrict(FormCollection form)
        {
          
            return View();
        }

        public ActionResult ReportList()
        {


            return View();
        }
        private IAccountantService GetAccountantService()
        {
            var identityId = User.Identity.GetUserId();

            IAccountantService accountant = new AccountantService("f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea");
            
            return accountant;
        }

    }
}
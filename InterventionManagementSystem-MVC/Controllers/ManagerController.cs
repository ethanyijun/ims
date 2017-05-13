using IMSLogicLayer.Services;
using IMSLogicLayer.ServiceInterfaces;
using InterventionManagementSystem_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace InterventionManagementSystem_MVC.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            var identityId = User.Identity.GetUserId();

            IManagerService manager = new ManagerService("2b8dbe21-cc7b-4794-bf3e-4a2d3a7b68e0");
            var user = manager.GetDetail();
            var model = new ManagerViewModels()
            {
                Name = user.Name,
                DistrictName = user.District.Name,
                AuthorisedCosts = user.AuthorisedCosts,
                AuthorisedHours = user.AuthorisedHours
            };

            return View(model);
        }
    }
}
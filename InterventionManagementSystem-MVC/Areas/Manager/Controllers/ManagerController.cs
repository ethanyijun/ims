using IMSLogicLayer.ServiceInterfaces;
using IMSLogicLayer.Services;
using InterventionManagementSystem_MVC.Models;
using InterventionManagementSystem_MVC.Areas.Manager;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.Manager.Models;

namespace InterventionManagementSystem_MVC.Areas.Manager.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager/Manager
        public ActionResult Index()
        {
            IManagerService manager = GetManagerService();
            var user = manager.GetDetail();
            var model = new ManagerViewModel()
            {
                Name = user.Name,
                DistrictName = user.District.Name,
                AuthorisedCosts = user.AuthorisedCosts,
                AuthorisedHours = user.AuthorisedHours
            };
            return View(model);
        }

        public ActionResult InterventionList()
        {
            IManagerService manager = GetManagerService();
            var interventionList = manager.GetApprovedInterventions();
            var interventions = new List<InterventionViewModel>();
            var viewList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text ="Approved",Value="Approved", Selected=true },
                new SelectListItem(){ Text="Proposed", Value="Proposed"}
            };
            BindIntervention(interventionList, interventions);
            var model = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions };
            return View(model);
        }

     
        [HttpPost]
        public ActionResult InterventionList(FormCollection form)
        {
            if (form["SelectedType"].ToString().Equals("Approved"))
            {
                return RedirectToAction("InterventionList", "Manager");
            }
            else
            {
               
                IManagerService manager = GetManagerService();
                var user = manager.GetDetail();
                var interventionList = manager.GetInterventionsByState(IMSLogicLayer.Enums.InterventionState.Proposed);
                var interventions = new List<InterventionViewModel>();
                var viewList = new List<SelectListItem>()
                {
                    new SelectListItem(){ Text="Proposed", Value="Proposed",Selected=true },
                    new SelectListItem(){ Text ="Approved",Value="Approved"},
                  
                };
                BindIntervention(interventionList, interventions);
                var model = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions };
                return View(model);

            }

        }

        private IManagerService GetManagerService()
        {
            var identityId = User.Identity.GetUserId();

            IManagerService manager = new ManagerService("2b8dbe21-cc7b-4794-bf3e-4a2d3a7b68e0");

            return manager;
        }

        private void BindIntervention(IEnumerable<IMSLogicLayer.Models.Intervention> interventionList, List<InterventionViewModel> interventions)
        {
            foreach (var intervention in interventionList)
            {
                interventions.Add(new InterventionViewModel()
                {
                    InterventionTypeName = intervention.InterventionType.Name,
                    ClientName = intervention.Client.Name,
                    DateCreate = intervention.DateCreate,
                    InterventionState = intervention.InterventionState.ToString(),
                    DistrictName = intervention.District.Name,
                    Costs = intervention.Costs,
                    Hours = intervention.Hours,
                    DateFinish = intervention.DateFinish,
                    Comments = intervention.Comments

                });
            }
        }
    }
}
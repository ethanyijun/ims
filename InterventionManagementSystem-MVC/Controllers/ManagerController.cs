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
            var model = new ManagerViewModel()
            {
                Name = user.Name,
                DistrictName = user.District.Name,
                AuthorisedCosts = user.AuthorisedCosts,
                AuthorisedHours = user.AuthorisedHours
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult ApprovedList(FormCollection form)
        {

            if (form["SelectedType"].ToString().Equals("Approved"))
            {
                return View();
            }else
            {
                var identityId = User.Identity.GetUserId();

                IManagerService manager = new ManagerService("2b8dbe21-cc7b-4794-bf3e-4a2d3a7b68e0");
                var user = manager.GetDetail();
                var interventionList = manager.GetInterventionsByState(IMSLogicLayer.Enums.InterventionState.Proposed);
                var interventions = new List<InterventionViewModel>();

                var viewList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text ="Approved",Value="Approved"},
                new SelectListItem(){ Text="Proposed", Value="Proposed",Selected=true }
            };


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


                var model = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions };
                return View(model);
            }
           

            
        }

        public ActionResult ApprovedList()
        {
            var identityId = User.Identity.GetUserId();

            IManagerService manager = new ManagerService("2b8dbe21-cc7b-4794-bf3e-4a2d3a7b68e0");
            var user = manager.GetDetail();
            var interventionList = manager.GetApprovedInterventions();
            var interventions = new List<InterventionViewModel>();

            var viewList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text ="Approved",Value="Approved"},
                new SelectListItem(){ Text="Proposed", Value="Proposed"}
            };


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


            var model = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions };
            return View(model);
        }

    }
}
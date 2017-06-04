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
using System.Net.Mail;
using Microsoft.AspNet.Identity.Owin;
using IMSLogicLayer.Models;

namespace InterventionManagementSystem_MVC.Areas.Manager.Controllers
{
    [ManagerAuthorize]
    
    public class ManagerController : Controller
    {
        private IManagerService manager;

        private IManagerService Manager
        {
            get
            {
                if (manager == null)
                {
                    manager = new ManagerService(System.Web.HttpContext.Current.User.Identity.GetUserId());
                }
                return manager;
            }
        }

        public ManagerController() { }
        
        public ManagerController(IManagerService manager)
        {
            this.manager = manager;
        }

        // GET: Manager/Manager
        public ActionResult Index()
        {
            var user = Manager.GetDetail();
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
            var user = Manager.GetDetail();
            var interventionList = Manager.GetApprovedInterventions();
            var interventions = new List<InterventionViewModel>();
            var viewList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text ="Approved",Value="Approved", Selected=true },
                new SelectListItem(){ Text="Proposed", Value="Proposed"}
            };
            BindIntervention(interventionList, interventions);
            var model = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions, AuthorisedCosts = user.AuthorisedCosts, AuthorisedHours = user.AuthorisedHours };
            return View(model);
        }
        
        [HttpPost]
        public ActionResult InterventionList(ManagerViewInterventionModel model)
        {
            if (model.SelectedType.Equals("Approved"))
            {
                return RedirectToAction("InterventionList", "Manager");
            }
            else
            {
                var user = Manager.GetDetail();
                var interventionList = Manager.GetInterventionsByState(IMSLogicLayer.Enums.InterventionState.Proposed);
                var interventions = new List<InterventionViewModel>();
                var viewList = new List<SelectListItem>()
                {
                    new SelectListItem(){ Text="Proposed", Value="Proposed", Selected=true },
                    new SelectListItem(){ Text ="Approved", Value="Approved"},

                };
                BindIntervention(interventionList, interventions);
                var m = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions, AuthorisedCosts = user.AuthorisedCosts, AuthorisedHours = user.AuthorisedHours };
                return View(m);
            }
        }
        
        public ActionResult ApproveIntervention(string id)
        {
            if (Manager.ApproveAnIntervention(new Guid(id)))
            {
                Intervention intervention = Manager.GetInterventionById(new Guid(id));

                string sendTo = "BurningGroupTestMail@gmail.com";

                Manager.SendEmailNotification(intervention, sendTo);

                return RedirectToAction("InterventionList","Manager");
            }
            return View("Error");
        }

        private void BindIntervention(IEnumerable<IMSLogicLayer.Models.Intervention> interventionList, List<InterventionViewModel> interventions)
        {
            foreach (var intervention in interventionList)
            {
                interventions.Add(new InterventionViewModel()
                {
                    Id = intervention.Id,
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
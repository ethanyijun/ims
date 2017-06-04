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

        public ManagerController()
        {
            manager = new ManagerService(User.Identity.GetUserId());
        }

        public ManagerController(IManagerService manager)
        {
            this.manager = manager;
        }

        // GET: Manager/Manager
        public ActionResult Index()
        {

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

            var user = manager.GetDetail();
            var interventionList = manager.GetApprovedInterventions();
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
                var user = manager.GetDetail();
                var interventionList = manager.GetInterventionsByState(IMSLogicLayer.Enums.InterventionState.Proposed);
                var interventions = new List<InterventionViewModel>();
                var viewList = new List<SelectListItem>()
                {
                    new SelectListItem(){ Text="Proposed", Value="Proposed",Selected=true },
                    new SelectListItem(){ Text ="Approved",Value="Approved"},

                };
                BindIntervention(interventionList, interventions);
                var m = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions, AuthorisedCosts = user.AuthorisedCosts, AuthorisedHours = user.AuthorisedHours };
                return View(m);
            }
        }
        
        public ActionResult ApproveIntervention(string id)
        {
            if(manager.ApproveAnIntervention(new Guid(id)))
            {
                sendEmailNotification(id);
                
                return RedirectToAction("InterventionList","Manager");
            }
            return View("Error");
        }

      
        private void sendEmailNotification(string id)
        {
            Intervention intervention = manager.GetInterventionById(new Guid(id));

            IEmailService email = new IMSLogicLayer.Services.EmailService();
            ApplicationUser fromUser = System.Web.HttpContext.Current.GetOwinContext()
                                    .GetUserManager<ApplicationUserManager>()
                                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //string from = fromUser.Email;
            string from = "BurningGroupTestMail@gmail.com";
            string fromName = fromUser.UserName;

            ApplicationUser toUser = System.Web.HttpContext.Current.GetOwinContext()
                                    .GetUserManager<ApplicationUserManager>()
                                    .FindById(intervention.CreatedBy.ToString());

            string to = "11998402@student.uts.edu.au";
            //string to=toUser.Email;
            //string toName = toUser.UserName;
            string toName = "Test";
            
            MailMessage message = email.CreateMessage(from,to,fromName,toName,intervention);
            email.SendEmail(message);

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
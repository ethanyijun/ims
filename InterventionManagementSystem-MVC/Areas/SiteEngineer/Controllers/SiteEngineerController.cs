using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMSLogicLayer.ServiceInterfaces;
using IMSLogicLayer.Services;
using InterventionManagementSystem_MVC.Models;
using Microsoft.AspNet.Identity;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Models;
using IMSLogicLayer.Models;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer.Controllers
{
    [SiteEngineerAuthorize]
    [HandleError]
    public class SiteEngineerController : Controller
    {
        private IEngineerService engineer;

        public SiteEngineerController()
        {
            engineer = GetEngineerService();
        }

        public SiteEngineerController(IEngineerService engineer)
        {
            this.engineer = engineer;
        }

        // GET: SiteEngineer/SiteEngineer
        public ActionResult Index()
        {
            var user = engineer.getDetail();
            var model = new SiteEngineerViewModel()
            {
                Name = user.Name,
                DistrictName = user.District.Name,
                AuthorisedCosts = user.AuthorisedCosts,
                AuthorisedHours = user.AuthorisedHours
            };
            return View(model);
        }
        // GET: SiteEngineer/Create
        public ActionResult CreateIntervention()
        {
            //mockup data
            List<Client> Clients = new List<Client>();
            Clients.Add(new Client("jammie", "chatswood", new Guid()));

            //var Clients = engineer.getClients();
            var viewClientsList = new List<SelectListItem>();
            foreach (var client in Clients)
            {
                viewClientsList.Add(new SelectListItem() { Text = client.Name, Value = client.Name });
            }

            var InterventionTypes = engineer.getInterventionTypes();
            var viewInterventionTypes = new List<SelectListItem>();
            foreach (var type in InterventionTypes)
            {
                viewInterventionTypes.Add(new SelectListItem() { Text = type.Name.ToString(), Value = type.Name.ToString() });
            }
            var model = new SiteEngineerViewInterventionModel() { ViewInterventionTypeList = viewInterventionTypes, ViewClientsList = viewClientsList };
            return View(model);



        }

        // POST: SiteEngineer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIntervention(InterventionViewModel interventionmodel)/*([Bind(Include = "Id,Name,Description,Length,Price,Rating,IncludesMeals")] Tour tour)*/
        {
            if (ModelState.IsValid)
            {
                //db.Tours.Add(tour);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interventionmodel);
        }

        //public ActionResult CreateIntervention() {
        //    IEngineerService engineer = GetEngineerService();

        //}


        // GET: SiteEngineer/Interventions
        public ActionResult InterventionList()
        {
            var interventionList = engineer.getInterventionListByCreator(engineer.getDetail().Id).ToList();
            var interventions = new List<InterventionViewModel>();

            //var viewList = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text ="Approved",Value="Approved", Selected=true },
            //    new SelectListItem(){ Text="Proposed", Value="Proposed"}
            //};
            BindIntervention(interventionList, interventions);
            var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
            return View(model);
        }

        // POST: SiteEngineer/Interventions
        [HttpPost]
        public ActionResult InterventionList(FormCollection form)
        {


            //if (form["SelectedType"].ToString().Equals("Approved"))
            //{
            //    return RedirectToAction("InterventionList", "SiteEngineer");
            //}
            //else
            //{

            //    IEngineerService engineer = GetEngineerService();
            //    //var user = engineer.GetDetail();
            //    var interventionList = engineer.GetInterventionsByState(IMSLogicLayer.Enums.InterventionState.Proposed);
            //    var interventions = new List<InterventionViewModel>();
            //    var viewList = new List<SelectListItem>()
            //    {
            //        new SelectListItem(){ Text="Proposed", Value="Proposed",Selected=true },
            //        new SelectListItem(){ Text ="Approved",Value="Approved"},

            //    };
            //    BindIntervention(interventionList, interventions);
            //    var model = new ManagerViewInterventionModel() { ViewList = viewList, Interventions = interventions };
            //    return View(model);

            //}
            return View();
        }

        private IEngineerService GetEngineerService()
        {
            var identityId = User.Identity.GetUserId();
            IEngineerService engineer = new EngineerService("03869985-ae09-4331-8b0a-68b98084132a");
            return engineer;
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
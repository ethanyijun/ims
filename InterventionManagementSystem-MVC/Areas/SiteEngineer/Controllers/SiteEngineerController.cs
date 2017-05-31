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
using IMSLogicLayer.Enums;

namespace InterventionManagementSystem_MVC.Areas.SiteEngineer.Controllers
{
    public class SiteEngineerController : Controller
    {
       

        // GET: SiteEngineer/SiteEngineer
        public ActionResult Index()
        {
            IEngineerService engineer = GetEngineerService();
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
        public ActionResult ClientList() {
            return View();

        }




        // GET: SiteEngineer/Create
        public ActionResult CreateIntervention()
        {
            IEngineerService engineer = GetEngineerService();
            //mockup data
        //    List<Client> Clients = new List<Client>();
         //   Clients.Add(new Client("jammie", "chatswood", new Guid()));

            var Clients = engineer.getClients();
            var viewClientsList = new List<SelectListItem>();
            foreach (var client in Clients)
            {
                viewClientsList.Add(new SelectListItem() { Text = client.Name, Value = client.Id.ToString() });
            }

            var InterventionTypes = engineer.getInterventionTypes();
            var viewInterventionTypes = new List<SelectListItem>();
            foreach (var type in InterventionTypes)
            {
                viewInterventionTypes.Add(new SelectListItem() { Text = type.Name.ToString(), Value = type.Id.ToString() });
            }
            var model = new SiteEngineerViewInterventionModel() { ViewInterventionTypeList = viewInterventionTypes, ViewClientsList = viewClientsList };
            return View(model);



        }

        // POST: SiteEngineer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateIntervention(SiteEngineerViewInterventionModel viewmodel)/*([Bind(Include = "Id,Name,Description,Length,Price,Rating,IncludesMeals")] Tour tour)*/
        {
            if (ModelState.IsValid)
            {

                IEngineerService engineer = GetEngineerService();

                decimal hours = (decimal)viewmodel.Intervention.Hours;

                decimal costs = (decimal)viewmodel.Intervention.Costs;
                int lifeRemaining = 100;
                string comments = viewmodel.Intervention.Comments;

                InterventionState state = InterventionState.Proposed;
                String test = Request.Form["ClientsList"];

                Guid clientId = new Guid(Request.Form["ClientsList"]);


                //DateTime dateCreate = (DateTime)viewmodel.Intervention.DateCreate;
                DateTime dateCreate = DateTime.Now;
                DateTime dateFinish = (DateTime)viewmodel.Intervention.DateFinish;
                DateTime dateRecentVisit = DateTime.Now;


                Guid createdBy = (Guid)engineer.getDetail().Id;
                Guid approvedBy = (Guid)engineer.getDetail().DistrictId;
                Guid typeId = new Guid(Request.Form["InterventionTypes"]);   // new Guid(viewmodel.Intervention.InterventionTypeName);

                   Intervention new_intervention = new Intervention(hours, costs, lifeRemaining, comments, state,
                        dateCreate, dateFinish, dateRecentVisit, typeId, clientId, createdBy, null);
               
             Intervention returninter= engineer.createIntervention(new_intervention);
             //   InterventionViewModel viewm = BindSingleIntervention(returninter);
                //    if (Page.IsValid)
                //    {
                //        decimal hour = decimal.Parse(InterventionHour.Text);
                //        decimal cost = decimal.Parse(InterventionCost.Text);
                //        string comments = InterventionComments.Text;
                //        Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
                //        DateTime createDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                //        DateTime? finishDate;
                //        if (String.IsNullOrEmpty(InterventionPerformDate.Text))
                //        {
                //            finishDate = null;
                //        }
                //        else
                //        {
                //            finishDate = DateTime.Parse(InterventionPerformDate.Text);
                //        }


                //        DateTime recentVisit = DateTime.Parse(DateTime.Now.ToShortDateString());
                //        var typeID = SeletedInterventionType.SelectedValue;
                //        var clientID = SelectClient.SelectedValue;
                //        InterventionState state = InterventionState.Proposed;

                //        Intervention intervention = new Intervention(hour, cost, 100, comments, state, createDate, finishDate, recentVisit, new Guid(typeID), new Guid(clientID), engineerService.getDetail().Id, null);
                //        engineerService.createIntervention(intervention);

                //        Response.Redirect("~/Engineer/InterventionList.aspx", false);
                //    }
                //}
                //db.Tours.Add(tour);
                //db.SaveChanges();

                // return RedirectToAction("Index");
                var interventionList = engineer.GetAllInterventions(engineer.getDetail().Id).ToList();
                var interventions = new List<InterventionViewModel>();
                BindIntervention(interventionList, interventions);
                var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
                ////return View(model);
                //return View("InterventionList", model);
                return View("InterventionList", model);

            }
            return View(viewmodel);
        }

        public ActionResult EditClient()
        {
            IEngineerService engineer = GetEngineerService();
           
            return View();
        }
        [HttpPost]
        public ActionResult EditClient(FormCollection form)
        {
            return View();
        }




        public ActionResult EditIntervention(Guid id)
        {
            
            IEngineerService engineer = GetEngineerService();
              Intervention inter= engineer.getNonGuidInterventionById(id);
            InterventionViewModel mo=BindSingleIntervention(inter);
            
             return View(mo);
     
             }
           [HttpPost]
        public ActionResult EditIntervention(InterventionViewModel interventionmodel){
            IEngineerService engineer = GetEngineerService();
            String new_comments = interventionmodel.Comments;
            int new_liferemaining = interventionmodel.LifeRemaining;
            DateTime new_recentvisit = interventionmodel.RecentiVisit;
            bool s;
          //  Guid interventionId = interventionmodel.Id;
         //   Intervention intervention = engineer.getNonGuidInterventionById(interventionmodel.Id);
            s=engineer.updateInterventionDetail(interventionmodel.Id,new_comments,new_liferemaining,new_recentvisit);
           

            var interventionList = engineer.GetAllInterventions(engineer.getDetail().Id).ToList();
            var interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList, interventions);
            var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
            //return View(model);
            return View("InterventionList",model);
          }



        // GET: SiteEngineer/InterventionList
        public ActionResult InterventionList()
        {
            IEngineerService engineer = GetEngineerService();
            Guid enigerrId = engineer.getDetail().Id;
            var interventionList = engineer.GetAllInterventions(engineer.getDetail().Id).ToList();

            //foreach (var intervention in interventionList)
            //{
            //    intervention.InterventionType = engineer.getInterventionTypes().Find(it => it.Id == intervention.InterventionTypeId);
            //}

            var interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList, interventions);
            var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
            return View(model);
        }

        // POST: SiteEngineer/InterventionList
        [HttpPost]
        public ActionResult InterventionList(SiteEngineerViewInterventionModel b)
        {
            
           // if (command == "Edit")
           // {
                InterventionViewModel selectedIntervention = b.Intervention;
       //     var interventionModel = new InterventionViewModel();
      //     interventionModel = BindSingleIntervention(selectedIntervention);
            //  if (command == "Edit"){

            return View("EditIntervention", selectedIntervention);
           // return View(selectedIntervention);


            //    }
            //     return View();



            //  RedirectToAction("InterventionList", "SiteEngineer");
            //    }

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
            //     return View();
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
                    Id = (Guid)intervention.Id,
                    // client id
                    ClientName = intervention.Client.Name,
                    DateCreate = intervention.DateCreate,
                    InterventionState = intervention.InterventionState.ToString(),

                    // ??
                    DistrictName = intervention.District.Name,
                    Costs = intervention.Costs,
                    Hours = intervention.Hours,
                    DateFinish = intervention.DateFinish,
                    Comments = intervention.Comments

                });
            }
        }

        private InterventionViewModel BindSingleIntervention(Intervention intervention)
        {
            InterventionViewModel interventionmodel = new InterventionViewModel()
            {
                InterventionTypeName = intervention.InterventionType.Name,

                // client id
                ClientName = intervention.Client.Name,
                DateCreate = intervention.DateCreate,
                InterventionState = intervention.InterventionState.ToString(),
                LifeRemaining = (int)intervention.LifeRemaining,
                RecentiVisit = (DateTime)intervention.DateRecentVisit,
                
                // ??
                DistrictName = intervention.District.Name,
                Costs = intervention.Costs,
                Hours = intervention.Hours,
                DateFinish = intervention.DateFinish,
                Comments = intervention.Comments
            };
            return interventionmodel;
        }

    }
}
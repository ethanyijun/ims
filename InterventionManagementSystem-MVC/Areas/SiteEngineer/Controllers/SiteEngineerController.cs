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
    [SiteEngineerAuthorize]
    [HandleError]
    public class SiteEngineerController : Controller
    {
        private IEngineerService engineer;
        
        private IEngineerService Engineer
        {
            get
            {
                if (engineer == null)
                {
                    engineer = new EngineerService(System.Web.HttpContext.Current.User.Identity.GetUserId());
                }
                return engineer;
            }
        }

        public SiteEngineerController() { }

        public SiteEngineerController(IEngineerService engineer)
        {
            this.engineer = engineer;
        }

        // GET: SiteEngineer/SiteEngineer
        public ActionResult Index()
        {
            var user = Engineer.getDetail();
            var model = new SiteEngineerViewModel()
            {
                Name = user.Name,
                DistrictName = user.District.Name,
                AuthorisedCosts = user.AuthorisedCosts,
                AuthorisedHours = user.AuthorisedHours
            };
            return View(model);
        }
        
        public ActionResult CreateClient()
        {
            String districtName = Engineer.getDetail().District.Name;

            ClientViewModel clientViewmodel = new ClientViewModel() { DistrictName=districtName};
            return View(clientViewmodel);
        }

        // POST: SiteEngineer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateClient(ClientViewModel clientVmodel)/*([Bind(Include = "Id,Name,Description,Length,Price,Rating,IncludesMeals")] Tour tour)*/
        {
            if (ModelState.IsValid)
            {
                Client client = Engineer.createClient(clientVmodel.Name, clientVmodel.Location);
                var clientList = Engineer.getClients();
                var clients = new List<ClientViewModel>();
                BindClient(clientList, clients);
                return View("ClientList", clients);
            }
            return View(clientVmodel);
        }

        // GET : SiteEngineer/ClientList
       
        public ActionResult ClientList() {
            
            var clientList = Engineer.getClients();
            List<ClientViewModel> clients = new List<ClientViewModel>();
            BindClient(clientList, clients);
            return View(clients);
        }

        // GET: SiteEngineer/Create
        public ActionResult CreateIntervention()
        {
            var Clients = Engineer.getClients();
            var viewClientsList = new List<SelectListItem>();
            foreach (var client in Clients)
            {
                viewClientsList.Add(new SelectListItem() { Text = client.Name, Value = client.Id.ToString() });
            }

            var InterventionTypes = Engineer.getInterventionTypes();
            var viewInterventionTypes = new List<SelectListItem>();
            foreach (var type in InterventionTypes)
            {
                viewInterventionTypes.Add(new SelectListItem() { Text = type.Name.ToString(), Value = type.Id.ToString() });
            }
            var model = new SiteEngineerViewInterventionModel() { ViewInterventionTypeList = viewInterventionTypes, ViewClientsList = viewClientsList };
            return View(model);
        }

        // GET: SiteEngineer/EditIntervention
        public ActionResult EditInterventionState(Guid id)
        {
            Intervention interention = Engineer.getNonGuidInterventionById(id);
            InterventionViewModel model = BindSingleIntervention(interention);
            
            ViewBag.state = interention.InterventionState;
            return View(model);
        }

        // POST: SiteEngineer/EditIntervention
        [HttpPost]
        public ActionResult EditInterventionState(InterventionViewModel interventionmodel)
        {
            string interventionState = Request.Form["StatesList"];
            InterventionState interventionStatus = new InterventionState();
            Enum.TryParse(interventionState, out interventionStatus);
            int interventionStateInt = (int)interventionStatus;
            InterventionState newState = (InterventionState)interventionStateInt;
            bool result = Engineer.updateInterventionState(interventionmodel.Id, newState);
            
            var interventionList = Engineer.GetAllInterventions(Engineer.getDetail().Id).ToList();
            var interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList, interventions);
            var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
            return View("InterventionList", model);
        }

        // POST: SiteEngineer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIntervention(SiteEngineerViewInterventionModel viewmodel)/*([Bind(Include = "Id,Name,Description,Length,Price,Rating,IncludesMeals")] Tour tour)*/
        {
            if (ModelState.IsValid)
            {
                decimal hours = (decimal)viewmodel.Intervention.Hours;
                decimal costs = (decimal)viewmodel.Intervention.Costs;
                int lifeRemaining = 100;
                string comments = viewmodel.Intervention.Comments;
                InterventionState state = InterventionState.Proposed;
                String test = Request.Form["ClientsList"];

                Guid clientId = new Guid(Request.Form["ClientsList"]);
                DateTime dateCreate = DateTime.Now;
                DateTime dateFinish = (DateTime)viewmodel.Intervention.DateFinish;
                DateTime dateRecentVisit = DateTime.Now;


                Guid createdBy = (Guid)Engineer.getDetail().Id;
                Guid approvedBy = (Guid)Engineer.getDetail().DistrictId;
                Guid typeId = new Guid(Request.Form["InterventionTypes"]);   
                Intervention new_intervention = new Intervention(hours, costs, lifeRemaining, comments, state,
                dateCreate, dateFinish, dateRecentVisit, typeId, clientId, createdBy, null);
                Engineer.createIntervention(new_intervention);          
                var interventionList = Engineer.GetAllInterventions(Engineer.getDetail().Id).ToList();
                var interventions = new List<InterventionViewModel>();
                BindIntervention(interventionList, interventions);
                var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
                return View("InterventionList", model);

            }
            return View(viewmodel);
        }
        
        // GET: SiteEngineer/EditIntervention
        public ActionResult EditIntervention(Guid id)
        {
            Intervention interention= Engineer.getNonGuidInterventionById(id);
            InterventionViewModel model=BindSingleIntervention(interention);
            
            return View(model);
        }

        // POST: SiteEngineer/EditIntervention
        [HttpPost]
        public ActionResult EditIntervention(InterventionViewModel interventionmodel)
        {
            String new_comments = interventionmodel.Comments;
            int new_liferemaining = interventionmodel.LifeRemaining;
            DateTime new_recentvisit = interventionmodel.RecentiVisit;
        
            Engineer.updateInterventionDetail(interventionmodel.Id,new_comments,new_liferemaining,new_recentvisit);
            
            var interventionList = Engineer.getInterventionsByClient(interventionmodel.ClientId);
            List<InterventionViewModel> interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList, interventions);
            
            Client client = Engineer.getClientById(interventionmodel.ClientId);
            ClientViewModel clientViewModel=  BindSingleClient(client);
            var model = new SiteEngineerViewClientModel() { Interventions = interventions,Client= clientViewModel };
          
            return View("ClientDetails", model);
          }


        // GET: SiteEngineer/ClientDetails/ClientId
        public ActionResult ClientDetails(Guid id)
        {
            var client= Engineer.getClientById(id);
            var interventionList = Engineer.getInterventionsByClient(id);
            List<InterventionViewModel> interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList,interventions);
            ClientViewModel clientViewModel=BindSingleClient(client);
            
            InterventionViewModel interview = new InterventionViewModel();
            var model = new SiteEngineerViewClientModel() {Interventions= interventions, Client= clientViewModel,Intervention= interview };
            return View(model);
        }

        // POST: SiteEngineer/InterventionList
        [HttpPost]
        public ActionResult ClientDetails(SiteEngineerViewClientModel SE_VclientModel)
        {
            List<InterventionViewModel> ClientList=new List<InterventionViewModel>();
           // SE_VclientModel.Clients
            return View();
        }


        // GET: SiteEngineer/InterventionList
        public ActionResult InterventionList()
        {
            Guid enigerrId = Engineer.getDetail().Id;
            var interventionList = Engineer.GetAllInterventions(Engineer.getDetail().Id).ToList();
            
            var interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList, interventions);
            var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
            return View(model);
        }

        // POST: SiteEngineer/InterventionList
        [HttpPost]
        public ActionResult InterventionList(SiteEngineerViewInterventionModel b)
        {
            InterventionViewModel selectedIntervention = b.Intervention;
            return View("EditIntervention", selectedIntervention);
        }

        public void BindClient(IEnumerable<IMSLogicLayer.Models.Client> clientList, List<ClientViewModel> clients)
        {
            foreach (var client in clientList)
            {
                clients.Add(new ClientViewModel()
                {
                    Id = client.Id,
                    DistrictName = Engineer.getDistrictName(client.DistrictId),
                    Location = client.Location,
                    Name = client.Name
                });
            }
        }

        private void BindIntervention(IEnumerable<IMSLogicLayer.Models.Intervention> interventionList, List<InterventionViewModel> interventions)
        {
            foreach (var intervention in interventionList)
            {
                interventions.Add(new InterventionViewModel()
                {
                    InterventionTypeName = intervention.InterventionType.Name,
                    ClientId = (Guid)intervention.ClientId,
                    Id = (Guid)intervention.Id,
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
                });
            }
        }

        private InterventionViewModel BindSingleIntervention(Intervention intervention)
        {
            var viewInterventionStates = new List<SelectListItem>();
            viewInterventionStates.Add(new SelectListItem() { Text = InterventionState.Approved.ToString(), Value = InterventionState.Approved.ToString() });
            viewInterventionStates.Add(new SelectListItem() { Text = InterventionState.Cancelled.ToString(), Value = InterventionState.Cancelled.ToString() });
            viewInterventionStates.Add(new SelectListItem() { Text = InterventionState.Completed.ToString(), Value = InterventionState.Completed.ToString() });
            viewInterventionStates.Add(new SelectListItem() { Text = InterventionState.Proposed.ToString(), Value = InterventionState.Proposed.ToString() });

            InterventionViewModel interventionmodel = new InterventionViewModel()
            {
                InterventionTypeName = intervention.InterventionType.Name,
                ClientId = (Guid)intervention.ClientId,

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
                Comments = intervention.Comments,
                InterventionStates = viewInterventionStates
            };
            return interventionmodel;
        }
        private ClientViewModel BindSingleClient(Client client)
        {
            ClientViewModel clientmodel = new ClientViewModel()
            {
                Id = client.Id,
                DistrictName = Engineer.getDistrictName(client.DistrictId),
                Location = client.Location,
                Name = client.Name
            };
            return clientmodel;
        }
    }
}
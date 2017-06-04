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
<<<<<<< HEAD
        private IEngineerService engineer;

        public SiteEngineerController()
        {
            engineer = GetEngineerService();
        }

        public SiteEngineerController(IEngineerService engineer)
        {
            this.engineer = engineer;
        }
=======
       
>>>>>>> G

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



        public ActionResult CreateClient()
        {
            IEngineerService engineer = GetEngineerService();
            String districtName = engineer.getDetail().District.Name;

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

                IEngineerService engineer = GetEngineerService();
                Client client=engineer.createClient(clientVmodel.Name,clientVmodel.Location);

                var clientList = engineer.getClients();
                var clients = new List<ClientViewModel>();
                BindClient(clientList, clients);
                return View("ClientList", clients);

     

            }
            return View(clientVmodel);
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


        // GET : SiteEngineer/ClientList
       
        public ActionResult ClientList() {

            IEngineerService engineer = GetEngineerService();
            var clientList = engineer.getClients();
            List<ClientViewModel> clients = new List<ClientViewModel>();
            BindClient(clientList, clients);

            //var viewClientsList = new List<SelectListItem>();
            //foreach (var client in Clients)
            //{
            //    viewClientsList.Add(new SelectListItem() { Text = client.Name, Value = client.Id.ToString() });
            //}
            //var model = new SiteEngineerViewInterventionModel() { ViewInterventionTypeList = viewInterventionTypes, ViewClientsList = viewClientsList };

            return View(clients);

       

        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ClientList(FormCollection form)
        {
            return View();

           
        }





            // GET: SiteEngineer/Create
            public ActionResult CreateIntervention()
        {
<<<<<<< HEAD
            //mockup data
            List<Client> Clients = new List<Client>();
            Clients.Add(new Client("jammie", "chatswood", new Guid()));
=======
            IEngineerService engineer = GetEngineerService();
>>>>>>> G

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
                DateTime dateCreate = DateTime.Now;
                DateTime dateFinish = (DateTime)viewmodel.Intervention.DateFinish;
                DateTime dateRecentVisit = DateTime.Now;


                Guid createdBy = (Guid)engineer.getDetail().Id;
                Guid approvedBy = (Guid)engineer.getDetail().DistrictId;
                Guid typeId = new Guid(Request.Form["InterventionTypes"]);   
                Intervention new_intervention = new Intervention(hours, costs, lifeRemaining, comments, state,
                dateCreate, dateFinish, dateRecentVisit, typeId, clientId, createdBy, null);
                engineer.createIntervention(new_intervention);          
                var interventionList = engineer.GetAllInterventions(engineer.getDetail().Id).ToList();
                var interventions = new List<InterventionViewModel>();
                BindIntervention(interventionList, interventions);
                var model = new SiteEngineerViewInterventionModel() { Interventions = interventions };
                return View("InterventionList", model);

            }
            return View(viewmodel);
        }


        // GET: SiteEngineer/EditIntervention
        public ActionResult EditInterventionState(Guid id)
        {

            IEngineerService engineer = GetEngineerService();
            Intervention interention = engineer.getNonGuidInterventionById(id);
            InterventionViewModel model = BindSingleIntervention(interention);
            return View(model);

        }

        // POST: SiteEngineer/EditIntervention
        [HttpPost]
        public ActionResult EditInterventionState(InterventionViewModel interventionmodel)
        {
<<<<<<< HEAD
            var interventionList = engineer.getInterventionListByCreator(engineer.getDetail().Id).ToList();
            var interventions = new List<InterventionViewModel>();
=======




            IEngineerService engineer = GetEngineerService();
           
            //String new_comments = interventionmodel.Comments;
            //int new_liferemaining = interventionmodel.LifeRemaining;
            //DateTime new_recentvisit = interventionmodel.RecentiVisit;
>>>>>>> G

         //   engineer.updateInterventionDetail(interventionmodel.Id, new_comments, new_liferemaining, new_recentvisit);



            var interventionList = engineer.getInterventionsByClient(interventionmodel.ClientId);
            List<InterventionViewModel> interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList, interventions);


            Client client = engineer.getClientById(interventionmodel.ClientId);
            ClientViewModel clientViewModel = BindSingleClient(client);
            var model = new SiteEngineerViewClientModel() { Interventions = interventions, Client = clientViewModel };

            return View("ClientDetails", model);
        }







        // GET: SiteEngineer/EditIntervention
        public ActionResult EditIntervention(Guid id)
        {
            
            IEngineerService engineer = GetEngineerService();
              Intervention interention= engineer.getNonGuidInterventionById(id);
            InterventionViewModel model=BindSingleIntervention(interention);
            
             return View(model);
     
             }

        // POST: SiteEngineer/EditIntervention
        [HttpPost]
        public ActionResult EditIntervention(InterventionViewModel interventionmodel){




            IEngineerService engineer = GetEngineerService();
            String new_comments = interventionmodel.Comments;
            int new_liferemaining = interventionmodel.LifeRemaining;
            DateTime new_recentvisit = interventionmodel.RecentiVisit;
        
            engineer.updateInterventionDetail(interventionmodel.Id,new_comments,new_liferemaining,new_recentvisit);


          
            var interventionList = engineer.getInterventionsByClient(interventionmodel.ClientId);
            List<InterventionViewModel> interventions = new List<InterventionViewModel>();
           BindIntervention(interventionList, interventions);

    
            Client client = engineer.getClientById(interventionmodel.ClientId);
            ClientViewModel clientViewModel=  BindSingleClient(client);
            var model = new SiteEngineerViewClientModel() { Interventions = interventions,Client= clientViewModel };
          
            return View("ClientDetails", model);
          }


        // GET: SiteEngineer/ClientDetails/ClientId
        public ActionResult ClientDetails(Guid id)
        {

            IEngineerService engineer = GetEngineerService();
            var client= engineer.getClientById(id);
            var interventionList = engineer.getInterventionsByClient(id);
             List<InterventionViewModel> interventions = new List<InterventionViewModel>();
            BindIntervention(interventionList,interventions);
           ClientViewModel clientViewModel=BindSingleClient(client);


            //  SiteEngineerViewClientModel SE_VclientModel = new SiteEngineerViewClientModel();

            //  SE_VclientModel

            //  return View(interventions);
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
            //InterventionViewModel selectedIntervention = b.Intervention;

            //return View("EditIntervention", selectedIntervention);

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
            
       
                InterventionViewModel selectedIntervention = b.Intervention;
    
            return View("EditIntervention", selectedIntervention);
          
        }

        private IEngineerService GetEngineerService()
        {
            var identityId = User.Identity.GetUserId();
            IEngineerService engineer = new EngineerService("03869985-ae09-4331-8b0a-68b98084132a");
            return engineer;
        }

        public void BindClient(IEnumerable<IMSLogicLayer.Models.Client> clientList, List<ClientViewModel> clients) {
            IEngineerService engineer = GetEngineerService();
            foreach (var client in clientList)
            {
               

                clients.Add(new ClientViewModel()
                {
                    Id = client.Id,
                    DistrictName = engineer.getDistrictName(client.DistrictId),
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


                    //InterventionTypeName = intervention.InterventionType.Name,
                    //Id = (Guid)intervention.Id,
                    //ClientId = (Guid)intervention.ClientId,
                    //ClientName = intervention.Client.Name,
                    //DateCreate = intervention.DateCreate,
                    //InterventionState = intervention.InterventionState.ToString(),

                    //// ??
                    //DistrictName = intervention.District.Name,
                    //Costs = intervention.Costs,
                    //Hours = intervention.Hours,
                    //DateFinish = intervention.DateFinish,
                    //Comments = intervention.Comments

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
            IEngineerService engineer = GetEngineerService();
            ClientViewModel clientmodel = new ClientViewModel()
            {
                Id = client.Id,
                DistrictName = engineer.getDistrictName(client.DistrictId),
                Location = client.Location,
                Name = client.Name
            };
            return clientmodel;
        }
    }
}
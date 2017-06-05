using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Controllers;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Models;
using IMSLogicLayer.ServiceInterfaces;
using Moq;
using InterventionManagementSystem_MVC.Models;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    /// <summary>
    /// Unit testing class for SiteEngineer Controller
    /// </summary>
    [TestClass]
    public class SiteEngineerControllerTest
    {
        private SiteEngineerController controller;
        private Mock<IEngineerService> engineerService;
        private IMSLogicLayer.Models.User engineer;
        private IMSLogicLayer.Models.Client client;
        private IMSLogicLayer.Models.Intervention intervention;
        private const string SUCCESS_TEST_GUID = "f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea";
        private const string FAILED_TEST_GUID = "11111111-7e2b-4095-bc8a-594cbbbd77ea";

        /// <summary>
        /// Test setup initialization
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            IMSDBLayer.Models.User db_engineer = new IMSDBLayer.Models.User()
            {
                Id = new Guid(),
                Name = "John Smith",
                Type = 1,
                AuthorisedCosts = 40,
                AuthorisedHours = 40,
                IdentityId = "",
                DistrictId = new Guid()
            };

            IMSDBLayer.Models.Client db_client = new IMSDBLayer.Models.Client()
            {
                Id = new Guid(),
                Name = "John",
                DistrictId = new Guid()
            };

            IMSDBLayer.Models.District db_district = new IMSDBLayer.Models.District()
            {
                Id = new Guid(),
                Name = "NSW"
            };

            intervention = new IMSLogicLayer.Models.Intervention(40, 40, 4, "comments", IMSLogicLayer.Enums.InterventionState.Approved, new DateTime(), new DateTime(), new DateTime(), new Guid(), new Guid(), new Guid(), new Guid());
            client = new IMSLogicLayer.Models.Client(db_client);
            IMSLogicLayer.Models.District district = new IMSLogicLayer.Models.District(db_district); 

            engineer = new IMSLogicLayer.Models.User(db_engineer);
            engineer.District = district;
            intervention.District = district;
            intervention.Client = client;
            intervention.InterventionType = new IMSLogicLayer.Models.InterventionType("", 5, 5);

            engineerService = new Mock<IEngineerService>();
            engineerService.Setup(e => e.getDetail()).Returns(engineer);
            engineerService.Setup(e => e.getInterventionTypes()).Returns(new List<IMSLogicLayer.Models.InterventionType>());
            engineerService.Setup(e => e.createClient(It.IsAny<string>(), It.IsAny<string>())).Returns(new IMSLogicLayer.Models.Client("","",new Guid()));
            engineerService.Setup(e => e.getClients()).Returns(new List<IMSLogicLayer.Models.Client>());
            engineerService.Setup(e => e.getNonGuidInterventionById(It.IsAny<Guid>())).Returns(intervention);
            engineerService.Setup(e => e.updateInterventionState(new Guid(SUCCESS_TEST_GUID), It.IsAny<IMSLogicLayer.Enums.InterventionState>())).Returns(true);
            engineerService.Setup(e => e.updateInterventionState(new Guid(FAILED_TEST_GUID), It.IsAny<IMSLogicLayer.Enums.InterventionState>())).Returns(false);
            engineerService.Setup(e => e.GetAllInterventions(It.IsAny<Guid>())).Returns(new List<IMSLogicLayer.Models.Intervention>());
            engineerService.Setup(e => e.updateInterventionDetail(new Guid(SUCCESS_TEST_GUID), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns(true);
            engineerService.Setup(e => e.updateInterventionDetail(new Guid(FAILED_TEST_GUID), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns(false);
            engineerService.Setup(e => e.getInterventionsByClient(It.IsAny<Guid>())).Returns(new List<IMSLogicLayer.Models.Intervention>());
            engineerService.Setup(e => e.getClientById(It.IsAny<Guid>())).Returns(client);

            controller = new SiteEngineerController(engineerService.Object);
        }

        /// <summary>
        /// test for Index()
        /// the return view should contain model with detail of engineer
        /// </summary>
        [TestMethod]
        public void SiteEngineer_IndexViewIndexViewModel()
        {
            var view = controller.Index() as ViewResult;
            var model = (SiteEngineerViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.IsNotNull(model.DistrictName);
            Assert.IsNotNull(model.AuthorisedHours);
            Assert.IsNotNull(model.AuthorisedCosts);
            Assert.AreEqual(engineer.Name, model.Name);
        }
        
        /// <summary>
        /// test for createclient()
        /// the return view should contain the detail of the district, which mataches the distrcit of the engineer
        /// </summary>
        [TestMethod]
        public void SiteEngineer_CreateClientViewModel()
        {
            var view = controller.CreateClient() as ViewResult;
            var model = view.ViewData.Model as ClientViewModel;

            Assert.IsNotNull(model.DistrictName);
            Assert.AreEqual(model.DistrictName, engineer.District.Name);
        }

        /// <summary>
        /// test for CreateClient() with post data
        /// the return view should be the client list page with a list of engineer's client
        /// </summary>
        [TestMethod]
        public void SiteEngineer_CreateClientPost()
        {
            ClientViewModel viewModel = new ClientViewModel()
            {
                DistrictName = "NSW",
                Id = new Guid(),
                Location = "NSW",
                Name = "Po"
            };

            var view = controller.CreateClient(viewModel) as ViewResult;
            var model = view.ViewData.Model as List<ClientViewModel>;

            Assert.IsNotNull(model);
        }

        /// <summary>
        /// test for ClientList() with post data
        /// the return view should be the client list page with a list of engineer's client 
        /// </summary>
        [TestMethod]
        public void SiteEngineer_ClientListViewModel()
        {
            var view = controller.ClientList() as ViewResult;

            Assert.IsNotNull(view.ViewData.Model);
        }
        
        /// <summary>
        /// test for CreateIntervention() view
        /// the view should contain a list of clients and type of interventions
        /// </summary>
        [TestMethod]
        public void SiteEngineer_CreateInterventionViewModel()
        {
            var view = controller.CreateIntervention() as ViewResult;
            var model = (SiteEngineerViewInterventionModel)view.ViewData.Model;
            
            Assert.IsNotNull(model.ViewClientsList);
            Assert.IsNotNull(model.ViewInterventionTypeList);
        }

        /// <summary>
        /// test for EditInterventionState() view
        /// the view should contain a model with the detail of the intervention that matches the supplied id
        /// </summary>
        [TestMethod]
        public void SiteEngineer_EditInterventionState()
        {
            var view = controller.EditInterventionState(intervention.Id) as ViewResult;
            var model = view.Model as InterventionViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Id, intervention.Id);
        }

        /// <summary>
        /// test for EditInterventionState() with post data, faield case
        /// if the editinterveionstate failed, the user should be given the same edit page, with no data being changed
        /// </summary>
        [TestMethod]
        public void SiteEngineer_EditInterventionStatePostFailed()
        {

            InterventionViewModel viewModel = new InterventionViewModel()
            {
                SelectedState = "Approved",
                Id = new Guid(FAILED_TEST_GUID)
            };

            var view = controller.EditInterventionState(viewModel) as ViewResult;

            Assert.IsNotNull(view.Model);
            engineerService.Verify(e => e.getNonGuidInterventionById(It.IsAny<Guid>()), Times.AtLeastOnce());
        }

        /// <summary>
        /// test for EditInterventionState() with post data, success case
        /// if the editinterveionstate success, the user should be directed to the view of the interventionlist
        /// </summary>
        [TestMethod]
        public void SiteEngineer_EditInterventionStatePostSuccess()
        {
            InterventionViewModel viewModel = new InterventionViewModel()
            {
                SelectedState = "Approved",
                Id = new Guid(SUCCESS_TEST_GUID)
            };

            var view = controller.EditInterventionState(viewModel) as ViewResult;

            Assert.IsNotNull(view.Model);
            Assert.AreEqual("InterventionList", view.ViewName);
        }

        /// <summary>
        /// test for EditIntervention() view
        /// the view should contain the model with the detail of the intervention that matches the id
        /// </summary>
        [TestMethod]
        public void SiteEngineer_EditIntervention()
        {
            var view = controller.EditIntervention(intervention.Id) as ViewResult;
            var model = view.Model as InterventionViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Id, intervention.Id);
        }

        /// <summary>
        /// test for EditIntervention() with post data, failed case
        /// if the operation failed, the user would remain at the original page
        /// </summary>
        [TestMethod]
        public void SiteEngineer_EditInterventionPostFailed()
        {
            InterventionViewModel viewModel = new InterventionViewModel()
            {
                Comments = "Approved",
                Id = new Guid(FAILED_TEST_GUID),
                ClientId = client.Id,
                RecentiVisit = new DateTime(),
                LifeRemaining = 4
            };

            var view = controller.EditIntervention(viewModel) as ViewResult;

            Assert.IsNotNull(view);
        }

        /// <summary>
        /// test for EditIntervetion() with post data, success case
        /// if the operation success, the user would be direct to the client detail page
        /// </summary>
        [TestMethod]
        public void SiteEngineer_EditInterventionPostSuccess()
        {
            InterventionViewModel viewModel = new InterventionViewModel()
            {
                Comments = "Approved",
                Id = new Guid(SUCCESS_TEST_GUID),
                ClientId = client.Id,
                RecentiVisit = new DateTime(),
                LifeRemaining = 4
            };

            var view = controller.EditIntervention(viewModel) as ViewResult;

            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);
            Assert.AreEqual("ClientDetails", view.ViewName);
        }

        /// <summary>
        /// test for ClientDetails()
        /// the view should contain the model with the detail of the client that matches the supply id
        /// </summary>
        [TestMethod]
        public void SiteEngineer_ClientDetails()
        {
            var view = controller.ClientDetails(client.Id) as ViewResult;
            var model = view.Model as SiteEngineerViewClientModel;

            Assert.IsNotNull(model.Interventions);
            Assert.AreEqual(model.Client.Id, client.Id);
        }

        /// <summary>
        /// test for InterventionList()
        /// the view should contain the model with a list of intertventions
        /// </summary>
        [TestMethod]
        public void SiteEngineer_InterventionListViewModel()
        {
            var view = controller.InterventionList() as ViewResult;
            var model = (SiteEngineerViewInterventionModel)view.ViewData.Model;

            Assert.IsNotNull(model.Interventions);
        }
    }
}

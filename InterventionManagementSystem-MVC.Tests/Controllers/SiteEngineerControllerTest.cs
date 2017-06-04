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
    [TestClass]
    public class SiteEngineerControllerTest
    {
        private SiteEngineerController controller;
        private IMSLogicLayer.Models.User engineer;

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

            IMSDBLayer.Models.District db_district = new IMSDBLayer.Models.District()
            {
                Id = new Guid(),
                Name = "NSW"
            };

            engineer = new IMSLogicLayer.Models.User(db_engineer);
            engineer.District = new IMSLogicLayer.Models.District(db_district);

            Mock<IEngineerService> engineerService = new Mock<IEngineerService>();
            engineerService.Setup(e => e.getDetail()).Returns(engineer);
            engineerService.Setup(e => e.getInterventionTypes()).Returns(new List<IMSLogicLayer.Models.InterventionType>());
            engineerService.Setup(e => e.createClient(It.IsAny<string>(), It.IsAny<string>())).Returns(new IMSLogicLayer.Models.Client("","",new Guid()));
            engineerService.Setup(e => e.getClients()).Returns(new List<IMSLogicLayer.Models.Client>());

            controller = new SiteEngineerController(engineerService.Object);
        }

        [TestMethod]
        public void SiteEngineer_IndexViewIndexViewModel()
        {
            var view = controller.Index() as ViewResult;
            var model = (SiteEngineerViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.IsNotNull(model.DistrictName);
            Assert.IsNotNull(model.AuthorisedHours);
            Assert.IsNotNull(model.AuthorisedCosts);
        }
        
        [TestMethod]
        public void SiteEngineer_CreateClientViewModel()
        {
            var view = controller.CreateClient() as ViewResult;
            var model = view.ViewData.Model as ClientViewModel;

            Assert.IsNotNull(model.DistrictName);
            Assert.AreEqual(model.DistrictName, engineer.District.Name);
        }

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

            var view = controller.CreateClient(viewModel);

        }
        
        [TestMethod]
        public void SiteEngineer_IndexViewCreateInterventionViewModel()
        {
            var view = controller.CreateIntervention() as ViewResult;
            var model = (SiteEngineerViewInterventionModel)view.ViewData.Model;

            //Assert.IsNotNull(model.Interventions);
            //Assert.IsNotNull(model.SelectedType);
            Assert.IsNotNull(model.ViewClientsList);
            Assert.IsNotNull(model.ViewInterventionTypeList);
        }

        [TestMethod]
        public void SiteEngineer_IndexViewCreateInterventionViewPost()
        {

        }

        //[TestMethod]
        //public void SiteEngineer_IndexViewInterventionListView()
        //{
        //    var view = controller.InterventionList() as ViewResult;
        //    Assert.AreEqual("Interventions", view.ViewName);
        //}

        [TestMethod]
        public void SiteEngineer_IndexViewInterventionListViewModel()
        {
            var view = controller.InterventionList() as ViewResult;
            var model = (SiteEngineerViewInterventionModel)view.ViewData.Model;

            Assert.IsNotNull(model.Interventions);
        }
    }
}

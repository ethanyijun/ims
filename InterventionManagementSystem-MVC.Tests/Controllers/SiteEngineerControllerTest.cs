using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Controllers;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Models;
using IMSLogicLayer.ServiceInterfaces;
using Moq;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class SiteEngineerControllerTest
    {
        private SiteEngineerController controller;

        [TestInitialize]
        public void SetUp()
        {
            Mock<IEngineerService> engineerService = new Mock<IEngineerService>();


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
        
        [TestMethod]
        public void SiteEngineer_IndexViewInterventionListViewModel()
        {
            var view = controller.InterventionList() as ViewResult;
            var model = (SiteEngineerViewInterventionModel)view.ViewData.Model;

            Assert.IsNotNull(model.Interventions);
        }
    }
}

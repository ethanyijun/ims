using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Controllers;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.SiteEngineer.Models;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class SiteEngineerControllerTest
    {
        private SiteEngineerController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new SiteEngineerController();
        }

        [TestMethod]
        public void SiteEngineer_IndexView()
        {
            var view = controller.Index() as ViewResult;
            Assert.AreEqual("Index", view.ViewName);
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
        public void SiteEngineer_IndexViewCreateInterventionView()
        {
            var view = controller.CreateIntervention() as ViewResult;
            Assert.AreEqual("Create", view.ViewName);
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
        public void SiteEngineer_IndexViewInterventionListView()
        {
            var view = controller.InterventionList() as ViewResult;
            Assert.AreEqual("Interventions", view.ViewName);
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

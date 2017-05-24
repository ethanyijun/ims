using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.Manager.Controllers;
using InterventionManagementSystem_MVC.Areas.Manager.Models;
using System.Web.Mvc;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class ManagerControllerTest
    {
        private ManagerController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new ManagerController();
        }
        [TestMethod]
        public void TestIndexView()
        {
            var view = controller.Index() as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        public void TestIndexViewModel()
        {
            var view = controller.Index() as ViewResult;
            var model = (ManagerViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.IsNotNull(model.DistrictName);
            Assert.IsNotNull(model.AuthorisedCosts);
            Assert.IsNotNull(model.AuthorisedHours);
        }

        [TestMethod]
        public void TestInterventionListView()
        {
            var view = controller.InterventionList() as ViewResult;

            Assert.AreEqual("InterventionList", view.ViewName);
        }

        [TestMethod]
        public void TestInterventionListViewModel()
        {
            var view = controller.InterventionList() as ViewResult;
            var model = (ManagerViewInterventionModel)view.ViewData.Model;

            Assert.IsNotNull(model.ViewList);
            Assert.IsNotNull(model.Interventions);
        }

        [TestMethod]
        public void TestInterventionListViewPost()
        {

        }
    }
}

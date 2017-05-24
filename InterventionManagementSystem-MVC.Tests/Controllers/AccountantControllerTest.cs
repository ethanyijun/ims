using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.Accountant.Controllers;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.Accountant.Models;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class AccountantControllerTest
    {
        private AccountantController controller;
        [TestInitialize]
        public void SetUp()
        {
            controller = new AccountantController();
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
            var model = (AccountantViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
        }

        [TestMethod]
        public void TestAccountListView()
        {
            var view = controller.AccountListView() as ViewResult;

            Assert.AreEqual("AccountListView", view.ViewName);
        }

        [TestMethod]
        public void TestAccountListViewModel()
        {

        }

        [TestMethod]
        public void TestEditDistrictView()
        {

        }

        [TestMethod]
        public void TestEditDistrictViewModel()
        {

        }

        [TestMethod]
        public void TestEditDistrictViewPost()
        {

        }

        [TestMethod]
        public void TestReportListView()
        {

        }
    }
}

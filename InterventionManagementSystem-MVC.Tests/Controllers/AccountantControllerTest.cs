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
        public void Accountant_IndexView()
        {
            var view = controller.Index() as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }

        [TestMethod]
        public void Accountant_IndexViewModel()
        {
            var view = controller.Index() as ViewResult;
            var model = (AccountantViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
        }

        [TestMethod]
        public void Accountant_AccountListView()
        {
            var view = controller.AccountListView() as ViewResult;

            Assert.AreEqual("AccountListView", view.ViewName);
        }

        [TestMethod]
        public void Accountant_AccountListViewModel()
        {

        }

        [TestMethod]
        public void Accountant_EditDistrictView()
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

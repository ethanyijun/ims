using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.Accountant.Controllers;
using System.Web.Mvc;
using InterventionManagementSystem_MVC.Areas.Accountant.Models;
using Moq;
using IMSLogicLayer.ServiceInterfaces;
using System.Collections.Generic;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class AccountantControllerTest
    {
        private AccountantController controller;
        [TestInitialize]
        public void SetUp()
        {
            IMSDBLayer.Models.User db_accountant = new IMSDBLayer.Models.User()
            {
                Id = new Guid(),
                Name = "John Smith",
                Type = 1,
                AuthorisedCosts = 40,
                AuthorisedHours = 40,
                IdentityId = "",
                DistrictId = new Guid()
            };
            IMSDBLayer.Models.User db_user = new IMSDBLayer.Models.User()
            {
                Id = new Guid(),
                Name = "Mr. Engineer",
                Type = 1,
                AuthorisedCosts = 20,
                AuthorisedHours = 20,
                IdentityId = "",
                DistrictId = new Guid()
            };
            IMSDBLayer.Models.District db_district = new IMSDBLayer.Models.District()
            {
                Id = new Guid(),
                Name = "NSW"
            };
            IMSLogicLayer.Models.User accountant = new IMSLogicLayer.Models.User(db_accountant);
            IMSLogicLayer.Models.User user = new IMSLogicLayer.Models.User(db_user);
            IMSLogicLayer.Models.District district = new IMSLogicLayer.Models.District(db_district);

            Mock<IAccountantService> accountantService = new Mock<IAccountantService>();
            accountantService.Setup(a => a.getDetail()).Returns(accountant);
            accountantService.Setup(a => a.getAllManger()).Returns(new List<IMSLogicLayer.Models.User>());
            accountantService.Setup(a => a.getAllSiteEngineer()).Returns(new List<IMSLogicLayer.Models.User>());
            accountantService.Setup(a => a.getUserById(It.IsAny<Guid>())).Returns(user);
            accountantService.Setup(a => a.getDistrictForUser(It.IsAny<Guid>())).Returns(district);
            accountantService.Setup(a => a.getDistricts()).Returns(new List<IMSLogicLayer.Models.District>());

            controller = new AccountantController(accountantService.Object);
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
            var view = controller.AccountListView() as ViewResult;
            var model = (AccountListViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Managers);
            Assert.IsNotNull(model.SiteEngineers);
        }

        [TestMethod]
        public void Accountant_EditDistrictView()
        {

        }

        [TestMethod]
        public void TestEditDistrictViewModel()
        {
            var view = controller.EditDistrict("9D2B0228-4444-4C23-8B49-01A698857709") as ViewResult;
            var model = (EditDistrictViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.IsNotNull(model.Id);
            Assert.IsNotNull(model.CurrentDistrict);
            Assert.IsNotNull(model.DistrictList);
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

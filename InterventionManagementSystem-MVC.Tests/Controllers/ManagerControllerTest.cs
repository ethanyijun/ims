using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterventionManagementSystem_MVC.Areas.Manager.Controllers;
using InterventionManagementSystem_MVC.Areas.Manager.Models;
using System.Web.Mvc;
using IMSLogicLayer.ServiceInterfaces;
using Moq;

namespace InterventionManagementSystem_MVC.Tests.Controllers
{
    [TestClass]
    public class ManagerControllerTest
    {
        private ManagerController controller;

        [TestInitialize]
        public void SetUp()
        {
            IMSDBLayer.Models.User db_manager = new IMSDBLayer.Models.User()
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

            IMSLogicLayer.Models.User manager = new IMSLogicLayer.Models.User(db_manager);
            manager.District = new IMSLogicLayer.Models.District(db_district);

            Mock<IManagerService> managerService = new Mock<IManagerService>();
            managerService.Setup(m => m.GetDetail()).Returns(manager);
            managerService.Setup(m => m.GetApprovedInterventions()).Returns(new List<IMSLogicLayer.Models.Intervention>());

            controller = new ManagerController(managerService.Object);
        }

        [TestMethod]
        public void Manager_IndexViewModel()
        {
            var view = controller.Index() as ViewResult;
            var model = (ManagerViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.IsNotNull(model.DistrictName);
            Assert.IsNotNull(model.AuthorisedCosts);
            Assert.IsNotNull(model.AuthorisedHours);
        }

        [TestMethod]
        public void Manager_InterventionListViewModel()
        {
            var view = controller.InterventionList() as ViewResult;
            var model = (ManagerViewInterventionModel)view.ViewData.Model;

            Assert.IsNotNull(model.ViewList);
            Assert.IsNotNull(model.Interventions);
        }

        [TestMethod]
        public void Manager_InterventionListViewPostSelectedApproved()
        {

        }

        [TestMethod]
        public void Manager_InterventionListViewPostSelectedOthers()
        {

        }

        [TestMethod]
        public void Manager_ApproveInterventionSuccess()
        {
        }

        [TestMethod]
        public void Manager_ApproveInterventionFailed()
        {

        }
    }
}

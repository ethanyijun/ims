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
        private IMSLogicLayer.Models.User manager;

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

            manager = new IMSLogicLayer.Models.User(db_manager);
            manager.District = new IMSLogicLayer.Models.District(db_district);

            Mock<IManagerService> managerService = new Mock<IManagerService>();
            managerService.Setup(m => m.GetDetail()).Returns(manager);
            managerService.Setup(m => m.GetApprovedInterventions()).Returns(new List<IMSLogicLayer.Models.Intervention>());
            //managerService.Setup(m => m.GetInterventionsByState(It.Is(IMSLogicLayer.Enums.InterventionState.Proposed))).Returns(new List<IMSLogicLayer.Models.Intervention>());
            managerService.Setup(m => m.SendEmailNotification(It.IsAny<IMSLogicLayer.Models.Intervention>(), It.IsAny<string>()));

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
            var dataModel = new ManagerViewInterventionModel() {
                SelectedType  = "Approved"
            };
            var result = controller.InterventionList(dataModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("InterventionList", result.RouteValues["Action"]);
            Assert.AreEqual("Manager", result.RouteValues["Controller"]);
        }

        [TestMethod]
        public void Manager_InterventionListViewPostSelectedOthers()
        {
            var dataModel = new ManagerViewInterventionModel() {
                SelectedType  = "Proposed"
            };
            var view = controller.InterventionList(dataModel) as ViewResult;
            var model = view.ViewData.Model as ManagerViewInterventionModel;

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.ViewList);
            Assert.IsNotNull(model.Interventions);
            Assert.IsNotNull(model.AuthorisedCosts);
            Assert.IsNotNull(model.AuthorisedHours);
            Assert.AreEqual(model.AuthorisedCosts, manager.AuthorisedCosts);
            Assert.AreEqual(model.AuthorisedHours, manager.AuthorisedHours);
        }

        [TestMethod]
        public void Manager_ApproveInterventionSuccess()
        {
            Mock<IManagerService> managerService = new Mock<IManagerService>();

            managerService.Setup(m => m.GetDetail()).Returns(manager);
            managerService.Setup(m => m.ApproveAnIntervention(It.IsAny<Guid>())).Returns(true);
            managerService.Setup(m => m.SendEmailNotification(It.IsAny<IMSLogicLayer.Models.Intervention>(), It.IsAny<string>()));
            managerService.Setup(m => m.GetInterventionById(It.IsAny<Guid>())).Returns(new IMSLogicLayer.Models.Intervention(40, 40, 4, "comments", IMSLogicLayer.Enums.InterventionState.Approved, new DateTime(), new DateTime(), new DateTime(), new Guid(), new Guid(), new Guid(), new Guid()));

            var controller = new ManagerController(managerService.Object);

            var result = controller.ApproveIntervention("f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea") as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("InterventionList", result.RouteValues["Action"]);
            Assert.AreEqual("Manager", result.RouteValues["Controller"]);
        }

        [TestMethod]
        public void Manager_ApproveInterventionFailed()
        {
            Mock<IManagerService> managerService = new Mock<IManagerService>();

            managerService.Setup(m => m.ApproveAnIntervention(It.IsAny<Guid>())).Returns(false);

            var controller = new ManagerController(managerService.Object);

            var view = controller.ApproveIntervention("f2c4f7b0-7e2b-4095-bc8a-594cbbbd77ea") as ViewResult;

            Assert.AreEqual("Error", view.ViewName);
        }
    }
}

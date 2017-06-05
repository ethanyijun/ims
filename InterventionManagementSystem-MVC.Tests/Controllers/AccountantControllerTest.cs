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
    /// <summary>
    /// Unit Testing class for Accountant Controller
    /// </summary>
    [TestClass]
    public class AccountantControllerTest
    {
        private AccountantController controller;
        private Mock<IAccountantService> accountantService;
        private IMSLogicLayer.Models.User user;

        /// <summary>
        /// test set up initialization
        /// </summary>
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
            user = new IMSLogicLayer.Models.User(db_user);
            IMSLogicLayer.Models.District district = new IMSLogicLayer.Models.District(db_district);

            accountantService = new Mock<IAccountantService>();
            accountantService.Setup(a => a.getDetail()).Returns(accountant);
            accountantService.Setup(a => a.getAllManger()).Returns(new List<IMSLogicLayer.Models.User>());
            accountantService.Setup(a => a.getAllSiteEngineer()).Returns(new List<IMSLogicLayer.Models.User>());
            accountantService.Setup(a => a.getUserById(It.IsAny<Guid>())).Returns(user);
            accountantService.Setup(a => a.getDistrictForUser(It.IsAny<Guid>())).Returns(district);
            accountantService.Setup(a => a.getDistricts()).Returns(new List<IMSLogicLayer.Models.District>());
            accountantService.Setup(a => a.printAverageCostByEngineer()).Returns(new List<IMSLogicLayer.Models.ReportRow>());
            accountantService.Setup(a => a.printMonthlyCostByDistrict(It.IsAny<Guid>())).Returns(new List<IMSLogicLayer.Models.ReportRow>());
            accountantService.Setup(a => a.printTotalCostByDistrict()).Returns(new List<IMSLogicLayer.Models.ReportRow>());
            accountantService.Setup(a => a.printTotalCostByEngineer()).Returns(new List<IMSLogicLayer.Models.ReportRow>());

            controller = new AccountantController(accountantService.Object);
        }
        
        /// <summary>
        /// test for Index()
        /// the view should contain the model with the detail of the accountant
        /// </summary>
        [TestMethod]
        public void Accountant_IndexViewModel()
        {
            var view = controller.Index() as ViewResult;
            var model = (AccountantViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.AreEqual("John Smith", model.Name);
        }
        
        /// <summary>
        /// test for AccountListView()
        /// the view should contain a list of managers and site engineers
        /// </summary>
        [TestMethod]
        public void Accountant_AccountListViewModel()
        {
            var view = controller.AccountListView() as ViewResult;
            var model = (AccountListViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Managers);
            Assert.IsNotNull(model.SiteEngineers);
        }

        /// <summary>
        /// test for EditDistrict()
        /// the view should contain a model with district details and list of district
        /// </summary>
        [TestMethod]
        public void Accountant_EditDistrictViewModel()
        {
            var view = controller.EditDistrict("9D2B0228-4444-4C23-8B49-01A698857709") as ViewResult;
            var model = (EditDistrictViewModel)view.ViewData.Model;

            Assert.IsNotNull(model.Name);
            Assert.IsNotNull(model.Id);
            Assert.IsNotNull(model.CurrentDistrict);
            Assert.IsNotNull(model.DistrictList);
        }

        /// <summary>
        /// test for success case of EditDistrict() with posting data
        /// if the changedistrict() success, the user should be redirect to the EditDistrict page
        /// </summary>
        [TestMethod]
        public void Accountant_EditDistrictViewPostSuccess()
        {
            Mock<IAccountantService> accountantService = new Mock<IAccountantService>();
            accountantService.Setup(a => a.changeDistrict(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(true);
            accountantService.Setup(a => a.getUserById(It.IsAny<Guid>())).Returns(user);

            AccountantController controller = new AccountantController(accountantService.Object);

            EditDistrictViewModel district = new EditDistrictViewModel()
            {
                Id = user.Id.ToString(),
                SelectedDistrict = user.DistrictId.ToString()
            };
            var result = controller.EditDistrict(district) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("EditDistrict", result.RouteValues["Action"]);
            Assert.AreEqual("Accountant", result.RouteValues["Controller"]);
        }

        /// <summary>
        /// test for failed case of EditDistrict() with posting data
        /// if the changedistrict() failed, the user should be redirect to the Error page
        /// </summary>
        [TestMethod]
        public void Accountant_EditDistrictViewPostFailed()
        {
            Mock<IAccountantService> accountantService = new Mock<IAccountantService>();
            accountantService.Setup(a => a.changeDistrict(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            accountantService.Setup(a => a.getUserById(It.IsAny<Guid>())).Returns(user);

            AccountantController controller = new AccountantController(accountantService.Object);

            EditDistrictViewModel district = new EditDistrictViewModel()
            {
                Id = "9D2B0228-4444-4C23-8B49-01A698857709",
                SelectedDistrict = "9D2B0228-5555-4C23-8B49-01A698857709"
            };
            var view = controller.EditDistrict(district) as ViewResult;

            Assert.IsNotNull(view);
            Assert.AreEqual("Error", view.ViewName);
        }

        /// <summary>
        /// test for ReportList()
        /// the view should containt a model with a list of Report generated from Database
        /// </summary>
        [TestMethod]
        public void Accountant_ReportListViewModel()
        {
            var view = controller.ReportList() as ViewResult;
            var model = view.ViewData.Model as ReportListViewModel;

            Assert.IsNotNull(model);
            Assert.IsNotNull(model.ReportList);
        }

        /// <summary>
        /// test for PrintReport(), failed case
        /// if no type of report is specified, the user is direct to an error page
        /// </summary>
        [TestMethod]
        public void Accountant_PrintReportFailed()
        {
            var view = controller.PrintReport("") as ViewResult;

            Assert.AreEqual("Error", view.ViewName);
        }

        /// <summary>
        /// test for PrintReport("AverageCostByEngineer")
        /// the return view should be the Report page with view model data populate by printAverageCostByEngineer();
        /// </summary>
        [TestMethod]
        public void Accountant_PrintReportAverageCostByEngineer()
        {
            var view = controller.PrintReport("AverageCostByEngineer") as ViewResult;

            Assert.AreEqual("Report", view.ViewName);
            Assert.IsNotNull(view.ViewData.Model);
            accountantService.Verify(a => a.printAverageCostByEngineer(), Times.AtLeastOnce());
        }

        /// <summary>
        /// test for PrintReport("TotalCostByDistrict")
        /// the return view should be the TotalDistrictReport page with view model data populate by printTotalCostByDistrict();
        /// </summary>
        [TestMethod]
        public void Accountant_PrintReportTotalCostByDistrict()
        {
            var view = controller.PrintReport("TotalCostByDistrict") as ViewResult;

            Assert.AreEqual("TotalDistrictReport", view.ViewName);
            Assert.IsNotNull(view.ViewData.Model);
            accountantService.Verify(a => a.printTotalCostByDistrict(), Times.AtLeastOnce());
        }

        /// <summary>
        /// test for PrintReport("TotalCostByEngineer")
        /// the return view should be the Report page with view model data populate by printTotalCostByEngineer();
        /// </summary>
        [TestMethod]
        public void Accountant_PrintReportTotalCostByEngineer()
        {
            var view = controller.PrintReport("TotalCostByEngineer") as ViewResult;

            Assert.AreEqual("Report", view.ViewName);
            Assert.IsNotNull(view.ViewData.Model);
            accountantService.Verify(a => a.printTotalCostByEngineer(), Times.AtLeastOnce());
        }

        /// <summary>
        /// test for PrintReport("MonthlyCostByDistrict")
        /// the return view should be the MonthlyDistrictReport page with view model data;
        /// </summary>
        [TestMethod]
        public void Accountant_PrintMonthlyReport()
        {
            var view = controller.PrintReport("MonthlyCostByDistrict") as ViewResult;

            Assert.AreEqual("MonthlyDistrictReport", view.ViewName);
            Assert.IsNotNull(view.ViewData.Model);
        }

        /// <summary>
        /// test for PrintMonthlyReport() null argument
        /// the user should be redirect to the basic view of report page, which is PrintMonthlyReport page
        /// </summary>
        [TestMethod]
        public void Accountant_PrintMonethlyReportPostNull()
        {
            var result = controller.PrintMonthlyReport(null) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("PrintMonthlyReport", result.RouteValues["Action"]);
            Assert.AreEqual("Accountant", result.RouteValues["Controller"]);
        }

        /// <summary>
        /// test for PrintMonthlyReport() with proper view model supplied
        /// the return view should be the MonthlyDistrictReport page with view model data populated by printMonthlycostbyDistrict();
        /// </summary>
        [TestMethod]
        public void Accountant_PrintMonethlyReportPost()
        {
            MonthlyDistrictReportViewModel viewModel = new MonthlyDistrictReportViewModel()
            {
                SelectedDistrict = "9D2B0228-5555-4C23-8B49-01A698857709"
            };

            var view = controller.PrintMonthlyReport(viewModel) as ViewResult;

            Assert.AreEqual("MonthlyDistrictReport", view.ViewName);
            Assert.IsNotNull(view.ViewData.Model);
            accountantService.Verify(a => a.printMonthlyCostByDistrict(It.IsAny<Guid>()), Times.AtLeastOnce());
        }
    }
}

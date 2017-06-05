using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMSLogicLayer.ServiceInterfaces;
using IMSLogicLayer.Services;
using System.Net.Mail;
using IMSLogicLayer.Models;

namespace InterventionManagementSystem_MVC.Tests.Service
{
    [TestClass]
    public class EmailServiceTest
    {
        private IEmailService emailService;
        private Intervention intervention;

        [TestInitialize]
        public void Setup()
        {
            emailService = new IMSLogicLayer.Services.EmailService();
            intervention = new Intervention(40, 40, 4, "comments", IMSLogicLayer.Enums.InterventionState.Approved, new DateTime(), new DateTime(), new DateTime(), new Guid(), new Guid(), new Guid(), new Guid());
        }
        [TestMethod]
        public void EmailService_CreateMessage()
        {
            string FromAddress = "test@google.com";
            string ToAddress = "to@google.com";
            string UserFrom = "me";
            string UserTo = "client";
            MailMessage message = emailService.CreateMessage(FromAddress, ToAddress, UserFrom, UserTo, intervention);

            Assert.AreEqual(message.From, new MailAddress(FromAddress));
            Assert.IsTrue(message.To.Contains(new MailAddress(ToAddress)));
        }
    }
}

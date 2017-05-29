using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using IMSLogicLayer.Models;

namespace IMSLogicLayer.Services
{
    public class EmailService : ServiceInterfaces.IEmailService
    {
        public MailMessage CreateMessage(string FromAddress, string ToAddress, string UserFrom, string UserTo,Intervention intervention)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("FromAddress");
            message.To.Add(ToAddress);
            message.Subject = "Approval Notification";

            message.Body = UserFrom + "has approved the intervention :" + intervention.InterventionType + " create by " + UserTo + " on " + DateTime.Now;
            return message;
        }

        public void SendEmail(MailMessage message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace IMSLogicLayer.ServiceInterfaces
{
    public interface IEmailService
    {
        MailMessage CreateMessage(string FromAddress,string ToAddress, string UserFrom, string UserTo, Models.Intervention intervention);

        void SendEmail(MailMessage message);

    }
}

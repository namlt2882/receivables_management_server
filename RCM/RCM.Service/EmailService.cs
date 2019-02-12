using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace RCM.Service
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        public void SendEmail(string email, string subject, string message)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("thongvhse61933@gmail.com", "VHTSE61933");
            client.EnableSsl = true;
            objeto_mail.Subject = subject;
            objeto_mail.From = new MailAddress("thongvhse61933@gmail.com");
            objeto_mail.To.Add(new MailAddress(email));
            objeto_mail.Body = message;
            client.Send(objeto_mail);
        }
    }
}

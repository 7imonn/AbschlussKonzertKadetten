using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Interface;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AbschlussKonzertKadetten.Repository
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(string email)
        {
            //var client = new SmtpClient("smtp.gmail.com")
            //{
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("nobelentimon@gmail.com", "#No14@bel7$en3.5"),
            //    Port = 587
            //};
            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress("confermation-noreply@yourdomain.com")
            //};
            //email = "nobelentimon@gmail.com";
            //mailMessage.To.Add(email);
            //mailMessage.Body = "body";
            //mailMessage.Subject = "subject";
            //await client.SendMailAsync(mailMessage);
        }
    }
}

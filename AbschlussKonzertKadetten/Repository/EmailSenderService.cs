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
            var client = new SmtpClient("mail.popnet.ch")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("info@schlusskonzert-kadetten-thun.ch", "z/tDwa=9Mk"),
                Port = 465,
                EnableSsl = true
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress("info@schlusskonzert-kadetten-thun.ch")
            };
            //email = "nobelentimon@gmail.com";
            mailMessage.To.Add(email);
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            await client.SendMailAsync(mailMessage);
        }
    }
}

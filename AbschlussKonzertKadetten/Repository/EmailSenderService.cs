using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AbschlussKonzertKadetten.Interface;

namespace AbschlussKonzertKadetten.Repository
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IRedactorRepo _redactorRepo;

        public EmailSenderService(IRedactorRepo redactorRepo)
        {
            _redactorRepo = redactorRepo;
        }
        public async Task SendEmailAsync(string email)
        {
            var body = await _redactorRepo.GetReactorByNameAsync("emailtext");
            string bodyText = Regex.Replace(body.Text , "<[^(?!br)>]*>", " ");
            var client = new SmtpClient("mail.popnet.ch")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("info@schlusskonzert-kadetten-thun.ch", "z/tDwa=9Mk")
            };
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("schlusskonzert@gmx.ch");
            mailMessage.To.Add(email);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body.Text;
            mailMessage.Subject = "Bestellung Schlusskonzert Kadetten Thun";

            client.Send(mailMessage);
        }
    }
}

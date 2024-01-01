using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace JamesRecipes.Models
{
    public class SendMail : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("");
                mailMessage.Subject = subject;
                mailMessage.Body = email + htmlMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(email));
                SmtpClient stmp = new SmtpClient();
                stmp.Host = "smtp.gmail.com";
                stmp.EnableSsl = true;
                System.Net.NetworkCredential NetwordCred = new System.Net.NetworkCredential();
                NetwordCred.UserName = "";
                NetwordCred.Password = "";
                stmp.UseDefaultCredentials = true;
                stmp.Credentials = NetwordCred;
                stmp.Port = 25;
                await stmp.SendMailAsync(mailMessage);
            }
        }
    }
}

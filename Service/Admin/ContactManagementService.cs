using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using model.Models;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class ContactManagementService: IContactManagementRepository
{
    private readonly JamesrecipesContext _db;

    public ContactManagementService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Contact> GetAllContact()
    {
        return _db.Contacts.ToList();
    }

    public Contact GetContact(int id)
    {
        return _db.Contacts.Find(id)!;
    }

    public IPagedList<Contact> PagedList(int page, int pageSize, List<Contact> contacts)
    {
        return contacts.ToPagedList(page, pageSize);
    }
    
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("JamesThrew", "jamesthrew1@gmail.com"));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("jamesthrew1@gmail.com", "vmne fxfr bbof gmbb");

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public void SwitchStatus(int  id)
    {
        var ct = _db.Contacts.Find(id)!;
        ct.Status = true;
        _db.SaveChanges();
    }
}
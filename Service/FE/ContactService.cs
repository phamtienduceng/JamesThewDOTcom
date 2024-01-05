using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class ContactService: IContact
{
    private readonly JamesrecipesContext _db;

    public ContactService(JamesrecipesContext db)
    {
        _db = db;
    }
    public void SendContact(Contact newContact)
    {
        _db.Contacts.Add(newContact);
        _db.SaveChanges();
    }
}
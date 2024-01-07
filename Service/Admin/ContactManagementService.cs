using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
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
}
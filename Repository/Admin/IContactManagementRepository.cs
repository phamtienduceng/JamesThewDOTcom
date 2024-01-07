using JamesRecipes.Models;
using model.Models;
using X.PagedList;

namespace JamesRecipes.Repository.Admin;

public interface IContactManagementRepository
{
    List<Contact> GetAllContact();

    Contact GetContact(int id);

    IPagedList<Contact> PagedList(int page, int pageSize, List<Contact> contacts);

}
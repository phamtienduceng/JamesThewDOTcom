using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;

namespace JamesRecipes.Service.Admin;

public class AccountManagementService : IAccountManagementRepository
{
    private readonly JamesrecipesContext _db;

    public AccountManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public void AddAdmin(User newAdmin)
    {
        _db.Add(newAdmin);
        _db.SaveChanges();
    }

    public User GetAdminByEmail(string email)
    {
        return _db.Users.SingleOrDefault(u => u.Email.Equals(email));
    }

    public List<ViewUserRole> GetAllUsers()
    {
        return _db.ViewUserRoles.OrderByDescending(u => u.UserId).ToList();
    }
}
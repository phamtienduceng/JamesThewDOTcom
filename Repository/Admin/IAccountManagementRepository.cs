using JamesRecipes.Models;
using JamesRecipes.Models;
using model.Models;

namespace JamesRecipes.Repository.Admin;

public interface IAccountManagementRepository
{
    void AddAdmin(User newAdmin);
    User GetAdminByEmail(string email);

    List<ViewUserRole> GetAllUsers();
    List<Membership> GetAllmember();
}
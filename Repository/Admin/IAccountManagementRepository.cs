using JamesRecipes.Models;
using JamesRecipes.Models.View;

namespace JamesRecipes.Repository.Admin;

public interface IAccountManagementRepository
{
    void AddAdmin(User newAdmin);
    User GetAdminByEmail(string email);

    List<ViewUserRole> GetAllUsers();
}
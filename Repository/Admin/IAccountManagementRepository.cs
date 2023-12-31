using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IAccountManagementRepository
{
    void AddAdmin(User newAdmin);
    User GetAdminByEmail(string email);

    List<ViewUserRole> GetAllUsers();
}
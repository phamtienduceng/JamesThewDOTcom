using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE
{
    public interface IAccount
    {
        User GetUserByEmail(string email);
        bool VerifyPassword(string password, string hashedPassword);
        void AddUser(User newUser);
        User GetUserById(int id);
        
        Membership GetMembershipById(int id);
        void UpdateUser(User user);
        
        void UpdateProfile(int id, UserMem user);

        void ChangeRole(int id);
    }
}

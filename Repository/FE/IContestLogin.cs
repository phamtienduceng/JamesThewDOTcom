using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE
{
    public interface IContestLogin
    {
        User GetUserByEmail(string email);
        bool VerifyPassword(string password, string hashedPassword);
        void AddUser(User newUser);
        User GetUserById(int id);
        void UpdateUser(User user);
    }
}

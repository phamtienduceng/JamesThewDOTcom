using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE
{
    public class AccountService : IAccount
    {
        private readonly JamesrecipesContext _db;

        public AccountService(JamesrecipesContext db)
        {
            _db = db;
        }
        public void UpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public void AddUser(User newUser)
        {
            _db.Users.Add(newUser);
            _db.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email.Equals(email));
        }

        public User GetUserById(int id)
        {
            return _db.Users.SingleOrDefault(u => u.UserId == id);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

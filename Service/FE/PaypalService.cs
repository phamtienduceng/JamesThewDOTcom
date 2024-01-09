using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using model.Models;

namespace JamesRecipes.Service.FE
{
    public class PaypalService : IPaypal
    {
        private readonly JamesrecipesContext _db;

        public PaypalService(JamesrecipesContext db)
        {
            _db = db;
        }

        public void AddMember(Membership newmember)
        {
            _db.Memberships.Add(newmember);
            _db.SaveChanges();
        }

        public Membership MemberById(int id)
        {
            return _db.Memberships.FirstOrDefault(m => m.UserId == id);
        }

        public void UpdateMember(Membership member)
        {
            _db.Memberships.Update(member);
            _db.SaveChanges();
        }
        
    }
}

using JamesRecipes.Models;
using model.Models;

namespace JamesRecipes.Repository.FE
{
    public interface IPaypal
    {
        Membership MemberById(int id);
        void AddMember(Membership newmember);
        void UpdateMember(Membership member);
        
    }
}

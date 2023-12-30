using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE
{
    public interface IPaypal
    {
        Membership MemberById(int id);
        void AddMember(Membership newmember);
        void UpdateMember(Membership member);
    }
}

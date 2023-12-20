using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Orders>> UserOrders();
    }
}

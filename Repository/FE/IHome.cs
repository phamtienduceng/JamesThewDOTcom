using JamesRecipes.Models;
using JamesRecipes.Models.View;

namespace JamesRecipes.Repository.FE;

public interface IHome
{
    List<ViewHomepage> GetHomepages();
}
using JamesRecipes.Models;

namespace JamesRecipes.Repository.FE;

public interface IFaq
{
    List<Faq> GetAllFaqs();
}
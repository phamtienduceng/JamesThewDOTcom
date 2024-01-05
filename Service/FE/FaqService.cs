using JamesRecipes.Models;
using JamesRecipes.Repository.FE;

namespace JamesRecipes.Service.FE;

public class FaqService: IFaq
{
    private readonly JamesrecipesContext _db;

    public FaqService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Faq> GetAllFaqs()
    {
        return _db.Faqs.ToList();
    }
}
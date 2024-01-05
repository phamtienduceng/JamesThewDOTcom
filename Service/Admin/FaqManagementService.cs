using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class FaqManagementService: IFaqManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public FaqManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    public List<Faq> GetAll()
    {
        return _db.Faqs.ToList();
    }

    public Faq GetOneFaq(int id)
    {
        return _db.Faqs.Find(id)!;
    }

    public Faq CreateFaq(Faq newFaq)
    {
        _db.Faqs.Add(newFaq);
        _db.SaveChanges();
        return newFaq;
    }

    public Faq UpdateFaq(int id, Faq faq)
    {
        var f = GetOneFaq(id);
        if (f != null)
        {
            f.Question = faq.Question;
            f.Answer = faq.Answer;
            _db.SaveChanges();
            return faq;
        }

        return null!;
    }

    public void DeleteFaq(int id)
    {
        var faq = GetOneFaq(id);
        if (faq != null)
        {
            _db.Faqs.Remove(faq);
            _db.SaveChanges();
        }
    }

    public IPagedList<Faq> PagedList(int page, int pageSize, List<Faq> faq)
    {
        return faq.ToPagedList(page, pageSize);
    }

    public List<Faq> Sorting(List<Faq> faqs, string sortOrder)
    {
        switch (sortOrder)
        {
            case "Date":
                faqs = faqs.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                faqs = faqs.OrderByDescending(r => r.CreatedAt).ToList();
                break;
            default:
                faqs = faqs.OrderByDescending(r => r.CreatedAt).ToList();
                break;
        }
        return faqs;
    }

    public List<Faq> Search(string searchString)
    {
        return _db.Faqs.Where(r => r.Question.Contains(searchString)).ToList();

    }
}
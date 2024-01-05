using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JamesRecipes.Service.Admin;

public class TipManagementService: ITipManagementRepository
{
    private readonly JamesrecipesContext _db;
    
    public TipManagementService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Tip> GetAllTip()
    {
        return _db.Tips.Include(f=>f.User)
            .ThenInclude(f=>f!.Role)
            .Include(f=>f.CategoryTip)
            .OrderByDescending(f=>f.CreatedAt)
            .ToList();
    }

    public Tip GetTip(int id)
    {
        return _db.Tips.Include(r=>r.Feedbacks)
            .ThenInclude(r=>r.User)
            .Include(f=>f.User).Include(r=>r.User!.Role)
            .Include(f=>f.CategoryTip)
            .SingleOrDefault(r => r.TipId == id)!;
    }

    public IPagedList<Tip> PagedList(int page, int pageSize, List<Tip> tips)
    {
        return tips.ToPagedList(page, pageSize);

    }


    public List<Tip> Sorting(List<Tip> tips, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                tips = tips.OrderByDescending(r => r.Title).ToList();
                break;
            case "name_asc":
                tips = tips.OrderBy(r => r.Title).ToList();
                break;
            case "Date":
                tips = tips.OrderBy(r => r.CreatedAt).ToList();
                break;
            case "date_desc":
                tips = tips.OrderByDescending(r => r.CreatedAt).ToList();
                break;
            case "rating":
                tips = tips.OrderBy(r => r.Rating).ToList();
                break;
            case "rating_desc":
                tips = tips.OrderByDescending(r => r.Rating).ToList();
                break;
            default:
                tips = tips.OrderByDescending(r => r.CreatedAt).ToList();
                break;
        }
        return tips;
    }

    public List<Tip> Search(string searchString)
    {
        return _db.Tips.Where(r => r.Title.Contains(searchString)).ToList();

    }
}
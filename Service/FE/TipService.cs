using JamesRecipes.Models;
using JamesRecipes.Repository.FE;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace JamesRecipes.Service.FE;

public class TipService: ITip
{
    private readonly JamesrecipesContext _db;

    public TipService(JamesrecipesContext db)
    {
        _db = db;
    }

    public List<Tip> GetAllTips()
    {
        return _db.Tips.Where(t => t.Status == true).ToList();
    }

    public List<Tip> GetAllTipsPremium()
    {
        throw new NotImplementedException();
    }

    public Tip GetTip(int id)
    {
        var tip = _db.Tips.Include(r=>r.Feedbacks).SingleOrDefault(t => t.TipId == id);
        return tip ?? null!;
    }

    public List<Tip> GetTipsByUser(int id)
    {
        return _db.Tips.Where(t => t.UserId == id).ToList();
    }

    public void PostTip(Tip newTip)
    {
        _db.Tips.Add(newTip);
        _db.SaveChanges();
    }

    public List<Tip> Sorting(List<Tip> tips, string sortOrder)
    {
        switch (sortOrder)
        {
            case "name_desc":
                tips = tips.OrderByDescending(t => t.Title).ToList();
                break;
            case "name_asc":
                tips = tips.OrderBy(t => t.Title).ToList();
                break;
            case "Date":
                tips = tips.OrderBy(t => t.CreatedAt).ToList();
                break;
            case "date_desc":
                tips = tips.OrderByDescending(t => t.CreatedAt).ToList();
                break;
            case "rating":
                tips = tips.OrderBy(t => t.Rating).ToList();
                break;
            case "rating_desc":
                tips = tips.OrderByDescending(t => t.Rating).ToList();
                break;
            default:
                tips = tips.OrderByDescending(t => t.CreatedAt).ToList();
                break;
        }
        return tips;
    }

    public List<Tip> Search(string searchString)
    {
        return _db.Tips.Where(t => t.Title.Contains(searchString)).ToList();
    }

    public void SwitchStatus(int id, bool status)
    {
        var tip = _db.Tips.SingleOrDefault(t => t.TipId == id);
        if (tip != null)
        {
            tip.Status = status;
            _db.SaveChanges(); 
        }
    }

    public IPagedList<Tip> PageList(int page, int pageSize, List<Tip> tips)
    {
        return tips.ToPagedList(page, pageSize);
    }

    public void UpdateRatingTip(int tipId)
    {
        var tip = _db.Tips.SingleOrDefault(t => t.TipId == tipId);
        if (tip != null)
        {
            var averageRating = _db.Feedbacks
                .Where(f => f.TipId == tipId && f.Rating.HasValue)
                .Select(f => f.Rating!.Value)
                .Average();

            tip.Rating = (int)Math.Round(averageRating);
            _db.SaveChanges();
        }
    }

    public List<Tip> Filter(int? categoryId, int? ratingMin, int? ratingMax, List<Tip> tips)
    {
        var ts = tips;

        if (categoryId.HasValue)
        {
            ts = ts.Where(t => t.CategoryTipId == categoryId).ToList();

        }

        if (ratingMin.HasValue && ratingMax.HasValue)
        {
            ts = ts.Where(t => t.Rating >= ratingMin && t.Rating <= ratingMax).ToList();

        }
        return ts;
    }

    public void DeleteMyTip(int id)
    {
        var tip = _db.Tips.Include(t => t.Feedbacks)
            .SingleOrDefault(t => t.TipId == id);

        if (tip != null)
        {
            _db.Feedbacks.RemoveRange(tip.Feedbacks);
            _db.Tips.Remove(tip);
            _db.SaveChanges();
        }
    }
}
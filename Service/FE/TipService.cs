using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
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
        return _db.Tips
            .Include(r=>r.User)
            .ThenInclude(r=>r.Role)
            .Include(r=>r.CategoryTip)
            .Where(r=>r.Status == true && r.IsMembershipOnly == false).ToList();
    }

    public List<Tip> GetAllTipsPremium()
    {
        return _db.Tips
            .Include(r=>r.User)
            .ThenInclude(r=>r.Role)
            .Include(r=>r.CategoryTip)
            .Where(r => r.Status == true).ToList();
    }

    public Tip GetTip(int id)
    {
        var tip = _db.Tips
            .Include(r=>r.Feedbacks)
            .Include(f=>f.CategoryTip)
            .ThenInclude(f=>f.Tips)
            .SingleOrDefault(r => r.TipId == id);
        return tip ?? null!;
    }

    public Tip GetOneTip(int id)
    {
        var rep = _db.Tips.SingleOrDefault(r => r.TipId == id);
        return rep ?? null!;
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

    public void PremiumStatus(int id, bool isPre)
    {
        var tip = _db.Tips.SingleOrDefault(t => t.TipId == id);
        if (tip != null)
        {
            tip.IsMembershipOnly = isPre;
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

    public void UpdateTip(int id, Tip newTip)
    {
        var tip = GetOneTip(id);
        if (tip != null)
        {
            tip!.Title = newTip.Title;
            tip.Content = newTip.Content;
            tip.Image = newTip.Image;
            tip.CategoryTipId = newTip.CategoryTipId;
            _db.Update(newTip);
            _db.SaveChanges();
        }
    }

    public List<Tip> RelatedTips()
    {
        return _db.Tips.Take(10).ToList();
    }

    public byte[] GeneratedWord(Tip tip)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                var mainPart = wordDocument.AddMainDocumentPart();
                var document = new Document();
                mainPart.Document = document;

                var body = new Body();
                document.Append(body);

                var paragraph = new Paragraph(
                    new Run(
                        new Text($"TipId: {tip.TipId}"),
                        new Break(),  
                        new Text($"Title: {tip.Title}"),
                        new Break(),
                        new Text($"Content: {tip.Content}")
                    )
                );
                
                body.Append(paragraph);

                wordDocument.Save();
            }

            return memoryStream.ToArray();
        }
    }
}
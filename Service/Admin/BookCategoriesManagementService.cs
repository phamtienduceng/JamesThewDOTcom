using System.Data.Entity;
using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;

namespace JamesRecipes.Service.Admin;

public class BookCategoriesManagementService: IBookCategoriesManagementRepository
{
    private readonly JamesrecipesContext _db;
    

    public BookCategoriesManagementService(JamesrecipesContext db)
    {
        _db = db;
    }
    
    public List<CategoriesBook> GetAllCategories()
    {
        return _db.CategoriesBooks.ToList();
    }

    public CategoriesBook GetCategory(int id)
    {
        var cateBook = _db.CategoriesBooks.SingleOrDefault(c => c.CategoryBookId == id);
        return cateBook;
    }

    public void PostCategory(CategoriesBook newCategoriesBook)
    {
        _db.CategoriesBooks.Add(newCategoriesBook);
        _db.SaveChanges();
    }

    public void PutCategory(int id, CategoriesBook newCategoriesBook)
    {
        var model = GetCategory(id);
        model!.CategoryName = newCategoriesBook.CategoryName;
        _db.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var model = _db.CategoriesBooks.SingleOrDefault(c => c.CategoryBookId == id);
        if (model != null)
        {
            _db.CategoriesBooks.Remove(model);
            _db.SaveChanges();
        }
    }

    public bool CheckBook(CategoriesBook newCategoriesBook)
    {
        var model = _db.CategoriesBooks.Include(c=>c.Books).SingleOrDefault(c => c.CategoryBookId == newCategoriesBook.CategoryBookId);
        if (model!.Books.Any())
        {
            return true;
        }
        return false;
    }

    public List<Book> GetBooksByCategory(int id)
    {
        return _db.Books.Where(r => r.CategoryId == id).ToList();
    }
}
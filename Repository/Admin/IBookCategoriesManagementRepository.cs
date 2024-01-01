using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IBookCategoriesManagementRepository
{
    List<CategoriesBook> GetAllCategories();

    CategoriesBook GetCategory(int id);

    void PostCategory(CategoriesBook newCategoriesBook);

    void PutCategory(int id, CategoriesBook newCategoriesBook);

    void DeleteCategory(int id);

    bool CheckBook(CategoriesBook newCategoriesBook);

    List<Book> GetBooksByCategory(int id);
}
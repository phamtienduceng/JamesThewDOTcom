using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface ITipCategoriesManagementRepository
{
    List<CategoriesTip> GetAllCategories();

    CategoriesTip GetCategory(int id);

    void PostCategory(CategoriesTip newCategoriesTip);

    void PutCategory(int id, CategoriesTip categoriesTip);

    void DeleteCategory(int id);

    bool CheckTip(CategoriesTip categoriesTip);

    List<Tip> GetTipsByCategory(int id);
}
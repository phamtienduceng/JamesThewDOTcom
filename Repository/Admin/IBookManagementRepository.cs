using JamesRecipes.Models;

namespace JamesRecipes.Repository.Admin;

public interface IBookManagementRepository
{

    Task<IEnumerable<Genre>> Genres();
    Task<IEnumerable<Book>> GetBooks(string sTerm = "", int genreId = 0);
}
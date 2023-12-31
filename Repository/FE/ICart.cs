using JamesRecipes.Models.Book;
using JamesRecipes.Repository.FE;
using Microsoft.AspNetCore.Mvc;

namespace JamesRecipes.Repository.Admin
{
    public interface ICart
    {
        Book GetBookById(int bookId);
        void UpdateBook(Book book);
    }
}

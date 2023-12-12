using JamesRecipes.Models;
using JamesRecipes.Repository.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;

namespace JamesRecipes.Service.Admin;

public class BookManagementService : IBookManagementRepository
{
    private readonly JamesrecipesContext _context;

    public BookManagementService(JamesrecipesContext context)
    {
        this._context = context;
    }

    public async Task<Book> CreateBook(Book newBook)
    {
        var book =  _context.Books.FirstOrDefault(book => book.BookId.Equals(newBook));
        if (book == null)
        {
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

        }
        return newBook;
    }

    public async Task<bool> DeleteBook(int bookId)
    {
        var book = await _context.Books.FirstOrDefaultAsync(book => book.BookId == bookId);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return await _context.Books.ToListAsync();
        
    }

    public async Task<Book> GetBook(int bookId)
    {
        return await _context.Books.FindAsync(bookId);
    }

    public async Task<Book> UpdateBook(int bookId, int price, int quantity)
    {
        var book = await _context.Books.FirstOrDefaultAsync(book => book.BookId == bookId);
        if (book == null)
        {
            book = new Book()
            {
                BookId = price,
                Price = price,
                StockQuantity = quantity,
            };
            _context.Books.Add(book);
        }
        else
        {
            book.Price = price;
            book.StockQuantity = quantity;
        }
        await _context.SaveChangesAsync();
        return book;
    }
}
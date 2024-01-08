using JamesRecipes.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Data.Helpers
{
	public class SeedData
	{
		public static void SeedingData(JamesrecipesContext _context) 
		{
			_context.Database.Migrate();
			if (!_context.Books.Any()) 
			{
				CategoryModel VietnamFood = new CategoryModel { CategoryName = "VietnamFood", Slug = "vietnamfood", Description = "Day la sach mon viet", Status = 1 };
				CategoryModel ChinaFood = new CategoryModel { CategoryName = "ChinaFood", Slug = "chinafood", Description = "Day la sach mon Hoa", Status = 1 };
				_context.Books.AddRange(
					new BookModel { Title = "The Food of Vietnam", Slug = "vietnam", Description = "Day la sach The Food of Vietnam", Author = "Luke Nguyễn", Price = 45, Image = "The-Food-of-Vietnam.jpg", Category = VietnamFood },
					new BookModel { Title = "Gok Cooks Chinese", Slug = "china", Description = "Day la sach Gok Cooks Chinese", Author = "LGok Wan", Price = 55, Image = "Chinese-Cookery.jpg", Category = ChinaFood }
				);
				_context.SaveChanges();
			}
		}
	}
}

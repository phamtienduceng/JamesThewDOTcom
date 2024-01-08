using Microsoft.EntityFrameworkCore;

namespace JamesRecipes.Models.Book
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new JamesrecipesContext(serviceProvider.GetRequiredService<DbContextOptions<JamesrecipesContext>>()))
            {
                if (context.Books.Any())    // Check if database contains any books
                {
                    return;     // Database contains books already
                }

                context.Books.AddRange(
                    new Book
                    {
                        Title = "5 Ingredient Cooking for Two",
                        ISBN = "9789129688313",
                        DatePublished = DateTime.Parse("2013-9-26"),
                        Price = 139,
                        Author = "Robin Donovan",
                        ImageUrl = "/images/5-Ingredient-Cooking-for-Two.jpg"
                    },

                    new Book
                    {
                        Title = "Bread Street Kitchen",
                        ISBN = "9780261102354",
                        DatePublished = DateTime.Parse("1991-7-4"),
                        Price = 100,
                        Author = "Gordon Ramsay",
                        ImageUrl = "/images/Bread-Street-Kitchen.jpg"
                    },

                    new Book
                    {
                        Title = "The Food of Vietnam",
                        ISBN = "9780062068408",
                        DatePublished = DateTime.Parse("2011-6-1"),
                        Price = 91,
                        Author = "Luke Nguyễn",
                        ImageUrl = "/images/Magnolia-Table.jpg"
                    },

                    new Book
                    {
                        Title = "The Pho Cookbook",
                        ISBN = "9780062068408",
                        DatePublished = DateTime.Parse("1994-1-2"),
                        Price = 166,
                        Author = "Andrea Nguyen",
                        ImageUrl = "/images/The-Pho-Cookbook.jpg"
                    },

                    new Book
                    {
                        Title = "Edomae Sushi ",
                        ISBN = "9780062068408",
                        DatePublished = DateTime.Parse("1994-8-18"),
                        Price = 84,
                        Author = "Kikuo Shimizu",
                        ImageUrl = "/images/Edomae-Sushi.jpg"
                    },

                    new Book
                    {
                        Title = "Gok Cooks Chinese",
                        ISBN = "9780307386458",
                        DatePublished = DateTime.Parse("2007-5-1"),
                        Price = 95,
                        Author = "Gok Wan",
                        ImageUrl = "/images/Once-Upon-a-Chef-the-Cookbook.jpg"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

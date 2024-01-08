using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models.Book
{
    public class CartItem
    {
        public int BookId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public BookModel books { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models.Book
{
    public class CartItems
    {
        public int CartItemsId { get; set; }
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Cart Cart { get; set; }
    }
}

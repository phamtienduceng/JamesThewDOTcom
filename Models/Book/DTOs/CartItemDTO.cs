namespace JamesRecipes.Models.Book.DTOs
{
    public class CartItemDTO
    {
        public int BookId { get; set; }
        public decimal Quantity { get; set; }
        public Book Books { get; set; }
    }
}

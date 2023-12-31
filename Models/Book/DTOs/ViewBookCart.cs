namespace JamesRecipes.Models.Book.DTOs
{
    public class ViewBookCart
    {
        public string Message { get; set; }
        public List<Cart> Cart { get; set; }
        public decimal Total { get; set; }
        public List<CartItemDTO> CartData { get; set; }
    }
}

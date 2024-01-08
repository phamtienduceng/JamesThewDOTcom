namespace JamesRecipes.Models.Book
{
	public class CartItemModel
	{
		public int BookID { get; set; }
		public string? Title { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total { get { return Quantity * Price; } }
		public string? Image { get; set; }
		public CartItemModel() { }

		public CartItemModel(BookModel book)
		{
			BookID = book.Id;
			Title = book.Title;
			Price = book.Price;
			Quantity = 1;
			Image = book.Image;
		}
	}
}

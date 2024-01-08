using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models.Book
{
	[Table("Categories")]
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? CategoryName { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		public string? Slug { get; set; }
		public int Status { get; set; }
	}
}

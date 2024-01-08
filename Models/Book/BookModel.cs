using JamesRecipes.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models.Book;
[Table("Books")]
public partial class BookModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Description { set; get; }
    public string? Author { get; set; }
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int CategoryId { get; set; }
    public string? Image { get; set; } = "noname.jpg";
    [NotMapped]
    [FileExtension]
    public IFormFile ImageUpload { get; set; }
    public CategoryModel? Category { get; set; }
}

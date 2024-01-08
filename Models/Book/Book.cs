using JamesRecipes.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamesRecipes.Models.Book;
public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity )]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [Required,MaxLength(13)]
    public string ISBN { get; set; }
    [Required,
    DataType(DataType.Date),
    Display(Name = "Date Published")]
    public DateTime DatePublished { get; set; }
    [Required,
    DataType(DataType.Currency)]
    public int Price { get; set; }
    [Required]
    public string Author { get; set; }
    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; }
}

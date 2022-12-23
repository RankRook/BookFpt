using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookFpt.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please fill book title!")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please fill date time!")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please fill book price!")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please fill book genre!")]
        public string? Genre { get; set; }
        [Required(ErrorMessage = "Please fill author!")]
        public string? Author { get; set; }
        public string? BookImagePath { get; set; }
        public string? BookFileName { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Please fill book qty!")]
        public int Qty { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public virtual Category? Category { get; set; }
    }
}

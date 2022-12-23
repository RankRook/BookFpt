using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookFpt.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}

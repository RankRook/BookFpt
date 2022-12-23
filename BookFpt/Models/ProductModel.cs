using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookFpt.Models
{
    public class ProductModel
    {
        public List<Category>? cat { get; set; }
        public List<Book>? book { get; set; }
    }
}


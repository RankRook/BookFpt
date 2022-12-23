using System.ComponentModel.DataAnnotations;

namespace BookFpt.Models
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}

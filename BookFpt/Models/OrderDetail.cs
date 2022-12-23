
namespace BookFpt.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order OrderNavigation { get; set; }
        public virtual Book BookNavigation { get; set; }
    }
}

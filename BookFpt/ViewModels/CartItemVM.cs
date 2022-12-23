namespace BookFpt.ViewModels
{
    public class CartItemVM
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ProfilePicture { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Sum => Price * Quantity;
    }
}

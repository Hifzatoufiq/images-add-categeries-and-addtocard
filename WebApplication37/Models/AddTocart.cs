namespace WebApplication37.Models
{
    public class AddTocart
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int productId { get; set; }
        public int QTY { get; set; }
        public decimal UnitPrice { get; set; }
        public Users User { get; set; }
        public int UserId { get; set; }
    }
}

namespace _4232_pp.Models
{
    public class OrderDetail
    {
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public float Price { get; set; }
    }
}

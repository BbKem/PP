namespace _4232_pp.Models
{
    public class CartItem
    {
        public long CartItemId { get; set; }
        public long CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; } 
    }
}

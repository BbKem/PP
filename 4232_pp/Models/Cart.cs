namespace _4232_pp.Models
{
    public class Cart
    {
        public long CartId { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}

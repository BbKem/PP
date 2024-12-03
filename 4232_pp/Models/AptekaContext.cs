using Microsoft.EntityFrameworkCore;

namespace _4232_pp.Models
{
    public class AptekaContext : DbContext
    {

        public DbSet<Product> Products{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public AptekaContext(DbContextOptions<AptekaContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}

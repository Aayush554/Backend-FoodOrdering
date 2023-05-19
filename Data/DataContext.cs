using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<Payment> Payment { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MenuItem>()
        //        .HasOne(m => m.Category)
        //        .WithMany(c => c.MenuItems)
        //        .HasForeignKey(m => m.CategoryId)
        //        .OnDelete(DeleteBehavior.NoAction);

        //    modelBuilder.Entity<Cart>()
        //        .HasOne(c => c.User)
        //        .WithOne(c => c.Cart)
        //        .HasForeignKey(c => c.UserId)
        //        .OnDelete(DeleteBehavior.NoAction);

        //    modelBuilder.Entity<CartItem>()
        //        .HasKey(po => new { po.MenuItemId, po.CartId });
        //    modelBuilder.Entity<CartItem>()
        //            .HasOne(p => p.Cart)
        //            .WithMany(pc => pc.CartItems)
        //            .HasForeignKey(p => p.CartId);
        //    modelBuilder.Entity<CartItem>()
        //            .HasOne(p => p.MenuItem)
        //            .WithMany(pc => pc.CartItems)
        //            .HasForeignKey(c => c.MenuItemId);

        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.User)
        //        .WithMany(u => u.Orders)
        //        .HasForeignKey(o => o.UserId)
        //        .OnDelete(DeleteBehavior.NoAction);

        //    modelBuilder.Entity<Payment>()
        //        .HasOne(o => o.User)
        //        .WithMany(u => u.Payments)
        //        .HasForeignKey(o => o.UserId)
        //        .OnDelete(DeleteBehavior.NoAction);

        //    modelBuilder.Entity<Review>()
        //        .HasOne(o => o.User)
        //        .WithMany(u => u.Reviews)
        //        .HasForeignKey(o => o.UserId)
        //        .OnDelete(DeleteBehavior.NoAction);
        //    modelBuilder.Entity<Order>()
        //        .HasOne(o => o.Payment)
        //        .WithMany(u => u.Orders)
        //        .HasForeignKey(o => o.PaymentId)
        //        .OnDelete(DeleteBehavior.NoAction);

        //}
    }
}

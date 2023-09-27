using FoodOrderingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApi.Data
{
    /*
     * NAME
     * 
     * DataContext - Represents the data context for the Food Ordering API.
     * 
     * DESCRIPTION
     * 
     * The DataContext class is responsible for defining the database context for the Food Ordering API. It inherits from Entity Framework Core's DbContext class and provides DbSet properties for accessing various entities in the database.
     */
    public class DataContext : DbContext
    {
        /*
         * CONSTRUCTOR
         * 
         * DataContext - Initializes a new instance of the DataContext class.
         * 
         * PARAMETERS
         * 
         * options - DbContextOptions<DataContext> object containing options for configuring the database context.
         */
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        /*
         * ENTITY SET
         * 
         * Users - Represents a DbSet for the User entity in the database.
         */
        public DbSet<User> Users { get; set; }

        /*
         * ENTITY SET
         * 
         * Categories - Represents a DbSet for the Category entity in the database.
         */
        public DbSet<Category> Categories { get; set; }

        /*
         * ENTITY SET
         * 
         * MenuItems - Represents a DbSet for the MenuItem entity in the database.
         */
        public DbSet<MenuItem> MenuItems { get; set; }

        /*
         * ENTITY SET
         * 
         * Carts - Represents a DbSet for the Cart entity in the database.
         */
        public DbSet<Cart> Carts { get; set; }

        /*
         * ENTITY SET
         * 
         * Orders - Represents a DbSet for the Order entity in the database.
         */
        public DbSet<Order> Orders { get; set; }

        /*
         * ENTITY SET
         * 
         * CartItems - Represents a DbSet for the CartItem entity in the database.
         */
        public DbSet<CartItem> CartItems { get; set; }

        /*
         * ENTITY SET
         * 
         * OrderedItems - Represents a DbSet for the OrderedItems entity in the database.
         */
        public DbSet<OrderedItems> OrderedItems { get; set; }

        /*
         * ENTITY SET
         * 
         * Reviews - Represents a DbSet for the Review entity in the database.
         */
        public DbSet<Review> Reviews { get; set; }

        /*
         * ENTITY SET
         * 
         * ContactUs - Represents a DbSet for the ContactUs entity in the database.
         */
        public DbSet<ContactUs> ContactUs { get; set; }

        /*
         * ENTITY SET
         * 
         * Payment - Represents a DbSet for the Payment entity in the database.
         */
        public DbSet<Payment> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
             * CONFIGURATION
             * 
             * Configure the primary key for the CartItem entity using a composite key.
             */
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.CartId, ci.MenuItemId });

            /*
             * RELATIONSHIP
             * 
             * Define the relationship between CartItem and Cart entities.
             */
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            /*
             * RELATIONSHIP
             * 
             * Define the relationship between CartItem and MenuItem entities.
             */
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.MenuItem)
                .WithMany(mi => mi.CartItems)
                .HasForeignKey(ci => ci.MenuItemId);

            /*
             * CONFIGURATION
             * 
             * Configure the primary key for the OrderedItems entity using a composite key.
             */
            modelBuilder.Entity<OrderedItems>()
                .HasKey(ci => new { ci.OrderId, ci.MenuItemId });

            /*
             * RELATIONSHIP
             * 
             * Define the relationship between OrderedItems and Order entities.
             */
            modelBuilder.Entity<OrderedItems>()
                .HasOne(ci => ci.Order)
                .WithMany(c => c.OrderedItems)
                .HasForeignKey(ci => ci.OrderId);

            /*
             * RELATIONSHIP
             * 
             * Define the relationship between OrderedItems and MenuItem entities.
             */
            modelBuilder.Entity<OrderedItems>()
                .HasOne(ci => ci.MenuItem)
                .WithMany(mi => mi.OrderedItems)
                .HasForeignKey(ci => ci.MenuItemId);
        }
    }
}

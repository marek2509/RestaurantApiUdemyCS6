using Microsoft.EntityFrameworkCore;

namespace RestaurantApiUdemyCS6.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> option) : base(option)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(u => u.Nationality)
               .IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.Firstname)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .Property(u => u.Lastname)
                .IsRequired(false);

            modelBuilder.Entity<Restaurant>()
                 .Property(r => r.Name)
                 .IsRequired()
                 .HasMaxLength(25);

            modelBuilder.Entity<Restaurant>()
               .Property(r => r.ContactEmail)
               .IsRequired(false);

            modelBuilder.Entity<Restaurant>()
               .Property(r => r.ContactNumber)
               .IsRequired(false);

            modelBuilder.Entity<Dish>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(r => r.Street)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(r => r.City)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}

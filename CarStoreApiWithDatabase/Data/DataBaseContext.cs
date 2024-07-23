using Microsoft.EntityFrameworkCore;

namespace CarStoreApi.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }

        public User GetUserWithCars(int userId)
        {
            return Users
                .Include(u => u.cars) // Include the related cars
                .FirstOrDefault(u => u.Id == userId);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u =>u.cars)
                .WithMany(c =>c.Users)
                .UsingEntity(j =>j.ToTable("Recommendtion"));

            modelBuilder.Entity<User>()
                .HasData(
                    new User (2,"amin" , "amin1234" , true)
                    );


           

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data.Entities;

namespace RetailxAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
            modelBuilder.Entity<QformCategory>().HasNoKey();
            modelBuilder.Entity<UserQform>().HasKey(u => new {u.QformId,u.UserId });

            modelBuilder.Entity<CategoryQuestions>().HasKey(q => new { q.CategoryID, q.RowOrder });

            modelBuilder.Entity<Shop>().Property(s => s.Latitude)
                .HasColumnType("NUMERIC(18,15)");
            modelBuilder.Entity<Shop>().Property(s => s.Longitude)
                .HasColumnType("NUMERIC(18,15)");
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryQuestions> CategoryQuestions { get; set; }
        public DbSet<Qform> Qforms { get; set; }
        public DbSet<QformCategory> QformCategories { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<UserQform> UserQform { get; set; }
    }
}

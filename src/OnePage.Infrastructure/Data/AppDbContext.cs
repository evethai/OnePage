using Microsoft.EntityFrameworkCore;
using OnePage.Domain.Entities;

namespace OnePage.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.SKU).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.SKU).IsUnique();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.PropertiesJson).HasDefaultValue("{}");
            });

            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();
            });
        }
    }
}

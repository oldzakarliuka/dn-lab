using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using shop.Entities;

namespace shop
{
  public class ApplicationContext : DbContext
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> Product_Category { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<History> History { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Product>()
     .HasMany<Review>(p => p.Reviews)
     .WithOne(r => r.Product)
     .HasForeignKey(r => r.ProductId);

      modelBuilder.Entity<Product>()
      .HasMany<History>(p => p.Histories)
      .WithOne(h => h.Product)
      .HasForeignKey(h => h.ProductId);


      modelBuilder.Entity<ProductCategory>().HasKey(u => new { u.ProductId, u.CategoryId });
      modelBuilder.Entity<ProductCategory>()
          .HasOne(pc => pc.Product)
          .WithMany(p => p.ProductCategory)
          .HasForeignKey(pc => pc.ProductId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<ProductCategory>()
         .HasOne(pc => pc.Category)
         .WithMany(c => c.ProductCategory)
         .HasForeignKey(pc => pc.CategoryId)
          .OnDelete(DeleteBehavior.Cascade);


      modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,4)");
    }

    public override int SaveChanges()
    {
      var entries = ChangeTracker
      .Entries()
      .Where(e => e.Entity is BaseEntity && (
              e.State == EntityState.Added
              || e.State == EntityState.Modified));

      foreach (var entityEntry in entries)
      {
        ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;

        if (entityEntry.State == EntityState.Added)
        {
          ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
        }
      }

      return base.SaveChanges();
    }

    private void updateUpdatedProperty<T>() where T : class
    {
      var modifiedCategory =
          ChangeTracker.Entries<T>()
          .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

      foreach (var entry in modifiedCategory)
      {
        entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
      }
    }
  }
}
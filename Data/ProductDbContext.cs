using Microsoft.EntityFrameworkCore;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", Price = 10.00m, Description = "Description 1", Stock = 100, Category = "Category 1" },
            new Product { Id = 2, Name = "Product 2", Price = 20.00m, Description = "Description 2", Stock = 200, Category = "Category 2" },
            new Product { Id = 3, Name = "Product 3", Price = 30.00m, Description = "Description 3", Stock = 300, Category = "Category 3" }
        );
    }
}
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess.DataAccess;

public class AppDbContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-JT5F1N0\SQLEXPRESS;Database=ShopDB1;Trusted_Connection=True;TrustServerCertificate=True");
    }
   
    public DbSet<User> users { get; set; } = null!;
    //public DbSet<Product> products { get; set; } = null!;
    //public DbSet<ProductInvoice> productsInvoices { get; set; } = null!;
    public DbSet<Wallet> wallets { get; set; } = null!;
    //public DbSet<Invoice> invoices { get; set; } = null!;
    //public DbSet<Discount> discounts { get; set; } = null!;
    //public DbSet<Category> categories { get; set; } = null!;
    //public DbSet<Brand> brands { get; set; } = null!;
    //public DbSet<Basket> baskets { get; set; }=null!;
    //public DbSet<BasketProduct> basketproducts { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User Configuration
        modelBuilder.Entity<User>()
            .Property(user => user.UserName).IsRequired();
        modelBuilder.Entity<User>()
            .Property(user => user.UserSurname).IsRequired();
        modelBuilder.Entity<User>()
            .Property(user => user.Email).IsRequired().HasMaxLength(255).IsUnicode(false);
        modelBuilder.Entity<User>()
            .Property(user => user.Password).IsRequired().HasMaxLength(255).IsUnicode(false);
        modelBuilder.Entity<User>()
            .Property(user => user.Phone).IsRequired().HasMaxLength(20).IsUnicode(false);

        // Wallet Configuration
        modelBuilder.Entity<Wallet>()
            .Property(wallet => wallet.CardName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Wallet>()
            .Property(wallet => wallet.CardNumber).IsRequired();
        modelBuilder.Entity<Wallet>()
            .Property(wallet => wallet.Balance).IsRequired();
        modelBuilder.Entity<Wallet>()
            .Property(wallet => wallet.UserId).IsRequired();

        // Relationship with User
        modelBuilder.Entity<Wallet>()
            .HasOne(wallet => wallet.User)
            .WithMany(user => user.Wallets)
            .HasForeignKey(wallet => wallet.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

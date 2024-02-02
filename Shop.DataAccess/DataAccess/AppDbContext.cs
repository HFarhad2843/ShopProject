using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;

namespace Shop.DataAccess.DataAccess;

public class AppDbContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-JT5F1N0\SQLEXPRESS;Database=ShopDB2;Trusted_Connection=True;TrustServerCertificate=True");
    }
   
    public DbSet<User> users { get; set; } = null!;
    public DbSet<Product> products { get; set; } = null!;
    public DbSet<ProductInvoice> productsInvoices { get; set; } = null!;
    public DbSet<Wallet> wallets { get; set; } = null!;
    public DbSet<Invoice> invoices { get; set; } = null!;
    public DbSet<Discount> discounts { get; set; } = null!;
    public DbSet<Category> categories { get; set; } = null!;
    public DbSet<Brand> brands { get; set; } = null!;
    public DbSet<Basket> baskets { get; set; } = null!;
    public DbSet<BasketProduct> basketproducts { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //// User Configuration
        //modelBuilder.Entity<User>()
        //    .Property(user => user.UserName).IsRequired();
        //modelBuilder.Entity<User>()
        //    .Property(user => user.UserSurname).IsRequired();
        //modelBuilder.Entity<User>()
        //    .Property(user => user.Email).IsRequired().HasMaxLength(255).IsUnicode(false);
        //modelBuilder.Entity<User>()
        //    .Property(user => user.Password).IsRequired().HasMaxLength(255).IsUnicode(false);
        //modelBuilder.Entity<User>()
        //    .Property(user => user.Phone).IsRequired().HasMaxLength(20).IsUnicode(false);

        //// Wallet Configuration
        //modelBuilder.Entity<Wallet>()
        //    .Property(wallet => wallet.CardName).IsRequired().HasMaxLength(100);
        //modelBuilder.Entity<Wallet>()
        //    .Property(wallet => wallet.CardNumber).IsRequired();
        //modelBuilder.Entity<Wallet>()
        //    .Property(wallet => wallet.Balance).IsRequired();
        //modelBuilder.Entity<Wallet>()
        //    .Property(wallet => wallet.UserId).IsRequired();

        //// Relationship with User
        //modelBuilder.Entity<Wallet>()
        //    .HasOne(wallet => wallet.User)
        //    .WithMany(user => user.Wallets)
        //    .HasForeignKey(wallet => wallet.UserId)
        //    .OnDelete(DeleteBehavior.Restrict);
        // Configure Basket entity
        modelBuilder.Entity<Basket>()
            .HasOne(b => b.User)
            .WithMany(u => u.Baskets)
            .HasForeignKey(b => b.UserId);

        // Configure BasketProduct entity
        modelBuilder.Entity<BasketProduct>()
            .HasOne(bp => bp.Product)
            .WithMany(p => p.BasketProducts)
            .HasForeignKey(bp => bp.ProductId);

        modelBuilder.Entity<BasketProduct>()
            .HasOne(bp => bp.Basket)
            .WithMany(b => b.BasketProducts)
            .HasForeignKey(bp => bp.BasketId);

        // Configure Brand entity
        modelBuilder.Entity<Brand>()
            .HasMany(b => b.Products)
            .WithOne(p => p.Brand)
            .HasForeignKey(p => p.BrandId);

        // Configure Category entity
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        // Configure Discount entity
        modelBuilder.Entity<Discount>()
            .HasMany(d => d.Products)
            .WithOne(p => p.Discount)
            .HasForeignKey(p => p.DiscountId);

        // Configure Invoice entity
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Wallet)
            .WithMany(w => w.Invoices)
            .HasForeignKey(i => i.WalletId);

        // Configure Product entity
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Discount)
            .WithMany(d => d.Products)
            .HasForeignKey(p => p.DiscountId);

        // Configure ProductInvoice entity
        modelBuilder.Entity<ProductInvoice>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInvoices)
            .HasForeignKey(pi => pi.ProductId);

        modelBuilder.Entity<ProductInvoice>()
            .HasOne(pi => pi.Invoice)
            .WithMany(i => i.ProductInvoices)
            .HasForeignKey(pi => pi.InvoiceId);

        // Configure User entity
        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);
    }
}

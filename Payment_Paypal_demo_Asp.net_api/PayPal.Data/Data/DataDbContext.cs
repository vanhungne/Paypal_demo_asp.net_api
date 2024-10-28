using Microsoft.EntityFrameworkCore;
using PayPal.Data.Data.Config;
using PayPal.Data.Entities;
using PayPal.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Data.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() { }
        public DataDbContext(DbContextOptions<DataDbContext> options)
             : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            // User configuration
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Address configuration
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Addresses)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Product configuration
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.SKU).HasMaxLength(50).IsRequired();
                entity.HasIndex(e => e.SKU).IsUnique();
            });

            // Order configuration
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderNumber).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.ShippingAddress)
                    .WithMany()
                    .HasForeignKey(e => e.ShippingAddressId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.BillingAddress)
                    .WithMany()
                    .HasForeignKey(e => e.BillingAddressId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Payment configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaypalOrderId).HasMaxLength(100);
                entity.HasOne(e => e.Order)
                    .WithOne(o => o.Payment)
                    .HasForeignKey<Payment>(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
                base.OnModelCreating(modelBuilder);
                // Seed Users
                modelBuilder.Entity<Users>().HasData(
                    new Users
                    {
                        Id = 1,
                        Email = "john.doe@example.com",
                        Username = "john_doe",
                        PasswordHash = "hashed_password",
                        FirstName = "John",
                        LastName = "Doe",
                        PhoneNumber = "123456789",
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    },
                    new Users
                    {
                        Id = 2,
                        Email = "jane.smith@example.com",
                        Username = "jane_smith",
                        PasswordHash = "hashed_password",
                        FirstName = "Jane",
                        LastName = "Smith",
                        PhoneNumber = "987654321",
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    }
                );
                // Seed Addresses
                modelBuilder.Entity<Address>().HasData(
                    new Address
                    {
                        Id = 1,
                        UserId = 1,
                        Street = "123 Main St",
                        City = "Los Angeles",
                        State = "CA",
                        Country = "USA",
                        PostalCode = "90001",
                        IsDefault = true
                    },
                    new Address
                    {
                        Id = 2,
                        UserId = 2,
                        Street = "456 Market St",
                        City = "San Francisco",
                        State = "CA",
                        Country = "USA",
                        PostalCode = "94101",
                        IsDefault = true
                    }
                );
                // Seed Products
                modelBuilder.Entity<Products>().HasData(
                    new Products
                    {
                        Id = 1,
                        Name = "Laptop",
                        Description = "High-performance laptop",
                        Price = 1200.99M,
                        StockQuantity = 50,
                        SKU = "LAP12345",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    },
                    new Products
                    {
                        Id = 2,
                        Name = "Smartphone",
                        Description = "Latest model smartphone",
                        Price = 799.99M,
                        StockQuantity = 200,
                        SKU = "SMT98765",
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    }
                );
                // Seed Orders
                modelBuilder.Entity<Orders>().HasData(
                    new Orders
                    {
                        Id = 1,
                        UserId = 1,
                        OrderNumber = "ORD12345",
                        OrderDate = DateTime.Now,
                        Status = OrderStatus.Pending,
                        TotalAmount = 1200.99M,
                        ShippingAddressId = 1,
                        BillingAddressId = 1
                    },
                    new Orders
                    {
                        Id = 2,
                        UserId = 2,
                        OrderNumber = "ORD67890",
                        OrderDate = DateTime.Now,
                        Status = OrderStatus.Processing,
                        TotalAmount = 799.99M,
                        ShippingAddressId = 2,
                        BillingAddressId = 2
                    }
                );

                // Seed OrderItems
                modelBuilder.Entity<OrderItem>().HasData(
                    new OrderItem
                    {
                        Id = 1,
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 1,
                        UnitPrice = 1200.99M,
                        Subtotal = 1200.99M
                    },
                    new OrderItem
                    {
                        Id = 2,
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 1,
                        UnitPrice = 799.99M,
                        Subtotal = 799.99M
                    }
                );
                // Seed Payments
                modelBuilder.Entity<Payment>().HasData(
                    new Payment
                    {
                        Id = 1,
                        OrderId = 1,
                        Amount = 1200.99M,
                        PaypalOrderId = "PAYPAL12345",
                        Status = PaymentStatus.Pending,
                        PaymentMethod = PaymentMethod.PayPal,
                        CreatedDate = DateTime.Now
                    },
                    new Payment
                    {
                        Id = 2,
                        OrderId = 2,
                        Amount = 799.99M,
                        PaypalOrderId = "PAYPAL67890",
                        Status = PaymentStatus.Processing,
                        PaymentMethod = PaymentMethod.CreditCard,
                        CreatedDate = DateTime.Now
                    }
                );
            });
        }
    }
}

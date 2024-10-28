﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayPal.Data.Data;

#nullable disable

namespace PayPal.Data.Migrations
{
    [DbContext(typeof(DataDbContext))]
    partial class DataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PayPal.Data.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Los Angeles",
                            Country = "USA",
                            IsDefault = true,
                            PostalCode = "90001",
                            State = "CA",
                            Street = "123 Main St",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            City = "San Francisco",
                            Country = "USA",
                            IsDefault = true,
                            PostalCode = "94101",
                            State = "CA",
                            Street = "456 Market St",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PayPal.Data.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 1,
                            Subtotal = 1200.99m,
                            UnitPrice = 1200.99m
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 2,
                            ProductId = 2,
                            Quantity = 1,
                            Subtotal = 799.99m,
                            UnitPrice = 799.99m
                        });
                });

            modelBuilder.Entity("PayPal.Data.Entities.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BillingAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ShippingAddressId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("ShippingAddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BillingAddressId = 1,
                            OrderDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(831),
                            OrderNumber = "ORD12345",
                            ShippingAddressId = 1,
                            Status = 1,
                            TotalAmount = 1200.99m,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BillingAddressId = 2,
                            OrderDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(837),
                            OrderNumber = "ORD67890",
                            ShippingAddressId = 2,
                            Status = 2,
                            TotalAmount = 799.99m,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PayPal.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("PaypalOrderId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 1200.99m,
                            CreatedDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(906),
                            OrderId = 1,
                            PaymentMethod = 1,
                            PaypalOrderId = "PAYPAL12345",
                            Status = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 799.99m,
                            CreatedDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(909),
                            OrderId = 2,
                            PaymentMethod = 2,
                            PaypalOrderId = "PAYPAL67890",
                            Status = 2
                        });
                });

            modelBuilder.Entity("PayPal.Data.Entities.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SKU")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(789),
                            Description = "High-performance laptop",
                            IsActive = true,
                            Name = "Laptop",
                            Price = 1200.99m,
                            SKU = "LAP12345",
                            StockQuantity = 50
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(793),
                            Description = "Latest model smartphone",
                            IsActive = true,
                            Name = "Smartphone",
                            Price = 799.99m,
                            SKU = "SMT98765",
                            StockQuantity = 200
                        });
                });

            modelBuilder.Entity("PayPal.Data.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(192),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            IsActive = true,
                            LastName = "Doe",
                            PasswordHash = "hashed_password",
                            PhoneNumber = "123456789",
                            Username = "john_doe"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 10, 25, 10, 38, 6, 651, DateTimeKind.Local).AddTicks(210),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            IsActive = true,
                            LastName = "Smith",
                            PasswordHash = "hashed_password",
                            PhoneNumber = "987654321",
                            Username = "jane_smith"
                        });
                });

            modelBuilder.Entity("PayPal.Data.Entities.Address", b =>
                {
                    b.HasOne("PayPal.Data.Entities.Users", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PayPal.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("PayPal.Data.Entities.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PayPal.Data.Entities.Products", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PayPal.Data.Entities.Orders", b =>
                {
                    b.HasOne("PayPal.Data.Entities.Address", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PayPal.Data.Entities.Address", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PayPal.Data.Entities.Users", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BillingAddress");

                    b.Navigation("ShippingAddress");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PayPal.Data.Entities.Payment", b =>
                {
                    b.HasOne("PayPal.Data.Entities.Orders", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("PayPal.Data.Entities.Payment", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PayPal.Data.Entities.Orders", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("PayPal.Data.Entities.Products", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("PayPal.Data.Entities.Users", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}

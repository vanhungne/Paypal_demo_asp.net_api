using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayPal.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "ModifiedDate", "Name", "Price", "SKU", "StockQuantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6544), "High-performance laptop", true, null, "Laptop", 1200.99m, "LAP12345", 50 },
                    { 2, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6548), "Latest model smartphone", true, null, "Smartphone", 799.99m, "SMT98765", 200 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6018), "john.doe@example.com", "John", true, "Doe", "hashed_password", "123456789", "john_doe" },
                    { 2, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6038), "jane.smith@example.com", "Jane", true, "Smith", "hashed_password", "987654321", "jane_smith" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "IsDefault", "PostalCode", "State", "Street", "UserId" },
                values: new object[,]
                {
                    { 1, "Los Angeles", "USA", true, "90001", "CA", "123 Main St", 1 },
                    { 2, "San Francisco", "USA", true, "94101", "CA", "456 Market St", 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BillingAddressId", "OrderDate", "OrderNumber", "ShippingAddressId", "Status", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6587), "ORD12345", 1, 1, 1200.99m, 1 },
                    { 2, 2, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6591), "ORD67890", 2, 2, 799.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "Subtotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1200.99m, 1200.99m },
                    { 2, 2, 2, 1, 799.99m, 799.99m }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CompletedDate", "CreatedDate", "OrderId", "PaymentMethod", "PaypalOrderId", "Status" },
                values: new object[,]
                {
                    { 1, 1200.99m, null, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6740), 1, 1, "PAYPAL12345", 1 },
                    { 2, 799.99m, null, new DateTime(2024, 10, 23, 20, 38, 15, 465, DateTimeKind.Local).AddTicks(6743), 2, 2, "PAYPAL67890", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

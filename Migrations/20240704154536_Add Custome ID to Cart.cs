using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBooking2.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomeIDtoCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerID",
                table: "Carts",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerID",
                table: "Carts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerID",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CustomerID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Carts");
        }
    }
}

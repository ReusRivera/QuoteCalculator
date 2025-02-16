using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductInFinance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Finance",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Finance_ProductId",
                table: "Finance",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Product_ProductId",
                table: "Finance",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Product_ProductId",
                table: "Finance");

            migrationBuilder.DropIndex(
                name: "IX_Finance_ProductId",
                table: "Finance");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Finance");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuotationAmountRequiredFromIntToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AmountRequired",
                table: "Quotation",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AmountRequired",
                table: "Quotation",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}

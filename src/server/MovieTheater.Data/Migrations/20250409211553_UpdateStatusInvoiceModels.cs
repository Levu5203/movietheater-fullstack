using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTheater.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusInvoiceModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceStatus",
                schema: "Common",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceStatus",
                schema: "Common",
                table: "Invoices");
        }
    }
}

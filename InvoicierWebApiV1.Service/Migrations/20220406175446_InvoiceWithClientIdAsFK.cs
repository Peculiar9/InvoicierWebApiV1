using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicierWebApiV1.Infrastructure.Migrations
{
    public partial class InvoiceWithClientIdAsFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Invoices");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicierWebApiV1.Migrations
{
    public partial class Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Organizations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

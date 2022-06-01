using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicierWebApiV1.Infrastructure.Migrations
{
    public partial class newOrganizationModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Organizations");
        }
    }
}

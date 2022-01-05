using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicierWebApiV1.Migrations
{
    public partial class AddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationAddress_Organizations_OrganizationId",
                table: "OrganizationAddress");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationAddress_OrganizationId",
                table: "OrganizationAddress");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "OrganizationAddress");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Organizations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_AddressId",
                table: "Organizations",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_OrganizationAddress_AddressId",
                table: "Organizations",
                column: "AddressId",
                principalTable: "OrganizationAddress",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_OrganizationAddress_AddressId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_AddressId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Organizations");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "OrganizationAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAddress_OrganizationId",
                table: "OrganizationAddress",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationAddress_Organizations_OrganizationId",
                table: "OrganizationAddress",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

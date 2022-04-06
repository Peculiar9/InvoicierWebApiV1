using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicierWebApiV1.Infrastructure.Migrations
{
    public partial class InvoiceItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Clients_clientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Organizations_OrganizationId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_clientId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_OrganizationId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Invoices");

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<double>(type: "float", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.ItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder) 
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_clientId",
                table: "Invoices",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrganizationId",
                table: "Invoices",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_clientId",
                table: "Invoices",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Organizations_OrganizationId",
                table: "Invoices",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

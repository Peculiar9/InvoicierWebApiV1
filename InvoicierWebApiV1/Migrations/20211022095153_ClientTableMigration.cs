using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicierWebApiV1.Migrations
{
    public partial class ClientTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "IsPaid");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Concept",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discount",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredOn",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Total",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "clientId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_clientId",
                table: "Invoices",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Clients_clientId",
                table: "Invoices",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Clients_clientId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_clientId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Concept",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ExpiredOn",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Invoices",
                newName: "Status");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScentifyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PaymentDate",
                table: "Invoice",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Status",
                table: "Invoice",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoice_PaymentDate",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_Status",
                table: "Invoice");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentDate",
                table: "Invoice",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}

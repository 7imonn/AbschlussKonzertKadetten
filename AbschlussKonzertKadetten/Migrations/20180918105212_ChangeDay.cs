using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbschlussKonzertKadetten.Migrations
{
    public partial class ChangeDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "TicketOrders",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Day",
                table: "TicketOrders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

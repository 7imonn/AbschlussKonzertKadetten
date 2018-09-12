using Microsoft.EntityFrameworkCore.Migrations;

namespace AbschlussKonzertKadetten.Migrations
{
    public partial class addFKtoTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Tickets_Id",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OrdersId",
                table: "Tickets",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Order_OrdersId",
                table: "Tickets",
                column: "OrdersId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Order_OrdersId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_OrdersId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Tickets_Id",
                table: "Order",
                column: "Id",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

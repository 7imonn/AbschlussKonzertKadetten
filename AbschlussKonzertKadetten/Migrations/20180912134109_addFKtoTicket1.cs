using Microsoft.EntityFrameworkCore.Migrations;

namespace AbschlussKonzertKadetten.Migrations
{
    public partial class addFKtoTicket1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "TicketOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    TicketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrders", x => new { x.OrderId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_TicketOrders_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketOrders_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrders_TicketId",
                table: "TicketOrders",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrders");

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
    }
}

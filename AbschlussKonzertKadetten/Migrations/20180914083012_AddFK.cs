using Microsoft.EntityFrameworkCore.Migrations;

namespace AbschlussKonzertKadetten.Migrations
{
    public partial class AddFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientsId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    TicketId = table.Column<int>(nullable: false),
                    Day = table.Column<string>(nullable: true)
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
                        name: "FK_TicketOrders_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientsId",
                table: "Order",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrders_TicketId",
                table: "TicketOrders",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_ClientsId",
                table: "Order",
                column: "ClientsId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_ClientsId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "TicketOrders");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientsId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ClientsId",
                table: "Order");
        }
    }
}

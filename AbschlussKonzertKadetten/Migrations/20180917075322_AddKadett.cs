using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbschlussKonzertKadetten.Migrations
{
    public partial class AddKadett : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KadettId",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Kadett",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kadett", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_KadettId",
                table: "Order",
                column: "KadettId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Kadett_KadettId",
                table: "Order",
                column: "KadettId",
                principalTable: "Kadett",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Kadett_KadettId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Kadett");

            migrationBuilder.DropIndex(
                name: "IX_Order_KadettId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "KadettId",
                table: "Order");
        }
    }
}

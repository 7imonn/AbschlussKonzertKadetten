using Microsoft.EntityFrameworkCore.Migrations;

namespace AbschlussKonzertKadetten.Migrations
{
    public partial class correctfirstname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ForeName",
                table: "Clients",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Clients",
                newName: "ForeName");
        }
    }
}

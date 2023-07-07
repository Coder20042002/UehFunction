using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update_chitiets_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stdhd",
                table: "Chitiets",
                newName: "sdthd");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Chitiets",
                newName: "emailhd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sdthd",
                table: "Chitiets",
                newName: "stdhd");

            migrationBuilder.RenameColumn(
                name: "emailhd",
                table: "Chitiets",
                newName: "email");
        }
    }
}

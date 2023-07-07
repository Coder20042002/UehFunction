using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update_chitiets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Giangviens");

            migrationBuilder.DropColumn(
                name: "sdt",
                table: "Giangviens");

            migrationBuilder.DropColumn(
                name: "emailsv",
                table: "Chitiets");

            migrationBuilder.DropColumn(
                name: "sdt",
                table: "Chitiets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Giangviens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sdt",
                table: "Giangviens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "emailsv",
                table: "Chitiets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sdt",
                table: "Chitiets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

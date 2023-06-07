using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class updateDBPhanCong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hoten",
                table: "Phancongs");

            migrationBuilder.DropColumn(
                name: "ngaysinh",
                table: "Phancongs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "hoten",
                table: "Phancongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ngaysinh",
                table: "Phancongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class updategiangvien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Khoamakhoa",
                table: "Giangviens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "makhoa",
                table: "Giangviens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_Khoamakhoa",
                table: "Giangviens",
                column: "Khoamakhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Giangviens_Khoas_Khoamakhoa",
                table: "Giangviens",
                column: "Khoamakhoa",
                principalTable: "Khoas",
                principalColumn: "makhoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giangviens_Khoas_Khoamakhoa",
                table: "Giangviens");

            migrationBuilder.DropIndex(
                name: "IX_Giangviens_Khoamakhoa",
                table: "Giangviens");

            migrationBuilder.DropColumn(
                name: "Khoamakhoa",
                table: "Giangviens");

            migrationBuilder.DropColumn(
                name: "makhoa",
                table: "Giangviens");
        }
    }
}

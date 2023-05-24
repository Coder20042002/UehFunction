using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sinhviens_Khoas_Khoamakhoa",
                table: "Sinhviens");

            migrationBuilder.DropIndex(
                name: "IX_Sinhviens_Khoamakhoa",
                table: "Sinhviens");

            migrationBuilder.DropColumn(
                name: "Khoamakhoa",
                table: "Sinhviens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Khoamakhoa",
                table: "Sinhviens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_Khoamakhoa",
                table: "Sinhviens",
                column: "Khoamakhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Sinhviens_Khoas_Khoamakhoa",
                table: "Sinhviens",
                column: "Khoamakhoa",
                principalTable: "Khoas",
                principalColumn: "makhoa");
        }
    }
}

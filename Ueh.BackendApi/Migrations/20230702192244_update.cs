using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dangkys_Loais_Loaimaloai",
                table: "Dangkys");

            migrationBuilder.DropIndex(
                name: "IX_Dangkys_Loaimaloai",
                table: "Dangkys");

            migrationBuilder.DropColumn(
                name: "Loaimaloai",
                table: "Dangkys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Loaimaloai",
                table: "Dangkys",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_Loaimaloai",
                table: "Dangkys",
                column: "Loaimaloai");

            migrationBuilder.AddForeignKey(
                name: "FK_Dangkys_Loais_Loaimaloai",
                table: "Dangkys",
                column: "Loaimaloai",
                principalTable: "Loais",
                principalColumn: "maloai");
        }
    }
}

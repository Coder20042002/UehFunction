using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class Dangkystb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Dangkys",
                newName: "ten");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Dangkys",
                newName: "ho");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys",
                column: "mssv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys");

            migrationBuilder.RenameColumn(
                name: "ten",
                table: "Dangkys",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "ho",
                table: "Dangkys",
                newName: "firstName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys",
                columns: new[] { "mssv", "magv", "makhoa" });
        }
    }
}

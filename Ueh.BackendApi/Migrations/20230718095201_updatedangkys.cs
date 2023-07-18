using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class updatedangkys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys");

            migrationBuilder.AlterColumn<string>(
                name: "madot",
                table: "Dangkys",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys",
                columns: new[] { "mssv", "madot" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys");

            migrationBuilder.AlterColumn<string>(
                name: "madot",
                table: "Dangkys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dangkys",
                table: "Dangkys",
                column: "mssv");
        }
    }
}

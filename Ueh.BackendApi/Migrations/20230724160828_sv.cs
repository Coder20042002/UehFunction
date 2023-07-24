using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class sv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv",
                table: "Phancongs");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Sinhviens_mssv",
                table: "Sinhviens");

            migrationBuilder.DropIndex(
                name: "IX_Phancongs_mssv",
                table: "Phancongs");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_mssv_madot",
                table: "Phancongs",
                columns: new[] { "mssv", "madot" });

            migrationBuilder.AddForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv_madot",
                table: "Phancongs",
                columns: new[] { "mssv", "madot" },
                principalTable: "Sinhviens",
                principalColumns: new[] { "mssv", "madot" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv_madot",
                table: "Phancongs");

            migrationBuilder.DropIndex(
                name: "IX_Phancongs_mssv_madot",
                table: "Phancongs");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Sinhviens_mssv",
                table: "Sinhviens",
                column: "mssv");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_mssv",
                table: "Phancongs",
                column: "mssv");

            migrationBuilder.AddForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv",
                table: "Phancongs",
                column: "mssv",
                principalTable: "Sinhviens",
                principalColumn: "mssv",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

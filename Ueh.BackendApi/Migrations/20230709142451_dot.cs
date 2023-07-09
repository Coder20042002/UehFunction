using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class dot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv",
                table: "Phancongs");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhvienKhoas_Sinhviens_mssv",
                table: "SinhvienKhoas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sinhviens",
                table: "Sinhviens");

            migrationBuilder.AlterColumn<string>(
                name: "madot",
                table: "Sinhviens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Sinhviens_mssv",
                table: "Sinhviens",
                column: "mssv");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sinhviens",
                table: "Sinhviens",
                columns: new[] { "mssv", "madot" });

            migrationBuilder.AddForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv",
                table: "Phancongs",
                column: "mssv",
                principalTable: "Sinhviens",
                principalColumn: "mssv",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinhvienKhoas_Sinhviens_mssv",
                table: "SinhvienKhoas",
                column: "mssv",
                principalTable: "Sinhviens",
                principalColumn: "mssv",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv",
                table: "Phancongs");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhvienKhoas_Sinhviens_mssv",
                table: "SinhvienKhoas");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Sinhviens_mssv",
                table: "Sinhviens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sinhviens",
                table: "Sinhviens");

            migrationBuilder.AlterColumn<string>(
                name: "madot",
                table: "Sinhviens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sinhviens",
                table: "Sinhviens",
                column: "mssv");

            migrationBuilder.AddForeignKey(
                name: "FK_Phancongs_Sinhviens_mssv",
                table: "Phancongs",
                column: "mssv",
                principalTable: "Sinhviens",
                principalColumn: "mssv");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhvienKhoas_Sinhviens_mssv",
                table: "SinhvienKhoas",
                column: "mssv",
                principalTable: "Sinhviens",
                principalColumn: "mssv",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

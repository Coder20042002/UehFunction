using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class dbTT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dangky_Dots_madot",
                table: "Dangky");

            migrationBuilder.DropIndex(
                name: "IX_Dangky_madot",
                table: "Dangky");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateEnd",
                table: "Dots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dateStart",
                table: "Dots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "madot",
                table: "Dangky",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "dotmadot",
                table: "Dangky",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Dangky_dotmadot",
                table: "Dangky",
                column: "dotmadot");

            migrationBuilder.AddForeignKey(
                name: "FK_Dangky_Dots_dotmadot",
                table: "Dangky",
                column: "dotmadot",
                principalTable: "Dots",
                principalColumn: "madot",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dangky_Dots_dotmadot",
                table: "Dangky");

            migrationBuilder.DropIndex(
                name: "IX_Dangky_dotmadot",
                table: "Dangky");

            migrationBuilder.DropColumn(
                name: "dateEnd",
                table: "Dots");

            migrationBuilder.DropColumn(
                name: "dateStart",
                table: "Dots");

            migrationBuilder.DropColumn(
                name: "dotmadot",
                table: "Dangky");

            migrationBuilder.AlterColumn<string>(
                name: "madot",
                table: "Dangky",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Dangky_madot",
                table: "Dangky",
                column: "madot");

            migrationBuilder.AddForeignKey(
                name: "FK_Dangky_Dots_madot",
                table: "Dangky",
                column: "madot",
                principalTable: "Dots",
                principalColumn: "madot",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

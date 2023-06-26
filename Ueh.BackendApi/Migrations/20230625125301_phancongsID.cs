using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class phancongsID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lichsus_Phancongs_phancongId",
                table: "Lichsus");

            migrationBuilder.DropIndex(
                name: "IX_Lichsus_phancongId",
                table: "Lichsus");

            migrationBuilder.DropColumn(
                name: "phancongId",
                table: "Lichsus");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "e49d19ff-f36d-4c6e-9c78-0e780e0e6c0e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "4775e234-dd30-4bc2-8bcb-e515c55c6351");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "66dbce83-cfbb-4866-baf3-ecb126ca4ab1");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4f7e1a1d-4d1e-46b7-afc2-76f220285e2d", "AQAAAAEAACcQAAAAENnGrKIys3v672qTNfCmeXYqj9lwk8I+wUgVkK98CJ9KPGo6Fo4kxrEXil5SUEWbbw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lichsus_Phancongs_Id",
                table: "Lichsus",
                column: "Id",
                principalTable: "Phancongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lichsus_Phancongs_Id",
                table: "Lichsus");

            migrationBuilder.AddColumn<Guid>(
                name: "phancongId",
                table: "Lichsus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "d5221578-36ce-40e9-99ed-085b20c4acd6");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "b889a8c0-4b60-4ca6-a7b0-f69b7a5fad00");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2c13d3d9-f215-41b1-a615-49efc3864420");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c04fba86-d9a0-4b9d-bd42-81584390053e", "AQAAAAEAACcQAAAAEE6DlO8+/6enamLCrrwfbvAC/gGM54kvfzUQByorUQZ5zdngCwoud/OI69Fb/R6qhA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Lichsus_phancongId",
                table: "Lichsus",
                column: "phancongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lichsus_Phancongs_phancongId",
                table: "Lichsus",
                column: "phancongId",
                principalTable: "Phancongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

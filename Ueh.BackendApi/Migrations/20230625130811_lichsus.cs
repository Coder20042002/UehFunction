using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class lichsus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lichsus_Phancongs_Id",
                table: "Lichsus");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "eb362a4e-c0a6-4642-bfe1-7067ee437c28");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "c00a4731-5dac-480d-9cc6-a351e3b6b04e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "6f665fe9-9e9f-4ece-b736-ee7f12da3ab2");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f0532b7-3248-4f45-9c6a-2964d5f8b420", "AQAAAAEAACcQAAAAEDtWrBShV6HEdiczkA7jycfAGVz1DHXL98zlksARrgGkrGpNPCMIFCv0waPKnDgaGQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lichsus_Phancongs_Id",
                table: "Lichsus",
                column: "Id",
                principalTable: "Phancongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lichsus_Phancongs_Id",
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
    }
}

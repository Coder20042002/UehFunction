using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class Dot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uploaded",
                table: "UploadResults");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "UploadResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mssv",
                table: "UploadResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Dots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "6821ebce-50f0-499d-b19e-ed904d391a8b");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "faa58de7-43da-43eb-9794-70c6982b2d2f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fc85c45a-257b-4023-9a86-519d5ff88f55");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a1aabe3-419f-4634-a73b-afbef800f6e8", "AQAAAAEAACcQAAAAEHCAEUdhjB8b6TKX+3KImlrHj0KPX3HpMsynv3pGtPRhtVJUtW8K05HZrkZ2LvLQLA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "UploadResults");

            migrationBuilder.DropColumn(
                name: "Mssv",
                table: "UploadResults");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Dots");

            migrationBuilder.AddColumn<bool>(
                name: "Uploaded",
                table: "UploadResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "10ccf55f-c7c8-41da-a772-c559a9e5782c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "04b5f24e-a545-4e68-8227-d01299c619e7");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0ab9c637-d98c-4bb7-842a-a1aacac0d3ba");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc553b3b-b544-4144-9b45-358d271a03f8", "AQAAAAEAACcQAAAAEBN+I/HIYjAQ9HaJQ+nxxkXrLgfMH/danMFQZgeiZLtG1O9mjLVbqBMADoUY+T2QEg==" });
        }
    }
}

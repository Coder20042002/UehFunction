using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class UploadResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Uploaded = table.Column<bool>(type: "bit", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadResults", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadResults");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "4af3f196-df15-43b5-be5f-2402d79df95f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "aeb322fc-4816-4b7d-a41f-4882f2aac45c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9ed56ba0-793c-477a-97cd-93b0dfec5ddf");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b8c85aab-0a4b-4a6f-b931-c3193a5239cc", "AQAAAAEAACcQAAAAECTV1WuZ1IsdqCtUy0mrKrRJ6m7FO8O2SZJMPoqqontYdyV9pzsGB0xiHlayu0iODg==" });
        }
    }
}

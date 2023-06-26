using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class lischsuKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lichsus",
                table: "Lichsus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lichsus",
                table: "Lichsus",
                columns: new[] { "Id", "ngay" });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "e4460f45-b34f-4668-9c01-69becaee290e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "ecf56c64-4a83-4970-884d-4d4f620ae908");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1928c9a0-9690-4a9f-92f6-a8bc58f90088");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6598de1d-854a-4827-8fb2-3afe4c5612ba", "AQAAAAEAACcQAAAAEPiC9bajgxQnqGtFQwl2mwmKAIxMLQJKNG9kBJlb0iIUJOcrxyi58rPw+F2o52qa3Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Lichsus",
                table: "Lichsus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lichsus",
                table: "Lichsus",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "a336703b-7aae-46f8-a485-36a129951c47");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "a05e34ea-f229-4f93-89d9-45626901bd33");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "414b4e80-a454-411c-8707-735e05e6e65e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c4d3c641-2c11-436d-875e-6a7c28188285", "AQAAAAEAACcQAAAAENx/O6qEMgPhmpr3fdKigXtW+nPg0GDoMbGGwfypxnTzWwF8guRDxiQh1jQXfCSzMg==" });
        }
    }
}

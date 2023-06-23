using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update_phancongs_ten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Sinhviens",
                newName: "ten");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Sinhviens",
                newName: "ho");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ten",
                table: "Sinhviens",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "ho",
                table: "Sinhviens",
                newName: "firstName");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "7ff3a01a-09c8-43d3-bc5d-2fc7eed15ece");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "383abc79-4e3e-4b8b-aa7d-2c7f07a23a98");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7eaf7674-5588-4594-9df4-454f9beaebe0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a243184-8210-48df-a6ce-cd91364e4ed1", "AQAAAAEAACcQAAAAEJFhGHLRNDjzT8d43hXwC/3Ew9Kgd8KOPKytVV66+R4G8JkODhzRmbbywlXVOR2jgg==" });
        }
    }
}

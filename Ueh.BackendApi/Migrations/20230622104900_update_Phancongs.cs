using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update_Phancongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "macn",
                table: "Phancongs");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "890fbfe4-7424-45ec-b2fd-b480252c67f6");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "7f45c5bd-2d83-4094-b832-bd4a4b24d23d");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e4e19357-56aa-425f-b25d-6e7cad8ff7bb");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5a3aef7b-2ac9-4036-bcd7-fb24adf39d18", "AQAAAAEAACcQAAAAECauWcoL+jEp6aAHzkSLNQpZ2MvKn6V+OYmzbDXFZdOcQrG5JVN5TqLn+oGSOEHm3A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "macn",
                table: "Phancongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "19b3a467-d2e2-4960-b960-a7e369982438");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "883d4057-f955-40d1-b555-246d4d8da23c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "34f521f3-333b-4ad0-97a8-a047bffaea84");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1b401a2-5c63-4cd8-b216-9c81c1a0c6e5", "AQAAAAEAACcQAAAAECJfFiLU8Y4psjoDQJZPOlvJMckeE6ZhhD3RR1r9wdBU7YxsEfO1iKkoIRwkh0tOMQ==" });
        }
    }
}

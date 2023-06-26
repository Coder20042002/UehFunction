using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class updatetableGiangviens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "makhoa",
                table: "Giangviens");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "a4098801-7ba4-4a05-8828-7db63a57f63f");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "d4f5d7b6-3737-4acd-a94d-93bc346e3232");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "8406b8ed-1607-4cd1-ba78-8972354f0c19");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8171980e-1495-4748-8303-412835351f54", "AQAAAAEAACcQAAAAEK1VhUxMKrDUxbuNYA3wzuTbjWk9wyNIJ6+DnNLqo/U0Wi4IepvlJT9MJpKPpJpwGQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "makhoa",
                table: "Giangviens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "23cae53a-6cb3-48d0-8f71-5ec407e29b75");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "114ddb90-b5b2-4220-a7d1-27f9cc0548c1");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f11187f7-dafa-4e51-9754-6d353edef7b4");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "af571455-c7a2-423c-9b90-ec6045164e97", "AQAAAAEAACcQAAAAEFr4Mtr7GDyFAGHvDtAS6zTgYo+4uyozZ/gY+g0pa2c54OIOwYvRXVfWteoObEtfVg==" });
        }
    }
}

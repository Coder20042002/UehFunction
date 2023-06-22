using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class addGiangvienKhoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Giangviens_Khoas_Khoamakhoa",
                table: "Giangviens");

            migrationBuilder.DropIndex(
                name: "IX_Giangviens_Khoamakhoa",
                table: "Giangviens");

            migrationBuilder.DropColumn(
                name: "Khoamakhoa",
                table: "Giangviens");

            migrationBuilder.CreateTable(
                name: "GiangvienKhoas",
                columns: table => new
                {
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangvienKhoas", x => new { x.magv, x.makhoa });
                    table.ForeignKey(
                        name: "FK_GiangvienKhoas_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiangvienKhoas_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_GiangvienKhoas_makhoa",
                table: "GiangvienKhoas",
                column: "makhoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiangvienKhoas");

            migrationBuilder.AddColumn<string>(
                name: "Khoamakhoa",
                table: "Giangviens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "bf1b9103-44dd-4b85-908b-bfba69482929");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "63937ea1-dd2a-42ab-a25a-f03e7d4c1eb8");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9eb9c395-aebd-4b5c-8f5e-c6033a9dd52f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b2560c79-62fc-4a99-9b60-ae12c4d7cd17", "AQAAAAEAACcQAAAAECwfc8Wc88oxy9nbbKtCNYScs7eFmMqugm7MqhOzZf2H1EEwSaE+wiQAXGtl/eVeYQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_Khoamakhoa",
                table: "Giangviens",
                column: "Khoamakhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_Giangviens_Khoas_Khoamakhoa",
                table: "Giangviens",
                column: "Khoamakhoa",
                principalTable: "Khoas",
                principalColumn: "makhoa");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update_Phancongs_tt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phancongs_Chuyennganhs_Chuyennganhmacn",
                table: "Phancongs");

            migrationBuilder.DropIndex(
                name: "IX_Phancongs_Chuyennganhmacn",
                table: "Phancongs");

            migrationBuilder.DropColumn(
                name: "Chuyennganhmacn",
                table: "Phancongs");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "3eba4d44-ff9b-4d78-b7ef-dddf21fd5321");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "c7a00e59-ba19-4042-9b9f-d78505ccd407");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fb79cc70-beb4-4793-9ca7-84146fb0999a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "252ea60f-b63b-4235-9bc7-bcaf4bd41b1a", "AQAAAAEAACcQAAAAECF2Xd67K2An6FG0j3y+/n7PJo3aa/ziDVtXEdOqRPQUObfg2+x/GfSWvHVu5aMn4w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chuyennganhmacn",
                table: "Phancongs",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_Chuyennganhmacn",
                table: "Phancongs",
                column: "Chuyennganhmacn");

            migrationBuilder.AddForeignKey(
                name: "FK_Phancongs_Chuyennganhs_Chuyennganhmacn",
                table: "Phancongs",
                column: "Chuyennganhmacn",
                principalTable: "Chuyennganhs",
                principalColumn: "macn");
        }
    }
}

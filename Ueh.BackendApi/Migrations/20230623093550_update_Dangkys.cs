using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class update_Dangkys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dangkys_Loais_maloai",
                table: "Dangkys");

            migrationBuilder.RenameColumn(
                name: "hotensv",
                table: "Dangkys",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "maloai",
                table: "Dangkys",
                newName: "makhoa");

            migrationBuilder.RenameIndex(
                name: "IX_Dangkys_maloai",
                table: "Dangkys",
                newName: "IX_Dangkys_makhoa");

            migrationBuilder.AddColumn<string>(
                name: "Loaimaloai",
                table: "Dangkys",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "Dangkys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_Loaimaloai",
                table: "Dangkys",
                column: "Loaimaloai");

            migrationBuilder.AddForeignKey(
                name: "FK_Dangkys_Khoas_makhoa",
                table: "Dangkys",
                column: "makhoa",
                principalTable: "Khoas",
                principalColumn: "makhoa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dangkys_Loais_Loaimaloai",
                table: "Dangkys",
                column: "Loaimaloai",
                principalTable: "Loais",
                principalColumn: "maloai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dangkys_Khoas_makhoa",
                table: "Dangkys");

            migrationBuilder.DropForeignKey(
                name: "FK_Dangkys_Loais_Loaimaloai",
                table: "Dangkys");

            migrationBuilder.DropIndex(
                name: "IX_Dangkys_Loaimaloai",
                table: "Dangkys");

            migrationBuilder.DropColumn(
                name: "Loaimaloai",
                table: "Dangkys");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "Dangkys");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Dangkys",
                newName: "hotensv");

            migrationBuilder.RenameColumn(
                name: "makhoa",
                table: "Dangkys",
                newName: "maloai");

            migrationBuilder.RenameIndex(
                name: "IX_Dangkys_makhoa",
                table: "Dangkys",
                newName: "IX_Dangkys_maloai");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                column: "ConcurrencyStamp",
                value: "a8aa6082-1379-4e55-8595-bdc3f1a9c103");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                column: "ConcurrencyStamp",
                value: "8fd6e7b2-374e-4447-aa32-bba2290a0403");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "33e8ccb3-c56e-4f4b-9c1e-b125632bc4c4");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "36e64470-3d7a-41c4-b1e2-c468d1c76911", "AQAAAAEAACcQAAAAED+zd9HZZUuPDgGnyKyMEpKmUGWY8DtTsgJXWiw9D13fSOj989ini0KT3iIMXbcuxw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Dangkys_Loais_maloai",
                table: "Dangkys",
                column: "maloai",
                principalTable: "Loais",
                principalColumn: "maloai",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

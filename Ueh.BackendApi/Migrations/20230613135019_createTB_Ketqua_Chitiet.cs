using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class createTB_Ketqua_Chitiet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chitiets",
                columns: table => new
                {
                    mapc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tencty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    huongdan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stdhd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendetai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chitiets", x => new { x.mapc, x.mssv });
                    table.ForeignKey(
                        name: "FK_Chitiets_Phancongs_mapc",
                        column: x => x.mapc,
                        principalTable: "Phancongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chitiets_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ketquas",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mapc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tieuchi1 = table.Column<int>(type: "int", nullable: false),
                    tieuchi2 = table.Column<int>(type: "int", nullable: false),
                    tieuchi3 = table.Column<int>(type: "int", nullable: false),
                    tieuchi4 = table.Column<int>(type: "int", nullable: false),
                    tieuchi5 = table.Column<int>(type: "int", nullable: false),
                    tieuchi6 = table.Column<int>(type: "int", nullable: false),
                    tieuchi7 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ketquas", x => new { x.mapc, x.mssv });
                    table.ForeignKey(
                        name: "FK_Ketquas_Phancongs_mapc",
                        column: x => x.mapc,
                        principalTable: "Phancongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ketquas_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2facf446-63fc-4615-b936-d18d6cc0e926");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"), "28bc86df-895d-4fc4-a904-78c1142acf2e", "Tearchistrator role", "tearch", "tearch" },
                    { new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"), "82fa29a0-1fda-4329-957f-48dcd04e00f4", "Studentistrator role", "student", "student" }
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "07f5e432-6281-4236-b28f-b0b27512b52d", "AQAAAAEAACcQAAAAEEfssU+xQn028NYq91byGcjfSQQVL56fSrx3E22WAeHyhomEFTy1ykcLaKtsNA2Jwg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Chitiets_mssv",
                table: "Chitiets",
                column: "mssv");

            migrationBuilder.CreateIndex(
                name: "IX_Ketquas_mssv",
                table: "Ketquas",
                column: "mssv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chitiets");

            migrationBuilder.DropTable(
                name: "Ketquas");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "44a152ea-a906-4864-b48b-a8e239ed24a4");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "440207f8-8f93-4284-85d8-6f0de126807c", "AQAAAAEAACcQAAAAEDdWcfhAYE5RYSMK0H+UPT6xAy9o+V7Fptg4vrlRF6NkfBxkjmCJ8QmKD5EiGk5MfQ==" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class table_Phancong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phancongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stt = table.Column<int>(type: "int", nullable: false),
                    mssv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lopsv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaysinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tengv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phancongs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phancongs");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class removeSVDot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhvienDots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SinhvienDots",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhvienDots", x => new { x.mssv, x.madot });
                    table.ForeignKey(
                        name: "FK_SinhvienDots_Dots_madot",
                        column: x => x.madot,
                        principalTable: "Dots",
                        principalColumn: "madot",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhvienDots_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienDots_madot",
                table: "SinhvienDots",
                column: "madot");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class dbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dots",
                columns: table => new
                {
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dots", x => x.madot);
                });

            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenkhoa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.makhoa);
                });

            migrationBuilder.CreateTable(
                name: "Loais",
                columns: table => new
                {
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loais", x => x.maloai);
                });

            migrationBuilder.CreateTable(
                name: "Reviewers",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hoten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviewers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Giangviens",
                columns: table => new
                {
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tengv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giangviens", x => x.magv);
                    table.ForeignKey(
                        name: "FK_GangVien_Khoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dangky",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mssv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dangky", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dangky_Dots_madot",
                        column: x => x.madot,
                        principalTable: "Dots",
                        principalColumn: "madot",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dangky_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dangky_Loais_maloai",
                        column: x => x.maloai,
                        principalTable: "Loais",
                        principalColumn: "maloai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sinhviens",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hoten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenlop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    khoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chuyennganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Khoamakhoa = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhviens", x => x.mssv);
                    table.ForeignKey(
                        name: "FK_Sinhviens_Dangky_mssv",
                        column: x => x.mssv,
                        principalTable: "Dangky",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sinhviens_Khoas_Khoamakhoa",
                        column: x => x.Khoamakhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    noidung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reviewerid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    sinhvienmssv = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Reviewers_reviewerid",
                        column: x => x.reviewerid,
                        principalTable: "Reviewers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Sinhviens_sinhvienmssv",
                        column: x => x.sinhvienmssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "SinhvienLoais",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhvienLoais", x => new { x.mssv, x.maloai });
                    table.ForeignKey(
                        name: "FK_SinhvienLoais_Loais_maloai",
                        column: x => x.maloai,
                        principalTable: "Loais",
                        principalColumn: "maloai",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhvienLoais_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dangky_madot",
                table: "Dangky",
                column: "madot");

            migrationBuilder.CreateIndex(
                name: "IX_Dangky_magv",
                table: "Dangky",
                column: "magv");

            migrationBuilder.CreateIndex(
                name: "IX_Dangky_maloai",
                table: "Dangky",
                column: "maloai");

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_makhoa",
                table: "Giangviens",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_reviewerid",
                table: "Reviews",
                column: "reviewerid");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_sinhvienmssv",
                table: "Reviews",
                column: "sinhvienmssv");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienDots_madot",
                table: "SinhvienDots",
                column: "madot");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienLoais_maloai",
                table: "SinhvienLoais",
                column: "maloai");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_Khoamakhoa",
                table: "Sinhviens",
                column: "Khoamakhoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SinhvienDots");

            migrationBuilder.DropTable(
                name: "SinhvienLoais");

            migrationBuilder.DropTable(
                name: "Reviewers");

            migrationBuilder.DropTable(
                name: "Sinhviens");

            migrationBuilder.DropTable(
                name: "Dangky");

            migrationBuilder.DropTable(
                name: "Dots");

            migrationBuilder.DropTable(
                name: "Giangviens");

            migrationBuilder.DropTable(
                name: "Loais");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chuyennganhs",
                columns: table => new
                {
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tencn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chuyennganhs", x => x.macn);
                });

            migrationBuilder.CreateTable(
                name: "Dots",
                columns: table => new
                {
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Sinhviens",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hoten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenlop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaysinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhviens", x => x.mssv);
                });

            migrationBuilder.CreateTable(
                name: "Giangviens",
                columns: table => new
                {
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tengv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giangviens", x => x.magv);
                    table.ForeignKey(
                        name: "FK_GangVien_ChuyenNganh",
                        column: x => x.macn,
                        principalTable: "Chuyennganhs",
                        principalColumn: "macn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GangVien_Khoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "SinhvienChuyenNganhs",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhvienChuyenNganhs", x => new { x.mssv, x.macn });
                    table.ForeignKey(
                        name: "FK_SinhvienChuyenNganhs_Chuyennganhs_macn",
                        column: x => x.macn,
                        principalTable: "Chuyennganhs",
                        principalColumn: "macn",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhvienChuyenNganhs_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Restrict);
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
                name: "SinhvienKhoas",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhvienKhoas", x => new { x.mssv, x.makhoa });
                    table.ForeignKey(
                        name: "FK_SinhvienKhoas_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhvienKhoas_Sinhviens_mssv",
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dangkys",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hotensv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dangkys", x => new { x.mssv, x.magv, x.maloai });
                    table.ForeignKey(
                        name: "FK_Dangkys_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dangkys_Loais_maloai",
                        column: x => x.maloai,
                        principalTable: "Loais",
                        principalColumn: "maloai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phancongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hoten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaysinh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phancongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phancongs_Chuyennganhs_macn",
                        column: x => x.macn,
                        principalTable: "Chuyennganhs",
                        principalColumn: "macn");
                    table.ForeignKey(
                        name: "FK_Phancongs_Dots_madot",
                        column: x => x.madot,
                        principalTable: "Dots",
                        principalColumn: "madot");
                    table.ForeignKey(
                        name: "FK_Phancongs_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv");
                    table.ForeignKey(
                        name: "FK_Phancongs_Loais_maloai",
                        column: x => x.maloai,
                        principalTable: "Loais",
                        principalColumn: "maloai");
                    table.ForeignKey(
                        name: "FK_Phancongs_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_magv",
                table: "Dangkys",
                column: "magv");

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_maloai",
                table: "Dangkys",
                column: "maloai");

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_macn",
                table: "Giangviens",
                column: "macn");

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_makhoa",
                table: "Giangviens",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_macn",
                table: "Phancongs",
                column: "macn");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_madot",
                table: "Phancongs",
                column: "madot");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_magv",
                table: "Phancongs",
                column: "magv");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_maloai",
                table: "Phancongs",
                column: "maloai");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_mssv",
                table: "Phancongs",
                column: "mssv");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_reviewerid",
                table: "Reviews",
                column: "reviewerid");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_sinhvienmssv",
                table: "Reviews",
                column: "sinhvienmssv");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienChuyenNganhs_macn",
                table: "SinhvienChuyenNganhs",
                column: "macn");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienDots_madot",
                table: "SinhvienDots",
                column: "madot");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienKhoas_makhoa",
                table: "SinhvienKhoas",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienLoais_maloai",
                table: "SinhvienLoais",
                column: "maloai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dangkys");

            migrationBuilder.DropTable(
                name: "Phancongs");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SinhvienChuyenNganhs");

            migrationBuilder.DropTable(
                name: "SinhvienDots");

            migrationBuilder.DropTable(
                name: "SinhvienKhoas");

            migrationBuilder.DropTable(
                name: "SinhvienLoais");

            migrationBuilder.DropTable(
                name: "Giangviens");

            migrationBuilder.DropTable(
                name: "Reviewers");

            migrationBuilder.DropTable(
                name: "Dots");

            migrationBuilder.DropTable(
                name: "Loais");

            migrationBuilder.DropTable(
                name: "Sinhviens");

            migrationBuilder.DropTable(
                name: "Chuyennganhs");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}

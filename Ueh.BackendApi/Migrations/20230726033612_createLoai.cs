using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class createLoai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Loais",
                columns: new[] { "maloai", "name" },
                values: new object[] { "HKDN", "Học kỳ doanh nghiệp" });

            migrationBuilder.InsertData(
                table: "Loais",
                columns: new[] { "maloai", "name" },
                values: new object[] { "KLTN", "Khoá luận tốt nghiệp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Loais",
                keyColumn: "maloai",
                keyValue: "HKDN");

            migrationBuilder.DeleteData(
                table: "Loais",
                keyColumn: "maloai",
                keyValue: "KLTN");
        }
    }
}

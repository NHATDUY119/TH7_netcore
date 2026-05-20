using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyHangHoa.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    MaNCC = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenNCC = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DiaChi = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DienThoai = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "HangHoas",
                columns: table => new
                {
                    MaHang = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenHang = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DonViTinh = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    SoLuongTon = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DonGia = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    MaNCC = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoas", x => x.MaHang);
                    table.ForeignKey(
                        name: "FK_HangHoas_NhaCungCaps_MaNCC",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNCC",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "NhaCungCaps",
                columns: new[] { "MaNCC", "DiaChi", "DienThoai", "TenNCC" },
                values: new object[,]
                {
                    { 1, "123 Nguyễn Huệ, TP.HCM", "0901234567", "Công ty TNHH A" },
                    { 2, "456 Lê Lợi, Hà Nội", "0912345678", "Công ty TNHH B" },
                    { 3, "789 Trần Hưng Đạo, Đà Nẵng", "0923456789", "Công ty TNHH C" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_MaNCC",
                table: "HangHoas",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_TenHang",
                table: "HangHoas",
                column: "TenHang",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhaCungCaps_TenNCC",
                table: "NhaCungCaps",
                column: "TenNCC",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HangHoas");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");
        }
    }
}

namespace Bai6.Models {
    public class NhanVien {
        public string? MaNV { get; set; }
        public string? TenNV { get; set; }
        public string? MaPB { get; set; }
        public string? TenPB { get; set; } // Dùng để hiển thị tên phòng ban sau khi JOIN
    }
}
namespace QuanLyHangHoa.Models
{
    public class HangHoa
    {
        public int MaHang { get; set; }
        public string TenHang { get; set; } = null!;
        public string DonViTinh { get; set; } = null!;
        public int SoLuongTon { get; set; }
        public decimal DonGia { get; set; }
        public int MaNCC { get; set; }
        public NhaCungCap? NhaCungCap { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace QuanLyHangHoa.Models
{
    public class NhaCungCap
    {
        public int MaNCC { get; set; }
        public string TenNCC { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? DienThoai { get; set; }

        [JsonIgnore]
        public ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
    }
}

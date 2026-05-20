namespace QuanLyHangHoa.Models
{
    public class HangHoaSearchRequest
    {
        public string TenHang { get; set; }
        public decimal? DonGiaMin { get; set; }
        public decimal? DonGiaMax { get; set; }
    }
}

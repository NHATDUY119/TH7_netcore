using QuanLyHangHoa.Models;

namespace QuanLyHangHoa.Services
{
    public interface IHangHoaService
    {
        Task<IEnumerable<HangHoa>> GetAllAsync();
        Task<HangHoa> GetByIdAsync(int id);
        Task<IEnumerable<HangHoa>> SearchAsync(string tenHang, decimal? donGiaMin, decimal? donGiaMax);
        Task<(bool success, string message, HangHoa data)> AddAsync(HangHoa hangHoa);
        Task<(bool success, string message)> UpdateAsync(int id, HangHoa hangHoa);
        Task<(bool success, string message)> DeleteAsync(int id);
    }
}

using QuanLyHangHoa.Models;

namespace QuanLyHangHoa.Repositories
{
    public interface IHangHoaRepository
    {
        Task<IEnumerable<HangHoa>> GetAllAsync();
        Task<HangHoa> GetByIdAsync(int id);
        Task<IEnumerable<HangHoa>> SearchAsync(string tenHang, decimal? donGiaMin, decimal? donGiaMax);
        Task<bool> TenHangExistsAsync(string tenHang, int? excludeId = null);
        Task AddAsync(HangHoa hangHoa);
        Task UpdateAsync(HangHoa hangHoa);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}

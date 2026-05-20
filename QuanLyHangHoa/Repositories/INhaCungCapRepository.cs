using QuanLyHangHoa.Models;

namespace QuanLyHangHoa.Repositories
{
    public interface INhaCungCapRepository
    {
        Task<IEnumerable<NhaCungCap>> GetAllAsync();
        Task<NhaCungCap> GetByIdAsync(int id);
        Task AddAsync(NhaCungCap nhaCungCap);
        Task UpdateAsync(NhaCungCap nhaCungCap);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}

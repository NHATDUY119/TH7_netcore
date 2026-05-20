using Microsoft.EntityFrameworkCore;
using QuanLyHangHoa.Data;
using QuanLyHangHoa.Models;

namespace QuanLyHangHoa.Repositories
{
    public class NhaCungCapRepository : INhaCungCapRepository
    {
        private readonly ApplicationDbContext _context;

        public NhaCungCapRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NhaCungCap>> GetAllAsync()
        {
            return await _context.NhaCungCaps.ToListAsync();
        }

        public async Task<NhaCungCap> GetByIdAsync(int id)
        {
            return await _context.NhaCungCaps.FirstOrDefaultAsync(n => n.MaNCC == id);
        }

        public async Task AddAsync(NhaCungCap nhaCungCap)
        {
            await _context.NhaCungCaps.AddAsync(nhaCungCap);
        }

        public async Task UpdateAsync(NhaCungCap nhaCungCap)
        {
            _context.NhaCungCaps.Update(nhaCungCap);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var nhaCungCap = await GetByIdAsync(id);
            if (nhaCungCap != null)
            {
                _context.NhaCungCaps.Remove(nhaCungCap);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

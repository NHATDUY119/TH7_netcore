using Microsoft.EntityFrameworkCore;
using QuanLyHangHoa.Data;
using QuanLyHangHoa.Models;

namespace QuanLyHangHoa.Repositories
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly ApplicationDbContext _context;

        public HangHoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HangHoa>> GetAllAsync()
        {
            return await _context.HangHoas.Include(h => h.NhaCungCap).ToListAsync();
        }

        public async Task<HangHoa> GetByIdAsync(int id)
        {
            return await _context.HangHoas
                .Include(h => h.NhaCungCap)
                .FirstOrDefaultAsync(h => h.MaHang == id);
        }

        public async Task<IEnumerable<HangHoa>> SearchAsync(string tenHang, decimal? donGiaMin, decimal? donGiaMax)
        {
            var query = _context.HangHoas.Include(h => h.NhaCungCap).AsQueryable();

            if (!string.IsNullOrWhiteSpace(tenHang))
            {
                query = query.Where(h => h.TenHang.Contains(tenHang));
            }

            if (donGiaMin.HasValue)
            {
                query = query.Where(h => h.DonGia >= donGiaMin.Value);
            }

            if (donGiaMax.HasValue)
            {
                query = query.Where(h => h.DonGia <= donGiaMax.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> TenHangExistsAsync(string tenHang, int? excludeId = null)
        {
            var query = _context.HangHoas.AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(h => h.MaHang != excludeId.Value);
            }

            return await query.AnyAsync(h => h.TenHang == tenHang);
        }

        public async Task AddAsync(HangHoa hangHoa)
        {
            await _context.HangHoas.AddAsync(hangHoa);
        }

        public async Task UpdateAsync(HangHoa hangHoa)
        {
            _context.HangHoas.Update(hangHoa);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var hangHoa = await GetByIdAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

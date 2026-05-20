using QuanLyHangHoa.Models;
using QuanLyHangHoa.Repositories;

namespace QuanLyHangHoa.Services
{
    public class HangHoaService : IHangHoaService
    {
        private readonly IHangHoaRepository _hangHoaRepository;
        private readonly INhaCungCapRepository _nhaCungCapRepository;

        public HangHoaService(IHangHoaRepository hangHoaRepository, INhaCungCapRepository nhaCungCapRepository)
        {
            _hangHoaRepository = hangHoaRepository;
            _nhaCungCapRepository = nhaCungCapRepository;
        }

        public async Task<IEnumerable<HangHoa>> GetAllAsync()
        {
            return await _hangHoaRepository.GetAllAsync();
        }

        public async Task<HangHoa> GetByIdAsync(int id)
        {
            return await _hangHoaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<HangHoa>> SearchAsync(string tenHang, decimal? donGiaMin, decimal? donGiaMax)
        {
            return await _hangHoaRepository.SearchAsync(tenHang, donGiaMin, donGiaMax);
        }

        public async Task<(bool success, string message, HangHoa data)> AddAsync(HangHoa hangHoa)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(hangHoa.TenHang))
                return (false, "Tên hàng không được để trống", null);

            if (hangHoa.SoLuongTon < 0)
                return (false, "Số lượng tồn phải >= 0", null);

            if (hangHoa.DonGia < 0)
                return (false, "Đơn giá phải >= 0", null);

            var validUnits = new[] { "kg", "thùng", "túi" };
            if (!validUnits.Contains(hangHoa.DonViTinh))
                return (false, "Đơn vị tính phải là: kg, thùng hoặc túi", null);

            // Check if product name already exists
            if (await _hangHoaRepository.TenHangExistsAsync(hangHoa.TenHang))
                return (false, "Tên hàng đã tồn tại", null);

            // Check if supplier exists
            var supplier = await _nhaCungCapRepository.GetByIdAsync(hangHoa.MaNCC);
            if (supplier == null)
                return (false, "Nhà cung cấp không tồn tại", null);

            try
            {
                await _hangHoaRepository.AddAsync(hangHoa);
                await _hangHoaRepository.SaveAsync();
                return (true, "Thêm hàng hóa thành công", hangHoa);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", null);
            }
        }

        public async Task<(bool success, string message)> UpdateAsync(int id, HangHoa hangHoa)
        {
            var existing = await _hangHoaRepository.GetByIdAsync(id);
            if (existing == null)
                return (false, "Hàng hóa không tồn tại");

            // Validate inputs
            if (string.IsNullOrWhiteSpace(hangHoa.TenHang))
                return (false, "Tên hàng không được để trống");

            if (hangHoa.SoLuongTon < 0)
                return (false, "Số lượng tồn phải >= 0");

            if (hangHoa.DonGia < 0)
                return (false, "Đơn giá phải >= 0");

            var validUnits = new[] { "kg", "thùng", "túi" };
            if (!validUnits.Contains(hangHoa.DonViTinh))
                return (false, "Đơn vị tính phải là: kg, thùng hoặc túi");

            // Check if product name is changed and already exists
            if (existing.TenHang != hangHoa.TenHang && await _hangHoaRepository.TenHangExistsAsync(hangHoa.TenHang, id))
                return (false, "Tên hàng đã tồn tại");

            // Check if supplier exists
            var supplier = await _nhaCungCapRepository.GetByIdAsync(hangHoa.MaNCC);
            if (supplier == null)
                return (false, "Nhà cung cấp không tồn tại");

            try
            {
                existing.TenHang = hangHoa.TenHang;
                existing.DonViTinh = hangHoa.DonViTinh;
                existing.SoLuongTon = hangHoa.SoLuongTon;
                existing.DonGia = hangHoa.DonGia;
                existing.MaNCC = hangHoa.MaNCC;

                await _hangHoaRepository.UpdateAsync(existing);
                await _hangHoaRepository.SaveAsync();
                return (true, "Cập nhật hàng hóa thành công");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            var hangHoa = await _hangHoaRepository.GetByIdAsync(id);
            if (hangHoa == null)
                return (false, "Hàng hóa không tồn tại");

            try
            {
                await _hangHoaRepository.DeleteAsync(id);
                await _hangHoaRepository.SaveAsync();
                return (true, "Xóa hàng hóa thành công");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }
    }
}

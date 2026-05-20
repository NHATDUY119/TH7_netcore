using Microsoft.AspNetCore.Mvc;
using QuanLyHangHoa.Models;
using QuanLyHangHoa.Repositories;

namespace QuanLyHangHoa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhaCungCapController : ControllerBase
    {
        private readonly INhaCungCapRepository _repository;

        public NhaCungCapController(INhaCungCapRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhaCungCap>>> GetAll()
        {
            var nhaCungCaps = await _repository.GetAllAsync();
            return Ok(nhaCungCaps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NhaCungCap>> GetById(int id)
        {
            var nhaCungCap = await _repository.GetByIdAsync(id);
            if (nhaCungCap == null)
                return NotFound(new { message = "Nhà cung cấp không tồn tại" });

            return Ok(nhaCungCap);
        }

        [HttpPost]
        public async Task<ActionResult<NhaCungCap>> Add(NhaCungCap nhaCungCap)
        {
            if (string.IsNullOrWhiteSpace(nhaCungCap.TenNCC))
                return BadRequest(new { message = "Tên nhà cung cấp không được để trống" });

            try
            {
                await _repository.AddAsync(nhaCungCap);
                await _repository.SaveAsync();
                return CreatedAtAction(nameof(GetById), new { id = nhaCungCap.MaNCC }, nhaCungCap);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, NhaCungCap nhaCungCap)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = "Nhà cung cấp không tồn tại" });

            if (string.IsNullOrWhiteSpace(nhaCungCap.TenNCC))
                return BadRequest(new { message = "Tên nhà cung cấp không được để trống" });

            try
            {
                existing.TenNCC = nhaCungCap.TenNCC;
                existing.DiaChi = nhaCungCap.DiaChi;
                existing.DienThoai = nhaCungCap.DienThoai;

                await _repository.UpdateAsync(existing);
                await _repository.SaveAsync();
                return Ok(new { message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var nhaCungCap = await _repository.GetByIdAsync(id);
            if (nhaCungCap == null)
                return NotFound(new { message = "Nhà cung cấp không tồn tại" });

            try
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveAsync();
                return Ok(new { message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}

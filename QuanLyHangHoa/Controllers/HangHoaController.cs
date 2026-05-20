using Microsoft.AspNetCore.Mvc;
using QuanLyHangHoa.Models;
using QuanLyHangHoa.Services;

namespace QuanLyHangHoa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaService _service;

        public HangHoaController(IHangHoaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoa>>> GetAll()
        {
            var hangHoas = await _service.GetAllAsync();
            return Ok(hangHoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HangHoa>> GetById(int id)
        {
            var hangHoa = await _service.GetByIdAsync(id);
            if (hangHoa == null)
                return NotFound(new { message = "Hàng hóa không tồn tại" });

            return Ok(hangHoa);
        }

        [HttpGet("search/filter")]
        public async Task<ActionResult<IEnumerable<HangHoa>>> Search([FromQuery] string tenHang, [FromQuery] decimal? donGiaMin, [FromQuery] decimal? donGiaMax)
        {
            var results = await _service.SearchAsync(tenHang, donGiaMin, donGiaMax);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<HangHoa>> Add(HangHoa hangHoa)
        {
            var (success, message, data) = await _service.AddAsync(hangHoa);
            
            if (!success)
                return BadRequest(new { message });

            return CreatedAtAction(nameof(GetById), new { id = data.MaHang }, data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, HangHoa hangHoa)
        {
            var (success, message) = await _service.UpdateAsync(id, hangHoa);

            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var (success, message) = await _service.DeleteAsync(id);

            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }
    }
}

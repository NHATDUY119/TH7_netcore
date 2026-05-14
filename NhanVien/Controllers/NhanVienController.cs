using Microsoft.AspNetCore.Mvc;

namespace NhanVien.Controllers {
    [ApiController]
    [Route("api/nhanvien")]
    public class NhanVienController: ControllerBase {
        
        // Dùng static để danh sách không bị mất khi bạn bấm thêm nhân viên
        private static List<object> ds = new List<object>();

        public NhanVienController() {
            // Nếu danh sách đang trống thì dùng vòng lặp for tạo 5 người
            if (ds.Count == 0) {
                for (int i = 1; i <= 5; i++) {
                    ds.Add(new { maNV = i, hoTen = "Nhân viên " + i });
                }
            }
        }

        [HttpGet]
        public ActionResult Lay() {
            return Ok(ds); // Trả về danh sách cho file index.html
        }

        [HttpPost]
        public ActionResult Them() {
            // Lấy số lượng hiện tại + 1 để ra mã nhân viên mới
            int tiepTheo = ds.Count + 1;
            ds.Add(new { maNV = tiepTheo, hoTen = "Nhân viên " + tiepTheo });
            return Ok();
        }
    }
}
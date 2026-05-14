using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Dapper;
using Bai6.Models;

namespace Bai6.Controllers {
    [ApiController]
    [Route("api/nhanvien")]
    public class ApiNhanVienController : ControllerBase {
        private string conn = "Data Source=qlnv2.db";

        // Lấy danh sách Phòng Ban
        [HttpGet("getphongban")]
        public IActionResult GetPhongBan() {
            using var db = new SqliteConnection(conn);
            return Ok(db.Query<PhongBan>("SELECT * FROM PhongBan"));
        }

        // Lấy danh sách Nhân viên (Có kết nối bảng để lấy Tên phòng ban)
        [HttpGet("getemployees")]
        public IActionResult GetEmployees() {
            using var db = new SqliteConnection(conn);
            string sql = @"SELECT nv.MaNV, nv.TenNV, nv.MaPB, pb.TenPB 
                           FROM NhanVien nv JOIN PhongBan pb ON nv.MaPB = pb.MaPB";
            return Ok(db.Query<NhanVien>(sql));
        }

        // Thêm nhân viên
        [HttpPost("create")]
        public IActionResult Create([FromBody] NhanVien nv) {
            using var db = new SqliteConnection(conn);
            db.Execute("INSERT INTO NhanVien (MaNV, TenNV, MaPB) VALUES (@MaNV, @TenNV, @MaPB)", nv);
            return Ok();
        }

        // Cập nhật nhân viên
        [HttpPost("update")]
        public IActionResult Update([FromBody] NhanVien nv) {
            using var db = new SqliteConnection(conn);
            db.Execute("UPDATE NhanVien SET TenNV=@TenNV, MaPB=@MaPB WHERE MaNV=@MaNV", nv);
            return Ok();
        }

        // Xóa nhân viên
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id) {
            using var db = new SqliteConnection(conn);
            db.Execute("DELETE FROM NhanVien WHERE MaNV=@id", new { id });
            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Dapper;

namespace NhanVien2.Controllers {

    public class NhanVien {
        public string? manv { get; set; }
        public string? hoten { get; set; }
    }

    [ApiController]
    [Route("api/nhanvien")]
    public class NhanVienController : ControllerBase {
        private string conn = "Data Source=qlnv.db";

        [HttpGet]
        public ActionResult Get() {
            using var db = new SqliteConnection(conn);
            return Ok(db.Query<NhanVien>("SELECT * FROM nhanvien"));
        }

        [HttpPost]
        public ActionResult Post([FromBody] NhanVien nv) {
            using var db = new SqliteConnection(conn);
            db.Execute("INSERT INTO nhanvien (manv, hoten) VALUES (@manv, @hoten)", nv);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] NhanVien nv) {
            using var db = new SqliteConnection(conn);
            db.Execute("UPDATE nhanvien SET hoten=@hoten WHERE manv=@manv", nv);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id) {
            using var db = new SqliteConnection(conn);
            db.Execute("DELETE FROM nhanvien WHERE manv=@id", new { id });
            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Để làm DropDownList
using System.Net.Http.Json; // Để gọi API kiểu mới
using Bai6.Models;

namespace Bai6.Controllers {
    public class HomeController : Controller {
        // Cố định cổng 5000 để HttpClient có thể tìm thấy API của chính nó
        private readonly string apiURL = "http://localhost:5000/api/nhanvien/";

        // TRANG CHỦ: HIỂN THỊ DANH SÁCH
        public async Task<IActionResult> Index() {
            using var client = new HttpClient();
            var list = await client.GetFromJsonAsync<List<NhanVien>>(apiURL + "getemployees");
            return View(list);
        }

        // Hàm phụ trợ nạp danh sách phòng ban cho Dropdown (Combobox)
        private async Task LoadPhongBanToViewBag() {
            using var client = new HttpClient();
            var pbList = await client.GetFromJsonAsync<List<PhongBan>>(apiURL + "getphongban");
            ViewBag.PhongBanList = new SelectList(pbList, "MaPB", "TenPB");
        }

        // TRANG THÊM MỚI (GET: Hiện form)
        public async Task<IActionResult> Create() {
            await LoadPhongBanToViewBag();
            return View();
        }

        // TRANG THÊM MỚI (POST: Nhận dữ liệu và gọi API lưu)
        [HttpPost]
        public async Task<IActionResult> Create(NhanVien nv) {
            using var client = new HttpClient();
            await client.PostAsJsonAsync(apiURL + "create", nv);
            return RedirectToAction("Index");
        }

        // TRANG SỬA (GET: Lấy dữ liệu cũ bỏ vào form)
        public async Task<IActionResult> Edit(string id) {
            await LoadPhongBanToViewBag();
            using var client = new HttpClient();
            var list = await client.GetFromJsonAsync<List<NhanVien>>(apiURL + "getemployees");
            var nv = list?.FirstOrDefault(x => x.MaNV == id);
            return View(nv);
        }

        // TRANG SỬA (POST: Nhận dữ liệu cập nhật và gọi API sửa)
        [HttpPost]
        public async Task<IActionResult> Edit(NhanVien nv) {
            using var client = new HttpClient();
            await client.PostAsJsonAsync(apiURL + "update", nv);
            return RedirectToAction("Index");
        }

        // HÀM XÓA
        public async Task<IActionResult> Remove(string id) {
            using var client = new HttpClient();
            await client.DeleteAsync(apiURL + "delete/" + id);
            return RedirectToAction("Index");
        }
    }
}
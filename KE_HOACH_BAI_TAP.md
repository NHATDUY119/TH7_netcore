# KẾ HOẠCH THỰC HIỆN BÀI TẬP: QUẢN LÝ HÀNG HÓA & NHÀ CUNG CẤP

## 📋 TỔNG QUAN
- **Backend**: .NET Core (ASP.NET Core Web API)
- **Database**: SQLite
- **Frontend**: HTML, CSS, JavaScript Vanilla
- **Kiến trúc**: Microservices / API-based architecture

---

## 1️⃣ CẤU TRÚC DỰ ÁN

```
QuanLyHangHoa/
├── QuanLyHangHoa.API/          (Backend - .NET Core)
│   ├── Models/
│   │   ├── NhaCungCap.cs
│   │   ├── HangHoa.cs
│   │   └── SearchRequest.cs
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── quanlyhh.db (SQLite)
│   ├── Controllers/
│   │   ├── NhaCungCapController.cs
│   │   └── HangHoaController.cs
│   ├── Repositories/
│   │   ├── INhaCungCapRepository.cs
│   │   ├── NhaCungCapRepository.cs
│   │   ├── IHangHoaRepository.cs
│   │   └── HangHoaRepository.cs
│   ├── Services/
│   │   ├── IHangHoaService.cs
│   │   └── HangHoaService.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── QuanLyHangHoa.API.csproj
│
└── wwwroot/                     (Frontend)
    ├── index.html              (Trang quản lý hàng hóa)
    ├── css/
    │   ├── style.css
    │   └── bootstrap.min.css (optional)
    ├── js/
    │   ├── app.js              (Main application logic)
    │   ├── api.js              (API calls)
    │   └── utils.js            (Helper functions)
    └── images/
```

---

## 2️⃣ MODELS & ENTITIES

### A. NhaCungCap.cs
```csharp
public class NhaCungCap
{
    public int MaNCC { get; set; }          // Primary Key
    public string TenNCC { get; set; }      // Required, unique
    public string DiaChi { get; set; }
    public string DienThoai { get; set; }
    public ICollection<HangHoa> HangHoas { get; set; }
}
```

### B. HangHoa.cs
```csharp
public class HangHoa
{
    public int MaHang { get; set; }         // Primary Key
    public string TenHang { get; set; }     // Required, unique
    public string DonViTinh { get; set; }   // kg, thùng, túi
    public int SoLuongTon { get; set; }     // >= 0
    public decimal DonGia { get; set; }     // >= 0
    public int MaNCC { get; set; }          // Foreign Key
    public NhaCungCap NhaCungCap { get; set; }
}
```

### C. SearchRequest.cs (cho tìm kiếm)
```csharp
public class HangHoaSearchRequest
{
    public string TenHang { get; set; }
    public decimal? DonGiaMin { get; set; }
    public decimal? DonGiaMax { get; set; }
}
```

---

## 3️⃣ DATABASE SCHEMA (SQLite)

### Bảng NhaCungCap
```sql
CREATE TABLE NhaCungCap (
    MaNCC INTEGER PRIMARY KEY AUTOINCREMENT,
    TenNCC NVARCHAR(200) NOT NULL UNIQUE,
    DiaChi NVARCHAR(500),
    DienThoai NVARCHAR(20)
);
```

### Bảng HangHoa
```sql
CREATE TABLE HangHoa (
    MaHang INTEGER PRIMARY KEY AUTOINCREMENT,
    TenHang NVARCHAR(200) NOT NULL UNIQUE,
    DonViTinh NVARCHAR(20) NOT NULL, -- kg, thùng, túi
    SoLuongTon INTEGER NOT NULL CHECK(SoLuongTon >= 0),
    DonGia DECIMAL(10,2) NOT NULL CHECK(DonGia >= 0),
    MaNCC INTEGER NOT NULL,
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);
```

---

## 4️⃣ API ENDPOINTS

### A. Nhà Cung Cấp (NhaCungCapController)
| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/nhacungcap` | Lấy danh sách tất cả nhà cung cấp |
| GET | `/api/nhacungcap/{id}` | Lấy chi tiết nhà cung cấp theo ID |
| POST | `/api/nhacungcap` | Thêm nhà cung cấp mới |
| PUT | `/api/nhacungcap/{id}` | Cập nhật nhà cung cấp |
| DELETE | `/api/nhacungcap/{id}` | Xóa nhà cung cấp |

### B. Hàng Hóa (HangHoaController)
| Method | Endpoint | Mô tả |
|--------|----------|-------|
| GET | `/api/hanghoa` | Lấy danh sách tất cả hàng hóa |
| GET | `/api/hanghoa/{id}` | Lấy chi tiết hàng hóa theo ID |
| GET | `/api/hanghoa/search` | Tìm kiếm theo tên & giá (query params: tenHang, donGiaMin, donGiaMax) |
| POST | `/api/hanghoa` | Thêm hàng hóa mới (kiểm tra trùng tên, validate) |
| PUT | `/api/hanghoa/{id}` | Cập nhật hàng hóa |
| DELETE | `/api/hanghoa/{id}` | Xóa hàng hóa |

---

## 5️⃣ FRONTEND STRUCTURE

### index.html - Layout chính
- **Header**: Tiêu đề ứng dụng, nav bar
- **Main Content**:
  - Tab 1: Hiển thị danh sách hàng hóa (table)
  - Tab 2: Tìm kiếm hàng hóa
  - Tab 3: Thêm hàng hóa
  - Tab 4: Quản lý nhà cung cấp
- **Modal**: Form sửa hàng hóa
- **Alert**: Thông báo thành công/lỗi

### js/app.js - Quản lý state & UI
- Quản lý tabs
- Load dữ liệu từ API
- Render table hàng hóa
- Handle form submit
- Handle delete confirmation

### js/api.js - API client
```javascript
const API_BASE_URL = 'http://localhost:5000/api';

// Hàng hóa
async function getHangHoas() { }
async function searchHangHoas(tenHang, donGiaMin, donGiaMax) { }
async function addHangHoa(data) { }
async function updateHangHoa(id, data) { }
async function deleteHangHoa(id) { }

// Nhà cung cấp
async function getNhaCungCaps() { }
async function addNhaCungCap(data) { }
async function deleteNhaCungCap(id) { }
```

### js/utils.js - Utilities
- Format currency
- Format date
- Validate form
- Show notification

### css/style.css
- Responsive design
- Table styling
- Form styling
- Modal styling
- Color scheme

---

## 6️⃣ VALIDATIONS & BUSINESS RULES

### Backend (Server-side)
1. **Nhà Cung Cấp**:
   - TenNCC: Required, unique, max 200 chars
   - DiaChi: Max 500 chars
   - DienThoai: Format phone number

2. **Hàng Hóa**:
   - TenHang: Required, unique, max 200 chars
   - DonViTinh: Must be one of: "kg", "thùng", "túi"
   - SoLuongTon: Must be >= 0, integer
   - DonGia: Must be >= 0, decimal
   - MaNCC: Must exist in NhaCungCap table
   - **Cannot add** if TenHang already exists

### Frontend (Client-side)
- Form validation trước khi gửi
- Confirm before delete
- Show loading state
- Show error messages
- Real-time search

---

## 7️⃣ STEPS THỰC HIỆN CỤ THỂ

### Phase 1: Setup Backend (.NET Core)
1. ✅ Tạo ASP.NET Core Web API project
2. ✅ Cài đặt NuGet packages (EF Core, SQLite)
3. ✅ Tạo Models (NhaCungCap, HangHoa, SearchRequest)
4. ✅ Tạo DbContext (ApplicationDbContext)
5. ✅ Tạo Migrations & Database initialization
6. ✅ Seed dữ liệu nhà cung cấp demo
7. ✅ Tạo Repositories + Interfaces
8. ✅ Tạo Services + Business logic
9. ✅ Tạo Controllers với validations
10. ✅ Configure CORS (để frontend kết nối)
11. ✅ Configure dependency injection (DI)

### Phase 2: Frontend Development
1. ✅ Tạo folder wwwroot & file cơ bản
2. ✅ Viết html/css layout
3. ✅ Viết api.js - API client
4. ✅ Viết utils.js - Helper functions
5. ✅ Viết app.js - Main logic
6. ✅ Implement: Hiển thị danh sách
7. ✅ Implement: Tìm kiếm
8. ✅ Implement: Thêm hàng hóa
9. ✅ Implement: Sửa hàng hóa (inline hoặc modal)
10. ✅ Implement: Xóa hàng hóa

### Phase 3: Testing & Refinement
1. ✅ Test API endpoints (Postman/REST Client)
2. ✅ Test frontend functionality
3. ✅ Validation testing
4. ✅ Error handling
5. ✅ Performance optimization

---

## 8️⃣ TECHNOLOGY STACK DETAILS

### Backend Dependencies
```xml
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.0" />
</ItemGroup>
```

### CORS Configuration (Program.cs)
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## 9️⃣ FILE LOCATIONS & STRUCTURE

- **Project**: `/workspaces/TH7_netcore/QuanLyHangHoa/`
- **Database**: `/workspaces/TH7_netcore/QuanLyHangHoa/QuanLyHangHoa.API/quanlyhh.db`
- **Frontend**: `/workspaces/TH7_netcore/QuanLyHangHoa/QuanLyHangHoa.API/wwwroot/`

---

## 🔟 TIMELINE ESTIMATE

- Phase 1 (Backend): 2-3 giờ
- Phase 2 (Frontend): 2-3 giờ
- Phase 3 (Testing): 1-2 giờ
- **Total**: 5-8 giờ

---

## ⚠️ CÁC LƯU Ý QUAN TRỌNG

1. ✅ Nhà cung cấp được **chọn từ dropdown** (danh mục sẵn có)
2. ✅ **Không cho thêm** hàng hóa nếu tên đã tồn tại
3. ✅ Số lượng tồn & đơn giá phải **validate kiểu dữ liệu**
4. ✅ Đơn vị tính: **chỉ cho phép** kg, thùng, túi
5. ✅ **CORS** cần được cấu hình để frontend gọi API
6. ✅ Sử dụng **async/await** trong backend
7. ✅ Error handling: Trả về proper HTTP status codes (400, 404, 409, 500)
8. ✅ Frontend: Hiển thị error messages rõ ràng

---

## ✨ CHỨC NĂNG BỔ SUNG (Optional)
- Pagination cho danh sách hàng hóa
- Export danh sách ra Excel/PDF
- Sort by cột trong table
- Filter by nhà cung cấp
- Responsive mobile design
- Authentication/Authorization

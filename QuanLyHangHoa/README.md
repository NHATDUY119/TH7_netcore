# Ứng Dụng Quản Lý Hàng Hóa & Nhà Cung Cấp

Ứng dụng web quản lý hàng hóa và nhà cung cấp được xây dựng với .NET Core, SQLite, HTML, CSS và JavaScript.

## 🎯 Chức Năng

- ✅ Hiển thị danh sách tất cả hàng hóa
- ✅ Tìm kiếm hàng hóa theo tên và khoảng giá
- ✅ Thêm hàng hóa mới (không trùng tên, validate dữ liệu)
- ✅ Sửa thông tin hàng hóa
- ✅ Xóa hàng hóa
- ✅ Quản lý nhà cung cấp
- ✅ Danh sách nhà cung cấp sẵn có để lựa chọn

## 🛠️ Công Nghệ Sử Dụng

- **Backend**: ASP.NET Core 10.0
- **Database**: SQLite
- **Frontend**: HTML5, CSS3, JavaScript (Vanilla)
- **ORM**: Entity Framework Core

## 📁 Cấu Trúc Dự Án

```
QuanLyHangHoa/
├── Controllers/          # API Controllers
├── Data/                 # DbContext & Database
├── Models/               # Data Models
├── Repositories/         # Data Access Layer
├── Services/             # Business Logic Layer
├── wwwroot/              # Frontend (HTML, CSS, JS)
│   ├── index.html
│   ├── css/style.css
│   └── js/
│       ├── api.js
│       ├── app.js
│       └── utils.js
└── Program.cs            # Application Startup
```

## 🚀 Hướng Dẫn Chạy Ứng Dụng

### Yêu Cầu
- .NET SDK 10.0 hoặc cao hơn
- Visual Studio Code / Visual Studio hoặc Command Line

### Bước 1: Khởi Động Backend

```bash
cd QuanLyHangHoa
dotnet run
```

Ứng dụng sẽ khởi động trên: `http://localhost:5135`

### Bước 2: Mở Frontend

Mở trình duyệt web và truy cập:
```
http://localhost:5135
```

### Bước 3: Sử Dụng Ứng Dụng

- **Danh Sách Hàng Hóa**: Xem tất cả hàng hóa hiện tại
- **Tìm Kiếm**: Tìm hàng hóa theo tên và khoảng giá (từ - đến)
- **Thêm Hàng Hóa**: Thêm hàng hóa mới với các thông tin cần thiết
- **Sửa**: Nhấn nút "Sửa" để cập nhật thông tin hàng hóa
- **Xóa**: Nhấn nút "Xóa" để xóa hàng hóa (cần xác nhận)
- **Nhà Cung Cấp**: Xem danh sách tất cả nhà cung cấp

## 📋 Validations

### Hàng Hóa
- **Tên Hàng**: Bắt buộc, không được trùng (max 200 ký tự)
- **Đơn Vị Tính**: Chỉ chấp nhận: kg, thùng, túi
- **Số Lượng Tồn**: Phải là số >= 0
- **Đơn Giá**: Phải là số >= 0
- **Nhà Cung Cấp**: Phải chọn từ danh mục sẵn có

### Nhà Cung Cấp
- **Tên NCC**: Bắt buộc, unique (max 200 ký tự)
- **Địa Chỉ**: Tùy chọn (max 500 ký tự)
- **Điện Thoại**: Tùy chọn (max 20 ký tự)

## 🗄️ Database

### Dữ Liệu Mẫu (Seed)

**Nhà Cung Cấp**:
1. Công ty TNHH A - TP.HCM
2. Công ty TNHH B - Hà Nội
3. Công ty TNHH C - Đà Nẵng

Database sẽ được tạo tự động khi chạy ứng dụng (`quanlyhh.db`)

## 🔌 API Endpoints

### Hàng Hóa
- `GET /api/hanghoa` - Lấy danh sách tất cả
- `GET /api/hanghoa/{id}` - Lấy chi tiết
- `GET /api/hanghoa/search/filter?tenHang=...&donGiaMin=...&donGiaMax=...` - Tìm kiếm
- `POST /api/hanghoa` - Thêm mới
- `PUT /api/hanghoa/{id}` - Cập nhật
- `DELETE /api/hanghoa/{id}` - Xóa

### Nhà Cung Cấp
- `GET /api/nhacungcap` - Lấy danh sách tất cả
- `GET /api/nhacungcap/{id}` - Lấy chi tiết
- `POST /api/nhacungcap` - Thêm mới
- `PUT /api/nhacungcap/{id}` - Cập nhật
- `DELETE /api/nhacungcap/{id}` - Xóa

## 🎨 Giao Diện

- Responsive Design - tương thích với mọi kích thước màn hình
- Theme: Gradient Purple (Modern & Professional)
- Navigation: Tab-based interface
- Modal: Cho việc chỉnh sửa hàng hóa
- Alerts: Thông báo thành công/lỗi

## 📝 Ghi Chú

- CORS đã được cấu hình để cho phép request từ mọi nguồn
- Tất cả lỗi từ server sẽ được hiển thị trên frontend
- Validation được thực hiện cả ở frontend và backend
- Database tự động migrate khi ứng dụng khởi động

## 🐛 Troubleshooting

**Lỗi: "Cannot connect to API"**
- Kiểm tra xem server đã khởi động chưa (`http://localhost:5135`)
- Kiểm tra CORS settings

**Lỗi: "Database is locked"**
- Dừng ứng dụng và chạy lại

**Lỗi: Port 5135 đã được sử dụng**
- Đổi port trong `launchSettings.json` và cập nhật `js/api.js`

## 📚 Tài Liệu Tham Khảo

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/dotnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SQLite Documentation](https://www.sqlite.org/docs.html)

---

**Phiên bản**: 1.0  
**Ngày tạo**: May 2026  
**Tác giả**: Development Team

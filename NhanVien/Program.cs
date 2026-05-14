var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ hỗ trợ Controller
builder.Services.AddControllers();

var app = builder.Build();

// Cấu hình Pipeline để đọc file tĩnh và map API
app.UseDefaultFiles(); // Tìm file index.html mặc định khi mở web
app.UseStaticFiles();  // Cho phép đọc nội dung trong thư mục wwwroot
app.MapControllers();  // Ánh xạ các route tới NhanVienController

app.Run();
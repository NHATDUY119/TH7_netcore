# 1. Giai đoạn Build (Sử dụng SDK .NET 10.0)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy file cấu hình dự án và khôi phục (restore) các thư viện
COPY ["Bai6.csproj", "./"]
RUN dotnet restore "./Bai6.csproj"

# Copy toàn bộ mã nguồn và tiến hành biên dịch (publish)
COPY . .
RUN dotnet publish "Bai6.csproj" -c Release -o /app/publish

# 2. Giai đoạn Runtime (Sử dụng ASPNET .NET 10.0 siêu nhẹ)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy các file đã được biên dịch từ giai đoạn build sang
COPY --from=build /app/publish .

# Copy database SQLite vào thư mục chạy
COPY qlnv2.db .

# Cấp quyền đọc/ghi (777) cho file database để không bị lỗi "Read-Only"
RUN chmod 777 qlnv2.db

# Ép Container chạy ở cổng 5000 (để khớp với code HttpClient gọi API)
ENV ASPNETCORE_URLS=http://+:5000

# Lệnh khởi động dự án
ENTRYPOINT ["dotnet", "Bai6.dll"]
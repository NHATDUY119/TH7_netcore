using Microsoft.EntityFrameworkCore;
using QuanLyHangHoa.Models;

namespace QuanLyHangHoa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure NhaCungCap
            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNCC);
                entity.Property(e => e.TenNCC).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DiaChi).HasMaxLength(500);
                entity.Property(e => e.DienThoai).HasMaxLength(20);
                entity.HasIndex(e => e.TenNCC).IsUnique();
            });

            // Configure HangHoa
            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.HasKey(e => e.MaHang);
                entity.Property(e => e.TenHang).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DonViTinh).IsRequired().HasMaxLength(20);
                entity.Property(e => e.SoLuongTon).HasDefaultValue(0);
                entity.Property(e => e.DonGia).HasPrecision(10, 2);
                entity.HasIndex(e => e.TenHang).IsUnique();
                
                // Foreign key relationship
                entity.HasOne(e => e.NhaCungCap)
                    .WithMany(n => n.HangHoas)
                    .HasForeignKey(e => e.MaNCC)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data for NhaCungCap
            modelBuilder.Entity<NhaCungCap>().HasData(
                new NhaCungCap { MaNCC = 1, TenNCC = "Công ty TNHH A", DiaChi = "123 Nguyễn Huệ, TP.HCM", DienThoai = "0901234567" },
                new NhaCungCap { MaNCC = 2, TenNCC = "Công ty TNHH B", DiaChi = "456 Lê Lợi, Hà Nội", DienThoai = "0912345678" },
                new NhaCungCap { MaNCC = 3, TenNCC = "Công ty TNHH C", DiaChi = "789 Trần Hưng Đạo, Đà Nẵng", DienThoai = "0923456789" }
            );
        }
    }
}

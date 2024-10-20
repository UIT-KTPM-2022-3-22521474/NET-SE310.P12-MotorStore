using Microsoft.EntityFrameworkCore;

namespace MotorbikeStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor nhận tham số options từ startup để cấu hình DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Khai báo bảng Motorbikes
        public DbSet<Motorbike> Motorbikes { get; set; }
        public DbSet<SalesRecords> SalesRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính cho SalesRecord nếu cần
            modelBuilder.Entity<SalesRecords>()
                .HasKey(sr => sr.SaleId); // Khóa chính là SaleId
        }
    }
}

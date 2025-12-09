using attendanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace attendanceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        // Example table
        public DbSet<User>? User { get; set; }
        public DbSet<Attendance>? Attendance { get; set; }
        public DbSet<Image>? Image { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure RefreshToken relationship
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add index for faster lookups
            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token);
        }

    }
}
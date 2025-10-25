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
    }
}
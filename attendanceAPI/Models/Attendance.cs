using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace attendanceAPI.Models
{
    public class Attendance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
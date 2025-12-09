
using attendanceAPI.Models;

namespace attendanceAPI.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Image? Image { get; set; } = null;
    }
}
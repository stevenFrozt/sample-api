using System.ComponentModel.DataAnnotations;

namespace attendanceAPI.Features.Auth.DTO
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
    }
}
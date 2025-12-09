
using System.ComponentModel.DataAnnotations;
using attendanceAPI.Models;


namespace attendanceAPI.Features.Users.Commands.CreateUser
{
    public class UpdateUserRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        public Image? Image { get; set; } = null;
    }
}
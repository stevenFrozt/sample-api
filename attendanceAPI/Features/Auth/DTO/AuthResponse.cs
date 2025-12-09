using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using attendanceAPI.Models;

namespace attendanceAPI.Features.Auth.DTO
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpires { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public UserInfo User { get; set; } = null!;
        // public User User { get; set; } = null!;

    }

    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Image? Image { get; set; }  // Use a DTO for Image too
    }
}
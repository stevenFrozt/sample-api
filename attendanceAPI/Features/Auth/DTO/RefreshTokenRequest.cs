using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace attendanceAPI.Features.Auth.DTO
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
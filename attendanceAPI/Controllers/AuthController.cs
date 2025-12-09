
using attendanceAPI.Data;
using attendanceAPI.Features.Auth.DTO;
using attendanceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace attendanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public AuthController(
            AppDbContext context,
             IConfiguration config,
             ITokenService tokenService
        )
        {
            _context = context;
            _config = config;
            _tokenService = tokenService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            // Find user by username (or email)
            var user = await _context.User!
                .Include(x => x.Image)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            if (user.Password != request.Password)
            {
                return Unauthorized("Invalid username or password.");
            }


            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var savedRefreshToken = await _tokenService.SaveRefreshTokenAsync(user.Id, refreshToken);
            var jwtSettings = _config.GetSection("JwtConfig");
            int accessValidity = jwtSettings.GetValue<int>("AccessTokenValidityMins");

            return Ok(new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(accessValidity),
                RefreshTokenExpires = savedRefreshToken.ExpiresAt,
                User = new UserInfo
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Gender = user.Gender,
                    Image = user.Image
                }


            });

        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token is required.");

            var refreshToken = await _tokenService.GetRefreshTokenAsync(request.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive)
                return Unauthorized("Invalid or expired refresh token.");

            var user = refreshToken.User;
            if (user == null)
                return Unauthorized("User not found.");

            // Generate new tokens
            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Revoke old refresh token and save new one
            await _tokenService.RevokeRefreshTokenAsync(refreshToken, newRefreshToken);
            var savedRefreshToken = await _tokenService.SaveRefreshTokenAsync(user.Id, newRefreshToken);

            var jwtSettings = _config.GetSection("JwtConfig");
            int accessValidity = jwtSettings.GetValue<int>("AccessTokenValidityMins");

            var response = new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(accessValidity),
                RefreshTokenExpires = savedRefreshToken.ExpiresAt,
                User = new UserInfo
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Image = user.Image
                }
            };

            return Ok(response);
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            if (!string.IsNullOrEmpty(request.RefreshToken))
            {
                var refreshToken = await _tokenService.GetRefreshTokenAsync(request.RefreshToken);
                if (refreshToken != null)
                {
                    await _tokenService.RevokeRefreshTokenAsync(refreshToken);
                }
            }

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Refresh token is required.");

            var refreshToken = await _tokenService.GetRefreshTokenAsync(request.RefreshToken);

            if (refreshToken == null)
                return NotFound("Refresh token not found.");

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (refreshToken.UserId != userId)
                return Forbid();

            await _tokenService.RevokeRefreshTokenAsync(refreshToken);

            return Ok(new { message = "Refresh token revoked successfully" });
        }

        [HttpPost("revoke-all")]
        [Authorize]
        public async Task<IActionResult> RevokeAll()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _tokenService.RevokeAllUserTokensAsync(userId);

            return Ok(new { message = "All refresh tokens revoked successfully" });
        }
    }
}
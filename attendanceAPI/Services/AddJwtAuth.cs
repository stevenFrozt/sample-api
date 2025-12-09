using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace attendanceAPI.Services
{
    public static class JwtAuth
    {
        public static IServiceCollection AddJwtAuth(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {

            // Log configuration values
            var key = configuration["JwtConfig:Key"];


            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("JWT Key is not configured!");
            }

            var keyBytes = Convert.FromBase64String(key);
            Console.WriteLine($"Key bytes length: {keyBytes.Length}");
            Console.WriteLine($"Key bytes: {BitConverter.ToString(keyBytes)}");
            Console.WriteLine("============================");


            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["JwtConfig:Issuer"],
                        ValidAudience = configuration["JwtConfig:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            // Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"])
                            Convert.FromBase64String(configuration["JwtConfig:Key"])
                        ),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                    // FOR LOGGING
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Authentication failed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
    }
}

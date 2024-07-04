using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Data.Entities;
using ToDoApp.Server.Authentication;
using ToDoApp.Server.Authentication.Models;
using ToDoApp.Service.IServices;
using ToDoApp.Service.Models;

namespace ToDoApp.Server.Services
{
    public class AuthenticationService : IAuthentication
    {
        private IUserService _userService;
        private AuthConfig _config;
        public AuthenticationService(IUserService userService,AuthConfig config)
        {
            _userService = userService;
            _config = config;
        }
        static private bool VerifyPassword(UserDto userRequest, UserDto userEntry)
        {
            PasswordHasher<UserDto> hasher = new PasswordHasher<UserDto>();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(userRequest, userEntry.Password, userRequest.Password);
            if (result == PasswordVerificationResult.Failed) return false;
            return true;
        }
        private string GenerateToken(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config._secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userDto.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config._issuer,
                _config._audience,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<AuthResponse> LoginUserAsync(UserDto userDto)
        {
            var result = await _userService.GetUserServiceAsync(userDto.Username);
            if (!result.IsSuccess)
            {
                UserDto user = result.Result!;
                if (VerifyPassword(user, userDto))
                {
                    return new AuthResponse
                    {
                        IsSuccess = true,
                        Message = "LoginSuccessful",
                        TokenString = GenerateToken(userDto)
                    };
                }
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "PasswordMisMatch"
                };
            }
            return new AuthResponse
            {
                IsSuccess = false,
                Message = "ServerError"
            };
        }

    }
}



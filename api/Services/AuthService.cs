using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.AuthDto;
using api.Dtos.LoginDto;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class AuthService : IAuthService
    {
        private readonly PmsatContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(PmsatContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !VerifyPassword(request.Password, user.Password))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        if (user.Status != "Active")
        {
            throw new UnauthorizedAccessException("User account is not active");
        }

        var token = GenerateJwtToken(user);

        return new LoginResponse
        {
            Id = user.Id,
            Token = token,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            throw new Exception("Email is already registered");
        }

        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User",
            Status = "Active"
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        var loginResponse = await Login(new LoginRequest
        {
            Email = request.Email,
            Password = request.Password
        });

        return new RegisterResponse
        {
            Id = Guid.NewGuid(),
            Token = loginResponse.Token,
            Name = newUser.Name,
            Email = newUser.Email,
            Role = newUser.Role
        };
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
    }
    }
}

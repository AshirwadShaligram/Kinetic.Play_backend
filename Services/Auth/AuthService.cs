using gamevault_backend.Data;
using gamevault_backend.DTOs.Auth;
using gamevault_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gamevault_backend.Services.Auth;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthService(AppDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            x => x.Email == dto.Email
        );

        if(user == null)
            throw new Exception("Invalid credentials");
        
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,user.Password
        );

        if(!isPasswordValid)
            throw new Exception("Invalid credentails");

        var accessToken = _jwtService.GenerateToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiredDate = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Email = user.Email,
            Role = user.Role,
        };
        
    }

    public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            x=> x.RefreshToken == refreshToken
        );

        if(user == null)
            throw new Exception("Invalid refresh token");

        if(user.RefreshTokenExpiredDate < DateTime.UtcNow)
            throw new Exception("Refresh token expired");

        var newAccessToken = _jwtService.GenerateToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiredDate = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<User> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(
            x => x.Email == dto.Email
        );

        if(existingUser != null)
            throw new Exception("Email already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email,
            Password = hashedPassword,
            Role = dto.Role
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
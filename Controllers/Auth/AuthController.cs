using gamevault_backend.DTOs.Auth;
using gamevault_backend.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace gamevault_backend.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        try
        {
            var user = await _authService.RegisterAsync(dto);

            return Ok(new
            {
                message = "User created successfully! Please login to continue"
            });
        } catch(Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        Response.Cookies.Append(
            "refreshToken",
            result.RefreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = false,
                Expires = DateTime.UtcNow.AddDays(7)
            }
        );

        return Ok(new
        {
            message = "Login successfully",
            accessToken = result.AccessToken,
            user = new
            {
                email = result.Email,
                role = result.Role
            }
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Delete(
            "refreshToken",
            new CookieOptions
            {
                Path = "/",
                Secure = false,
                SameSite = SameSiteMode.Strict
            }
        );

        return Ok(new {message = "Logged out successfully"});
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new { message = "Refresh token expired" });
        }

        try
        {
            var result = await _authService.RefreshAsync(refreshToken);

            Response.Cookies.Append(
                "refreshToken",
                result.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = false,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            return Ok(new
            {
                accessToken = result.AccessToken,
                user = new
                {
                    email = result.Email,
                    role = result.Role
                }
            });

        } catch(Exception ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
}
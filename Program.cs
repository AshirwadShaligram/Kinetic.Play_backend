using System.Text;
using System.Text.Json.Serialization;
using gamevault_backend.Data;
using gamevault_backend.Services.Admin;
using gamevault_backend.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var secret = builder.Configuration["Jwt:SecretKey"];

// JWT Secret check
if (string.IsNullOrEmpty(secret))
{
    throw new InvalidOperationException("JWT secret is missing.");
}

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=gamevault.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           ValidateIssuer = false,
           ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret!)
            )
       }; 
    });

builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", 
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
    );
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// AUTH SERVICES
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// ADMIN SERVICES
builder.Services.AddScoped<ICustomerInfoInterface, CustomerInfoService>();
builder.Services.AddScoped<ISellerInfoInterface, SellerInfoService>();

// SELLER SERVICES

// CUSTOMER SERVIECS



var app = builder.Build();

// Enable cors
app.UseCors("AllowFrontend");

// Middleware
app.UseAuthentication();
app.UseAuthorization();

// Enable Controller
app.MapControllers();

// Root endpoint
app.MapGet("/", () => "GameVault server is running.");

app.Run();


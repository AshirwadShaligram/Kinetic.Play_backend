using gamevault_backend.Data;
using gamevault_backend.DTOs.Admin.SellerInfo;
using gamevault_backend.Enums.Role;
using Microsoft.EntityFrameworkCore;

namespace gamevault_backend.Services.Admin;

public class SellerInfoService : ISellerInfoInterface
{
    private readonly AppDbContext _context;

    public SellerInfoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SellerInfoResponseDto>> GetAllSellerInfoAsync()
    {
        return await _context.Users
            .Where(u => u.Role == Role.Seller)
            .Select(u => new SellerInfoResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            })
            .ToListAsync();
    }
}
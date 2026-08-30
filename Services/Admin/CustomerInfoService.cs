using gamevault_backend.Data;
using gamevault_backend.DTOs.Admin.CustomerInfo;
using gamevault_backend.Enums.Role;
using Microsoft.EntityFrameworkCore;

namespace gamevault_backend.Services.Admin;

public class CustomerInfoService : ICustomerInfoInterface
{
    private readonly AppDbContext _context;

    public CustomerInfoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerInfoResponseDto>> GetCustomerInfoAsync()
    {
        return await _context.Users
            .Where(u => u.Role == Role.Customer)
            .Select(u => new CustomerInfoResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            })
            .ToListAsync();
    }
}
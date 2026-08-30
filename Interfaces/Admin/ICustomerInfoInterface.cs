using gamevault_backend.DTOs.Admin.CustomerInfo;

namespace gamevault_backend.Services.Admin;

public interface ICustomerInfoInterface
{
    Task<List<CustomerInfoResponseDto>> GetCustomerInfoAsync();
}
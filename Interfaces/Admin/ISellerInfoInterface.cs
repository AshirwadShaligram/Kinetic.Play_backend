using gamevault_backend.DTOs.Admin.SellerInfo;

namespace gamevault_backend.Services.Admin;

public interface ISellerInfoInterface
{
    Task<List<SellerInfoResponseDto>> GetAllSellerInfoAsync();
}
using gamevault_backend.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gamevault_backend.Controllers.Admin;

[ApiController]
[Route("api/admin/seller")]
public class SellerInfoController : ControllerBase
{
    private readonly ISellerInfoInterface _sellerInfoInterface;

    public SellerInfoController(ISellerInfoInterface sellerInfoInterface)
    {
        _sellerInfoInterface = sellerInfoInterface;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("info")]
    public async Task<IActionResult> GetAllSellers()
    {
        var sellers = await _sellerInfoInterface.GetAllSellerInfoAsync();

        return Ok(sellers);
    }
}
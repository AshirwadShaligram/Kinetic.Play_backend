using gamevault_backend.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gamevault_backend.Controllers.Admin;

[ApiController]
[Route("api/admin/customer")]
public class CustomerInfoController: ControllerBase
{
    private readonly ICustomerInfoInterface _customerInfoInterface;

    public CustomerInfoController(ICustomerInfoInterface customerInfoInterface)
    {
        _customerInfoInterface = customerInfoInterface;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("info")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerInfoInterface.GetCustomerInfoAsync();

        return Ok(customers);
    }
}
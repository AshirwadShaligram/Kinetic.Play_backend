using gamevault_backend.DTOs.Category;
using gamevault_backend.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gamevault_backend.Controllers.Category;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryInterface _catergoryInterface;

    public CategoryController(ICategoryInterface categoryInterface)
    {
        _catergoryInterface = categoryInterface;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create-category")]
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto dto)
    {
        var category = await _catergoryInterface.CreateCategoryAsync(dto);

        return Ok(new
        {
            category,
            message = "Category created successfully."
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("update-category/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromForm] UpdateCategoryDto dto)
    {
        var updatedCategory = await _catergoryInterface.UpdateCategoryAsync(id, dto);

        return Ok(new
        {
            updatedCategory,
            message = "Category updated successfully"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-category/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var deleted = await _catergoryInterface.DeleteCategoryAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        return Ok(new
        {
            message = "Category deleted successfully"
        });
    }

    [Authorize(Roles = "Admin,Seller,Customer")]
    [HttpGet("get-all-categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _catergoryInterface.GetAllCategoriesAsync());
    }

    [Authorize(Roles = "Admin,Seller,Customer")]
    [HttpGet("get-category-by-id/{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _catergoryInterface.GetCategoryByIdAsync(id);

        if(category == null)
            return NotFound();
        
        return Ok(category);
    }
}
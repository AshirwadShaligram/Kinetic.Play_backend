using gamevault_backend.DTOs.Admin.Category;
using gamevault_backend.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gamevault_backend.Controllers.Admin.Category;

[ApiController]
[Route("api/Admin/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryInterface _categoryInterface;

    public CategoryController(ICategoryInterface categoryInterface)
    {
        _categoryInterface = categoryInterface;
    }

    // -------------------- CREATE CATEGORY API-------------------------
    // POST {HOST_ADDRESS/api/admin/category/create-category}

    [Authorize(Roles = "Admin")]
    [HttpPost("create-category")]
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDto dto)
    {
        var category = await _categoryInterface.CreateCategoryAsync(dto);

        return Ok(new
        {
            category,
            message = "Category created successfully."
        });
    }

    // -------------------- DELETE CATEGORY API-------------------------
    // DELETE {HOST_ADDRESS/api/admin/category/{id:guid}}

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var deleted = await _categoryInterface.DeleteCategoryAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Category not found."
            });
        }

        return Ok(new
        {
            message = "Category deleted successfully."
        });
    }

    // -------------------- UPDATE CATEGORY API-------------------------
    // PATCH {HOST_ADDRESS/api/admin/category/{id:guid}}

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromForm] UpdateCategoryDto dto)
    {
        var updatedCategory = await _categoryInterface.UpdateCategoryAsync(id, dto);

        return Ok(new
        {
            updatedCategory,
            message = "Category updated successfully."
        });
    }

    // -------------------- GET ALL CATEGORIES API-------------------------
    // GET {HOST_ADDRESS/api/admin/category/get-all-categories}

    [Authorize(Roles = "Admin")]
    [HttpGet("get-all-categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryInterface.GetAllCategoriesAsync();

        if(categories == null)
            return NotFound();

        return Ok(categories);
    }

    // -------------------- GET CATEGORY BY ID API-------------------------
    // GET {HOST_ADDRESS/api/admin/category/{guid}}

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryInterface.GetCategoryByIdAsync(id);

        if(category == null)
            return NotFound();

        return Ok(category);
    }

    // -------------------- TOGGLE CATEGORY VISIBILITY API-------------------------
    // PATCH {HOST_ADDRESS/api/admin/category/{guid}}

    [Authorize(Roles = "Admin")]
    [HttpPatch("toggle/{id:guid}")]
    public  async Task<IActionResult> ToggleCategoryVisibility(Guid id)
    {
       var isVisible = await _categoryInterface.ToggleCategoryVisibilityAsync(id);

       return Ok(new {
            message = isVisible ? "Category is now visible" : "Category is now hidden"
        });
    }
}


using gamevault_backend.DTOs.Admin.SubCategory;
using gamevault_backend.Services.Admin.SubCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gamevault_backend.Controllers.Admin.SubCategory;

[ApiController]
[Route("api/admin/[controller]")]
public class SubCategoryController : ControllerBase
{
   private  readonly ISubCategoryInterface _subCategoryInterface;

   public SubCategoryController(ISubCategoryInterface subCategoryInterface)
    {
        _subCategoryInterface = subCategoryInterface;
    }


    // -------------------- CREATE SUBCATEGORY API-------------------------
    // POST {HOST_ADDRESS/api/admin/subCategory/create}

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public  async Task<IActionResult> CreateSubCategory(CreateSubCategory dto)
    {
        var subCategory = await _subCategoryInterface.CreateSubCategoryAsync(dto);

        return Ok(new
        {
            subCategory,
            message = "SubCategory created successfully"
        });
    }

    // -------------------- Update SUBCATEGORY API-------------------------
    // POST {HOST_ADDRESS/api/admin/subCategory/{guid}}

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:guid}")]
    public  async Task<IActionResult> UpdateSubCategory(Guid id, UpdateSubCategory dto)
    {
        var result = await _subCategoryInterface.UpdateSubCategoryAsync(id, dto);

        return Ok(new
        {
            result,
            message = "SubCategory updated successfully"
        });
    }

    // -------------------- DELETE SUBCATEGORY API-------------------------
    // POST {HOST_ADDRESS/api/admin/subCategory/{guid}}

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public  async Task<IActionResult> DeleteSubCategory(Guid id)
    {
       var deleted = await _subCategoryInterface.DeleteSubCategory(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Sub-Category not found."
            });
        }

        return Ok(new
        {
            message = "Sub-Category deleted successfully."
        });
    }


    // -------------------- GETALL SUBCATEGORY API-------------------------
    // POST {HOST_ADDRESS/api/admin/subCategory/all}

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public  async Task<IActionResult> GetAllSubCategories()
    {
       var subCategories = await _subCategoryInterface.GetAllSubCategoryAsync();

        if (subCategories == null)
            return NotFound();
        

        return Ok(subCategories);
    }

    // -------------------- GET SUBCATEGORY BY ID API-------------------------
    // POST {HOST_ADDRESS/api/admin/subCategory/{guid}}

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public  async Task<IActionResult> GetSubCategoryById(Guid id)
    {
       var subCategory = await _subCategoryInterface.GetSubCategoryById(id);

        if (subCategory == null)
            return NotFound();
        

        return Ok(subCategory);
    }

    // -------------------- TOGGLE SUBCATEGORY VISIBILITY API-------------------------
    // PATCH {HOST_ADDRESS/api/admin/subCategory/{guid}}

    [Authorize(Roles = "Admin")]
    [HttpPatch("visibility/{id:guid}")]
    public  async Task<IActionResult> ToggleSubCategoryVisibility(Guid id)
    {
       var isVisible = await _subCategoryInterface.ToggleSubCategoryVisibilityAsync(id);

       return Ok(new {
            message = isVisible ? "SubCategory is now visible" : "SubCategory is now hidden"
        });
    }
}
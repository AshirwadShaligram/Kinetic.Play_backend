using gamevault_backend.Data;
using gamevault_backend.DTOs.Admin.SubCategory;
using Microsoft.EntityFrameworkCore;

namespace gamevault_backend.Services.Admin.SubCategory;

public class SubCategoryService : ISubCategoryInterface
{
    private readonly AppDbContext _context;

    public SubCategoryService(AppDbContext context)
    {
        _context = context;
    }

    // ----------------------CREATE SUB-CATEGORY----------------------
    public async Task<SubCategoryResponseDto> CreateSubCategoryAsync(CreateSubCategory dto)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

        if(category == null)
            throw new KeyNotFoundException("Category not found");

        var subCategory = new Models.SubCategory
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            IsVisible = true
        };

        _context.SubCategories.Add(subCategory);

        await _context.SaveChangesAsync();

        return new SubCategoryResponseDto
        {
            Id = subCategory.Id,
            Name = subCategory.Name,
            IsVisible = subCategory.IsVisible,
            CategoryId = subCategory.CategoryId
        };
    }

    // ----------------------DELETE SUB_CATEGORY----------------------
    public async Task<bool> DeleteSubCategory(Guid id)
    {
        var subCategory = await _context.SubCategories.FirstOrDefaultAsync(sc=> sc.Id == id);

        if(subCategory == null)
            throw new KeyNotFoundException("Sub Category not found.");

        var hasProduct = await _context.Products.AnyAsync(p => p.SubCategoryId == id);

        if(hasProduct)
            throw new InvalidOperationException(
                "Cannot delete this sub-category because products are associate with it."
            );

        _context.SubCategories.Remove(subCategory);

        await _context.SaveChangesAsync();

        return true;
    }

    // ----------------------GET ALL SUB_CATEGORY----------------------
    public async Task<List<SubCategoryResponseDto>> GetAllSubCategoryAsync()
    {
        var subCategories = await _context.SubCategories.ToListAsync();

        return subCategories.Select(subCategory => new SubCategoryResponseDto
        {
            Id = subCategory.Id,
            Name = subCategory.Name,
            IsVisible = subCategory.IsVisible,
            CategoryId = subCategory.CategoryId
        }).ToList();
    }

    // ----------------------GET SUB_CATEGORY BY ID_-------------------
    public async Task<SubCategoryResponseDto?> GetSubCategoryById(Guid id)
    {
        return await _context.SubCategories
            .Where(c => c.Id == id)
            .Select(subCategory => new SubCategoryResponseDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                IsVisible = subCategory.IsVisible,
                CategoryId = subCategory.CategoryId
            })
            .FirstOrDefaultAsync();
    }

    // ---------------------- TOGGLE SUB_CATEGORY VISIBILITY ----------------------
    public async Task<bool> ToggleSubCategoryVisibilityAsync(Guid id)
    {
        var subCategory = await _context.SubCategories.FirstOrDefaultAsync(sc => sc.Id == id);

        if(subCategory == null)
            throw new KeyNotFoundException("Sub-Category not found.");

        subCategory.IsVisible = !subCategory.IsVisible;

        await _context.SaveChangesAsync();

        return subCategory.IsVisible;
    }

    // ----------------------UPDATE SUB_CATEGORY----------------------
    public async Task<SubCategoryResponseDto> UpdateSubCategoryAsync(Guid id, UpdateSubCategory dto)
    {
        var subCategory = await _context.SubCategories.FirstOrDefaultAsync(sc => sc.Id == id);

        if(subCategory == null)
            throw new KeyNotFoundException("Sub-Category not found");

        // Checking if new category exists nor not
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

        if(category == null)
            throw new KeyNotFoundException("Category does not exists.");

        if(dto.Name != null)
            subCategory.Name = dto.Name;

        subCategory.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return new SubCategoryResponseDto
        {
            Id = subCategory.Id,
            Name = subCategory.Name,
            IsVisible = subCategory.IsVisible,
            CategoryId = subCategory.CategoryId
        };

    }
}
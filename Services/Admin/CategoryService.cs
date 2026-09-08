using gamevault_backend.Data;
using gamevault_backend.DTOs.Admin.Category;
using gamevault_backend.DTOs.Admin.SubCategory;
using gamevault_backend.Services.Image;
using Microsoft.EntityFrameworkCore;

namespace gamevault_backend.Services.Admin.Category;

public class CategoryService : ICategoryInterface
{
    private readonly AppDbContext _context;
    private readonly IImageInterface _imageInterface;

    public CategoryService(AppDbContext context, IImageInterface imageInterface)
    {
        _context = context;
        _imageInterface = imageInterface;
    }

    // ----------------------------CATEGORY CREATE SERVICE----------------------------------
    public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var uploadResult = await _imageInterface.UploadImageAsync(dto.Image);

        var category = new Models.Category
        {
            Title = dto.Title,
            Description = dto.Description,
            Image = uploadResult.Url,
            ImagePublicId = uploadResult.PublicId,
            Logo = dto.Logo,
            IsVisible = true
        };

        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            Title = category.Title,
            Description = category.Description,
            Image = category.Image,
            IsVisible = category.IsVisible,
            Logo = category.Logo,
            ActiveProducts = category.ActiveProducts,
            SubCategories = new List<SubCategoryResponseDto>()
        };
    }

    // ----------------------------CATEGORY DELETE SERVICE----------------------------------
    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if(category == null)
            return false;

        if (!string.IsNullOrEmpty(category.ImagePublicId))
        {
            await _imageInterface.DeleteImageAsync(category.ImagePublicId);
        }

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return true;
    }

    // ----------------------------GET ALL CATEGORIES---------------------------------------
    public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories
            .Include(c => c.SubCategories)
            .ToListAsync();

        return categories.Select(category => new CategoryResponseDto
        {
           Id = category.Id,
           Title = category.Title,
           Description = category.Description,
           Image = category.Image,
           Logo = category.Logo,
           IsVisible = category.IsVisible,
           ActiveProducts = category.ActiveProducts,
           SubCategories = category.SubCategories.Select(subCategory => new SubCategoryResponseDto
           {
                Id = subCategory.Id,
                Name = subCategory.Name,
                IsVisible = subCategory.IsVisible,
                CategoryId = subCategory.CategoryId
           }).ToList()
        }).ToList();
    }


     // ----------------------------GET CATEGORIES BY ID---------------------------------------
    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id)
    {
        return await _context.Categories
            .Where(c => c.Id == id)
            .Select(category => new CategoryResponseDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    Description = category.Description,
                    Image = category.Image,
                    Logo = category.Logo,
                    IsVisible = category.IsVisible,
                    ActiveProducts = category.ActiveProducts,
                    SubCategories = category.SubCategories.Select(subCategory => new SubCategoryResponseDto
                    {
                        Id = subCategory.Id,
                        Name = subCategory.Name,
                        IsVisible = subCategory.IsVisible,
                        CategoryId = subCategory.CategoryId
                    }).ToList()
                }).FirstOrDefaultAsync();
    }

    // ---------------------- TOGGLE CATEGORY VISIBILITY ----------------------
    public async Task<bool> ToggleCategoryVisibilityAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if(category == null)
            throw new KeyNotFoundException("Category not found");
        
        category.IsVisible = !category.IsVisible;

        await _context.SaveChangesAsync();

        return category.IsVisible;
    }

    // ----------------------------CATEGORY UPDATE SERVICE----------------------------------
    public async Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.Include(c => c.SubCategories).FirstOrDefaultAsync(c => c.Id == id);

        if(category == null)
            throw new KeyNotFoundException("Category not found");

        if(dto.Title != null)
            category.Title = dto.Title;
        if(dto.Description != null)
            category.Description = dto.Description;

        // Updating image
        if(dto.Image != null)
        {
            var oldImagePublicId = category.ImagePublicId;
            var uploadResult = await _imageInterface.UploadImageAsync(dto.Image);

            category.Image = uploadResult.Url;
            category.ImagePublicId = uploadResult.PublicId;

            if (!string.IsNullOrEmpty(oldImagePublicId))
            {
                await _imageInterface.DeleteImageAsync(oldImagePublicId);
            }
        }

        if(dto.Logo != null)
            category.Logo = dto.Logo;

        await _context.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            Title = category.Title,
            Description = category.Description,
            Image = category.Image,
            IsVisible = category.IsVisible,
            Logo = category.Logo,
            ActiveProducts = category.ActiveProducts,
            SubCategories = category.SubCategories.Select(subCategory => new SubCategoryResponseDto
                    {
                        Id = subCategory.Id,
                        Name = subCategory.Name,
                        IsVisible = subCategory.IsVisible
                    }).ToList()
        };
    }
}
using gamevault_backend.Data;
using gamevault_backend.DTOs.Category;
using gamevault_backend.Services.Image;
using Microsoft.EntityFrameworkCore;


namespace gamevault_backend.Services.Category;

public class CategoryService : ICategoryInterface
{
    private readonly AppDbContext _context;
    private readonly IImageInterface _imageInterface;

    public CategoryService(AppDbContext context, IImageInterface imageInterface)
    {
        _context = context;
        _imageInterface = imageInterface;
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var uploadResult = await _imageInterface.UploadImageAsync(dto.CategoryImage);

        var category = new Models.Category
        {
            CategoryTitle = dto.CategoryTitle,
            CategoryImage = uploadResult.Url,
            CategoryImagePublicId = uploadResult.PublicId
        };

       _context.Categories.Add(category);
       await _context.SaveChangesAsync();

       return new CategoryResponseDto
       {
           Id = category.Id,
           CategoryTitle = category.CategoryTitle,
           CategoryImage = category.CategoryImage
       };
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if(category == null)
            return false;

        // Deleting from Cloudinary Storage
        if (!string.IsNullOrEmpty(category.CategoryImagePublicId))
        {
            await _imageInterface.DeleteImageAsync(category.CategoryImagePublicId);
        }

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        return await _context.Categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            CategoryTitle = c.CategoryTitle,
            CategoryImage = c.CategoryImage
        })
        .ToListAsync();
    }

    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id)
    {
        return await _context.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                CategoryTitle = c.CategoryTitle,
                CategoryImage = c.CategoryTitle
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);

        if(category == null)
            throw new Exception("Category not found");

        if(!string.IsNullOrWhiteSpace(dto.CategoryTitle))
            category.CategoryTitle = dto.CategoryTitle;
    
        if(dto.CategoryImage != null)
        {
            if (!string.IsNullOrWhiteSpace(category.CategoryImagePublicId))
                await _imageInterface.DeleteImageAsync(category.CategoryImagePublicId);

            var uploadResult = await _imageInterface.UploadImageAsync(dto.CategoryImage);
            category.CategoryImage = uploadResult.Url;
            category.CategoryImagePublicId = uploadResult.PublicId;
        }
               
        await _context.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            CategoryTitle = category.CategoryTitle,
            CategoryImage = category.CategoryImage
        };
    }
}
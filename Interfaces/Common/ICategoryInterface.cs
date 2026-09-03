using gamevault_backend.DTOs.Category;

namespace gamevault_backend.Services.Category;

public interface ICategoryInterface
{
    Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);
    Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
    Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id);
    Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);
    Task<bool> DeleteCategoryAsync(Guid id);   
}
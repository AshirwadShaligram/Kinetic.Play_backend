using gamevault_backend.DTOs.Admin.SubCategory;

namespace gamevault_backend.Services.Admin.SubCategory;

public interface ISubCategoryInterface
{
    Task<List<SubCategoryResponseDto>> GetAllSubCategoryAsync();
    Task<SubCategoryResponseDto> CreateSubCategoryAsync(CreateSubCategory dto);
    Task<SubCategoryResponseDto?> GetSubCategoryById(Guid id);
    Task<SubCategoryResponseDto> UpdateSubCategoryAsync(Guid id, UpdateSubCategory dto);
    Task<bool> DeleteSubCategory(Guid id);
    Task<bool> ToggleSubCategoryVisibilityAsync(Guid id);
}
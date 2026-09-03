using gamevault_backend.DTOs.Image;

namespace gamevault_backend.Services.Image;

public interface IImageInterface
{
    Task<ImageUploadResultDto> UploadImageAsync(IFormFile file);
    Task<List<ImageUploadResultDto>> UploadImagesAsync(List<IFormFile> files);
    Task<bool> DeleteImageAsync(string id); 
}
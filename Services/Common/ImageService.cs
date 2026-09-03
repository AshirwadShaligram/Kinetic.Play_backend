using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using gamevault_backend.DTOs.Image;
using gamevault_backend.Models;
using Microsoft.Extensions.Options;

namespace gamevault_backend.Services.Image;

public class ImageService : IImageInterface
{
    private readonly Cloudinary _cloudinary;

    public ImageService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<bool> DeleteImageAsync(string id)
    {
        var deleteParams = new DeletionParams(id);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result.Result == "ok";   
    }

    public async Task<ImageUploadResultDto> UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "Pachama"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        return new ImageUploadResultDto
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId
        };
    }

    public async Task<List<ImageUploadResultDto>> UploadImagesAsync(List<IFormFile> files)
    {
        var results = new List<ImageUploadResultDto>();

        foreach(var file in files)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "Pachama"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            results.Add(new ImageUploadResultDto
            {
                Url = result.SecureUrl.ToString(),
                PublicId = result.PublicId
            });
        }

        return results;
    }
}
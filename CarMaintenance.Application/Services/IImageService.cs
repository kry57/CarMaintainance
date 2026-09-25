using Microsoft.AspNetCore.Http;

namespace CarMaintenance.Application.Services
{
    public interface IImageService
    {
        Task<(string ImageUrl, string PublicId)> UploadImage(IFormFile image);

        Task<bool> DeleteImage(string publicId);
    }
}

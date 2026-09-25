using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace CarMaintenance.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinarySettings> settings)
        {
            var account = new Account(
                settings.Value.CloudName,
                settings.Value.ApiKey,
                settings.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<bool> DeleteImage(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
        public async Task<(string ImageUrl, string PublicId)> UploadImage(IFormFile image)
        {
            {
                if (image is null || image.Length == 0)
                    throw new ArgumentException("Image is required.");

                await using var stream = image.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName,stream),Folder = "CarMaintenance/Products"
                };

                var result = await _cloudinary.UploadAsync(uploadParams);

                if (result.Error is not null)
                    throw new Exception(result.Error.Message);

                return (result.SecureUrl.ToString(),result.PublicId);
            }
        }
    }
}

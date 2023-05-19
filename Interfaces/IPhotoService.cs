using CloudinaryDotNet.Actions;

namespace FoodOrderingApi.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DelePhotoAsync(string publicId);
    }
}

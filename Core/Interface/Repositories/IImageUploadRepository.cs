using Microsoft.AspNetCore.Http;

namespace Core.Interface.Repositories
{
    public interface IImageUploadRepository
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}

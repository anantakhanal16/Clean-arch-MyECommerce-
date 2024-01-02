
using Core.Interface.Repositories;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Data
{
    public class ImageUploadRepository : IImageUploadRepository
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

            string filePath = Path.Combine("wwwroot/uploads", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
    }
}

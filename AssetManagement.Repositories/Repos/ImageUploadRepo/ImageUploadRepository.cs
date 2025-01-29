using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Repositories.IRepos.IImageUpload;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Repositories.Repos.ImageUploadRepo
{
    public class ImageUploadRepository : IImageUploadRepository
    {
        private readonly IWebHostEnvironment _env;
        public ImageUploadRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void ImageDelete(string imageUrl)
        {
            // Get the root path of wwwroot
            var rootPath = _env.WebRootPath;
            // Delete old NID picture if it exists
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var filePath = Path.Combine(rootPath, imageUrl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        public async Task<string> ImageUpload(IFormFile file)
        {
            // Get the root path of wwwroot
            var rootPath = _env.WebRootPath;

            // Generate unique names for the files
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Combine root path with file names to create file paths
            var filePath = Path.Combine(rootPath, "images", fileName);

            // Ensure the "images" folder exists in wwwroot
            var imagesFolder = Path.Combine(rootPath, "images");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            // Save the profile picture
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create URLs for the saved files
            var fileUrl = $"/images/{fileName}";

            return fileUrl;
        }
    }
}
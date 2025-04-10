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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ImageUploadRepository(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public void ImageDelete(string imageUrl)
        {
            // Get the root path of wwwroot
            var rootPath = _env.WebRootPath;
            var uri = new Uri(imageUrl);
            var relativePath = uri.AbsolutePath.TrimStart('/');
            // Delete old NID picture if it exists
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var filePath = Path.Combine(rootPath, relativePath);
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
            // var fileUrl = $"/images/{fileName}";
            var fileUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}/images/{fileName}";
            return fileUrl;
        }
    }
}
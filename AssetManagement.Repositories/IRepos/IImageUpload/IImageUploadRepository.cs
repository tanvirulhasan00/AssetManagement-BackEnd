using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Repositories.IRepos.IImageUpload
{
    public interface IImageUploadRepository
    {
        Task<string> ImageUpload(IFormFile file);
        void ImageDelete(string imageUrl);
    }
}
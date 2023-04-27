using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Cloud
{
    public interface ICloudService
    {
        Task<string> UploadAsync(IFormFile file);
        Task DeleteAsync(string publicId);
    }
}

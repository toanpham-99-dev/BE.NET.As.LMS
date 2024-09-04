using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Interfaces
{
    public interface IFileStorageServices
    {
        string GetFileUrl(string fileName);
        Task<string> SaveFileAsync(IFormFile file);
        Task DeleteFileAsync(string fileName);
    }
}

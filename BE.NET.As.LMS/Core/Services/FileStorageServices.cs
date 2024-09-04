using BE.NET.As.LMS.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BE.NET.As.LMS.Core.Services
{
    public class FileStorageServices : IFileStorageServices
    {
        private readonly string _fileFolder;
        private readonly string FolderName = "file";
        public FileStorageServices(IWebHostEnvironment webHostEnvironment)
        {
            _fileFolder = Path.Combine(webHostEnvironment.WebRootPath, FolderName);
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_fileFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{FolderName}/{fileName}";
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await SaveFile(file.OpenReadStream(), fileName);
            return fileName;
        }

        private async Task SaveFile(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_fileFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }   
    }
}

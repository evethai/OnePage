using System.IO;
using System.Threading.Tasks;
using OnePage.Domain.Interfaces;

namespace OnePage.Infrastructure.Services
{
    public class LocalStorageService : IStorageService
    {
        private readonly string _uploadFolder;

        public LocalStorageService(string webRootPath)
        {
            // Ensure directory for storing uploads exists
            _uploadFolder = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var filePath = Path.Combine(_uploadFolder, fileName);
            using var outputStream = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(outputStream);
            
            // Return relative path URL for direct client access
            return $"/uploads/{fileName}";
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl)) return Task.CompletedTask;
            
            var fileName = Path.GetFileName(fileUrl);
            var filePath = Path.Combine(_uploadFolder, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }
    }
}

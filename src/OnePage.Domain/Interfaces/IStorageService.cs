using System.IO;
using System.Threading.Tasks;

namespace OnePage.Domain.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
        Task DeleteFileAsync(string fileUrl);
    }
}

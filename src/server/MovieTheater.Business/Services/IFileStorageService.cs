using Microsoft.AspNetCore.Http;

namespace MovieTheater.Business.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file);
    Task DeleteFileAsync(string filePath);
}

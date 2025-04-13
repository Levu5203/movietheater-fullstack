using Microsoft.AspNetCore.Http;

namespace MovieTheater.Business.Services;

public interface IAzureService
{
    Task<string> UploadFileAsync(IFormFile file, string fileName);
}

using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MovieTheater.Business.Services;

public class AzureService : IAzureService
{
    private readonly IConfiguration _configuration;
    public AzureService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string fileName)
    {
        var connectionString = _configuration["AzureBlobStorage:ConnectionString"];
        var containerName = _configuration["AzureBlobStorage:ContainerName"];

        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);
        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true);

        return blobClient.Uri.ToString();
    }
}

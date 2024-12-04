using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class BlobService
{
    private readonly string _connectionString;
    private readonly string _containerName;

    public BlobService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];
    }

    // Méthode pour télécharger un fichier dans le Blob Storage
    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var blobClient = new BlobServiceClient(_connectionString);
        var containerClient = blobClient.GetBlobContainerClient(_containerName);
        var blobClientFile = containerClient.GetBlobClient(fileName);

        // Upload du fichier dans le container
        await blobClientFile.UploadAsync(fileStream, overwrite: true);

        // Retourne l'URL du fichier
        return blobClientFile.Uri.ToString();
    }

    // Méthode pour récupérer un fichier
    public async Task<Stream> DownloadFileAsync(string fileName)
    {
        var blobClient = new BlobServiceClient(_connectionString);
        var containerClient = blobClient.GetBlobContainerClient(_containerName);
        var blobClientFile = containerClient.GetBlobClient(fileName);

        var blobDownload = await blobClientFile.OpenReadAsync();
        return blobDownload.Value.Content;
    }
}

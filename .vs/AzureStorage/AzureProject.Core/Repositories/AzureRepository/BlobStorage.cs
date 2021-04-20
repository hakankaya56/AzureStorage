using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using AzureProject.Entities.Enums;

namespace AzureProject.Core.Repositories.AzureRepository
{
    public class BlobStorage : IBlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = new BlobServiceClient(ConnectionStrings.AzureConnectionString);
        }


        public string BlobUrl { get; set; } = "https://testdevelopment56.blob.core.windows.net/";

        public async Task UploadAsync(Stream fileStream, string blobClientName, EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString());
            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            var blobClient = containerClient.GetBlobClient(blobClientName);
            await blobClient.UploadAsync(fileStream);
        }

        public async Task<Stream> DownloadAsync(string blobClientName, EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString());
            var blobClient = containerClient.GetBlobClient(blobClientName);
            var downloadedFile = await blobClient.DownloadAsync();
            return downloadedFile.Value.Content;
        }

        public async Task DeleteAsync(string blobClientName, EContainerName eContainerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString());
            var blobClient = containerClient.GetBlobClient(blobClientName);
            await blobClient.DeleteAsync();
        }

        public async Task SetLogAsync(string text, string blobClientName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(EContainerName.logs.ToString());
            var appendBlobClient = containerClient.GetAppendBlobClient(blobClientName);
            await appendBlobClient.CreateIfNotExistsAsync();
            await using var memoryStream = new MemoryStream();
            await using var streamWriter = new StreamWriter(memoryStream);
            await streamWriter.WriteAsync($"{DateTime.Now}: {text}/n");
            await streamWriter.FlushAsync();
            memoryStream.Position = 0;
            await appendBlobClient.AppendBlockAsync(memoryStream);

        }

        public async Task<List<string>> GetLogAsync(string blobClientName)
        {
            var logs = new List<string>();
            var containerClient = _blobServiceClient.GetBlobContainerClient(EContainerName.logs.ToString());
            var appendBobClient = containerClient.GetAppendBlobClient(blobClientName);
            await appendBobClient.CreateIfNotExistsAsync();
            var downloadedFile = await appendBobClient.DownloadAsync();

            using var streamReader = new StreamReader(downloadedFile.Value.Content);
            string line = string.Empty;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                logs.Add(line);
            }
            return logs;
        }

        public List<string> GetNames(EContainerName eContainerName)
        {
            var blobNames = new List<string>();
            var containerClient = _blobServiceClient.GetBlobContainerClient(eContainerName.ToString());
            var blobs = containerClient.GetBlobs();
            blobs.ToList().ForEach(b=> blobNames.Add(b.Name));
            return blobNames;
        }
    }
}
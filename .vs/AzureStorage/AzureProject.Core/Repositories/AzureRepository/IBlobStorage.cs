using System.Collections.Generic;
using AzureProject.Entities.Enums;
using System.IO;
using System.Threading.Tasks;

namespace AzureProject.Core.Repositories.AzureRepository
{
    public interface IBlobStorage
    {
        string BlobUrl { get; set; }
        Task UploadAsync(Stream fileStream, string blobClientName, EContainerName eContainerName);
        Task<Stream> DownloadAsync(string blobClientName, EContainerName eContainerName);
        Task DeleteAsync(string blobClientName, EContainerName eContainerName);
        Task SetLogAsync(string text, string blobClientName);
        Task<List<string>> GetLogAsync(string blobClientName);
        List<string> GetNames(EContainerName eConatinerName);

    }
}
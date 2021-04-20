using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzureProject.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace AzureProject.Business.Abstract.Services
{
    public interface IPictureService
    {
        List<FileModel> GetPictureList();
        void UploadImage(IFormFile picture);
        void DeletePicture(string fileName);
        Task<Stream> DownloadPicture(string fileName);
    }
}
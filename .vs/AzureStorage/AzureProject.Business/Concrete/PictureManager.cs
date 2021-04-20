using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureProject.Business.Abstract.Services;
using AzureProject.Core.Repositories.AzureRepository;
using AzureProject.Entities.Enums;
using AzureProject.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace AzureProject.Business.Concrete
{
    public class PictureManager:IPictureService
    {
        private readonly IBlobStorage _blobStorage;
        public PictureManager(IBlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }
        public List<FileModel> GetPictureList()
        {
            var names = _blobStorage.GetNames(EContainerName.pictures);
            string blobUrl = $"{_blobStorage.BlobUrl}/{EContainerName.pictures.ToString()}";
            var pictureList = names.Select(x => new FileModel {Name = x, Url = $"{blobUrl}/{x}"}).ToList();
            return pictureList;
        }

        public async void UploadImage(IFormFile picture)
        {
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
            await _blobStorage.UploadAsync(picture.OpenReadStream(), newFileName, EContainerName.pictures);
        }

        public async void DeletePicture(string fileName)
        {
            await _blobStorage.DeleteAsync(fileName, EContainerName.pictures);
        }

        public async Task<Stream>  DownloadPicture(string fileName)
        {
            var stream = await _blobStorage.DownloadAsync(fileName, EContainerName.pictures);
            return stream;
        }
    }
}
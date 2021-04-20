using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureProject.Business.Abstract.Services;
using Microsoft.AspNetCore.Http;

namespace NetCoreWebUI.Controllers
{
    public class PicturesController : Controller
    {
        private readonly IPictureService _pictureService;
        public PicturesController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IActionResult Index()
        {
            var pictureList = _pictureService.GetPictureList();
            return View(pictureList);
        }

        public IActionResult Upload(IFormFile picture)
        {
             _pictureService.UploadImage(picture);
             return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var stream =await _pictureService.DownloadPicture(fileName);
            return File(stream, "application/octet-stream", fileName);
        }

        public IActionResult Delete(string fileName)
        {
            _pictureService.DeletePicture(fileName);
            return RedirectToAction("Index");
        }
    }
}

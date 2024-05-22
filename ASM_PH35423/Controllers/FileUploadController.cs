using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
namespace ASM_PH35423.Controllers
{
    [ApiController]
    [Route("/FileUpLoad")]
    public class FileUploadController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<string?> Upload([FromForm] IList<IFormFile> files)
        {
            if (files != null )
            {
                var uploadPath = Path.Combine(_environment.WebRootPath, "Uploads");

                // Đảm bảo thư mục uploads tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in files)
                {
                    var idImg = Guid.NewGuid().ToString();
                    
                    var filePath = Path.Combine(uploadPath, idImg + file.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    string[] pathCut = filePath.Split(new string[] { "wwwroot" }, StringSplitOptions.None);
                    
                    return pathCut[1];
                }

            }
            return null;
        }
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;

namespace UploadFileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDownloadController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> DownloadFile(string NameFile)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"Upload\\files", NameFile);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath,out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}

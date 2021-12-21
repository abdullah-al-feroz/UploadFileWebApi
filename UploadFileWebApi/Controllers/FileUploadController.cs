using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFileWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            int flag = 0;

            var names = new List<string>();

            foreach (var formFile in files)
            {
                string fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(files[flag].FileName));

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }
                var path = Path.Combine(pathBuilt, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                flag++;
                names.Add(fileName);
            }
            return Ok(new { count = files.Count, size, names });
        }
    }
}

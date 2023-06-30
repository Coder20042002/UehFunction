using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Text;
using System.Net;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly UehDbContext context;

        public FileController(IWebHostEnvironment env, UehDbContext context)
        {
            this.env = env;
            this.context = context;
        }

        // GET api/files?mssv={mssv}
        [HttpGet]
        public async Task<IActionResult> GetFiles(string mssv)
        {
            List<UploadResult> files = await context.UploadResults.Where(u => u.Mssv == mssv).ToListAsync(); ;
            return Ok(files);
        }

        [HttpGet("{fileName}/{mssv}")]
        public async Task<IActionResult> DownloadFile(string fileName, string mssv)
        {
            var uploadResult = await context.UploadResults.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName) && u.Mssv == mssv && u.FileType == "baocao");
            var path = Path.Combine(env.ContentRootPath, "uploads", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, uploadResult.ContentType, Path.GetFileName(path));
        }

        [HttpPost("{mssv}")]
        public async Task<ActionResult<List<UploadResult>>> PostFile(
             [FromForm] IEnumerable<IFormFile> files, string mssv)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();
            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrusteFilename = file.FileName;
                uploadResult.FileName = untrusteFilename;
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrusteFilename);

                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(env.ContentRootPath, "uploads", trustedFileNameForFileStorage);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);
                uploadResult.Id = Guid.NewGuid();
                uploadResult.FileType = "baocao";
                uploadResult.ContentType = file.ContentType;
                uploadResult.Mssv = mssv;
                uploadResult.StoredFileName = trustedFileNameForFileStorage;
                uploadResults.Add(uploadResult);

                context.Add(uploadResult);
            }
            await context.SaveChangesAsync();
            return Ok(uploadResults);
        }
    }
}


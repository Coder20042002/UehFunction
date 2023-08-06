using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Text;
using System.Net;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

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
        public async Task<IActionResult> GetFiles(string madot, string mssv)
        {
            List<UploadResult> files = await context.UploadResults.Where(u => u.Mssv == mssv && u.Madot == madot && u.Status == "true").ToListAsync(); ;
            return Ok(files);
        }

        [HttpGet("GetDsFileGvhd")]
        public async Task<IActionResult> GetDsFileGvhd(string madot, string magv)
        {
            var phancong = await context.Phancongs
                .Where(p => p.madot == madot && p.magv == magv && p.status == "true")
                .ToListAsync();

            var mssvList = phancong.Select(p => p.mssv).ToList();

            List<UploadResult> files = await context.UploadResults
                .Where(u => mssvList.Contains(u.Mssv) && u.Madot == madot && u.Status == "true")
                .ToListAsync();

            return Ok(files);
        }


        [HttpPut("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {

            var uploadResult = await context.UploadResults.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName));
            if (uploadResult != null)
            {
                uploadResult.Status = "false";
                context.Update(uploadResult);
                await context.SaveChangesAsync();
            }

            return Ok();
        }


        [HttpGet("{fileName}/{mssv}")]
        public async Task<IActionResult> DownloadFile(string fileName, string mssv)
        {
            var uploadResult = await context.UploadResults.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName) && u.Mssv == mssv);
            var path = Path.Combine(env.ContentRootPath, "uploads", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, uploadResult.ContentType, Path.GetFileName(path));
        }






        [HttpPost("PostFile")]
        public async Task<ActionResult> PostFileXacNhan(List<UploadResultDto> files, string madot, string mssv, string loai)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();
            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                uploadResult.Id = Guid.NewGuid();
                uploadResult.FileName = file.FileName;
                uploadResult.FileType = loai;
                uploadResult.ContentType = file.ContentType;
                uploadResult.Mssv = mssv;
                uploadResult.StoredFileName = file.StoredFileName;
                uploadResult.Madot = madot;
                uploadResults.Add(uploadResult);

                context.Add(uploadResult);
            }
            await context.SaveChangesAsync();
            return Ok(uploadResults);
        }
    }
}


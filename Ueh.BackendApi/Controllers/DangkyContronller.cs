using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangkyContronller : ControllerBase
    {
        private readonly IDangkyRepository _DangkyRepository;
        private readonly IMapper _mapper;

        public DangkyContronller(IDangkyRepository DangkyRepository, IMapper mapper)
        {
            _DangkyRepository = DangkyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dangky>))]
        public async Task<IActionResult> GetDangkys()
        {
            var Dangkys = _mapper.Map<List<DangkyDto>>(await _DangkyRepository.GetDangkys());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Dangkys);
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(Dangky))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDangky(string mssv)
        {
            if (!await _DangkyRepository.DangkyExists(mssv))
                return NotFound();

            var Dangky = _mapper.Map<DangkyDto>(await _DangkyRepository.GetDangky(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Dangky);
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile)
        {
            try
            {
                bool success = await _DangkyRepository.ImportExcelFile(formFile);
                if (success)
                {
                    return Ok("Import successful"); // Trả về thông báo thành công
                }
                else
                {
                    return BadRequest("Import failed"); // Trả về thông báo lỗi
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }

            // Trường hợp không xử lý được, trả về BadRequest
            return BadRequest("Unknown error occurred");
        }

        [HttpGet("generate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var content = await _DangkyRepository.ExportToExcel();
                if (content != null)
                {
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSdangky.xlsx");
                }
                else
                {
                    return BadRequest("No data available to export.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Something went wrong: {ex.Message}");
            }
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDangky([FromBody] DangkyDto DangkyCreate)
        {
            if (DangkyCreate == null)
                return BadRequest(ModelState);

            bool Dangkys = await _DangkyRepository.DangkyExists(DangkyCreate.mssv);

            if (Dangkys == true)
            {
                ModelState.AddModelError("", " Sinh vien đã được đăng ký");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var DangkyMap = _mapper.Map<Dangky>(DangkyCreate);


            if (!await _DangkyRepository.CreateDangky(DangkyMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(DangkyMap);
        }

        [HttpPut("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDangky(string mssv,
            [FromBody] DangkyDto updatedDangky)
        {
            if (updatedDangky == null)
                return BadRequest(ModelState);

            if (mssv != updatedDangky.mssv)
                return BadRequest(ModelState);

            if (!await _DangkyRepository.DangkyExists(mssv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var DangkyMap = _mapper.Map<Dangky>(updatedDangky);

            if (!await _DangkyRepository.UpdateDangky(DangkyMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDangky(string mssv)
        {
            if (!await _DangkyRepository.DangkyExists(mssv))
            {
                return NotFound();
            }
            var DangkyToDelete = await _DangkyRepository.GetDangky(mssv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _DangkyRepository.DeleteDangky(DangkyToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa");
            }

            return NoContent();
        }
    }
}

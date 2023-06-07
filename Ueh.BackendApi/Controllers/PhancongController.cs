using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongController : ControllerBase
    {
        private readonly IPhanCongRepository _PhanCongRepository;
        private readonly IMapper _mapper;

        public PhanCongController(IPhanCongRepository PhanCongRepository, IMapper mapper)
        {
            _PhanCongRepository = PhanCongRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PhanCong>))]
        public async Task<IActionResult> GetPhanCongs()
        {
            var PhanCongs = _mapper.Map<List<PhanCongDto>>(await _PhanCongRepository.GetPhanCongs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(PhanCongs);
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(PhanCong))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPhanCong(string mssv)
        {
            if (!await _PhanCongRepository.PhanCongExists(mssv))
                return NotFound();

            var PhanCong = _mapper.Map<PhanCongDto>(await _PhanCongRepository.GetPhanCong(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(PhanCong);
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile)
        {
            try
            {
                bool success = await _PhanCongRepository.ImportExcelFile(formFile);
                if (success)
                {
                    return Ok("Import thành công"); // Trả về thông báo thành công
                }
                else
                {
                    return BadRequest("Import thất bại"); // Trả về thông báo lỗi
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

            // Trường hợp không xử lý được, trả về BadRequest
            return BadRequest("Xảy ra lỗi không xác định được");
        }

        [HttpGet("generate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var content = await _PhanCongRepository.ExportToExcel();
                if (content != null)
                {
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSPhanCongChinhThuc.xlsx");
                }
                else
                {
                    return BadRequest("Không có dữ liệu để xuất.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePhanCong([FromBody] PhanCongDto PhanCongCreate)
        {
            if (PhanCongCreate == null)
                return BadRequest(ModelState);

            bool PhanCongs = await _PhanCongRepository.PhanCongExists(PhanCongCreate.mssv);

            if (PhanCongs == true)
            {
                ModelState.AddModelError("", " Sinh vien đã được đăng ký");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PhanCongMap = _mapper.Map<PhanCong>(PhanCongCreate);


            if (!await _PhanCongRepository.CreatePhanCong(PhanCongMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(PhanCongMap);
        }

        [HttpPut("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePhanCong(string mssv,
            [FromBody] PhanCongDto updatedPhanCong)
        {
            if (updatedPhanCong == null)
                return BadRequest(ModelState);

            if (mssv != updatedPhanCong.mssv)
                return BadRequest(ModelState);

            if (!await _PhanCongRepository.PhanCongExists(mssv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var PhanCongMap = _mapper.Map<PhanCong>(updatedPhanCong);

            if (!await _PhanCongRepository.UpdatePhanCong(PhanCongMap))
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
        public async Task<IActionResult> DeletePhanCong(string mssv)
        {
            if (!await _PhanCongRepository.PhanCongExists(mssv))
            {
                return NotFound();
            }
            var PhanCongToDelete = await _PhanCongRepository.GetPhanCong(mssv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _PhanCongRepository.DeletePhanCong(PhanCongToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa");
            }

            return NoContent();
        }
    }
}

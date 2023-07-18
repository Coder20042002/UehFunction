using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangkyController : ControllerBase
    {
        private readonly IDangkyRepository _DangkyRepository;
        private readonly IMapper _mapper;

        public DangkyController(IDangkyRepository DangkyRepository, IMapper mapper)
        {
            _DangkyRepository = DangkyRepository;
            _mapper = mapper;
        }


        [HttpGet("GetGiangvienListFromDangky")]
        public async Task<ActionResult<List<GiangvienRequest>>> GetGiangvienListFromDangky(string madot, string makhoa)
        {
            try
            {
                var giangVienList = await _DangkyRepository.GetGiangvienListFromDangky(madot, makhoa);
                return Ok(giangVienList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }
        }

        [HttpGet("GetSinhVienByGiaoVien")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dangky>))]
        public async Task<IActionResult> GetSinhVienByGiaoVien([FromQuery] string madot, [FromQuery] string makhoa, [FromQuery] string magv)
        {
            var Dangkys = _mapper.Map<List<DangkyDto>>(await _DangkyRepository.GetSinhVienByGiaoVien(madot, makhoa, magv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Dangkys);
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

        [HttpGet("GetDangky")]
        [ProducesResponseType(200, Type = typeof(Dangky))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDangky(string madot, string mssv)
        {
            if (!await _DangkyRepository.DangkyExists(mssv))
                return NotFound();

            var Dangky = _mapper.Map<DangkyDto>(await _DangkyRepository.GetDangky(madot, mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Dangky);
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile, [FromQuery] string madot, [FromQuery] string makhoa, [FromQuery] string magv)
        {
            try
            {
                bool success = await _DangkyRepository.ImportExcelFile(formFile, madot, makhoa, magv);
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
        public async Task<IActionResult> ExportToExcel(string madot, string makhoa)
        {
            try
            {
                var content = await _DangkyRepository.ExportToExcel(madot, makhoa);
                if (content != null)
                {
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSdangky.xlsx");
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


        [HttpPost("createdangky")]
        public async Task<IActionResult> CreateDangky(DangkyDto DangkyCreate)
        {
            if (DangkyCreate == null)
                return BadRequest(ModelState);

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

        [HttpPut("UpdateDangky")]
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

        [HttpDelete("DeleteDangky")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDangky(string madot, string mssv)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!await _DangkyRepository.DeleteDangky(madot, mssv))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa");
            }

            return NoContent();
        }
    }
}

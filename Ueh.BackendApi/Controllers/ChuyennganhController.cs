using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuyennganhController : ControllerBase
    {
        private readonly IChuyennganhRepository _chuyennganhRepository;
        private readonly IMapper _mapper;

        public ChuyennganhController(IChuyennganhRepository chuyennganhRepository, IMapper mapper)
        {
            _chuyennganhRepository = chuyennganhRepository;
            _mapper = mapper;
        }

        [HttpGet("GetChuyennganhsByKhoa/{makhoa}")]
        public async Task<IActionResult> GetChuyennganhsByKhoa(string makhoa)
        {
            var Chuyennganhs = _mapper.Map<List<ChuyennganhDto>>(await _chuyennganhRepository.GetChuyennganhsByKhoa(makhoa));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chuyennganhs);
        }

        [HttpGet("{macn}")]
        public async Task<IActionResult> GetChuyennganhById(string macn)
        {
            if (!await _chuyennganhRepository.ChuyennganhExists(macn))
                return NotFound();

            var Chuyennganh = _mapper.Map<ChuyennganhDto>(await _chuyennganhRepository.GetChuyennganhById(macn));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chuyennganh);
        }

        [HttpPost("formFile/{makhoa}")]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile, string makhoa)
        {
            try
            {
                bool success = await _chuyennganhRepository.ImportExcelFile(formFile, makhoa);
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
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var content = await _chuyennganhRepository.ExportToExcel();
                if (content != null)
                {
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSChuyennganh.xlsx");
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
        public async Task<IActionResult> CreateChuyennganh([FromBody] ChuyennganhDto ChuyennganhCreate)
        {
            if (ChuyennganhCreate == null)
                return BadRequest(ModelState);

            bool Chuyennganhs = await _chuyennganhRepository.ChuyennganhExists(ChuyennganhCreate.macn);

            if (Chuyennganhs == true)
            {
                ModelState.AddModelError("", " Sinh vien đã được đăng ký");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ChuyennganhMap = _mapper.Map<Chuyennganh>(ChuyennganhCreate);


            if (!await _chuyennganhRepository.CreateChuyennganh(ChuyennganhMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(ChuyennganhMap);
        }

        [HttpPut("{macn}")]
        public async Task<IActionResult> UpdateChuyennganh(string macn,
            [FromBody] ChuyennganhDto updatedChuyennganh)
        {
            if (updatedChuyennganh == null)
                return BadRequest(ModelState);

            if (macn != updatedChuyennganh.macn)
                return BadRequest(ModelState);

            if (!await _chuyennganhRepository.ChuyennganhExists(macn))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ChuyennganhMap = _mapper.Map<Chuyennganh>(updatedChuyennganh);

            if (!await _chuyennganhRepository.UpdateChuyennganh(ChuyennganhMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{macn}")]
        public async Task<IActionResult> DeleteChuyennganh(string macn)
        {
            if (!await _chuyennganhRepository.ChuyennganhExists(macn))
            {
                return NotFound();
            }
            var ChuyennganhToDelete = await _chuyennganhRepository.GetChuyennganhById(macn);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _chuyennganhRepository.DeleteChuyennganh(ChuyennganhToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa");
            }

            return NoContent();
        }
    }
}


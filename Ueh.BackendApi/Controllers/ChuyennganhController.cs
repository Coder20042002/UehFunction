using AutoMapper;
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
        private readonly IChuyennganhRepository _ChuyennganhRepository;
        private readonly IMapper _mapper;

        public ChuyennganhController(IChuyennganhRepository ChuyennganhRepository, IMapper mapper)
        {
            _ChuyennganhRepository = ChuyennganhRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chuyennganh>))]
        public async Task<IActionResult> GetChuyennganhs()
        {
            var Chuyennganhs = _mapper.Map<List<ChuyennganhDto>>(await _ChuyennganhRepository.GetChuyennganhs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chuyennganhs);
        }

        [HttpGet("{macn}")]
        [ProducesResponseType(200, Type = typeof(Chuyennganh))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetChuyennganh(string macn)
        {
            if (!await _ChuyennganhRepository.ChuyennganhExists(macn))
                return NotFound();

            var Chuyennganh = _mapper.Map<ChuyennganhDto>(await _ChuyennganhRepository.GetChuyennganh(macn));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chuyennganh);
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile)
        {
            try
            {
                bool success = await _ChuyennganhRepository.ImportExcelFile(formFile);
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
                var content = await _ChuyennganhRepository.ExportToExcel();
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
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateChuyennganh([FromBody] ChuyennganhDto ChuyennganhCreate)
        {
            if (ChuyennganhCreate == null)
                return BadRequest(ModelState);

            bool Chuyennganhs = await _ChuyennganhRepository.ChuyennganhExists(ChuyennganhCreate.macn);

            if (Chuyennganhs == true)
            {
                ModelState.AddModelError("", " Sinh vien đã được đăng ký");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ChuyennganhMap = _mapper.Map<Chuyennganh>(ChuyennganhCreate);


            if (!await _ChuyennganhRepository.CreateChuyennganh(ChuyennganhMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(ChuyennganhMap);
        }

        [HttpPut("{macn}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateChuyennganh(string macn,
            [FromBody] ChuyennganhDto updatedChuyennganh)
        {
            if (updatedChuyennganh == null)
                return BadRequest(ModelState);

            if (macn != updatedChuyennganh.macn)
                return BadRequest(ModelState);

            if (!await _ChuyennganhRepository.ChuyennganhExists(macn))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ChuyennganhMap = _mapper.Map<Chuyennganh>(updatedChuyennganh);

            if (!await _ChuyennganhRepository.UpdateChuyennganh(ChuyennganhMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{macn}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteChuyennganh(string macn)
        {
            if (!await _ChuyennganhRepository.ChuyennganhExists(macn))
            {
                return NotFound();
            }
            var ChuyennganhToDelete = await _ChuyennganhRepository.GetChuyennganh(macn);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _ChuyennganhRepository.DeleteChuyennganh(ChuyennganhToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa");
            }

            return NoContent();
        }
    }
}


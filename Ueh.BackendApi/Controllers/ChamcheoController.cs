using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamcheoController : ControllerBase
    {
        private readonly IChamcheoRepository _chamcheoRepository;
        private readonly IMapper _mapper;

        public ChamcheoController(IChamcheoRepository chamcheoRepository, IMapper mapper)
        {
            _chamcheoRepository = chamcheoRepository;
            _mapper = mapper;
        }


        [HttpGet("chamcheo")]
        public async Task<ActionResult<List<ChamcheoRequest>>> GetChamcheoByGiangVien(string makhoa)
        {
            try
            {
                var chamcheoList = await _chamcheoRepository.GetChamcheoByGiangVien(makhoa);
                return Ok(chamcheoList);
            }
            catch (Exception ex)
            {
                // Xử lý khi có lỗi xảy ra
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }


        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile, string madot, string makhoa)
        {
            try
            {

                bool success = await _chamcheoRepository.ImportExcelFile(formFile, madot, makhoa);
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

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateChamcheo(Guid id, [FromBody] ChamcheoDto updatedChamcheo)
        {
            try
            {
                if (updatedChamcheo == null)
                    return BadRequest(ModelState);

                if (id != updatedChamcheo.id)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest();

                var ChamcheoMap = _mapper.Map<Chamcheo>(updatedChamcheo);

                if (!await _chamcheoRepository.UpdateChamcheo(ChamcheoMap))
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

        }

    }


}

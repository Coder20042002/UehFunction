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
    public class GiangvienController : ControllerBase
    {
        private readonly IGiangvienRepository _giangvienRepository;
        private readonly IMapper _mapper;

        public GiangvienController(IGiangvienRepository giangvienRepository, IMapper mapper)
        {
            _giangvienRepository = giangvienRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Giangvien>))]
        public async Task<IActionResult> GetGiangviens()
        {
            var Giangviens = _mapper.Map<List<GiangvienDto>>(await _giangvienRepository.GetGiangviens());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Giangviens);
        }

        [HttpGet("{magv}")]
        [ProducesResponseType(200, Type = typeof(Giangvien))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGiangvien(string magv)
        {
            if (!await _giangvienRepository.GiangvienExists(magv))
                return NotFound();

            var Giangvien = _mapper.Map<GiangvienDto>(await _giangvienRepository.GetGiangvien(magv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Giangvien);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGiangvien([FromQuery] string makhoa, [FromBody] GiangvienDto GiangvienCreate)
        {
            if (GiangvienCreate == null)
                return BadRequest(ModelState);

            bool Giangviens = await _giangvienRepository.GiangvienExists(GiangvienCreate.magv);

            if (Giangviens == true)
            {
                ModelState.AddModelError("", "Giangvien đã tồn tại");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var GiangvienMap = _mapper.Map<Giangvien>(GiangvienCreate);


            if (!await _giangvienRepository.CreateGiangvien(makhoa, GiangvienMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(GiangvienMap);
        }

        [HttpPut("{magv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateGiangvien(string magv,
            [FromBody] GiangvienDto updatedGiangvien)
        {
            if (updatedGiangvien == null)
                return BadRequest(ModelState);

            if (magv != updatedGiangvien.magv)
                return BadRequest(ModelState);

            if (!await _giangvienRepository.GiangvienExists(magv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var GiangvienMap = _mapper.Map<Giangvien>(updatedGiangvien);

            if (!await _giangvienRepository.UpdateGiangvien(GiangvienMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{magv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGiangvien(string magv)
        {
            if (!await _giangvienRepository.GiangvienExists(magv))
            {
                return NotFound();
            }
            var GiangvienToDelete = await _giangvienRepository.GetGiangvien(magv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _giangvienRepository.DeleteGiangvien(GiangvienToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa ");
            }

            return NoContent();
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile([FromQuery] string makhoa, IFormFile formFile)
        {
            try
            {

                bool success = await _giangvienRepository.ImportExcelFile(makhoa, formFile);
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
    }
}

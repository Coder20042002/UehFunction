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
    public class GiangvienController : ControllerBase
    {
        private readonly IGiangvienRepository _giangvienRepository;
        private readonly IMapper _mapper;

        public GiangvienController(IGiangvienRepository giangvienRepository, IMapper mapper)
        {
            _giangvienRepository = giangvienRepository;
            _mapper = mapper;
        }

        [HttpGet("SinhVienByGiangVien")]
        public async Task<ActionResult<List<Sinhvien>>> GetSinhVienByGiangVien(string madot, string magv)
        {
            var sinhviens = await _giangvienRepository.GetSinhVienByGiangVien(madot, magv);
            if (sinhviens == null || sinhviens.Count == 0)
            {
                return NotFound();
            }

            return sinhviens;
        }

        [HttpGet("GetDanhSachDiem")]
        public async Task<ActionResult<List<KetquaRequest>>> GetDanhSachDiem(string madot, string magv)
        {
            var sinhviens = await _giangvienRepository.GetDanhSachDiem(madot, magv);
            if (sinhviens == null || sinhviens.Count == 0)
            {
                return NotFound();
            }

            return sinhviens;
        }



        [HttpGet("GetThongtinGiangvien")]
        [ProducesResponseType(200, Type = typeof(Giangvien))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGiangvien(string magv)
        {
            if (!await _giangvienRepository.GiangvienExists(magv))
                return NotFound();

            var Giangvien = await _giangvienRepository.GetThongtinGiangvien(magv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Giangvien);
        }

        [HttpGet("khoa/{makhoa}/giangvien")]
        [ProducesResponseType(200, Type = typeof(List<Giangvien>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGiangvienByKhoa(string makhoa)
        {
            var giangvienList = _mapper.Map<List<GiangvienDto>>(await _giangvienRepository.GetGiangvienByKhoa(makhoa));

            if (giangvienList == null)
                return NotFound();

            return Ok(giangvienList);
        }

        [HttpGet("sinhvientotal")]
        public async Task<ActionResult<List<GiangvienRequest>>> GetGiangVienAndSinhVienHuongDan(string madot, string makhoa)
        {
            try
            {
                var giangVienList = await _giangvienRepository.GetGiangVienAndSinhVienHuongDan(madot, makhoa);
                return Ok(giangVienList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGiangvien([FromQuery] string makhoa, GiangvienDto GiangvienCreate)
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

        [HttpPut("UpdateThongTin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateGiangvien(GiangvienUpdateRequest updatedGiangvien, string magv)
        {
            if (updatedGiangvien == null)
                return BadRequest(ModelState);

            if (magv != updatedGiangvien.magv)
                return BadRequest(ModelState);

            if (!await _giangvienRepository.GiangvienExists(magv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            if (!await _giangvienRepository.UpdateGiangvien(updatedGiangvien))
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

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return NoContent();
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile, string makhoa)
        {
            try
            {

                bool success = await _giangvienRepository.ImportExcelFile(formFile, makhoa);
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

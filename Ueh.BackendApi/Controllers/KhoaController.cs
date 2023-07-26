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
    public class KhoaController : ControllerBase
    {
        private readonly IKhoaRepository _khoaRepository;
        private readonly IMapper _mapper;

        public KhoaController(IKhoaRepository khoaRepository, IMapper mapper)
        {
            _khoaRepository = khoaRepository;
            _mapper = mapper;
        }
        [HttpGet("GetSinhvienByKhoas")]
        public async Task<IActionResult> GetSinhvienByKhoas(string madot, string makhoa)
        {
            var Sinhviens = _mapper.Map<List<SinhvienDto>>(await _khoaRepository.GetKhoaBySinhviens(madot, makhoa));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Sinhviens);
        }

        [HttpGet("giangviens/{makhoa}")]
        public async Task<IActionResult> GetGiangvienByKhoas(string makhoa)
        {
            var giangviens = _mapper.Map<List<GiangvienDto>>(await _khoaRepository.GetKhoaByGiangviens(makhoa));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(giangviens);
        }

        [HttpGet("GetListKhoa")]
        public async Task<IActionResult> GetListKhoa()
        {
            var khoas = _mapper.Map<List<KhoaDto>>(await _khoaRepository.GetListKhoa());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(khoas);
        }


        [HttpGet("GetKhoaById")]
        public async Task<IActionResult> GetKhoaById(string makhoa)
        {
            if (!await _khoaRepository.KhoaExists(makhoa))
                return NotFound();

            var khoa = _mapper.Map<KhoaDto>(await _khoaRepository.GetKhoaById(makhoa));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(khoa);
        }

        [HttpPost("formFile")]
        public async Task<IActionResult> ImportExcelFile( IFormFile formFile)
        {
            try
            {
                bool success = await _khoaRepository.ImportExcelFile(formFile);
                if (success)
                {
                    return Ok("Import thành công");
                }
                else
                {
                    return BadRequest("Import thất bại");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

            // Trường hợp không xử lý được, trả về BadRequest
            return BadRequest("Xảy ra lỗi không xác định được");
        }



        [HttpPost("CreateKhoa")]
        public async Task<IActionResult> CreateKhoa([FromBody] KhoaDto KhoaCreate)
        {
            if (KhoaCreate == null)
                return BadRequest(ModelState);

            bool Khoas = await _khoaRepository.KhoaExists(KhoaCreate.makhoa);

            if (Khoas == true)
            {
                ModelState.AddModelError("", " Sinh vien đã được đăng ký");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var KhoaMap = _mapper.Map<Khoa>(KhoaCreate);


            if (!await _khoaRepository.CreateKhoa(KhoaMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(KhoaMap);
        }

        [HttpPut("UpdateKhoa")]
        public async Task<IActionResult> UpdateKhoa(string makhoa,KhoaDto updatedKhoa)
        {
            if (updatedKhoa == null)
                return BadRequest(ModelState);


            if (!await _khoaRepository.KhoaExists(makhoa))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var KhoaMap = _mapper.Map<Khoa>(updatedKhoa);

            if (!await _khoaRepository.UpdateKhoa(KhoaMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteKhoa")]
        public async Task<IActionResult> DeleteKhoa(string makhoa)
        {
            if (!await _khoaRepository.KhoaExists(makhoa))
            {
                return NotFound();
            }
            var KhoaToDelete = await _khoaRepository.GetKhoaById(makhoa);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _khoaRepository.DeleteKhoa(KhoaToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa");
            }

            return NoContent();
        }
    }
}

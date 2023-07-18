using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhancongController : ControllerBase
    {
        private readonly IPhancongRepository _PhancongRepository;
        private readonly IMapper _mapper;

        public PhancongController(IPhancongRepository PhancongRepository, IMapper mapper)
        {
            _PhancongRepository = PhancongRepository;
            _mapper = mapper;
        }


        [HttpGet("kiemtra/{mssv}")]
        public async Task<bool> KiemTraMaloai(string mssv)
        {
            bool hasHKDN = await _PhancongRepository.KiemTraMaloai(mssv);

            return hasHKDN;
        }

        [HttpGet("getphancongkhoas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Phancong>))]
        public async Task<IActionResult> GetPhancongKhoas(string madot, string makhoa)
        {
            try
            {
                var Phancongs = _mapper.Map<List<PhancongDto>>(await _PhancongRepository.GetPhancongKhoas(madot, makhoa));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(Phancongs);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(Phancong))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPhancong(string mssv)
        {
            try
            {
                if (!await _PhancongRepository.PhancongExists(mssv))
                    return NotFound();

                var Phancong = _mapper.Map<PhancongDto>(await _PhancongRepository.GetPhancong(mssv));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(Phancong);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile, string madot)
        {
            try
            {

                bool success = await _PhancongRepository.ImportExcelFile(formFile, madot);
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
        public async Task<IActionResult> ExportToExcel(string madot, string makhoa)
        {
            try
            {
                var content = await _PhancongRepository.ExportToExcel(madot, makhoa);
                if (content != null)
                {
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSPhancongChinhThuc.xlsx");
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
        public async Task<IActionResult> CreatePhancong(PhancongRequest PhancongCreate)
        {
            try
            {
                if (PhancongCreate == null)
                    return BadRequest(ModelState);

                bool Phancongs = await _PhancongRepository.PhancongExists(PhancongCreate.mssv);

                if (Phancongs == true)
                {
                    ModelState.AddModelError("", " Sinh vien đã được đăng ký");
                    return StatusCode(422, ModelState);
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await _PhancongRepository.CreatePhancong(PhancongCreate))
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                    return StatusCode(500, ModelState);
                }

                return Ok(PhancongCreate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

        }

        [HttpPut("UpdateLoaiHinhThucTap")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateLoaiHinhThucTap(string mssv, string maloai)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (!await _PhancongRepository.UpdateLoaiHinhThucTap(mssv, maloai))
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

        [HttpPut("UpdatePhancong")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePhancong(string mssv, string magv)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (!await _PhancongRepository.UpdatePhancong(mssv, magv))
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

        [HttpPut("DeletePhancong")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeletePhancong(string mssv)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (!await _PhancongRepository.DeletePhancong(mssv))
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

        [HttpGet("search")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Phancong>))]
        public async Task<ActionResult<List<Phancong>>> SearchByTenSinhVien([FromQuery(Name = "tensinhvien")] string tenSinhVien)
        {
            try
            {
                var phanCongList = await _PhancongRepository.SearchByTenSinhVien(tenSinhVien);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(phanCongList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

        }


        [HttpGet("getphancongbymagv")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PhancongDto>))]
        public async Task<ActionResult<ICollection<PhancongDto>>> GetPhanCongByMaGV(string magv)
        {
            try
            {
                var phanCongList = await _PhancongRepository.GetPhanCongByMaGV(magv);
                var phanCongDtoList = _mapper.Map<List<PhancongDto>>(phanCongList);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(phanCongDtoList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

        }

    }


}

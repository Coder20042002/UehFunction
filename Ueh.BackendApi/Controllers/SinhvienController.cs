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
    public class SinhvienController : ControllerBase
    {
        private readonly ISinhvienRepository _sinhvienRepository;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public SinhvienController(ISinhvienRepository sinhvienRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _sinhvienRepository = sinhvienRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sinhvien>))]
        public async Task<IActionResult> GetSinhviens()
        {
            var Sinhviens = _mapper.Map<List<SinhvienDto>>(await _sinhvienRepository.GetSinhviens());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Sinhviens);
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(Sinhvien))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSinhvien(string mssv)
        {
            if (!await _sinhvienRepository.SinhvienExists(mssv))
                return NotFound();

            var Sinhvien = _mapper.Map<SinhvienDto>(await _sinhvienRepository.GetSinhvien(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Sinhvien);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateSinhvien([FromQuery] string madot, [FromQuery] string makhoa, [FromBody] SinhvienDto SinhvienCreate)
        {
            if (SinhvienCreate == null)
                return BadRequest(ModelState);

            bool Sinhviens = await _sinhvienRepository.SinhvienExists(SinhvienCreate.mssv);

            if (Sinhviens == true)
            {
                ModelState.AddModelError("", "Sinhvien đã tồn tại");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SinhvienMap = _mapper.Map<Sinhvien>(SinhvienCreate);


            if (!await _sinhvienRepository.CreateSinhvien(madot, makhoa, SinhvienMap))
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(SinhvienMap);
        }

        [HttpPut("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateSinhvien(string mssv,
            [FromQuery] string madot, [FromQuery] string makhoa,
            [FromBody] SinhvienDto updatedSinhvien)
        {
            if (updatedSinhvien == null)
                return BadRequest(ModelState);

            if (mssv != updatedSinhvien.mssv)
                return BadRequest(ModelState);

            if (!await _sinhvienRepository.SinhvienExists(mssv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var SinhvienMap = _mapper.Map<Sinhvien>(updatedSinhvien);

            if (!await _sinhvienRepository.UpdateSinhvien(madot, makhoa, SinhvienMap))
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi update ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSinhvien(string mssv)
        {
            if (!await _sinhvienRepository.SinhvienExists(mssv))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfASinhvien(mssv);
            var SinhvienToDelete = await _sinhvienRepository.GetSinhvien(mssv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa bài đánh giá");
            }

            if (!await _sinhvienRepository.DeleteSinhvien(SinhvienToDelete))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa ");
            }

            return NoContent();
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile([FromQuery] string madot, [FromQuery] string makhoa, IFormFile formFile)
        {
            try
            {
                bool success = await _sinhvienRepository.ImportExcelFile(madot, makhoa, formFile);
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


    }
}

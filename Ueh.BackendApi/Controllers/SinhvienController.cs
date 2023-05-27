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
        public IActionResult GetSinhviens()
        {
            var Sinhviens = _mapper.Map<List<SinhvienDto>>(_sinhvienRepository.GetSinhviens());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Sinhviens);
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(Sinhvien))]
        [ProducesResponseType(400)]
        public IActionResult GetSinhvien(string mssv)
        {
            if (!_sinhvienRepository.SinhvienExists(mssv))
                return NotFound();

            var Sinhvien = _mapper.Map<SinhvienDto>(_sinhvienRepository.GetSinhvien(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Sinhvien);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSinhvien([FromQuery] string madot, [FromQuery] string maloai, [FromBody] SinhvienDto SinhvienCreate)
        {
            if (SinhvienCreate == null)
                return BadRequest(ModelState);

            bool Sinhviens = _sinhvienRepository.SinhvienExists(SinhvienCreate.mssv);

            if (Sinhviens == true)
            {
                ModelState.AddModelError("", "Sinhvien already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SinhvienMap = _mapper.Map<Sinhvien>(SinhvienCreate);


            if (!_sinhvienRepository.CreateSinhvien(madot, maloai, SinhvienMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(SinhvienMap);
        }

        [HttpPut("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSinhvien(string mssv,
            [FromQuery] string madot, [FromQuery] string maloai,
            [FromBody] SinhvienDto updatedSinhvien)
        {
            if (updatedSinhvien == null)
                return BadRequest(ModelState);

            if (mssv != updatedSinhvien.mssv)
                return BadRequest(ModelState);

            if (!_sinhvienRepository.SinhvienExists(mssv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var SinhvienMap = _mapper.Map<Sinhvien>(updatedSinhvien);

            if (!_sinhvienRepository.UpdateSinhvien(madot, maloai, SinhvienMap))
            {
                ModelState.AddModelError("", "Something went wrong updating ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSinhvien(string mssv)
        {
            if (!_sinhvienRepository.SinhvienExists(mssv))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfASinhvien(mssv);
            var SinhvienToDelete = _sinhvienRepository.GetSinhvien(mssv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_sinhvienRepository.DeleteSinhvien(SinhvienToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ");
            }

            return NoContent();
        }
    }
}

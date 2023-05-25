using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
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
        public IActionResult GetGiangviens()
        {
            var Giangviens = _mapper.Map<List<GiangvienDto>>(_giangvienRepository.GetGiangviens());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Giangviens);
        }

        [HttpGet("{magv}")]
        [ProducesResponseType(200, Type = typeof(Giangvien))]
        [ProducesResponseType(400)]
        public IActionResult GetGiangvien(string magv)
        {
            if (!_giangvienRepository.GiangvienExists(magv))
                return NotFound();

            var Giangvien = _mapper.Map<GiangvienDto>(_giangvienRepository.GetGiangvien(magv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Giangvien);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGiangvien([FromBody] GiangvienDto GiangvienCreate)
        {
            if (GiangvienCreate == null)
                return BadRequest(ModelState);

            bool Giangviens = _giangvienRepository.GiangvienExists(GiangvienCreate.magv);

            if (Giangviens == true)
            {
                ModelState.AddModelError("", "Giangvien already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var GiangvienMap = _mapper.Map<Giangvien>(GiangvienCreate);


            if (!_giangvienRepository.CreateGiangvien(GiangvienMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(GiangvienMap);
        }

        [HttpPut("{magv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGiangvien(string magv,
            [FromBody] GiangvienDto updatedGiangvien)
        {
            if (updatedGiangvien == null)
                return BadRequest(ModelState);

            if (magv != updatedGiangvien.magv)
                return BadRequest(ModelState);

            if (!_giangvienRepository.GiangvienExists(magv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var GiangvienMap = _mapper.Map<Giangvien>(updatedGiangvien);

            if (!_giangvienRepository.UpdateGiangvien(GiangvienMap))
            {
                ModelState.AddModelError("", "Something went wrong updating ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{magv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGiangvien(string magv)
        {
            if (!_giangvienRepository.GiangvienExists(magv))
            {
                return NotFound();
            }
            var GiangvienToDelete = _giangvienRepository.GetGiangvien(magv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!_giangvienRepository.DeleteGiangvien(GiangvienToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ");
            }

            return NoContent();
        }
    }
}

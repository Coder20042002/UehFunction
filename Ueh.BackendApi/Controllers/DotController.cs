using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotController : ControllerBase
    {
        private readonly IDotRepository _dotRepository;
        private readonly IMapper _mapper;

        public DotController(IDotRepository dotRepository, IMapper mapper)
        {
            _dotRepository = dotRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDots()
        {
            var categories = _mapper.Map<List<DotDto>>(_dotRepository.GetAllDot());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{dotId}")]
        [ProducesResponseType(200, Type = typeof(Dot))]
        [ProducesResponseType(400)]
        public IActionResult GetDot(string dotId)
        {
            if (!_dotRepository.DotExists(dotId))
                return NotFound();

            var dot = _mapper.Map<DotDto>(_dotRepository.GetDot(dotId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dot);
        }

        [HttpGet("sinhvien/{dotId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sinhvien>))]
        [ProducesResponseType(400)]
        public IActionResult GetSinhvienByDotId(string dotId)
        {
            var sinhviens = _mapper.Map<List<SinhvienDto>>(
                _dotRepository.GetSinhvienByDot(dotId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(sinhviens);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDot([FromBody] DotDto dotcreate)
        {
            if (dotcreate == null)
                return BadRequest(ModelState);

            var dot = _dotRepository.GetAllDot()
                .Where(c => c.madot == dotcreate.madot)
                .FirstOrDefault();

            if (dot != null)
            {
                ModelState.AddModelError("", "Dot already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dotMap = _mapper.Map<Dot>(dotcreate);

            if (!_dotRepository.CreateDot(dotMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{dotId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(string dotId, [FromBody] DotDto updateDot)
        {
            if (updateDot == null)
                return BadRequest(ModelState);

            if (dotId != updateDot.madot)
                return BadRequest(ModelState);

            if (!_dotRepository.DotExists(dotId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dotMap = _mapper.Map<Dot>(updateDot);

            if (!_dotRepository.UpdateDot(dotMap))
            {
                ModelState.AddModelError("", "Something went wrong updating ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{dotId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(string dotId)
        {
            if (!_dotRepository.DotExists(dotId))
            {
                return NotFound();
            }

            var dotToDelete = _dotRepository.GetDot(dotId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_dotRepository.DeleteDot(dotToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ");
            }

            return NoContent();
        }

    }

}

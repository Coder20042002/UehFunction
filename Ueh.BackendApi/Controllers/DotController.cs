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
            var getalldot = await _dotRepository.GetAllDot();
            var categories = _mapper.Map<List<DotDto>>(getalldot);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{dotId}")]
        [ProducesResponseType(200, Type = typeof(Dot))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDot(string dotId)
        {
            bool exists = await _dotRepository.DotExists(dotId);
            if (!exists)
                return NotFound();
            Dot dotid = await _dotRepository.GetDot(dotId);
            var dot = _mapper.Map<DotDto>(dotid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dot);
        }

        [HttpGet("sinhvien/{dotId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sinhvien>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSinhvienByDotId(string dotId)
        {
            var sinhvien = await _dotRepository.GetSinhvienByDot(dotId);
            var sinhviens = _mapper.Map<List<SinhvienDto>>(sinhvien
               );

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(sinhviens);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDot([FromBody] DotDto dotcreate)
        {
            if (dotcreate == null)
                return BadRequest(ModelState);

            var dots = await _dotRepository.GetAllDot();
            var dot = dots.Where(c => c.madot == dotcreate.madot)
                .FirstOrDefault();


            if (dot != null)
            {
                ModelState.AddModelError("", "Đã tồn tại");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dotMap = _mapper.Map<Dot>(dotcreate);

            bool createdot = await _dotRepository.CreateDot(dotMap);
            if (!createdot)
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
        public async Task<IActionResult> UpdateCategory(string dotId, [FromBody] DotDto updateDot)
        {
            if (updateDot == null)
                return BadRequest(ModelState);

            if (dotId != updateDot.madot)
                return BadRequest(ModelState);

            bool exists = await _dotRepository.DotExists(dotId);

            if (!exists)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dotMap = _mapper.Map<Dot>(updateDot);
            bool update = await _dotRepository.UpdateDot(dotMap);
            if (!update)
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
        public async Task<IActionResult> DeleteCategory(string dotId)
        {
            bool exists = await _dotRepository.DotExists(dotId);
            if (!exists)
            {
                return NotFound();
            }

            var dotToDelete = await _dotRepository.GetDot(dotId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool delete = await _dotRepository.DeleteDot(dotToDelete);
            if (!delete)
            {
                ModelState.AddModelError("", "Something went wrong deleting ");
            }

            return NoContent();
        }

    }

}

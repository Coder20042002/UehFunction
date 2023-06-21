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
    public class ChitietController : ControllerBase
    {
        private readonly IChitietRepository _ChitietRepository;
        private readonly IMapper _mapper;

        public ChitietController(IChitietRepository ChitietRepository, IMapper mapper)
        {
            _ChitietRepository = ChitietRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chitiet>))]
        public async Task<IActionResult> GetChitiets()
        {
            var Chitiets = _mapper.Map<List<ChitietDto>>(await _ChitietRepository.GetChitiets());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chitiets);
        }

        [HttpGet("{mapc}")]
        [ProducesResponseType(200, Type = typeof(Chitiet))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetChitiet(Guid mapc)
        {
            if (!await _ChitietRepository.ChitietExists(mapc))
                return NotFound();

            var Chitiet = _mapper.Map<ChitietDto>(await _ChitietRepository.GetChitiet(mapc));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chitiet);
        }






        [HttpPut("{mapc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateChitiet(Guid mapc,
            [FromBody] ChitietDto updatedChitiet)
        {
            if (updatedChitiet == null)
                return BadRequest(ModelState);

            if (mapc != updatedChitiet.mapc)
                return BadRequest(ModelState);

            if (!await _ChitietRepository.ChitietExists(mapc))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ChitietMap = _mapper.Map<Chitiet>(updatedChitiet);

            if (!await _ChitietRepository.UpdateChitiet(ChitietMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}


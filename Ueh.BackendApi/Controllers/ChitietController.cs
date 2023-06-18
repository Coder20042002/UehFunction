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
            var Chitiets = _mapper.Map<List<ChitietDto>>(await _ChitietRepository.GetChitiet());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chitiets);
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(Chitiet))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetChitiet(string mssv)
        {
            if (!await _ChitietRepository.ChitietExists(mssv))
                return NotFound();

            var Chitiet = _mapper.Map<ChitietDto>(await _ChitietRepository.GetChitiet(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Chitiet);
        }






        [HttpPut("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateChitiet(string mssv,
            [FromBody] ChitietDto updatedChitiet)
        {
            if (updatedChitiet == null)
                return BadRequest(ModelState);

            if (mssv != updatedChitiet.mssv)
                return BadRequest(ModelState);

            if (!await _ChitietRepository.ChitietExists(mssv))
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


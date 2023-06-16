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
    public class KetquaController : ControllerBase
    {
        private readonly IKetquaRepository _KetquaRepository;
        private readonly IMapper _mapper;

        public KetquaController(IKetquaRepository KetquaRepository, IMapper mapper)
        {
            _KetquaRepository = KetquaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ketqua>))]
        public async Task<IActionResult> GetKetquas()
        {
            var Ketquas = _mapper.Map<List<KetquaDto>>(await _KetquaRepository.GetScores());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Ketquas);
        }

        [HttpGet("{mssv}")]
        [ProducesResponseType(200, Type = typeof(Ketqua))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetKetqua(string mssv)
        {
            if (!await _KetquaRepository.ScoresExists(mssv))
                return NotFound();

            var Ketqua = _mapper.Map<KetquaDto>(await _KetquaRepository.GetScores(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Ketqua);
        }






        [HttpPut("{mssv}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateKetqua(string mssv,
            [FromBody] KetquaDto updatedKetqua)
        {
            if (updatedKetqua == null)
                return BadRequest(ModelState);

            if (mssv != updatedKetqua.mssv)
                return BadRequest(ModelState);

            if (!await _KetquaRepository.ScoresExists(mssv))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var KetquaMap = _mapper.Map<Ketqua>(updatedKetqua);

            if (!await _KetquaRepository.UpdateScores(KetquaMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}


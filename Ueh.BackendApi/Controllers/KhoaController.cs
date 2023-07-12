using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var Sinhviens = _mapper.Map<List<SinhvienkhoaDto>>(await _khoaRepository.GetKhoaBySinhviens(madot, makhoa));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Sinhviens);
        }

        [HttpGet("giangviens/{makhoa}")]
        public async Task<IActionResult> GetGiangvienByKhoas(string makhoa)
        {
            var giangviens = _mapper.Map<List<GiangvienkhoaDto>>(await _khoaRepository.GetKhoaByGiangviens(makhoa));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(giangviens);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichsuController : ControllerBase
    {
        private readonly ILichsuRepository _lichsuRepository;
        private readonly IMapper _mapper;


        public LichsuController(ILichsuRepository lichsuRepository, IMapper mapper)
        {
            _lichsuRepository = lichsuRepository;
            _mapper = mapper;

        }


        [HttpGet("sinhvien")]
        public async Task<IActionResult> GetLichSuByMssv(string madot,string mssv)
        {
            var lichsus = _mapper.Map<List<LichsuDto>>(await _lichsuRepository.GetLichSuByMssv(madot,mssv));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lichsus);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Lichsu>))]
        public async Task<IActionResult> GetLichsus()
        {
            var lichsus = _mapper.Map<List<LichsuDto>>(await _lichsuRepository.GetLichsus());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lichsus);
        }

        [HttpGet("{mapc}")]
        [ProducesResponseType(200, Type = typeof(Lichsu))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Getsinhvien(Guid mapc, string date)
        {
            if (!await _lichsuRepository.LichsuExists(mapc, date))
                return NotFound();

            var lichsu = _mapper.Map<LichsuDto>(_lichsuRepository.GetLichsu(mapc));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lichsu);
        }

        [HttpGet("sinhvien/{mapc}")]
        [ProducesResponseType(200, Type = typeof(Lichsu))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLichsusForAsinhvien(Guid mapc)
        {
            var lichsus = _mapper.Map<List<LichsuDto>>(await _lichsuRepository.GetLichsusOfASinhvien(mapc));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(lichsus);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateLichsu(LichsuRequest lichsuRequest, string mssv)
        {

            if (_lichsuRepository == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!await _lichsuRepository.CreateLichsu(lichsuRequest, mssv))
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(lichsuRequest);
        }

        [HttpPut("{mapc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateLichsu(Guid mapc, string dateTime, [FromBody] LichsuDto updatedLichsu)
        {
            if (updatedLichsu == null)
                return BadRequest(ModelState);


            if (!await _lichsuRepository.LichsuExists(mapc, dateTime))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var lichsuMap = _mapper.Map<Lichsu>(updatedLichsu);

            if (!await _lichsuRepository.UpdateLichsu(lichsuMap))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật đánh giá");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{mapc}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteLichsu(Guid mapc, string dateTime)
        {
            if (!await _lichsuRepository.LichsuExists(mapc, dateTime))
            {
                return NotFound();
            }

            var lichsuToDelete = await _lichsuRepository.GetLichsu(mapc);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _lichsuRepository.DeleteLichsu(lichsuToDelete) == true)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa chủ sở hữu");
            }

            return NoContent();
        }



    }
}

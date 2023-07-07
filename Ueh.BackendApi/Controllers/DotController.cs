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

        //[HttpGet]
        //public async Task<IActionResult> GetAllDots()
        //{
        //    var getalldot = await _dotRepository.GetAllDot();
        //    var categories = _mapper.Map<List<DotDto>>(getalldot);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(categories);
        //}

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Dot))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDot()
        {
            Dot dotid = await _dotRepository.GetDot();
            var dot = _mapper.Map<DotDto>(dotid);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dot);
        }



        [HttpPost("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDot(DotDto dotcreate)
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
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok("Tạo thành công");
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
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
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

            var dotToDelete = await _dotRepository.GetDot();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool delete = await _dotRepository.DeleteDot(dotToDelete);
            if (!delete)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xoá ");
            }

            return NoContent();
        }

    }

}

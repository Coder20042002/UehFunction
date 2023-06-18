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
    public class LoaiController : ControllerBase
    {
        private readonly ILoaiRepository _loaiRepository;
        private readonly UehDbContext _uehDbContext;
        private readonly IMapper _mapper;

        public LoaiController(ILoaiRepository loaiRepository, UehDbContext uehDbContext, IMapper mapper)
        {
            _loaiRepository = loaiRepository;
            _uehDbContext = uehDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var loai = _mapper.Map<List<LoaiDto>>(_loaiRepository.GetLoai());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(loai);
        }
    }
}

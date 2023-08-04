using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;
using Ueh.BackendApi.Request;

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


        [HttpGet("GetDanhSachDiemChiTietSv")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiemchitietRequest>))]
        public async Task<IActionResult> GetDanhSachDiemChiTietSv(string madot,string magv)
        {
            var Ketquas = await _KetquaRepository.GetDanhSachDiemChiTietSv(madot,magv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Ketquas);
        }


        [HttpGet("DiemChiTietSv")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiemchitietRequest>))]
        public async Task<IActionResult> DiemChiTietSv(string mssv)
        {
            var Ketquas = await _KetquaRepository.DiemChiTietSv(mssv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Ketquas);
        }

        [HttpGet("DsDiemTong")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DsDiemGvHuongDanRequest>))]
        public async Task<IActionResult> DsDiemTong(string madot, string maloai, string magv)
        {
            var Ketquas = await _KetquaRepository.DsDiemGvHuongDanRequest(madot, maloai, magv);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Ketquas);
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

        [HttpGet("sinhvien")]
        [ProducesResponseType(200, Type = typeof(Ketqua))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDiemByMssv([FromQuery] string mssv)
        {

            var Ketqua = _mapper.Map<KetquaDto>(await _KetquaRepository.GetDiemByMssv(mssv));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Ketqua);
        }

        [HttpGet("GetKetQuaByMaGV")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ketqua>))]
        public async Task<ActionResult<List<Ketqua>>> GetKetQuaByMaGV(string maGV)
        {
            var ketQuaList = await _KetquaRepository.GetKetQuaByMaGV(maGV);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ketQuaList);
        }




        [HttpPut("diem")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDiem(KetquaDto updatedKetqua, [FromQuery] string mssv)
        {
            if (updatedKetqua == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var KetquaMap = _mapper.Map<Ketqua>(updatedKetqua);

            if (!await _KetquaRepository.UpdateDiem(KetquaMap, mssv))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }






        [HttpGet("ExportToExcelByKhoa")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ExportToExcel(string madot, string makhoa)
        {
            try
            {
                var content = await _KetquaRepository.ExportToExcelByKhoa(madot, makhoa);
                if (content != null)
                {
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DSdiemtonghop.xlsx");
                }
                else
                {
                    return BadRequest("Không có dữ liệu để xuất.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }
        }


    }
}


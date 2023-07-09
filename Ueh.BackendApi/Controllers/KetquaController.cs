using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

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

        [HttpGet("generatepdfbygv")]
        [ProducesResponseType(200, Type = typeof(FileContentResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GeneratePdfByGv([FromQuery] string magv)
        {
            try
            {
                var content = await _KetquaRepository.GeneratePdfByGv(magv);
                if (content != null)
                {
                    return File(content, "application/pdf", "BaoCao.pdf");
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

        [HttpGet("generatepdfbysv")]
        [ProducesResponseType(200, Type = typeof(FileContentResult))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GeneratePdfBySv([FromQuery] string mssv)
        {
            try
            {
                var content = await _KetquaRepository.GeneratePdfBySv(mssv);
                if (content != null)
                {
                    string fileName = $"BaoCaoChitiet_{mssv}.pdf";

                    return File(content, "application/pdf", fileName);
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
        [HttpGet("generate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var content = await _KetquaRepository.ExportToExcel();
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

        [HttpGet("generatezip")]
        public async Task<IActionResult> GenerateZip([FromQuery] string giangVienId)
        {
            try
            {

                byte[]? zipBytes = await _KetquaRepository.GenerateZipFileForGv(giangVienId);

                if (zipBytes == null)
                {
                    return NotFound("Không có dữ liệu để tạo tệp tin nén.");
                }

                string fileName = $"Dsbaocaobangdiemchitiet_{giangVienId}.zip";

                return File(zipBytes, "application/zip", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}


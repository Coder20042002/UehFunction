using AutoMapper;
using Login.ST.UEH;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Host;
using ServiceStack.Web;
using System.Data;
using System.Security.Cryptography;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("formFile")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ImportExcelFile(IFormFile formFile)
        { 
            try
            {

                bool success = await _userRepository.ImportExcelFile(formFile);
                if (success)
                {
                    return Ok("Import thành công"); // Trả về thông báo thành công
                }
                else
                {
                    return BadRequest("Import thất bại"); // Trả về thông báo lỗi
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra sự cố: {ex.Message}");
            }

            // Trường hợp không xử lý được, trả về BadRequest
            return BadRequest("Xảy ra lỗi không xác định được");
        }
    

        [HttpPost("CreateUserRoleAdmin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUserRoleAdmin([FromQuery] string makhoa, GiangvienUpdateRequest giangvienCreate)
        {


            bool Giangviens = await _userRepository.UserExists(giangvienCreate.magv);

            if (Giangviens == true)
            {
                ModelState.AddModelError("", "User đã tồn tại");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            if (!await _userRepository.CreateUserRoleAdmin(makhoa, giangvienCreate))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(giangvienCreate);
        }

        [HttpPost("LoginUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser(string encryptedJson)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userRepository.CreateUser(encryptedJson);
            if (user == null)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return Ok(user);
        }

        [HttpPost("DecryptData")]
        public async Task<IActionResult> DecryptData(EncryptedDto encryptedData)
        {
            try
            {
                string decryptedJson = await _userRepository.Decrypt(encryptedData.EncryptedJson);
                UserDto user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(decryptedJson);
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return BadRequest("Không thể giải mã dữ liệu.");
            }
        }

        [HttpGet("GetInfoUser")]
        public async Task<IActionResult> GetInfoUser(string id)
        {

            var user = _mapper.Map<UserDto>(await _userRepository.GetInfoUser(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("GetUserRoleAdminRequests")]
        public async Task<IActionResult> GetUserRoleAdminRequests()
        {

            var user = await _userRepository.GetUserRoleAdminRequests();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        // [HttpGet("KiemTraUser")]
        // public async Task<IActionResult> KiemTraUser(string id)
        // {

        //     int kiemtra = await _userRepository.KiemTraUser(id);

        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     return Ok(kiemtra);
        // }


        [HttpPut("UpdateInfoUser")]
        public async Task<IActionResult> UpdateInfoUser(UpdateUserRequest updateUser, string id)
        {
            if (updateUser == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();


            if (!await _userRepository.UpdateInfoUser(updateUser, id))
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi update ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}

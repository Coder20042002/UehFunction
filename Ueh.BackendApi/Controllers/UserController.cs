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


        [HttpPost("CreateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser(string encryptedJson)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.CreateUser(encryptedJson))
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu");
                return StatusCode(500, ModelState);
            }

            return NoContent();
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



        [HttpGet("KiemTraUser")]
        public async Task<IActionResult> KiemTraUser(string id)
        {

            int kiemtra = await _userRepository.KiemTraUser(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(kiemtra);
        }


        [HttpPut("UpdateInfoUser")]
        public async Task<IActionResult> UpdateInfoUser(UserDto updateUser, string id)
        {
            if (updateUser == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(updateUser);

            if (!await _userRepository.UpdateInfoUser(user, id))
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi update ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}

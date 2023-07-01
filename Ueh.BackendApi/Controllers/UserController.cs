using Login.ST.UEH;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Host;
using ServiceStack.Web;
using System.Data;
using System.Security.Cryptography;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }





        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userRepository.RoleAssign(id, request);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("EncryptData")]
        public async Task<IActionResult> EncryptData([FromBody] UserDto user)
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                string key = "0DiKx5N5PU60jOozndHWHISm/MzAgRjDcfkLXxqDELQ=";
                string iv = "Wo/6qIrrDMAYPlE2aZrmdQ==";
                string encryptedJson = await _userRepository.Encrypt(json, key, iv);

                return Ok(encryptedJson);
            }
            catch (Exception ex)
            {
                return BadRequest("Không thể mã hóa dữ liệu.");
            }
        }


        [HttpPost("DecryptData")]
        public async Task<IActionResult> DecryptData([FromBody] EncryptedDto encryptedData)
        {
            try
            {
                string key = "0DiKx5N5PU60jOozndHWHISm/MzAgRjDcfkLXxqDELQ=";
                string iv = "Wo/6qIrrDMAYPlE2aZrmdQ==";
                string decryptedJson = await _userRepository.Decrypt(encryptedData.EncryptedJson, key, iv);
                UserDto user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDto>(decryptedJson);

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return BadRequest("Không thể giải mã dữ liệu.");
            }
        }


        //[HttpGet("GenerateIV")]
        //public IActionResult GenerateIV()
        //{
        //    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        //    {
        //        byte[] ivBytes = new byte[16]; // 128 bit = 16 byte
        //        rng.GetBytes(ivBytes);
        //        string iv = Convert.ToBase64String(ivBytes);
        //        return Ok(iv);
        //    }
        //}

        //[HttpGet("GenerateKey")]
        //public IActionResult GenerateKey()
        //{
        //    using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        //    {
        //        byte[] keyBytes = new byte[32]; // 256 bit = 32 byte
        //        rng.GetBytes(keyBytes);
        //        string key = Convert.ToBase64String(keyBytes);
        //        return Ok(key);
        //    }
        //}

    }
}

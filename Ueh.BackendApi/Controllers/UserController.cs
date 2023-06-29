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
using Ueh.BackendApi.User;

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

        [HttpGet("login")]
        public ActionResult LoginStUeh(string returnUrl = "/Account")
        {
            string uri = "https://localhost:7125/Account/callback/?returnUrl=" + returnUrl;

            return Redirect("https://loginst.ueh.edu.vn/?returnUrl=" + uri);
        }

        [HttpGet("callback")]
        public async Task<ActionResult> LoginSTUehCallback(string t, string returnUrl = null)
        {
            var obj = LoginStUEH.GetInfo(t);
            if (obj == null || string.IsNullOrEmpty(obj.email))
            {
                BadRequest();
            }
            if (obj.email.EndsWith("@st.ueh.edu.vn"))
            {

                if (await _userRepository.UserExist(obj.mssv))
                {
                    var result = await _userRepository.Authencate(obj.mssv, obj.Password);
                    return Ok(result);
                }
                else
                {
                    if (await _userRepository.Register(obj.mssv, obj.Password, obj.email, "student"))
                    {
                        var result = await _userRepository.Authencate(obj.mssv, obj.Password);
                        return Ok(result);

                    }
                    return BadRequest();

                }

            }
            else
            {
                throw new HttpException(404, "File Not Found");
            }


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


        [HttpGet("GenerateIV")]
        public IActionResult GenerateIV()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] ivBytes = new byte[16]; // 128 bit = 16 byte
                rng.GetBytes(ivBytes);
                string iv = Convert.ToBase64String(ivBytes);
                return Ok(iv);
            }
        }

        [HttpGet("GenerateKey")]
        public IActionResult GenerateKey()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[32]; // 256 bit = 32 byte
                rng.GetBytes(keyBytes);
                string key = Convert.ToBase64String(keyBytes);
                return Ok(key);
            }
        }

    }
}

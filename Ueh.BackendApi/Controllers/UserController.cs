using Login.ST.UEH;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Host;
using ServiceStack.Web;
using System.Data;
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
    }
}

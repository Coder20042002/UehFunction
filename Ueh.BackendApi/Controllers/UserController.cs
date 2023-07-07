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

        [HttpGet("GetInfoUser")]
        public async Task<IActionResult> GetInfoUser(string id)
        {

            var user = _mapper.Map<UserDto>(await _userRepository.GetInfoUser(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
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

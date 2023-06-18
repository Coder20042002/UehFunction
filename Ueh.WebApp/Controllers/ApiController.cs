using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ueh.WebApp
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        // [HttpGet]
        // public IActionResult GetSessionData()
        // {
        //     // Retrieve session data from HttpContext.Session
        //     var sessionData = HttpContext.Session.GetString("mySessionData");

        //     // Return session data to the client
        //     return Ok(sessionData);
        // }
        [HttpGet]
        public ActionResult GetSpecials()
        {
            return Ok("hello");
        }
    }
}

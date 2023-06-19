using Login.ST.UEH;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Host;

namespace Ueh.WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult LoginStUeh(string returnUrl = "/home")
        {
            if (HttpContext.Session.Get("account") != null)
            {
                return Redirect(returnUrl);
            }
            string uri = Url.Action("LoginSTUehCallback", "Account", new RouteValueDictionary(new { returnUrl }),
                HttpContext.Request.Scheme, string.Empty);

            return Redirect("https://loginst.ueh.edu.vn/?returnUrl=" + uri);
        }

        [HttpGet]
        public ActionResult LoginSTUehCallback(string t, string returnUrl = null)
        {
            var obj = LoginStUEH.GetInfo(t);
            if (obj == null || string.IsNullOrEmpty(obj.email))
            {
                BadRequest();
            }
            if (obj.email.EndsWith("@st.ueh.edu.vn"))
            {

            }
            else
            {
                throw new HttpException(404, "File Not Found");
            }

            return Redirect(returnUrl);
        }
    }
}

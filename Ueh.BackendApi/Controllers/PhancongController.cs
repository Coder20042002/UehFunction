using Microsoft.AspNetCore.Mvc;

namespace Ueh.BackendApi.Controllers
{
    public class PhancongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

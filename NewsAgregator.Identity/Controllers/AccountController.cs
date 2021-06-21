using Microsoft.AspNetCore.Mvc;

namespace NewsAgregator.Identity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}

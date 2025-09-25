using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        // Rgister
        public IActionResult Register()
        {
            return View();
        }
        // Login
    }
}

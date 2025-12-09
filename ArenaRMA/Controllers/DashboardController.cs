using Microsoft.AspNetCore.Mvc;

namespace ArenaRMA.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }
    }
}

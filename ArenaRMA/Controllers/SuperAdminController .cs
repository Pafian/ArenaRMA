using Microsoft.AspNetCore.Mvc;

namespace ArenaRMA.Controllers
{
    public class SuperAdminController : Controller
    {
        private bool IsSuperAdmin()
        {
            return HttpContext.Session.GetString("Role") == "SuperAdmin";
        }

        // --- DASHBOARD ---
        public IActionResult Dashboard()
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // --- LOGOUT ---
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}

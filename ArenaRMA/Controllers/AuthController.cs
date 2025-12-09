using ArenaRMA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaRMA.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username
                                       && u.Password == password
                                       && u.IsActive);

            if (user == null)
            {
                ViewBag.Error = "Hibás bejelentkezés!";
                return View();
            }

            HttpContext.Session.SetString("UserID", user.UserID.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role.RoleName);

            // ROLE ALAPÚ REDIRECT
            switch (user.Role.RoleName)
            {
                case "SuperAdmin":
                    return RedirectToAction("Index", "Dashboard");

                case "Admin":
                    return RedirectToAction("Index", "Dashboard");

                case "User":
                    return RedirectToAction("Index", "Warranty");

                case "Guest":
                    return RedirectToAction("Index", "Warranty");

                default:
                    return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

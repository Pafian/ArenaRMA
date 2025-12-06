using ArenaRMA.Data;
using ArenaRMA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaRMA.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly AppDbContext _context;

        public UserAdminController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsSuperAdmin()
        {
            return HttpContext.Session.GetString("Role") == "SuperAdmin";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            ViewBag.Roles = await _context.Roles.ToListAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User updated)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var user = await _context.Users.FindAsync(updated.UserID);
            if (user == null) return NotFound();

            user.FullName = updated.FullName;
            user.Email = updated.Email;
            user.RoleID = updated.RoleID;
            user.IsActive = updated.IsActive;

            if (!string.IsNullOrWhiteSpace(updated.Password))
                user.Password = updated.Password;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            user.UserID = Guid.NewGuid();
            user.IsActive = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}

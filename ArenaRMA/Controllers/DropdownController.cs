using ArenaRMA.Data;
using ArenaRMA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaRMA.Controllers
{
    public class DropdownController : Controller
    {
        private readonly AppDbContext _context;

        public DropdownController(AppDbContext context)
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

            var dropdowns = await _context.Dropdowns
                .Include(d => d.Options)
                .ToListAsync();

            return View(dropdowns);
        }

        public IActionResult Create()
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string dropdownName)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var dropdown = new Dropdown
            {
                DropdownName = dropdownName
            };

            _context.Dropdowns.Add(dropdown);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult AddOption(int id)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            ViewBag.DropdownID = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOption(int dropdownID, string optionValue)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var opt = new DropdownOption
            {
                DropdownID = dropdownID,
                OptionValue = optionValue,
                Enabled = true,
                EditableByAdmin = false
            };

            _context.DropdownOptions.Add(opt);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOption(int id)
        {
            if (!IsSuperAdmin())
                return RedirectToAction("Login", "Auth");

            var opt = await _context.DropdownOptions.FindAsync(id);

            if (opt != null)
            {
                _context.DropdownOptions.Remove(opt);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}

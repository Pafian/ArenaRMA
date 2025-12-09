using ArenaRMA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaRMA.Controllers
{
    public class WarrantyController : Controller
    {
        private readonly AppDbContext _context;

        public WarrantyController(AppDbContext context)
        {
            _context = context;
        }

        // LISTA + KERESÉS + ÁLLAPOT SZŰRŐ (szöveges)
        public async Task<IActionResult> Index(string search, string stateFilter)
        {
            var query = _context.Warranties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w =>
                    (w.BuyerName != null && w.BuyerName.Contains(search)) ||
                    (w.ProductName != null && w.ProductName.Contains(search)) ||
                    (w.SerialNumber != null && w.SerialNumber.Contains(search)) ||
                    (w.WarrantyCode != null && w.WarrantyCode.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(stateFilter) && stateFilter != "ALL")
            {
                query = query.Where(w => w.State == stateFilter);
            }

            // dropdownhoz: egyedi státuszok
            var states = await _context.Warranties
                .Select(w => w.State)
                .Where(s => s != null && s != "")
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            ViewBag.States = states;

            var list = await query.ToListAsync();
            return View(list);
        }
    }
}

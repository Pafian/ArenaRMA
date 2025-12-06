using ArenaRMA.Data;
using ArenaRMA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaRMA.Controllers
{
    public class EmailsController : Controller
    {

        private readonly AppDbContext _context;

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("Role") != null;
        }


        public EmailsController(AppDbContext context)
        {
            _context = context;
        }

        // LISTA
        public async Task<IActionResult> Index()
        {
            var emails = await _context.Emails.ToListAsync();
            return View(emails);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
                return NotFound();

            return View(email);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(Email model)
        {
            _context.Emails.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
                return NotFound();

            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

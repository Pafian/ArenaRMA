using ArenaRMA.Data;
using ArenaRMA.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArenaRMA.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(AppDbContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ========== LISTA OLDAL ==========
        public IActionResult Index()
        {
            _logger.LogInformation("Customer lista megnyitva {Time}", DateTime.Now);

            var customers = _context.Customers
                .OrderBy(x => x.CompanyName)
                .ToList();

            return View(customers);
        }

        // ========== IMPORT GET ==========
        public IActionResult Import()
        {
            _logger.LogInformation("Import oldal megnyitva {Time}", DateTime.Now);
            return View();
        }

        // ========== IMPORT POST ==========
        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["error"] = "Nem választottál ki fájlt!";
                return RedirectToAction("Import");
            }

            try
            {
                _logger.LogInformation("Excel import elkezdődött: {File}", file.FileName);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using var stream = file.OpenReadStream();
                using var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                var result = reader.AsDataSet();

                var table = result.Tables[0];

                int imported = 0;

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];

                    var customer = new Customer
                    {
                        CustomerCode = row[0]?.ToString() ?? "",
                        CompanyName = row[1]?.ToString() ?? "",
                        PhoneNumber = row[2]?.ToString() ?? "",
                        Email = row[3]?.ToString() ?? ""
                    };

                    if (string.IsNullOrWhiteSpace(customer.CompanyName))
                        continue;

                    _context.Customers.Add(customer);
                    imported++;
                }

                _context.SaveChanges();

                TempData["success"] = $"{imported} ügyfél sikeresen importálva!";
                _logger.LogInformation("{Count} ügyfél importálva.", imported);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Hiba történt az importálás során!";
                _logger.LogError(ex, "Import error");
            }

            return RedirectToAction("Index");
        }


        // ========== CREATE GET ==========
        public IActionResult Create()
        {
            return View();
        }

        // ========== CREATE POST ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create hibás adatokkal lett elküldve.");
                return View(model);
            }

            _context.Customers.Add(model);
            _context.SaveChanges();

            TempData["success"] = "Ügyfél sikeresen hozzáadva!";
            _logger.LogInformation("Új ügyfél létrehozva: {Name}", model.CompanyName);

            return RedirectToAction("Index");
        }

        // ========== EDIT GET ==========
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                TempData["error"] = "A keresett ügyfél nem található!";
                _logger.LogWarning("Edit sikertelen – ügyfél ID nem található: {ID}", id);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // ========== EDIT POST ==========
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Customers.Update(model);
            _context.SaveChanges();

            TempData["success"] = "Ügyfél adatai módosítva!";
            _logger.LogInformation("Ügyfél módosítva: {Name}", model.CompanyName);

            return RedirectToAction("Index");
        }

        // ========== DELETE ==========
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
            {
                TempData["error"] = "A törlendő ügyfél nem található!";
                _logger.LogWarning("Delete sikertelen – ID nincs: {ID}", id);
                return RedirectToAction("Index");
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            TempData["success"] = "Ügyfél törölve!";
            _logger.LogInformation("Ügyfél törölve: {Name}", customer.CompanyName);

            return RedirectToAction("Index");
        }
    }
}

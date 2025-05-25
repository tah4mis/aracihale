using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAuction.Web.Data;
using VehicleAuction.Web.Models;
using BCrypt.Net;

namespace VehicleAuction.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TaxNumber,Phone,Email,Address")] Company company)
        {
            if (ModelState.IsValid)
            {
                company.CreatedAt = DateTime.UtcNow;
                company.IsActive = true;
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }

        // GET: Company/Login
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Company/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CompanyLoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var company = await _context.Companies
                    .FirstOrDefaultAsync(c => c.Email == model.Email && c.IsActive);

                if (company != null && BCrypt.Net.BCrypt.Verify(model.Password, company.Password))
                {
                    // Session'a şirket bilgilerini kaydet
                    HttpContext.Session.SetInt32("CompanyId", company.Id);
                    HttpContext.Session.SetString("CompanyEmail", company.Email);
                    HttpContext.Session.SetString("CompanyName", company.Name);

                    if (model.RememberMe)
                    {
                        // Cookie'ye şirket bilgilerini kaydet (30 gün süreyle)
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        };

                        Response.Cookies.Append("CompanyEmail", company.Email, cookieOptions);
                    }

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre");
            }

            return View(model);
        }

        // GET: Company/Logout
        public IActionResult Logout()
        {
            // Session'ı temizle
            HttpContext.Session.Clear();

            // Cookie'yi temizle
            Response.Cookies.Delete("CompanyEmail");

            return RedirectToAction("Index", "Home");
        }

        // GET: Company/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Company/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Company company)
        {
            if (ModelState.IsValid)
            {
                // E-posta adresi kontrolü
                if (await _context.Companies.AnyAsync(c => c.Email == company.Email))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(company);
                }

                // Şifreyi hashle
                company.Password = BCrypt.Net.BCrypt.HashPassword(company.Password);

                // Şirketi kaydet
                _context.Add(company);
                await _context.SaveChangesAsync();

                // Oturum aç
                HttpContext.Session.SetInt32("CompanyId", company.Id);
                HttpContext.Session.SetString("CompanyEmail", company.Email);
                HttpContext.Session.SetString("CompanyName", company.Name);

                TempData["SuccessMessage"] = "Kayıt başarıyla tamamlandı.";
                return RedirectToAction("Index", "Home");
            }

            return View(company);
        }

        // GET: Company/Profile
        public async Task<IActionResult> Profile()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId == null)
            {
                return RedirectToAction("Login", new { returnUrl = "/Company/Profile" });
            }

            var company = await _context.Companies
                .Include(c => c.Auctions)
                    .ThenInclude(a => a.Vehicle)
                .FirstOrDefaultAsync(c => c.Id == companyId);

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Company/Edit
        public async Task<IActionResult> Edit()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (!companyId.HasValue)
            {
                return RedirectToAction("Login");
            }

            var company = await _context.Companies.FindAsync(companyId.Value);
            if (company == null)
            {
                return NotFound();
            }

            var editModel = new CompanyEditViewModel
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                Phone = company.Phone,
                Address = company.Address,
                TaxNumber = company.TaxNumber,
                TaxOffice = company.TaxOffice,
                BankName = company.BankName,
                IBAN = company.IBAN,
                AccountHolder = company.AccountHolder,
                BranchCode = company.BranchCode,
                AccountNumber = company.AccountNumber
            };

            return View(editModel);
        }

        // POST: Company/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (!companyId.HasValue || companyId.Value != model.Id)
            {
                return RedirectToAction("Login");
            }

            var company = await _context.Companies.FindAsync(companyId.Value);
            if (company == null)
            {
                return NotFound();
            }

            // E-posta değişikliği varsa ve başka bir şirket tarafından kullanılıyorsa
            if (company.Email != model.Email && await _context.Companies.AnyAsync(c => c.Email == model.Email && c.Id != company.Id))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi başka bir şirket tarafından kullanılıyor.");
                return View(model);
            }

            company.Name = model.Name;
            company.Email = model.Email;
            company.Phone = model.Phone;
            company.Address = model.Address;
            company.TaxNumber = model.TaxNumber;
            company.TaxOffice = model.TaxOffice;
            company.BankName = model.BankName;
            company.IBAN = model.IBAN;
            company.AccountHolder = model.AccountHolder;
            company.BranchCode = model.BranchCode;
            company.AccountNumber = model.AccountNumber;

            // Şifre değişikliği varsa
            if (!string.IsNullOrEmpty(model.Password))
            {
                company.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Profil bilgileriniz başarıyla güncellendi.";
                return RedirectToAction(nameof(Profile));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Profil güncellenirken bir hata oluştu: " + ex.Message);
                return View(model);
            }
        }
    }
} 
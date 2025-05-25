using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAuction.Web.Data;
using VehicleAuction.Web.Models;
using BCrypt.Net;

namespace VehicleAuction.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: User/Login
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password && u.IsActive);

                if (user != null)
                {
                    // Session'a kullanıcı bilgilerini kaydet
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserName", user.FullName);

                    if (model.RememberMe)
                    {
                        // Cookie'ye kullanıcı bilgilerini kaydet (30 gün süreyle)
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        };

                        Response.Cookies.Append("UserEmail", user.Email, cookieOptions);
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

        // GET: User/Logout
        public IActionResult Logout()
        {
            // Session'ı temizle
            HttpContext.Session.Clear();

            // Cookie'yi temizle
            Response.Cookies.Delete("UserEmail");

            return RedirectToAction("Index", "Home");
        }

        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // E-posta adresi kontrolü
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(user);
                }

                // Şifreyi hashle
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Kullanıcıyı kaydet
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Oturum aç
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);

                TempData["SuccessMessage"] = "Kayıt başarıyla tamamlandı.";
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // GET: User/Profile
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = "/User/Profile" });
            }

            var user = await _context.Users
                .Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Teklifleri BidderId'ye göre getir
            var userBids = await _context.Bids
                .Include(b => b.Auction)
                    .ThenInclude(a => a.Vehicle)
                .Where(b => b.BidderId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            ViewBag.UserBids = userBids;
            return View(user);
        }

        // POST: User/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(User model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                TempData["ErrorMessage"] = "Profil sayfasını görüntülemek için giriş yapmalısınız.";
                return RedirectToAction("Login", "User", new { returnUrl = Url.Action("Profile", "User") });
            }

            if (userId.Value != model.Id)
            {
                TempData["ErrorMessage"] = "Bu işlem için yetkiniz yok.";
                return RedirectToAction("Profile", "User");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FindAsync(model.Id);
                    if (user == null)
                    {
                        TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                        return RedirectToAction("Logout", "User");
                    }

                    // E-posta değişmişse ve başka bir kullanıcı tarafından kullanılıyorsa hata ver
                    if (user.Email != model.Email && await _context.Users.AnyAsync(u => u.Email == model.Email && u.Id != model.Id))
                    {
                        ModelState.AddModelError("Email", "Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");
                        return View(model);
                    }

                    // Sadece güncellenebilir alanları güncelle
                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Address = model.Address;

                    // Şifre değiştirilmek isteniyorsa
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        user.Password = model.Password;
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    // Session'daki kullanıcı bilgilerini güncelle
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserName", user.FullName);

                    TempData["SuccessMessage"] = "Profil bilgileriniz başarıyla güncellendi.";
                    return RedirectToAction(nameof(Profile));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Users.AnyAsync(e => e.Id == model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Kullanıcının tekliflerini getir
            var userBids = await _context.Bids
                .Include(b => b.Auction)
                    .ThenInclude(a => a.Vehicle)
                .Where(b => b.BidderId == model.Id)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            ViewBag.UserBids = userBids;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
            {
                return NotFound();
            }

            var editModel = new EditViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue || userId.Value != model.Id)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
            {
                return NotFound();
            }

            // E-posta değişikliği varsa ve başka bir kullanıcı tarafından kullanılıyorsa
            if (user.Email != model.Email && await _context.Users.AnyAsync(u => u.Email == model.Email && u.Id != user.Id))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi başka bir kullanıcı tarafından kullanılıyor.");
                return View(model);
            }

            user.Email = model.Email;
            user.FullName = model.FullName;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            // Şifre değişikliği varsa
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
} 
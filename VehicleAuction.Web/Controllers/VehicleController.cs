using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAuction.Web.Data;
using VehicleAuction.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;

namespace VehicleAuction.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _logPath;

        public VehicleController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logPath = Path.Combine(_webHostEnvironment.ContentRootPath, "logs");
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }
        }

        private void LogToFile(string message)
        {
            string logFile = Path.Combine(_logPath, $"vehicle_log_{DateTime.Now:yyyyMMdd}.txt");
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            System.IO.File.AppendAllText(logFile, logMessage + Environment.NewLine);
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicles
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
            return View(vehicles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brand,Model,Year,Mileage,Price,Description,IsAvailable,ABS,ASR,CocukKilidi,Distronic,ESP,GeceGorusSistemi,HavaYastigiSurucu,HavaYastigiYolcu,Immobilizer,Isofix,KorNoktaUyariSistemi,MerkeziKilit,SeritTakipSistemi,YokusKalkisDesteği,YorgunlukTespitSistemi,ZirhliArac")] Vehicle vehicle, IFormFile? imageFile)
        {
            LogToFile("Create metodu başladı");
            
            if (ModelState.IsValid)
            {
                LogToFile("ModelState geçerli");
                try
                {
                    // Tahmini fiyatı otomatik hesapla
                    vehicle.EstimatedPrice = vehicle.CalculateEstimatedPrice();
                    
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        LogToFile($"Resim yükleniyor: {imageFile.FileName}");
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        vehicle.ImageUrl = "/uploads/" + uniqueFileName;
                        LogToFile($"Resim yüklendi: {vehicle.ImageUrl}");
                    }

                    vehicle.CreatedAt = DateTime.Now;
                    LogToFile($"Araç oluşturuldu: {vehicle.Brand} {vehicle.Model}");
                    
                    _context.Add(vehicle);
                    LogToFile("Araç context'e eklendi");
                    
                    var result = await _context.SaveChangesAsync();
                    LogToFile($"Veritabanı kayıt sonucu: {result} satır etkilendi");

                    if (result > 0)
                    {
                        LogToFile("Araç başarıyla eklendi");
                        TempData["SuccessMessage"] = "Araç başarıyla eklendi.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        LogToFile("Araç eklenemedi: Veritabanı kaydı başarısız");
                        TempData["ErrorMessage"] = "Araç eklenemedi: Veritabanı kaydı başarısız.";
                    }
                }
                catch (Exception ex)
                {
                    LogToFile($"HATA: {ex.Message}\nStack Trace: {ex.StackTrace}");
                    TempData["ErrorMessage"] = $"Araç eklenirken bir hata oluştu: {ex.Message}";
                    ModelState.AddModelError("", "Araç eklenirken bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                LogToFile($"ModelState geçersiz: {errors}");
                TempData["ErrorMessage"] = $"ModelState geçersiz: {errors}";
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Year,Mileage,Price,Description,ImageUrl,IsAvailable,ABS,ASR,CocukKilidi,Distronic,ESP,GeceGorusSistemi,HavaYastigiSurucu,HavaYastigiYolcu,Immobilizer,Isofix,KorNoktaUyariSistemi,MerkeziKilit,SeritTakipSistemi,YokusKalkisDesteği,YorgunlukTespitSistemi,ZirhliArac,CreatedAt")] Vehicle vehicle, IFormFile? imageFile)
        {
            if (id != vehicle.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Tahmini fiyatı otomatik hesapla
                    vehicle.EstimatedPrice = vehicle.CalculateEstimatedPrice();
                    
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Delete old image if exists
                        if (!string.IsNullOrEmpty(vehicle.ImageUrl))
                        {
                            string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, vehicle.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        vehicle.ImageUrl = "/uploads/" + uniqueFileName;
                    }

                    vehicle.UpdatedAt = DateTime.Now;
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Araç başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            // Araç için aktif (AuctionStatus.Active) bir müzayede (Auction) kaydı var mı kontrol et
            var activeAuction = await _context.Auctions
                .AnyAsync(a => a.VehicleId == id && a.Status == AuctionStatus.Active);
            if (activeAuction)
            {
                TempData["ErrorMessage"] = "Bu araç için aktif bir müzayede bulunduğundan silinemiyor.";
                 return RedirectToAction(nameof(Index));
            }

            // (Aktif müzayede yoksa) Araç resmi varsa, resmi sil
            if (!string.IsNullOrEmpty(vehicle.ImageUrl))
            {
                 string filePath = Path.Combine(_webHostEnvironment.WebRootPath, vehicle.ImageUrl.TrimStart('/'));
                 if (System.IO.File.Exists(filePath))
                 {
                     System.IO.File.Delete(filePath);
                 }
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Araç başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();

            // Aracın aktif müzayedelerini getir
            ViewBag.ActiveAuctions = await _context.Auctions
                .Include(a => a.Bids)
                .Where(a => a.VehicleId == id && a.Status == AuctionStatus.Active)
                .OrderByDescending(a => a.StartDate)
                .ToListAsync();

            return View(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
} 
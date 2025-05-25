using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleAuction.Web.Data;
using VehicleAuction.Web.Models;

namespace VehicleAuction.Web.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuctionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Auction
        public async Task<IActionResult> Index()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Vehicle)
                .Include(a => a.Company)
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
            return View(auctions);
        }

        // GET: Auction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions
                .Include(a => a.Vehicle)
                .Include(a => a.Company)
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // GET: Auction/Create
        public IActionResult Create(int? vehicleId = null)
        {
            var vehicles = _context.Vehicles
                .Where(v => v.IsAvailable && !_context.Auctions.Any(a => a.VehicleId == v.Id && a.Status == AuctionStatus.Active))
                .Select(v => new
                {
                    Id = v.Id,
                    DisplayText = $"{v.Brand} {v.Model} ({v.Year}) - {v.Price:C}"
                });

            ViewData["Vehicles"] = new SelectList(vehicles, "Id", "DisplayText", vehicleId);

            if (vehicleId.HasValue)
            {
                var vehicle = _context.Vehicles.Find(vehicleId.Value);
                if (vehicle != null)
                {
                    var auction = new Auction
                    {
                        VehicleId = vehicle.Id,
                        StartingPrice = vehicle.Price,
                        MinimumIncrement = vehicle.Price * 0.05m, // Başlangıç fiyatının %5'i
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(7) // Varsayılan olarak 7 gün
                    };
                    return View(auction);
                }
            }

            return View();
        }

        // POST: Auction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,StartingPrice,MinimumIncrement,StartDate,EndDate")] Auction auction)
        {
            // ModelState hatalarını kontrol et
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = $"Form geçersiz: {string.Join(", ", errors)}";
                
                // Araç listesini yeniden yükle
                var vehicles = _context.Vehicles
                    .Where(v => v.IsAvailable)
                    .Select(v => new
                    {
                        Id = v.Id,
                        DisplayText = $"{v.Brand} {v.Model} ({v.Year}) - {v.Price:C}"
                    });
                ViewData["Vehicles"] = new SelectList(vehicles, "Id", "DisplayText", auction.VehicleId);
                return View(auction);
            }

            try
            {
                // Araç kontrolü
                var vehicle = await _context.Vehicles.FindAsync(auction.VehicleId);
                if (vehicle == null)
                {
                    TempData["ErrorMessage"] = "Seçilen araç bulunamadı.";
                    var vehicles = _context.Vehicles
                        .Where(v => v.IsAvailable)
                        .Select(v => new
                        {
                            Id = v.Id,
                            DisplayText = $"{v.Brand} {v.Model} ({v.Year}) - {v.Price:C}"
                        });
                    ViewData["Vehicles"] = new SelectList(vehicles, "Id", "DisplayText", auction.VehicleId);
                    return View(auction);
                }

                // Tarih kontrolü
                if (auction.StartDate >= auction.EndDate)
                {
                    TempData["ErrorMessage"] = "Başlangıç tarihi bitiş tarihinden önce olmalıdır.";
                    var vehicles = _context.Vehicles
                        .Where(v => v.IsAvailable)
                        .Select(v => new
                        {
                            Id = v.Id,
                            DisplayText = $"{v.Brand} {v.Model} ({v.Year}) - {v.Price:C}"
                        });
                    ViewData["Vehicles"] = new SelectList(vehicles, "Id", "DisplayText", auction.VehicleId);
                    return View(auction);
                }

                auction.Status = AuctionStatus.Pending;
                auction.CreatedAt = DateTime.UtcNow;
                _context.Add(auction);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "İhale başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"İhale oluşturulurken bir hata oluştu: {ex.Message}";
                var vehiclesList = _context.Vehicles
                    .Where(v => v.IsAvailable)
                    .Select(v => new
                    {
                        Id = v.Id,
                        DisplayText = $"{v.Brand} {v.Model} ({v.Year}) - {v.Price:C}"
                    });
                ViewData["Vehicles"] = new SelectList(vehiclesList, "Id", "DisplayText", auction.VehicleId);
                return View(auction);
            }
        }

        // GET: Auction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            ViewData["Vehicles"] = new SelectList(_context.Vehicles.Where(v => v.IsAvailable), "Id", "Brand", auction.VehicleId);
            return View(auction);
        }

        // POST: Auction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleId,StartingPrice,MinimumIncrement,StartDate,EndDate,Status,CreatedAt")] Auction auction)
        {
            if (id != auction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuctionExists(auction.Id))
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
            ViewData["Vehicles"] = new SelectList(_context.Vehicles.Where(v => v.IsAvailable), "Id", "Brand", auction.VehicleId);
            return View(auction);
        }

        // GET: Auction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auction = await _context.Auctions
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            return View(auction);
        }

        // POST: Auction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                _context.Auctions.Remove(auction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Auction/Bid/5
        public async Task<IActionResult> Bid(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var auction = await _context.Auctions
                .Include(a => a.Vehicle)
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null)
            {
                return NotFound();
            }

            // İhale başlama saati geldi (StartDate <= DateTime.Now) ve bitiş saati dolmadıysa (EndDate > DateTime.Now) ihale durumunu (Status) AuctionStatus.Active olarak güncelle.
            if (auction.StartDate <= DateTime.Now && auction.EndDate > DateTime.Now && auction.Status != AuctionStatus.Active)
            {
                auction.Status = AuctionStatus.Active;
                _context.Update(auction);
                await _context.SaveChangesAsync();
            }

            // Mevcut en yüksek teklifi (veya başlangıç fiyatını) ViewBag'e aktar.
            ViewBag.CurrentPrice = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault()?.Amount ?? auction.StartingPrice;
            return View(auction);
        }

        // POST: Auction/Bid/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bid(int id, decimal amount)
        {
            // Oturum kontrolü
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                TempData["ErrorMessage"] = "Teklif verebilmek için giriş yapmalısınız.";
                return RedirectToAction("Login", "User", new { returnUrl = Url.Action("Details", "Auction", new { id }) });
            }

            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null)
            {
                return NotFound();
            }

            // İhale başlama saati geldi (StartDate <= DateTime.Now) ve bitiş saati dolmadıysa (EndDate > DateTime.Now) ihale durumunu (Status) AuctionStatus.Active olarak güncelle.
            if (auction.StartDate <= DateTime.Now && auction.EndDate > DateTime.Now && auction.Status != AuctionStatus.Active)
            {
                auction.Status = AuctionStatus.Active;
                _context.Update(auction);
                await _context.SaveChangesAsync();
            }

            // İhale durumu (Status) AuctionStatus.Active değilse, teklif verilemez.
            if (auction.Status != AuctionStatus.Active)
            {
                return BadRequest("Bu ihale aktif değil.");
            }

            var now = DateTime.Now;
            if (now < auction.StartDate || now > auction.EndDate)
            {
                return BadRequest($"İhale süresi dışında teklif veremezsiniz. İhale {auction.StartDate:dd.MM.yyyy HH:mm} - {auction.EndDate:dd.MM.yyyy HH:mm} tarihleri arasında aktif olacaktır.");
            }

            var lastBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            if (lastBid != null && amount <= lastBid.Amount)
            {
                return BadRequest("Teklifiniz son tekliften yüksek olmalıdır.");
            }

            if (amount < auction.StartingPrice)
            {
                return BadRequest("Teklifiniz başlangıç fiyatından düşük olamaz.");
            }

            if (lastBid != null && amount - lastBid.Amount < auction.MinimumIncrement)
            {
                return BadRequest($"Minimum artış miktarı: {auction.MinimumIncrement:C}");
            }

            var bid = new Bid
            {
                AuctionId = id,
                Amount = amount,
                CreatedAt = now,
                IsActive = true,
            };

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Teklifiniz başarıyla kaydedildi.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Auction/PlaceBid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBid(int auctionId, decimal amount)
        {
            // Oturum kontrolü
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                TempData["ErrorMessage"] = "Teklif verebilmek için giriş yapmalısınız.";
                return RedirectToAction("Login", "User", new { returnUrl = Url.Action("Details", "Auction", new { id = auctionId }) });
            }

            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == auctionId);

            if (auction == null)
            {
                return NotFound();
            }

            // İhale durumunu kontrol et ve güncelle
            var now = DateTime.Now;
            if (now >= auction.StartDate && now <= auction.EndDate && auction.Status != AuctionStatus.Active)
            {
                auction.Status = AuctionStatus.Active;
                _context.Update(auction);
                await _context.SaveChangesAsync();
            }

            if (auction.Status != AuctionStatus.Active)
            {
                return BadRequest("Bu ihale aktif değil.");
            }

            if (now < auction.StartDate)
            {
                return BadRequest($"İhale henüz başlamadı. Başlangıç tarihi: {auction.StartDate:dd.MM.yyyy HH:mm}");
            }

            if (now > auction.EndDate)
            {
                return BadRequest("İhale süresi doldu.");
            }

            var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
            if (highestBid != null && amount <= highestBid.Amount)
            {
                return BadRequest($"Teklifiniz en yüksek tekliften ({highestBid.Amount:C}) daha yüksek olmalıdır.");
            }

            if (amount < auction.StartingPrice)
            {
                return BadRequest($"Teklifiniz başlangıç fiyatından ({auction.StartingPrice:C}) düşük olamaz.");
            }

            if (highestBid != null && amount - highestBid.Amount < auction.MinimumIncrement)
            {
                return BadRequest($"Minimum artış miktarı: {auction.MinimumIncrement:C}");
            }

            var user = await _context.Users.FindAsync(userId.Value);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var bid = new Bid
            {
                AuctionId = auctionId,
                Amount = amount,
                CreatedAt = now,
                IsActive = true,
                BidderId = userId.Value,
                BidderName = user.FullName,
                BidderEmail = user.Email,
                BidderPhone = user.PhoneNumber
            };

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Teklifiniz başarıyla kaydedildi.";
            return RedirectToAction(nameof(Details), new { id = auctionId });
        }

        private bool AuctionExists(int id)
        {
            return _context.Auctions.Any(e => e.Id == id);
        }
    }
} 
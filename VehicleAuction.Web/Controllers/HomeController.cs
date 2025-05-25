using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAuction.Web.Data;
using VehicleAuction.Web.Models;

namespace VehicleAuction.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // İstatistikler
        ViewBag.TotalVehicles = await _context.Vehicles.CountAsync();
        ViewBag.ActiveAuctions = await _context.Auctions.CountAsync(a => a.IsActive && a.Status == AuctionStatus.Active);
        ViewBag.TotalUsers = await _context.Users.CountAsync();
        ViewBag.CompletedAuctions = await _context.Auctions.CountAsync(a => a.Status == AuctionStatus.Completed);

        var activeAuctions = await _context.Auctions
            .Include(a => a.Vehicle)
            .Include(a => a.Company)
            .Where(a => a.IsActive && a.EndDate > DateTime.UtcNow)
            .OrderByDescending(a => a.CreatedAt)
            .Take(6)
            .ToListAsync();

        var recentVehicles = await _context.Vehicles
            .Where(v => v.IsActive)
            .OrderByDescending(v => v.CreatedAt)
            .Take(6)
            .ToListAsync();

        ViewBag.ActiveAuctionsList = activeAuctions;
        ViewBag.RecentVehicles = recentVehicles;

        var viewModel = new HomeViewModel
        {
            ActiveAuctions = activeAuctions,
            RecentVehicles = recentVehicles
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class HomeViewModel
{
    public List<Auction> ActiveAuctions { get; set; } = new List<Auction>();
    public List<Vehicle> RecentVehicles { get; set; } = new List<Vehicle>();
}

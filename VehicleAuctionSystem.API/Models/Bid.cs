using System;

namespace VehicleAuctionSystem.API.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
        public string? Status { get; set; } // Active, Won, Lost
        
        // Navigation properties
        public int AuctionId { get; set; }
        public Auction? Auction { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
} 
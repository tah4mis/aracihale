using System;
using System.Collections.Generic;

namespace VehicleAuctionSystem.API.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public string? Status { get; set; } // Active, Completed, Cancelled
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<User>? Participants { get; set; }
        public ICollection<Bid>? Bids { get; set; }
    }
} 
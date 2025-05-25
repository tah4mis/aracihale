using System;
using System.Collections.Generic;

namespace VehicleAuctionSystem.API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public ICollection<User>? Users { get; set; }
        public ICollection<Vehicle>? Vehicles { get; set; }
        public ICollection<Auction>? Auctions { get; set; }
    }
} 
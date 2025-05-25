using System;
using System.Collections.Generic;

namespace VehicleAuctionSystem.API.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? LicensePlate { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // Available, Sold, InAuction
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<Auction>? Auctions { get; set; }
        public ICollection<VehicleImage>? Images { get; set; }
    }
} 
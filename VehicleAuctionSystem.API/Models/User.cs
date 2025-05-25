using System;
using System.Collections.Generic;

namespace VehicleAuctionSystem.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; } // Admin, Company, User
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<Auction>? ParticipatedAuctions { get; set; }
        public ICollection<Bid>? Bids { get; set; }
    }
} 
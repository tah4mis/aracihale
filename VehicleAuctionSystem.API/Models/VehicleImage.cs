using System;

namespace VehicleAuctionSystem.API.Models
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime UploadDate { get; set; }
        
        // Navigation property
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
} 
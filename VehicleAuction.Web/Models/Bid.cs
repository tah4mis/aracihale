using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleAuction.Web.Models
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AuctionId { get; set; }

        [ForeignKey("AuctionId")]
        public Auction Auction { get; set; } = null!;

        [Required]
        public int BidderId { get; set; }

        [ForeignKey("BidderId")]
        public User Bidder { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string BidderName { get; set; } = string.Empty;

        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string BidderPhone { get; set; } = string.Empty;

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string BidderEmail { get; set; } = string.Empty;
    }
} 
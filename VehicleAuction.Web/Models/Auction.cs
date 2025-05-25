using System.ComponentModel.DataAnnotations;

namespace VehicleAuction.Web.Models
{
    public class Auction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen bir araç seçin")]
        [Display(Name = "Araç")]
        public int? VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        [Required(ErrorMessage = "Başlangıç fiyatı zorunludur")]
        [Display(Name = "Başlangıç Fiyatı")]
        [Range(0, double.MaxValue, ErrorMessage = "Başlangıç fiyatı 0'dan büyük olmalıdır")]
        public decimal StartingPrice { get; set; }

        [Required(ErrorMessage = "Minimum artış miktarı zorunludur")]
        [Display(Name = "Minimum Artış Miktarı")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum artış miktarı 0'dan büyük olmalıdır")]
        public decimal MinimumIncrement { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur")]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi zorunludur")]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Durum")]
        public AuctionStatus Status { get; set; } = AuctionStatus.Pending;

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum AuctionStatus
    {
        [Display(Name = "Beklemede")]
        Pending,
        [Display(Name = "Aktif")]
        Active,
        [Display(Name = "Tamamlandı")]
        Completed,
        [Display(Name = "İptal Edildi")]
        Cancelled
    }
} 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleAuction.Web.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Şirket adı en fazla 100 karakter olabilir.")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vergi numarası zorunludur.")]
        [StringLength(10, ErrorMessage = "Vergi numarası 10 karakter olmalıdır.")]
        [Column(TypeName = "nvarchar(10)")]
        public string TaxNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vergi dairesi zorunludur.")]
        [StringLength(100, ErrorMessage = "Vergi dairesi en fazla 100 karakter olabilir.")]
        [Column(TypeName = "nvarchar(100)")]
        public string TaxOffice { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir.")]
        [Column(TypeName = "nvarchar(20)")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir.")]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        // Banka Bilgileri
        [Display(Name = "Banka Adı")]
        public string? BankName { get; set; }

        [Display(Name = "IBAN")]
        [RegularExpression(@"^TR\d{2}\s?(\d{4}\s?){5}\d{2}$", ErrorMessage = "Geçerli bir IBAN numarası giriniz.")]
        public string? IBAN { get; set; }

        [Display(Name = "Hesap Sahibi")]
        public string? AccountHolder { get; set; }

        [Display(Name = "Şube Kodu")]
        public string? BranchCode { get; set; }

        [Display(Name = "Hesap Numarası")]
        public string? AccountNumber { get; set; }

        // Navigation properties
        public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }

    public class CompanyLoginViewModel
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }

    public class CompanyEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Şirket adı en fazla 100 karakter olabilir.")]
        [Display(Name = "Şirket Adı")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vergi numarası zorunludur.")]
        [StringLength(10, ErrorMessage = "Vergi numarası 10 karakter olmalıdır.")]
        [Display(Name = "Vergi Numarası")]
        public string TaxNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vergi dairesi zorunludur.")]
        [StringLength(100, ErrorMessage = "Vergi dairesi en fazla 100 karakter olabilir.")]
        [Display(Name = "Vergi Dairesi")]
        public string TaxOffice { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(500, ErrorMessage = "Adres en fazla 500 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [StringLength(20, ErrorMessage = "Telefon numarası en fazla 20 karakter olabilir.")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100, ErrorMessage = "E-posta adresi en fazla 100 karakter olabilir.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        // Banka Bilgileri
        [Display(Name = "Banka Adı")]
        public string? BankName { get; set; }

        [Display(Name = "IBAN")]
        [RegularExpression(@"^TR\d{2}\s?(\d{4}\s?){5}\d{2}$", ErrorMessage = "Geçerli bir IBAN numarası giriniz.")]
        public string? IBAN { get; set; }

        [Display(Name = "Hesap Sahibi")]
        public string? AccountHolder { get; set; }

        [Display(Name = "Şube Kodu")]
        public string? BranchCode { get; set; }

        [Display(Name = "Hesap Numarası")]
        public string? AccountNumber { get; set; }
    }
} 
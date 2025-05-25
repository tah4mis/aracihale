using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleAuction.Web.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Banka adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Banka adı en fazla 100 karakter olabilir.")]
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; } = string.Empty;

        [Required(ErrorMessage = "IBAN zorunludur.")]
        [StringLength(34, ErrorMessage = "IBAN en fazla 34 karakter olabilir.")]
        [Column(TypeName = "nvarchar(34)")]
        public string IBAN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hesap numarası zorunludur.")]
        [StringLength(20, ErrorMessage = "Hesap numarası en fazla 20 karakter olabilir.")]
        [Column(TypeName = "nvarchar(20)")]
        public string AccountNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şube kodu zorunludur.")]
        [StringLength(10, ErrorMessage = "Şube kodu en fazla 10 karakter olabilir.")]
        [Column(TypeName = "nvarchar(10)")]
        public string BranchCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hesap sahibi adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Hesap sahibi adı en fazla 100 karakter olabilir.")]
        [Column(TypeName = "nvarchar(100)")]
        public string AccountHolderName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
} 
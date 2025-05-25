using System;
using System.Collections.Generic;

namespace VehicleAuction.Web.TempModels;

public partial class BankAccount
{
    public int Id { get; set; }

    public string BankName { get; set; } = null!;

    public string Iban { get; set; } = null!;

    public string? BranchCode { get; set; }

    public string? AccountNumber { get; set; }

    public string AccountHolder { get; set; } = null!;

    public bool IsDefault { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}

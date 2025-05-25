using System;
using System.Collections.Generic;

namespace VehicleAuction.Web.TempModels;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public int UserType { get; set; }

    public int? CompanyId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

    public virtual ICollection<Bid> BidUserId1Navigations { get; set; } = new List<Bid>();

    public virtual ICollection<Bid> BidUsers { get; set; } = new List<Bid>();

    public virtual Company? Company { get; set; }

    public virtual Company? CompanyNavigation { get; set; }
}

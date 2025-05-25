using System;
using System.Collections.Generic;

namespace VehicleAuction.Web.TempModels;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TaxNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? AuthorizedPerson { get; set; }

    public string? AuthorizedPersonPhone { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public int UserId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

using System;
using System.Collections.Generic;

namespace VehicleAuction.Web.TempModels;

public partial class Bid
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Status { get; set; }

    public int? UserId1 { get; set; }

    public virtual Auction Auction { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual User? UserId1Navigation { get; set; }
}

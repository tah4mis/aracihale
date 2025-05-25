using System;
using System.Collections.Generic;

namespace VehicleAuction.Web.TempModels;

public partial class Auction
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public decimal StartingPrice { get; set; }

    public decimal MinimumIncrement { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}

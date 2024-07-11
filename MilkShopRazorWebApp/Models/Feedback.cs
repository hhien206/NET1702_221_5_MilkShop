using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public int? Rating { get; set; }

    public string? Description { get; set; }

    public DateTime? DatePost { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}

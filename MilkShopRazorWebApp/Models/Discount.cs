using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public double? DiscountPercent { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

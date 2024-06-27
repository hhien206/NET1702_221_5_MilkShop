using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? TotalAmount { get; set; }

    public string? Status { get; set; }

    public double? TotalPrice { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? NameReceiver { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

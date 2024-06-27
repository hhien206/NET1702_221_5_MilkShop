using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class PreOrder
{
    public int PreOrderId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? PreOrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int? CustomerId { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}

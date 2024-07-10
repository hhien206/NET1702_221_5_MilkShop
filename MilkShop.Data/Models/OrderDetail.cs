using System;
using System.Collections.Generic;

namespace MilkShop.Data.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public double? UnitPrice { get; set; }

    public int? Quantity { get; set; }

    public double? Total { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}

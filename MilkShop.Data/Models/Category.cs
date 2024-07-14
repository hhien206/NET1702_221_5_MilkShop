using System;
using System.Collections.Generic;

namespace MilkShop.Data.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? ProductId { get; set; }

    public string? CategoryName { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

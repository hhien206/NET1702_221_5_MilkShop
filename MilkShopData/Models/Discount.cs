﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MilkShopData.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    //public int? ProductId { get; set; }

    public double? DiscountPercent { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
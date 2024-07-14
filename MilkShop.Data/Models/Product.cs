using System;
using System.Collections.Generic;

namespace MilkShop.Data.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public int? Quantity { get; set; }

    public int? DiscountId { get; set; }

    public string? Brand { get; set; }

    public string? Origin { get; set; }

    public double? Weight { get; set; }

    public DateTime? DateOfManufacture { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string? Warning { get; set; }

    public string? Producer { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<PreOrder> PreOrders { get; set; } = new List<PreOrder>();
}

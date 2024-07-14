using System;
using System.Collections.Generic;

namespace MilkShop.Data.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? AccountId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? Dob { get; set; }

    public bool? Gender { get; set; }

    public int? Point { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PreOrder> PreOrders { get; set; } = new List<PreOrder>();
}

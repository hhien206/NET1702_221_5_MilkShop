using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? AccountRoleId { get; set; }

    public virtual AccountRole? AccountRole { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

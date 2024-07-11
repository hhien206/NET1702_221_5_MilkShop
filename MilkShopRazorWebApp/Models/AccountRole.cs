using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class AccountRole
{
    public int AccountRoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}

using System;
using System.Collections.Generic;

namespace MilkShop.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? UserRoleId { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual UserRole? UserRole { get; set; }
}

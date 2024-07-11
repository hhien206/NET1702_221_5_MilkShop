﻿using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

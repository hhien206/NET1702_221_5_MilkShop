﻿using System;
using System.Collections.Generic;

namespace MilkShopRazorWebApp.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public string? MetaKeywords { get; set; }

    public byte? Status { get; set; }

    public int? SortOrder { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }
}

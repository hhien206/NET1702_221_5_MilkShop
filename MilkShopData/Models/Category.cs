﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MilkShopData.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public int? ParentCategoryID { get; set; }
    public string ImageURL { get; set; }
    public string MetaKeywords { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool Status { get; set; }
    public int SortOrder { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.DTO
{
    public class DiscountDTO
    {
        public int DiscountId { get; set; }

        public double? DiscountPercent { get; set; }

        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public string Status { get; set; }

        public string Condition { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string DiscountCode { get; set; }
        public int UsageLimit { get; set; }
    }
}

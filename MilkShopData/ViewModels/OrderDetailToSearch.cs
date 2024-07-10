using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.ViewModels
{
    public class OrderDetailToSearch
    {
        public float UnitPriceMin { get; set; }
        public float UnitPriceMax { get; set;}
        public float TotalPriceMin { get; set; }
        public float TotalPriceMax { get; set;}
    }
}

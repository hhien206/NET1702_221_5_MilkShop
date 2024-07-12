using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.ViewModels
{
    public class OrderDetailToUpdate
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public float? UnitPrice { get; set; }
        public int? Quantity { get; set; }
    }
}

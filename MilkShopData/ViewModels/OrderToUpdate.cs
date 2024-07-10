using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.ViewModels
{
    public class OrderToUpdate
    {
        public int? CustomerId { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NameReceiver { get; set; }
    }
}

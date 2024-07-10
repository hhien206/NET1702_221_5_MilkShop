using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        public OrderDetailRepository()
        {

        }
        public async Task CalculateTotal(OrderDetail orderDetail)
        {
            if(orderDetail.UnitPrice == null || orderDetail.Quantity == null)
            {
                return;
            }
            orderDetail.Total = orderDetail.UnitPrice * orderDetail.Quantity;
        }
    }
}

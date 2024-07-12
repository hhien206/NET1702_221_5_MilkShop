using Microsoft.EntityFrameworkCore;
using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.Repository
{
    public class OrderRepository:GenericRepository<Order>
    {
        public OrderRepository()
        {

        }

        public void CreateOrder(Order order)
        {
             order.DateCreate = DateTime.Now;
             Create(order);
        }
        public async Task CreateOrderAsync(Order order)
        {
            order.DateCreate = DateTime.Now;
            await CreateAsync(order);
        }

        public void UpdateOrder(Order order)
        {
            order.DateUpdate = DateTime.Now;
            Update(order);
        }
        public async Task UpdateOrderAsync(Order order)
        {
            order.DateUpdate = DateTime.Now;
            await UpdateAsync(order);
        }

        public async Task CalculatorTotalAmountAndPrice(int orderId, List<OrderDetail> list)
        {
            var order = GetById(orderId);
            if (order == null) return;
            int totalAmount = 0;
            float totalPrice = 0;
            foreach (OrderDetail orderDetail in list)
            {
                if(orderDetail.Quantity != null)
                {
                    totalAmount += (int)orderDetail.Quantity;
                }
                if(orderDetail.Total != null)
                {
                    totalPrice += (float)orderDetail.Total;
                }
            }

            order.TotalAmount = totalAmount;
            order.TotalPrice = totalPrice;
            await UpdateAsync(order);
        }
    }
}

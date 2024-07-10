using MilkShopBusiness.Base;
using MilkShopData.DAO;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBusiness.Category
{
    public class CurrencyBusiness
    {
        CurrencyDAO dao;
        public CurrencyBusiness()
        {
            dao = new CurrencyDAO();
        }
        public async Task<BusinessResult> GetAllOrder()
        {
            try
            {
                var list = await dao.GetAllAsync();
                List<Order> listOrder = new List<Order>();
                foreach (var item in list)
                {
                    listOrder.Add(item);
                }
                return new BusinessResult(1, "successful", listOrder);
            }
            catch (Exception)
            {
                return new BusinessResult();
            }
        }
    }
}

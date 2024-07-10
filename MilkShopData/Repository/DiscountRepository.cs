using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.Repository
{
    public class DiscountRepository : GenericRepository<Discount>
    {
        private readonly NET1702_PRN221_MilkShopContext _dbContext;

        public DiscountRepository()
        {
            //_dbProductSet = _context.Set<Product>();
            _dbContext = new NET1702_PRN221_MilkShopContext();
        }
        public DiscountRepository(NET1702_PRN221_MilkShopContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

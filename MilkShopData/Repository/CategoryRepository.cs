using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository()
        {

        }
        public CategoryRepository(NET1702_PRN221_MilkShopContext context) => _context = context;

        ////TO-DO CODE HERE/////////////////
    }
}

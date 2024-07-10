using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopData.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository()
        {
            //write me updateCustomer repository here


        }


        public CustomerRepository(NET1702_PRN221_MilkShopContext context) => _context = context;
    }
}

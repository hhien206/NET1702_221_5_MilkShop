using Microsoft.EntityFrameworkCore;
using MilkShop.common;
using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections;
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
            


        }

        //=================================================================
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            var tracker = _context.Update(customer);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> SearchCustomer(string value, int? pageIndex, int pageSize)
        {
            value = value.Trim().ToLower();
            DateOnly dateValue;
            bool isValidDate = DateOnly.TryParse(value, out dateValue);

            var customersQuery = _context.Customers
                                        .OrderByDescending(m => m.CustomerId)
                                        .Where(m =>

                                            m.Name.Contains(value) ||
                                            m.Email.Contains(value) ||
                                            m.Address.Contains(value) ||
                                            m.PhoneNumber.Contains(value));

            var paginatedCustomers = await PaginatedList<Customer>.CreateAsync(customersQuery.AsNoTracking(), pageIndex ?? 1, pageSize);
            return paginatedCustomers;
        }

        public bool CustomerExisted(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        public IEnumerable GetAllMentor()
        {
            return _context.Customers;
        }

        public async Task<PaginatedList<Customer>> GetCustomerPagingAsync(int? pageIndex, int pageSize)
        {
            var paginatedCustomer = await PaginatedList<Customer>.CreateAsync(
                _context.Customers.AsNoTracking().OrderByDescending(m => m.CustomerId), pageIndex ?? 1, pageSize);
            return paginatedCustomer;
        }

        //=================================================================


        public CustomerRepository(NET1702_PRN221_MilkShopContext context) => _context = context;
    }
}

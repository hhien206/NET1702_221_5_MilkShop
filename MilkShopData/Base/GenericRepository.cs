using Microsoft.EntityFrameworkCore;
using MilkShop.common;
using MilkShopData.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MilkShopData.Base
{
    public class GenericRepository<T> where T : class
    {
        protected NET1702_PRN221_MilkShopContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context = new NET1702_PRN221_MilkShopContext();
            _dbSet = _context.Set<T>();
        }

        #region Separating asign entity and save operators

        public GenericRepository(NET1702_PRN221_MilkShopContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void PrepareCreate(T entity)
        {
            _dbSet.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<List<Category>> GetAllCategory()
        {
            var list = await _context.Categories.ToListAsync();
            return list;
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public async Task<int> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync();
        }

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

        public async Task<int> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            //tracker.State = EntityState.Modified;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        public async Task<int> RemoveCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetById(string code)
        {
            return _dbSet.Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _dbSet.FindAsync(code);
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
    }
}

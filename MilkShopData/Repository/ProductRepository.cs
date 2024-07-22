using Microsoft.EntityFrameworkCore;
using MilkShopData.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MilkShopData.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        //private readonly DbSet<Product> _dbProductSet;
        private readonly NET1702_PRN221_MilkShopContext _dbContext;

        public ProductRepository() 
        {
            //_dbProductSet = _context.Set<Product>();
            _dbContext = new NET1702_PRN221_MilkShopContext();
        }
        public ProductRepository(NET1702_PRN221_MilkShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> GetProductByName(string Product, string Category, double Discount)
        {
            var query = _dbContext.Products.AsQueryable();
            if (Product != null)
            {
                query = query.Where(p => p.Name.Contains(Product));
            }
            if (Category != null)
            {
                query = query.Where(p => p.Category.CategoryName.Contains(Category));
            }

            if (Discount != 0)
            {
               
                query = query.Where(p => p.Discount.DiscountPercent == Discount);
                
            }
            var products = await query
    .Include(p => p.Category)
    .Include(p => p.Discount)
    .ToListAsync();
            return products;
        //    }else {
        //        return await _dbContext.Products
        //.Where(p => p.Name.Contains(Product) &&
        //            p.Category.CategoryName.Contains(Category) &&
        //            p.Discount.DiscountPercent == Discount)
        //.Include(p => p.Category)
        //.Include(p => p.Discount)
        //.ToListAsync();
        //    }


        }
        public async Task<Product> GetProductByIdInclude(int id)
        {
            return await _dbContext.Products.Where(p => p.ProductId== id )
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .AsNoTracking()
                .SingleOrDefaultAsync();

        }


        public async Task<List<Product>> GetAllProduct()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .AsNoTracking()
                .ToListAsync();

        }
    }
}

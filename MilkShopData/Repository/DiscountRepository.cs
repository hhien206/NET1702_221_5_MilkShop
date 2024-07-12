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
    public class DiscountRepository : GenericRepository<Discount>
    {
        public DiscountRepository()
        {

        }

        public DiscountRepository(NET1702_PRN221_MilkShopContext context) => _context = context;

        public async Task<List<Discount>> GetByPercentAsync(float percent)
        {
            return await _dbSet.Where(m => m.DiscountPercent == percent).ToListAsync();
        }


        public async Task<List<Discount>> GetDiscountsByDiscountIDAsync(int id)
        {
            return await _dbSet.Where(d => d.DiscountId == id).ToListAsync();
        }
        public async Task<Discount> GetDiscountsByIDAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.DiscountId == id);
        }

        public async Task<List<Discount>> GetDiscountAsync(string searchDiscountID, string searchDiscountPercent, DateTime? searchFromDate)
        {
            if (string.IsNullOrEmpty(searchDiscountID) && string.IsNullOrEmpty(searchDiscountPercent) && searchFromDate == null)
            {
                throw new ArgumentNullException("Các tham số tìm kiếm không được null cùng một lúc.");
            }
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchDiscountID))
            {
                int discountID = int.Parse(searchDiscountID);
                query = query.Where(d => d.DiscountId == discountID);
            }
            if (!string.IsNullOrEmpty(searchDiscountPercent))
            {
                double percent = double.Parse(searchDiscountPercent);
                query = query.Where(d => d.DiscountPercent ==  percent);
            }
            if(searchFromDate != null)
            {
                query = query.Where(d => d.FromDate == searchFromDate);
            }
            return await query.ToListAsync();




            /*List<Discount> listSearchDiscountID = new List<Discount>();
            List<Discount> listSearchDiscountPercent = new List<Discount>();
            List<Discount> listSearchFromDate = new List<Discount>();
            List<Discount> listDiscount = new List<Discount>();
            if (!string.IsNullOrEmpty(searchDiscountID))
            {
                int discountID = int.Parse(searchDiscountID);
                listSearchDiscountID = query.Where(d => d.DiscountId == discountID).ToList();
                foreach (var discount in listSearchDiscountID)
                {
                    listDiscount.Add(discount);
                }
            }

            if (!string.IsNullOrEmpty(searchDiscountPercent))
        {
                double discountPercent = double.Parse(searchDiscountPercent);
                listSearchDiscountPercent = query.Where(d => d.DiscountPercent == discountPercent).ToList();
                foreach (var discount in listSearchDiscountPercent)
                {
                    listDiscount.Add(discount);
        }
            }
            if (searchFromDate.HasValue)
            {
                listSearchFromDate = query.Where(d => d.FromDate == searchFromDate).ToList();
                foreach (var discount in listSearchFromDate)
        {
                    listDiscount.Add(discount);
                }
            }

            return listDiscount.Distinct().ToList();*/
        }
    }
}

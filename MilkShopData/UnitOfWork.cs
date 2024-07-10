using MilkShopData.Models;
using MilkShopData.Repository;

namespace MilkShopData
{
    public class UnitOfWork
    {
        private NET1702_PRN221_MilkShopContext _context;
        private CategoryRepository _category;
        private ProductRepository _productRepository;
        private DiscountRepository _discountRepository;
        public UnitOfWork()
        {
            _context ??= new NET1702_PRN221_MilkShopContext();
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                return _category ??= new CategoryRepository(_context);
            }
        }
        public ProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new Repository.ProductRepository(_context);
            }
        }
        public DiscountRepository DiscountRepository
        {
            get
            {
                return _discountRepository ??= new Repository.DiscountRepository(_context);
            }
        }
    }
}

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
        private OrderRepository _order;
        private OrderDetailRepository _orderDetail;
        private DiscountRepository _discount;
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
      
        public OrderRepository OrderRepository
        {
            get
            {
                return _order ??= new Repository.OrderRepository();
            }
        }
        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                return _orderDetail ??= new Repository.OrderDetailRepository();
            }
        }
        public DiscountRepository DiscountRepository
        {
            get
            {
                return _discount ??= new DiscountRepository(_context);
                //return _discount ??= new DiscountRepository(_context);
            }

        }
    }
}

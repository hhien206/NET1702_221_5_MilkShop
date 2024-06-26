﻿using MilkShopData.Models;
using MilkShopData.Repository;

namespace MilkShopData
{
    public class UnitOfWork
    {
        private NET1702_PRN221_MilkShopContext _context;
        private CategoryRepository _category;
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
    }
}

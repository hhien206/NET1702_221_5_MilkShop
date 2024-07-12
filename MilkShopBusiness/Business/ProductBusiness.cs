using MilkShopBusiness.Base;
using MilkShopData;
using MilkShopData.DAO;
using MilkShopData.Models;
using MilkShop.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MilkShopBussiness.Business
{
    public interface IProductBusiness
    {
        Task<IMilkShopResult> GetAll();
        Task<IMilkShopResult> GetByIdNomall(int id);
        Task<IMilkShopResult> GetProductByIdInclude(int id);
        Task<IMilkShopResult> GetByName(string Product, string Category, string Discount);
        Task<IMilkShopResult> Save(Product Product);
        Task<IMilkShopResult> Update(Product Product);
        Task<IMilkShopResult> DeleteById(int id);
    }
    public class ProductBusiness : IProductBusiness
    {
        //private readonly ProductDAO _DAO;        
        //private readonly ProductRepository _ProductRepository;
        private readonly UnitOfWork _unitOfWork;

        public ProductBusiness()
        {
            //_ProductRepository ??= new ProductRepository();            
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IMilkShopResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                var products = await _unitOfWork.ProductRepository.GetAllProduct();


                if (products == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, products);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IMilkShopResult> GetProductByIdInclude(int id)
        {
            try
            {
                #region Business rule
                #endregion

                //var Product = await _ProductRepository.GetByIdAsync(code);
                var Product = await _unitOfWork.ProductRepository.GetProductByIdInclude(id);

                if (Product == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Product);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IMilkShopResult> GetByIdNomall(int id)
        {
            try
            {
                #region Business rule
                #endregion

                //var Product = await _ProductRepository.GetByIdAsync(code);
                var Product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

                if (Product == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Product);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IMilkShopResult> GetByName(string Product, string Category, string Discount)
        {
            try
            {
                #region Business rule
                #endregion
                //if (Discount != null && !double.TryParse(Discount, out double discountValue))
                //{
                //    return new MilkShopResult(Const.ERROR_EXCEPTION, "DiscountPercent is not a valid number");
                //    //throw new ArgumentException("DiscountPercent is not a valid number");
                //}
                //var products = await _unitOfWork.ProductRepository.GetProductByName(Product, Category, discountValue);
                //var category
                double discountValue = 0;
                if (Discount != null && !double.TryParse(Discount, out  discountValue))
                {
                    return new MilkShopResult(Const.ERROR_EXCEPTION, "DiscountPercent is not a valid number");
                    //throw new ArgumentException("DiscountPercent is not a valid number");
                }
                var products = await _unitOfWork.ProductRepository.GetProductByName(Product, Category, discountValue);


                if (products == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, products);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IMilkShopResult> Save(Product Product)
        {
            try
            {
                //int result = await _ProductRepository.CreateAsync(Product);
                int result = await _unitOfWork.ProductRepository.CreateAsync(Product);
                if (result > 0)
                {
                    return new MilkShopResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new MilkShopResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IMilkShopResult> Update(Product Product)
        {
            try
            {
                //int result = await _ProductRepository.UpdateAsync(Product);
                int result = await _unitOfWork.ProductRepository.UpdateAsync(Product);

                if (result > 0)
                {
                    return new MilkShopResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new MilkShopResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(-4, ex.ToString());
            }
        }

        public async Task<IMilkShopResult> DeleteById(int id)
        {
            try
            {
                //var Product = await _ProductRepository.GetByIdAsync(code);
                var Product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                if (Product != null)
                {
                    //var result = await _ProductRepository.RemoveAsync(Product);
                    var result = await _unitOfWork.ProductRepository.RemoveAsync(Product);
                    if (result)
                    {
                        return new MilkShopResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new MilkShopResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(-4, ex.ToString());
            }
        }

    }
}

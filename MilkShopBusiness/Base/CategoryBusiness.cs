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

namespace MilkShopBusiness.Base
{
        public interface ICategoryBusiness
        {
            Task<IMilkShopResult> GetAll();
            Task<IMilkShopResult> GetById(int code);
            Task<IMilkShopResult> Save(Category category);
            Task<IMilkShopResult> Update(Category category);
            Task<IMilkShopResult> DeleteById(int code);
        }

    public class CategoryBusiness : ICategoryBusiness
    {
        //private readonly CategoryDAO _DAO;        
        //private readonly CategoryRepository _CategoryRepository;
        private readonly UnitOfWork _unitOfWork;

        public CategoryBusiness()
        {
            //_CategoryRepository ??= new CategoryRepository();            
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IMilkShopResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _CategoryRepository.GetAllAsync();
                var currencies = await _unitOfWork.CategoryRepository.GetAllCategory();


                if (currencies == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, currencies);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var list = await _unitOfWork.CategoryRepository.GetAllAsync();
            return list;
            
        }

        public async Task<IMilkShopResult> GetById(int id)
        {
            try
            {
                #region Business rule
                #endregion

                //var Category = await _CategoryRepository.GetByIdAsync(code);
                var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

                if (Category == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Category);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
 

        public async Task<IMilkShopResult> Save(Category category)
        {
            int result = await _unitOfWork.CategoryRepository.AddCategoryAsync(category);
            if (result > 0)
            {
                return new MilkShopResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            else
            {
                return new MilkShopResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
            try
            {
                //int result = await _DiscountRepository.CreateAsync(Discount);
               
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IMilkShopResult> Update(Category Category)
        {
            //int result = await _CategoryRepository.UpdateAsync(Category);
            int result = await _unitOfWork.CategoryRepository.UpdateAsync(Category);

            if (result > 0)
            {
                return new MilkShopResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }
            else
            {
                return new MilkShopResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
            try
            {
               
            }
            catch (Exception ex)
            {
                return new MilkShopResult(-4, ex.ToString());
            }
        }

        public async Task<IMilkShopResult> DeleteById(int code)
        {
            //var Category = await _CategoryRepository.GetByIdAsync(code);
            var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(code);
            if (Category != null)
            {
                //var result = await _CategoryRepository.RemoveAsync(Category);
                var result = await _unitOfWork.CategoryRepository.RemoveCategoryAsync(Category);
                if (result>0)
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
            try
            {
              
            }
            catch (Exception ex)
            {
                return new MilkShopResult(-4, ex.ToString());
            }
        }

    }
}

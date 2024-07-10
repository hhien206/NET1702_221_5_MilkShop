using MilkShopBusiness.Base;
using MilkShopData;
using MilkShop.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBussiness.Business
{
    public interface IDiscountBusiness
    {
        Task<IMilkShopResult> GetAll();
    }
    public class DiscountBusiness : IDiscountBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public DiscountBusiness()
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

                var discount = await _unitOfWork.DiscountRepository.GetAllAsync();


                if (discount == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, discount);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}

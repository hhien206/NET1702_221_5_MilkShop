using MilkShopBusiness.Base;
using MilkShopData;
using MilkShopData.DAO;
using MilkShopData.DTO;
using MilkShopData.Models;
using MilkShop.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBusiness.Business
{
    public interface IDiscountBusiness
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetAllDiscount();
        Task<IBusinessResult> GetByID(int id);
        Task<IBusinessResult> Save(Discount discount);
        Task<IBusinessResult> Update(Discount discount);
        Task<IBusinessResult> DeleteById(int id);
        Task<IBusinessResult> GetDiscountAsync(string searchDiscountID, string searchDiscountPercent, DateTime? searchFromDate);
    }

    public class DiscountBusiness : IDiscountBusiness
    {
        //private readonly DiscountDAO _DAO;        
        //private readonly DiscountRepository _DiscountRepository;
        private readonly UnitOfWork _unitOfWork;

        public DiscountBusiness()
        {
            //_DiscountRepository ??= new DiscountRepository();            
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _DiscountRepository.GetAllAsync();
                var discount = await _unitOfWork.DiscountRepository.GetAllAsync();
                List<DiscountDTO> listDiscountDTO = new List<DiscountDTO>();
                foreach (var item in discount)
                {
                    var discountDTO = new DiscountDTO()
                    {
                        DiscountId = item.DiscountId,
                        DiscountPercent = item.DiscountPercent,
                        FromDate = item.FromDate.HasValue ? item.FromDate.Value.ToString("dd/MM/yyyy") : null,
                        ToDate = item.ToDate.HasValue ? item.ToDate.Value.ToString("dd/MM/yyyy") : null,
                        Status = item.Status,
                        Condition = item.Condition,
                        Type = item.Type,
                        Description = item.Description,
                        DiscountCode = item.DiscountCode,
                        UsageLimit = item.UsageLimit
                    };
                    listDiscountDTO.Add(discountDTO);
                }

                if (discount == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, listDiscountDTO);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetByID(int id)
        {
            try
            {
                #region Business rule
                #endregion

                //var Discount = await _DiscountRepository.GetByIdAsync(code);
                var Discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);

                if (Discount == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Discount);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(Discount Discount)
        {
            try
            {
                //int result = await _DiscountRepository.CreateAsync(Discount);
                int result = await _unitOfWork.DiscountRepository.CreateAsync(Discount);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(Discount Discount)
        {
            try
            {
                //int result = await _DiscountRepository.UpdateAsync(Discount);
                int result = await _unitOfWork.DiscountRepository.UpdateAsync(Discount);

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int id)
        {
            try
            {
                //var Discount = await _DiscountRepository.GetByIdAsync(code);
                var Discount = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
                if (Discount != null)
                {
                    //var result = await _DiscountRepository.RemoveAsync(Discount);
                    var result = await _unitOfWork.DiscountRepository.RemoveAsync(Discount);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetDiscountAsync(string searchDiscountID, string searchDiscountPercent, DateTime? searchFromDate)
        {
            try
            {
                #region Business rule
                #endregion

                //var Discount = await _DiscountRepository.GetByIdAsync(code);
                var Discount = await _unitOfWork.DiscountRepository.GetDiscountAsync(searchDiscountID, searchDiscountPercent, searchFromDate);
                List<DiscountDTO> listDiscountDTO = new List<DiscountDTO>();
                foreach (var item in Discount)
                {
                    var discountDTO = new DiscountDTO()
                    {
                        DiscountId = item.DiscountId,
                        DiscountPercent = item.DiscountPercent,
                        FromDate = item.FromDate.HasValue ? item.FromDate.Value.ToString("dd/MM/yyyy") : null,
                        ToDate = item.ToDate.HasValue ? item.ToDate.Value.ToString("dd/MM/yyyy") : null,
                        Status = item.Status,
                        Condition = item.Condition,
                        Type = item.Type,
                        Description = item.Description,
                        DiscountCode = item.DiscountCode,
                        UsageLimit = item.UsageLimit
                    };
                    listDiscountDTO.Add(discountDTO);
                }

                if (Discount == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, listDiscountDTO);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetAllDiscount()
        {
            try
            {
                #region Business rule
                #endregion

                //var currencies = _DAO.GetAll();
                //var currencies = await _DiscountRepository.GetAllAsync();
                var discount = await _unitOfWork.DiscountRepository.GetAllAsync();

                if (discount == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, discount);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}

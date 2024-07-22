using MilkShop.common;
using MilkShopData;
using MilkShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBusiness.Base
{
    public interface ICustomerBusiness
    {
        Task<IMilkShopResult> GetAll();
        Task<IMilkShopResult> GetById(int code);
        Task<IMilkShopResult> Save(Customer Customer);
        Task<IMilkShopResult> Update(Customer Customer);
        Task<IMilkShopResult> DeleteById(int code);
    }

    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomerBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IMilkShopResult> DeleteById(int code)
        {
            var Customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
            if (Customer != null)
            {
                Customer.Status = "Inactive"; // Change the Customer Status to "Inactive"
                var result = await _unitOfWork.CustomerRepository.UpdateAsync(Customer);
                if (result != null)
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

        public async Task<IMilkShopResult> GetAll()
        {
            try
            {
                var currencies = await _unitOfWork.CustomerRepository.GetAllAsync();

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

        public async Task<IMilkShopResult> GetById(int code)
        {
            try
            {
                #region Business rule
                #endregion

                
                var Customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);

                if (Customer == null)
                {
                    return new MilkShopResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Customer);
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IMilkShopResult> Save(Customer customer)
        {
            int result = await _unitOfWork.CustomerRepository.AddCustomerAsync(customer);
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
                

            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IMilkShopResult> Update(Customer Customer)
        {
            try
            {
                var updateCustomer = await _unitOfWork.CustomerRepository.UpdateAsync(Customer);
                if (updateCustomer != null)
                {
                    return new MilkShopResult(1, "Update successfully");
                }
                else
                {
                    return new MilkShopResult(-1, "Update fail");
                }
            }
            catch (Exception ex)
            {
                return new MilkShopResult(-4, ex.Message);
            }
        }


        public async Task<IMilkShopResult> DeleteAsync(int id)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
                if (customer != null)
                {
                    var result = await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                    if (result)
                        return new MilkShopResult(1, "success");
                    else
                        return new MilkShopResult(0, "error");
                }
                return new MilkShopResult(0, "no content");
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IMilkShopResult> SearchCustomer(string value1, string value2, string value3, int? pageIndex, int pageSize)
        {
            var customer = await _unitOfWork.CustomerRepository.SearchCustomer(value1, value2, value3, pageIndex, pageSize);
            try
            {
                if (customer == null)
                {
                    return new MilkShopResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, null);
                }
                return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

        public async Task<IMilkShopResult> GetAllCustomerPagingAsync(int? pageIndex, int pageSize)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomerPagingAsync(pageIndex, pageSize);
            try
            {
                if (customer == null)
                {
                    return new MilkShopResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, null);
                }
                return new MilkShopResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, customer);
            }
            catch (Exception ex)
            {
                return new MilkShopResult(Const.FAIL_READ_CODE, ex.ToString());
            }
        }

    }
}

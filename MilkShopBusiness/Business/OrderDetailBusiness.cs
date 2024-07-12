using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MilkShopBusiness.Base;
using MilkShop.common;
using MilkShopData;
using MilkShopData.Models;
using MilkShopData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBusiness.Business
{
    public class OrderDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderDetailBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<BusinessResult> GetAllOrderDetail()
        {
            try
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }
        public async Task<BusinessResult> GetOrderDetailById(int id)
        {
            try
            {
                var orderDetail = _unitOfWork.OrderDetailRepository.GetById(id);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }
        public async Task<BusinessResult> AddOrderDetail(OrderDetailToAdd key)
        {
            try
            {
                OrderDetail ordDetail = new OrderDetail
                {
                    OrderId = key.OrderId,
                    ProductId = key.ProductId,
                    UnitPrice = key.UnitPrice,
                    Quantity = key.Quantity,
                };
                await _unitOfWork.OrderDetailRepository.CalculateTotal(ordDetail);
                await _unitOfWork.OrderDetailRepository.CreateAsync(ordDetail);
                List<OrderDetail> ordDetailList = (await _unitOfWork.OrderDetailRepository.GetAllAsync()).FindAll(l => l.OrderId == key.OrderId);
                await _unitOfWork.OrderRepository.CalculatorTotalAmountAndPrice((int)key.OrderId, ordDetailList);
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }
        public async Task<BusinessResult> UpdateOrderDetail(int orderDetailId, OrderDetailToUpdate key)
        {
            try
            {
                var ordDetail = _unitOfWork.OrderDetailRepository.GetById(orderDetailId);
                ordDetail.UnitPrice = key.UnitPrice;
                ordDetail.Quantity = key.Quantity;
                await _unitOfWork.OrderDetailRepository.CalculateTotal(ordDetail);
                await _unitOfWork.OrderDetailRepository.UpdateAsync(ordDetail);
                List<OrderDetail> ordDetailList = (await _unitOfWork.OrderDetailRepository.GetAllAsync()).FindAll(l => l.OrderId == ordDetail.OrderId);
                await _unitOfWork.OrderRepository.CalculatorTotalAmountAndPrice((int)ordDetail.OrderId, ordDetailList);
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
        }
        public async Task<BusinessResult> DeleteOrderDetail(int orderDetailId)
        {
            try
            {
                var ordDetail = _unitOfWork.OrderDetailRepository.GetById(orderDetailId);
                await _unitOfWork.OrderDetailRepository.RemoveAsync(ordDetail);
                List<OrderDetail> ordDetailList = (await _unitOfWork.OrderDetailRepository.GetAllAsync()).FindAll(l => l.OrderId == ordDetail.OrderId);
                await _unitOfWork.OrderRepository.CalculatorTotalAmountAndPrice((int)ordDetail.OrderId, ordDetailList);
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            }
        }
        public async Task<List<OrderDetail>> SearchOrderDetail(OrderDetailToSearch key)
        {
            try
            {
                var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();
                if (key.UnitPriceMin >0) orderDetails = orderDetails.FindAll(l=>l.UnitPrice >= key.UnitPriceMin);
                if (key.UnitPriceMax >0) orderDetails = orderDetails.FindAll(l => l.UnitPrice <= key.UnitPriceMax);
                if (key.TotalPriceMin >0) orderDetails = orderDetails.FindAll(l => l.Total >= key.TotalPriceMin);
                if (key.TotalPriceMax >0) orderDetails = orderDetails.FindAll(l => l.Total <= key.TotalPriceMax);
                return orderDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<int> TotalPage(List<OrderDetail> list, int sizePage)
        {
            try
            {
                if (sizePage <= 0) return 0;
                return (list.Count() % sizePage == 0) ? list.Count() / sizePage : list.Count() / sizePage + 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public async Task<List<OrderDetail>> PagingOrderDetail(List<OrderDetail> list, int sizePage, int currentPage)
        {
            try
            {
                if (currentPage <= 0 && sizePage <= 0) return new List<OrderDetail>();
                List<OrderDetail> newList = new();
                for (int i = (currentPage - 1) * sizePage; i < currentPage * sizePage; i++)
                {
                    if (list.Count() > i)
                    {
                        newList.Add(list[i]);
                    }

                }
                return newList;
            }
            catch (Exception)
            {
                return new List<OrderDetail>();
            }
        }
    }
}

using MilkShopBusiness.Base;
using MilkShopData;
using MilkShopData.ViewModels;
using MilkShopData.Models;
using MilkShop.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkShopBusiness.Business
{
    public class OrderBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<BusinessResult> GetAllOrder()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
            }
            catch(Exception)
            {
                return new BusinessResult(Const.FAIL_READ_CODE,Const.FAIL_READ_MSG);
            }
        }
        public async Task<BusinessResult> GetOrderById(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, order);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
        }
        public async Task<BusinessResult> AddOrder(OrderToAdd key)
        {
            try
            {
                await _unitOfWork.OrderRepository.CreateOrderAsync(new Order
                {
                    CustomerId = key.CustomerId,
                    Address = key.Address,
                    PhoneNumber = key.PhoneNumber,
                    NameReceiver = key.NameReceiver,
                });
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
            }
        }
        public async Task<BusinessResult> UpdateOrder(int orderId, OrderToUpdate key)
        {
            try
            {
                var ord = _unitOfWork.OrderRepository.GetById(orderId);
                ord.CustomerId = key.CustomerId;
                ord.Address = key.Address;
                ord.PhoneNumber = key.PhoneNumber;
                ord.NameReceiver = key.NameReceiver;
                await _unitOfWork.OrderRepository.UpdateOrderAsync(ord);
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
            }
        }
        public async Task<BusinessResult> DeleteOrder(int orderId)
        {
            try
            {
                var ord = _unitOfWork.OrderRepository.GetById(orderId);
                await _unitOfWork.OrderRepository.RemoveAsync(ord);
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
            }
            catch (Exception)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            }
        }
        public async Task<List<Order>> SearchOrder(OrderToSearch key)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                if (key.Address != null) orders = orders.FindAll(l => l.Address != null && l.Address.Contains(key.Address));
                if (key.PhoneNumber != null) orders = orders.FindAll(l => l.PhoneNumber != null && l.PhoneNumber.Contains(key.PhoneNumber));
                if (key.NameReceiver != null) orders = orders.FindAll(l => l.NameReceiver != null && l.NameReceiver.Contains(key.NameReceiver));
                return orders;
            }
            catch(Exception)
            {
                return null;
            }
        }
        public async Task<int> TotalPage(List<Order> list, int sizePage)
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
        public async Task<List<Order>> PagingOrder(List<Order> list, int sizePage, int currentPage)
        {
            try
            {
                if (currentPage <=0 && sizePage<= 0) return new List<Order>();
                List<Order> newList = new();
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
                return new List<Order>();
            }
        }
    }
}

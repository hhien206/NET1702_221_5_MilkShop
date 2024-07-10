using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MilkShopBusiness.Category;
using MilkShopData.Models;
using MilkShopData.Repository;
using MilkShopData.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MilkShopRazorWebApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly OrderBusiness _orderBusiness;
        public Order order { get; set; }
        public OrderModel()
        {
            _orderBusiness = new OrderBusiness();
        }
        public List<Order>? Orders { get; set; }
        public int PageNumber;
        public int CurrentPageNumber;
        public static OrderToSearch? dataSearch = new();
        public async Task<IActionResult> OnGet()
        {
            OrderToSearch orderToSearch = new OrderToSearch();
            orderToSearch = null;
            if(TempData.ContainsKey("addressSearch") || TempData.ContainsKey("phoneNumberSearch") || TempData.ContainsKey("nameReceiverSearch"))
            {
                orderToSearch = new OrderToSearch()
                {
                    Address = (string)TempData["addressSearch"],
                    PhoneNumber = (string)TempData["phoneNumberSearch"],
                    NameReceiver = (string)TempData["nameReceiverSearch"],
                };
                dataSearch = orderToSearch;
            }
            if (orderToSearch!=null && TempData.ContainsKey("currentPageNumber"))
            {
                var data = await _orderBusiness.SearchOrder(orderToSearch);
                CurrentPageNumber = (int)TempData["currentPageNumber"];
                PageNumber = ((List<Order>?)data == null) ? 1 : await _orderBusiness.TotalPage((List<Order>?)data, 3);
                Orders = await _orderBusiness.PagingOrder((List<Order>?)data, 3, CurrentPageNumber);
            }
            else if (orderToSearch != null)
            {
                var data = await _orderBusiness.SearchOrder(orderToSearch);
                CurrentPageNumber = 1;
                PageNumber = ((List<Order>?)data == null) ? 1 : await _orderBusiness.TotalPage((List<Order>?)data, 3);
                Orders = await _orderBusiness.PagingOrder((List<Order>?)data, 3, CurrentPageNumber);
            }
            else if (TempData.ContainsKey("currentPageNumber"))
            {
                dataSearch = null;
                var data = await _orderBusiness.GetAllOrder();
                CurrentPageNumber = (int)TempData["currentPageNumber"];
                PageNumber = ((List<Order>?)data.Data == null) ? 1 : await _orderBusiness.TotalPage((List<Order>?)data.Data, 3);
                Orders = await _orderBusiness.PagingOrder((List<Order>?)data.Data,3,CurrentPageNumber);
            }
            else
            {
                dataSearch = null;
                var data = await _orderBusiness.GetAllOrder();
                CurrentPageNumber = 1;
                PageNumber = ((List<Order>?)data.Data == null) ? 1 : await _orderBusiness.TotalPage((List<Order>?)data.Data, 3);
                Orders = await _orderBusiness.PagingOrder((List<Order>?)data.Data, 3, CurrentPageNumber);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(OrderToAdd key)
        {
            await _orderBusiness.AddOrder(key);
            return RedirectToPage("/Order");
        }
        public async Task<IActionResult> OnPostUpdate(int orderId, OrderToUpdate key)
        {
            await _orderBusiness.UpdateOrder(orderId, key);
            return RedirectToPage("/Order");
        }
        public async Task<IActionResult> OnPostDelete(int orderId)
        {
            await _orderBusiness.DeleteOrder(orderId);
            return RedirectToPage("/Order");
        }
        public async Task<IActionResult> OnPostSearch(OrderToSearch key)
        {
            //TempData["dataSearch"] = phoneNumber;
            TempData["addressSearch"] = key.Address;
            TempData["phoneNumberSearch"] = key.PhoneNumber;
            TempData["nameReceiverSearch"] = key.NameReceiver;
            return RedirectToPage("/Order");
        }
        public async Task<IActionResult> OnPostPage(int number)
        {
            TempData["currentPageNumber"] = number;
            if(dataSearch!= null)
            {
                TempData["addressSearch"] = dataSearch.Address;
                TempData["phoneNumberSearch"] = dataSearch.PhoneNumber;
                TempData["nameReceiverSearch"] = dataSearch.NameReceiver;
            }
            return RedirectToPage("/Order");
        }
    }
}

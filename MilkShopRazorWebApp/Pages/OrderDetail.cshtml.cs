using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MilkShopBusiness.Category;
using MilkShopData.Models;
using MilkShopData.ViewModels;

namespace MilkShopRazorWebApp.Pages
{
    public class OrderDetailModel : PageModel
    {
        private readonly OrderDetailBusiness _orderDetailBusiness;
        public OrderDetail orderDetail { get; set; }
        public OrderDetailModel()
        {
            _orderDetailBusiness = new OrderDetailBusiness();
        }
        public List<OrderDetail>? OrderDetails { get; set; }
        public int PageNumber;
        public int CurrentPageNumber;
        public static OrderDetailToSearch? dataSearch = new();

        public async Task<IActionResult> OnGet()
        {
            OrderDetailToSearch orderDetailToSearch = new OrderDetailToSearch();
            orderDetailToSearch = null;
            if (TempData.ContainsKey("unitPriceMinSearch") || TempData.ContainsKey("unitPriceMaxSearch") || TempData.ContainsKey("totalPriceMinSearch") || TempData.ContainsKey("totalPriceMaxSearch"))
            {
                orderDetailToSearch = new OrderDetailToSearch()
                {
                    UnitPriceMin = float.Parse((string)TempData["unitPriceMinSearch"]),
                    UnitPriceMax = float.Parse((string)TempData["unitPriceMaxSearch"]),
                    TotalPriceMin = float.Parse((string)TempData["totalPriceMinSearch"]),
                    TotalPriceMax = float.Parse((string)TempData["totalPriceMaxSearch"]),
                };
                dataSearch = orderDetailToSearch;
            }
            if (orderDetailToSearch != null && TempData.ContainsKey("currentPageNumber"))
            {
                var data = await _orderDetailBusiness.SearchOrderDetail(orderDetailToSearch);
                CurrentPageNumber = (int)TempData["currentPageNumber"];
                PageNumber = ((List<OrderDetail>?)data == null) ? 1 : await _orderDetailBusiness.TotalPage((List<OrderDetail>?)data, 3);
                OrderDetails = await _orderDetailBusiness.PagingOrderDetail((List<OrderDetail>?)data, 3, CurrentPageNumber);
            }
            else if (orderDetailToSearch != null)
            {
                var data = await _orderDetailBusiness.SearchOrderDetail(orderDetailToSearch);
                CurrentPageNumber = 1;
                PageNumber = ((List<OrderDetail>?)data == null) ? 1 : await _orderDetailBusiness.TotalPage((List<OrderDetail>?)data, 3);
                OrderDetails = await _orderDetailBusiness.PagingOrderDetail((List<OrderDetail>?)data, 3, CurrentPageNumber);
            }
            else if (TempData.ContainsKey("currentPageNumber"))
            {
                dataSearch = null;
                var data = await _orderDetailBusiness.GetAllOrderDetail();
                CurrentPageNumber = (int)TempData["currentPageNumber"];
                PageNumber = ((List<OrderDetail>?)data.Data == null) ? 1 : await _orderDetailBusiness.TotalPage((List<OrderDetail>?)data.Data, 3);
                OrderDetails = await _orderDetailBusiness.PagingOrderDetail((List<OrderDetail>?)data.Data, 3, CurrentPageNumber);
            }
            else
            {
                dataSearch = null;
                var data = await _orderDetailBusiness.GetAllOrderDetail();
                CurrentPageNumber = 1;
                PageNumber = ((List<OrderDetail>?)data.Data == null) ? 1 : await _orderDetailBusiness.TotalPage((List<OrderDetail>?)data.Data, 3);
                OrderDetails = await _orderDetailBusiness.PagingOrderDetail((List<OrderDetail>?)data.Data, 3, CurrentPageNumber);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(OrderDetailToAdd key)
        {
            await _orderDetailBusiness.AddOrderDetail(key);
            return RedirectToPage("/OrderDetail");
        }
        public async Task<IActionResult> OnPostUpdate(int orderDetailId, OrderDetailToUpdate key)
        {
            await _orderDetailBusiness.UpdateOrderDetail(orderDetailId, key);
            return RedirectToPage("/OrderDetail");
        }
        public async Task<IActionResult> OnPostDelete(int orderDetailId)
        {
            await _orderDetailBusiness.DeleteOrderDetail(orderDetailId);
            return RedirectToPage("/OrderDetail");
        }
        public async Task<IActionResult> OnPostSearch(OrderDetailToSearch key)
        {
            TempData["unitPriceMinSearch"] = key.UnitPriceMin.ToString();
            TempData["unitPriceMaxSearch"] = key.UnitPriceMax.ToString();
            TempData["totalPriceMinSearch"] = key.TotalPriceMin.ToString();
            TempData["totalPriceMaxSearch"] = key.TotalPriceMax.ToString();
            return RedirectToPage("/OrderDetail");
        }
        public async Task<IActionResult> OnPostPage(int number)
        {
            TempData["currentPageNumber"] = number;
            if (dataSearch != null)
            {
                TempData["unitPriceMinSearch"] = dataSearch.UnitPriceMin.ToString();
                TempData["unitPriceMaxSearch"] = dataSearch.UnitPriceMax.ToString();
                TempData["totalPriceMinSearch"] = dataSearch.TotalPriceMin.ToString();
                TempData["totalPriceMaxSearch"] = dataSearch.TotalPriceMax.ToString();
            }
            return RedirectToPage("/OrderDetail");
        }
    }
}

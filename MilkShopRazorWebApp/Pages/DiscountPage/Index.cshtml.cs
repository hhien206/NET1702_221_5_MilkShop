using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MilkShopBusiness.Business;
using MilkShopData.DTO;
using MilkShopData.Models;
using MilkShopData.Repository;

namespace MilkShopRazorWebApp.Pages.DicountPage
{
    public class IndexModel : PageModel
    {
        //private readonly MilkShopData.Models.NET1702_PRN221_MilkShopContext _context;
        private readonly IDiscountBusiness business;
        public IndexModel()
        {
            //_context = context;
            business ??= new DiscountBusiness();
        }

        public IList<DiscountDTO> Discounts { get;set; } = default!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;
        public int TotalPages { get; set; }

        [BindProperty(Name = "SearchDiscountID", SupportsGet = true)] public string SearchDiscountID { get; set; }
        [BindProperty(Name = "SearchDiscountPercent", SupportsGet = true)] public string SearchDiscountPercent { get; set; }
        [BindProperty(Name = "SearchFromDate", SupportsGet = true)] public DateTime? SearchFromDate { get; set; }


        public async Task OnGetAsync(int? pageNumber)
        {
            PageNumber = pageNumber ?? 1;
            if(string.IsNullOrEmpty(SearchDiscountID) && string.IsNullOrEmpty(SearchDiscountPercent) && SearchFromDate == null)
            {
                var result = await business.GetAll();
                if (result.Status > 0 && result.Data != null)
                {
                    Discounts = result.Data as List<DiscountDTO>;
                    TotalPages = (int)Math.Ceiling(Discounts.Count / (double)PageSize);
                    Discounts = Discounts.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
                }
            }
            else
            {

                var result = await business.GetDiscountAsync(SearchDiscountID, SearchDiscountPercent, SearchFromDate);
                if (result.Status > 0 && result.Data != null)
                {
                    Discounts = result.Data as List<DiscountDTO>;
                    TotalPages = (int)Math.Ceiling(Discounts.Count / (double)PageSize);
                    Discounts = Discounts.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
                }
            }
        }
    }
}

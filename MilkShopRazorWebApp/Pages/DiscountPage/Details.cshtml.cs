using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkShopBusiness.Business;
using MilkShopData.Models;

namespace MilkShopRazorWebApp.Pages.DicountPage
{
    public class DetailsModel : PageModel
    {
        private readonly IDiscountBusiness business;

        public DetailsModel()
        {
            business ??= new DiscountBusiness();
        }

        public Discount Discount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await business.GetByID((int)id); 
            if (discount == null)
            {
                return NotFound();
            }
            else
            {
                Discount = discount.Data as Discount;
            }
            return Page();
        }
    }
}

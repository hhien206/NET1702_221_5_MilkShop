using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MilkShopBusiness.Business;
using MilkShopData.Models;

namespace MilkShopRazorWebApp.Pages.DicountPage
{
    public class CreateModel : PageModel
    {
        //private readonly MilkShopData.Models.NET1702_PRN221_MilkShopContext _context;
        private readonly IDiscountBusiness business;

        public CreateModel()
        {
            //_context = context;
            business ??= new DiscountBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Discount Discount { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Discount.FromDate.HasValue && Discount.ToDate.HasValue && Discount.FromDate.Value > Discount.ToDate.Value)
            {
                ModelState.AddModelError(string.Empty, "FromDate must be less than ToDate");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await business.Save(Discount);
            return RedirectToPage("./Index");
           
        }
    }
}

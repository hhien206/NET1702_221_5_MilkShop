using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MilkShopBusiness.Business;
using MilkShopData.Models;

namespace MilkShopRazorWebApp.Pages.DicountPage
{
    public class EditModel : PageModel
    {
        //private readonly MilkShopData.Models.NET1702_PRN221_MilkShopContext _context;
        private readonly IDiscountBusiness business;

        public EditModel()
        {
            //_context = context;
            business ??= new DiscountBusiness();
        }

        [BindProperty]
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
            Discount = discount.Data as Discount;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (Discount.FromDate.HasValue && Discount.ToDate.HasValue && Discount.FromDate.Value > Discount.ToDate.Value)
            {
                ModelState.AddModelError(string.Empty, "FromDate must be less than ToDate");
                return Page();
            }

            if (Discount == null)
            {
                return NotFound();
            }
            else
            {
                await business.Update(Discount);
            }
            
            return RedirectToPage("./Index");
        }
    }
}

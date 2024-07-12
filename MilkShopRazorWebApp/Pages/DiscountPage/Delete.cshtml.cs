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
    public class DeleteModel : PageModel
    {
        //private readonly MilkShopData.Models.NET1702_PRN221_MilkShopContext _context;
        private readonly IDiscountBusiness business;

        public DeleteModel()
        {
           // _context = context;
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
            else
            {
                Discount = discount.Data as Discount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await business.DeleteById((int)id);

            return RedirectToPage("./Index");
        }
    }
}

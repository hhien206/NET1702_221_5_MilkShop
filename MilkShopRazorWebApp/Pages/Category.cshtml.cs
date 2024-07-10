using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkShopBusiness.Base;
using MilkShopData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkShopRazorWebApp.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly CategoryBusiness _categoryBusiness = new CategoryBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Category category { get; set; } = default;
        public List<Category> categories { get; set; } = new List<Category>();

        public void OnGet()
        {
            categories = this.GetCategories();
        }

        public IActionResult OnPost()
        {
                this.SaveCategory();
                return RedirectToPage();        
        }

        [HttpPost]
        public IActionResult OnPostUpdate()
        {
            this.UpdateCategory();
            return RedirectToPage();
        }

        [HttpPost]
        public IActionResult OnPostDelete()
        {
            this.DeleteCategory();
            return RedirectToPage();
        }

        private List<Category> GetCategories()
        {
            var categoryBusiness = _categoryBusiness.GetAll();
            if (categoryBusiness.Status > 0 && categoryBusiness.Result != null)
            {
                var categories = (List<Category>)categoryBusiness.Result.Data;
                return categories;
            }
            return new List<Category>();
        }

        private void SaveCategory()
        {
            this.category.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            this.category.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);

            var categoryBusiness = _categoryBusiness.Save(this.category);
            if (categoryBusiness != null)
            {
                this.Message = categoryBusiness.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        private void UpdateCategory()
        {
            this.category.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

            this.category.UpdatedDate = DateOnly.FromDateTime(DateTime.Now);

            var categoryBusiness = _categoryBusiness.Update(this.category);
            if (categoryBusiness != null)
            {
                this.Message = categoryBusiness.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }


        private void DeleteCategory()
        {
            var categoryBusiness = _categoryBusiness.DeleteById(category.CategoryId);
            if (categoryBusiness != null)
            {
                this.Message = categoryBusiness.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        public string GetWelcomeMsg()
        {
            return "Welcome Razor Page Web Application";
        }
    }
}

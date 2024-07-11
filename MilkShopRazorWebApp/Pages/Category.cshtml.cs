using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkShopBusiness.Base;
using MilkShopData.Models;
using System.Collections.Generic;
using System.Linq;

namespace MilkShopRazorWebApp.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly CategoryBusiness _categoryBusiness = new CategoryBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Category category { get; set; } = default;
        public List<Category> categories { get; set; } = new List<Category>();

        // New properties for search
        [BindProperty(SupportsGet = true)]
        public string SearchCategoryName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchMetaKeywords { get; set; }

        public async Task<IActionResult> OnGet()
        {
            categories = GetCategories(SearchCategoryName, SearchType, SearchMetaKeywords);
            return Page();
        }

        public IActionResult OnPost()
        {
            SaveCategory();
            return RedirectToPage();
        }

        [HttpPost]
        public IActionResult OnPostUpdate()
        {
            UpdateCategory();
            return RedirectToPage();
        }

        [HttpPost]
        public IActionResult OnPostDelete()
        {
            DeleteCategory();
            return RedirectToPage();
        }

        private List<Category> GetCategories(string categoryName, string type, string metaKeywords)
        {
            var categoryBusiness = _categoryBusiness.GetAll();
            if (categoryBusiness.Status > 0 && categoryBusiness.Result != null)
            {
                var categories = (List<Category>)categoryBusiness.Result.Data;

                if (categoryName != null) categories = categories.FindAll(l => l.CategoryName != null && l.CategoryName.Contains(categoryName));
                if (type != null) categories = categories.FindAll(l => l.Type != null && l.Type.Contains(type));
                if (metaKeywords != null) categories = categories.FindAll(l => l.MetaKeywords != null && l.MetaKeywords.Contains(metaKeywords));

                return categories;
            }
            return new List<Category>();
        }

        private void SaveCategory()
        {

            var categoryBusiness = _categoryBusiness.Save(category);
            if (categoryBusiness != null)
            {
                Message = categoryBusiness.Result.Message;
            }
            else
            {
                Message = "Error system";
            }
        }

        private void UpdateCategory()
        {

            var categoryBusiness = _categoryBusiness.Update(category);
            if (categoryBusiness != null)
            {
                Message = categoryBusiness.Result.Message;
            }
            else
            {
                Message = "Error system";
            }
        }

        private void DeleteCategory()
        {
            var categoryBusiness = _categoryBusiness.DeleteById(category.CategoryId);
            if (categoryBusiness != null)
            {
                Message = categoryBusiness.Result.Message;
            }
            else
            {
                Message = "Error system";
            }
        }

        public string GetWelcomeMsg()
        {
            return "Welcome Razor Page Web Application";
        }
    }
}

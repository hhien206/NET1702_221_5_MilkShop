using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkShopBussiness.Business;
using MilkShopBusiness.Base;
using MilkShopData;
using MilkShopData.Models;
using System.Linq;
using MilkShopBusiness.Business;
using MilkShopData.DTO;


namespace MilkShopRazorWebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductBusiness _productBusiness = new ProductBusiness();
        private readonly IDiscountBusiness _discountBusiness = new DiscountBusiness();
        private readonly ICategoryBusiness _categoryBusiness = new CategoryBusiness();
        public string Message { get; set; } = default;
        [BindProperty(SupportsGet = true)]
        public string searchProduct { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchDiscount { get; set; }
        [BindProperty]
        public Product Product { get; set; } = default;
        public Discount Discount { get; set; } = default;
        public Category Category { get; set; } = default;
        public List<Product> Products { get; set; } = new List<Product>();
        public List<DiscountDTO> Discounts { get; set; } = new List<DiscountDTO>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; set; } = 7;

        public int TotalPages { get; set; }
        public bool IsIdFound { get; set; } = false;
        public bool IsDataFound { get; set; } = true;

        public void  OnGet(string searchProduct, string searchCategory, string searchDiscount, int PageNumber)
        {
            Categories = this.GetCategory();
            Discounts = this.GetDiscount();
            //if (searchProduct == null && searchCategory == null && searchDiscount == null)
            //{
            //    Products = this.GetProducts();

            //}
            //else
            //{

            //Products = this.Search(searchProduct, searchCategory, searchDiscount, PageNumber);
            var productResult = _productBusiness.GetByName(searchProduct, searchCategory, searchDiscount);
            //var result = await customerBusiness.GetByName(SearchKey);
            if (productResult != null && productResult.Result.Status > 0 && productResult.Result.Data != null)
            {
                var products = productResult.Result.Data as List<Product>;
                //Products = productResult.Result.Data as List<Product>;
                TotalPages = (int)Math.Ceiling(products.Count / (double)PageSize);
                //pageNumber = pageNumber < 1 ? 1 : (pageNumber > TotalPages ? TotalPages : pageNumber);
                Products = products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                // C?p nh?t PageNumber trong ViewModel ho?c PageModel
                //PageNumber = pageNumber;
                IsDataFound = Products.Any();
                //return Products.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();



                //    if (productResult.Status > 0 && productResult.Result.Data != null)
                //{
                //    var product = (List<Product>)productResult.Result.Data;
                //    return product;
            }
            else
            {
                Products = new List<Product>();
                IsDataFound = false;
            }

            //}

        }
        //public void OnGetSearch(string searchProduct, string searchCategory, string searchDiscount)
        //{
        //    Products = this.Search(searchProduct, searchCategory, searchDiscount);
        //}
        [HttpPost]
        public void OnPostAdd()
        {
            this.SaveProduct();
            //return RedirectToPage("/Product");
            OnGet(null,null, null, PageNumber);
        }

        [HttpPost]
        public void OnPostRemove()
        {
            this.Delete();
            //return RedirectToAction("/Product");
            OnGet(null, null, null, PageNumber);
        }
        [HttpPost]
        public void OnPostUpdate()
        {
            this.Update();
            //return RedirectToAction("/Product");
            OnGet(null, null, null, PageNumber);
        }
        public void OnDelete()
        {
        }


        private List<Product> GetProducts()
        {
            var productResult = _productBusiness.GetAll();
            

            if (productResult.Status > 0 && productResult.Result.Data != null)
            {
                var Products = (List<Product>)productResult.Result.Data;
                return Products;
            }

            return new List<Product>();
        }
        private List<Category> GetCategory()
        {

            var categoryResult = _categoryBusiness.GetAll();


            if (categoryResult.Status > 0 && categoryResult.Result.Data != null)
            {
                var Categories = (List<Category>)categoryResult.Result.Data;
                return Categories;
            }

            return new List<Category>();
        }
        private List<DiscountDTO> GetDiscount()
        {
            
            var discountResult = _discountBusiness.GetAll();
           

            if (discountResult.Status > 0 && discountResult.Result.Data != null)
            {
                var Discounts = (List<DiscountDTO>)discountResult.Result.Data;
                return Discounts;
            }

            return new List<DiscountDTO>();
        }

        private void Search(string Product, string Category, string Discount, int pageNumber)
        {
            var productResult = _productBusiness.GetByName(Product, Category, Discount);
            //var result = await customerBusiness.GetByName(SearchKey);
            if (productResult != null && productResult.Result.Status > 0 && productResult.Result.Data != null)
            {
                var products = productResult.Result.Data as List <Product>;
                //Products = productResult.Result.Data as List<Product>;
                TotalPages = (int)Math.Ceiling(Products.Count / (double)PageSize);
                //pageNumber = pageNumber < 1 ? 1 : (pageNumber > TotalPages ? TotalPages : pageNumber);
                Products = products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                // C?p nh?t PageNumber trong ViewModel ho?c PageModel
                //PageNumber = pageNumber;
                IsDataFound = Product.Any();
                //return Products.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();



                //    if (productResult.Status > 0 && productResult.Result.Data != null)
                //{
                //    var product = (List<Product>)productResult.Result.Data;
                //    return product;
            }
            else
            {
                Products = new List<Product>();
                IsDataFound = false;
            }
            //var result = await _business.AdvancedSearch(SearchId, SearchName, SearchBySchoolName);

            //if (result != null && result.Status > 0 && result.Data != null)
            //{
            //    var allCvs = result.Data as List<Cv>;
            //    TotalPages = (int)Math.Ceiling(allCvs.Count / (double)PageSize);
            //    Cv = allCvs.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            //    //Cv = result.Data as List<Cv>;
            //    IsDataFound = Cv.Any();
            //}
            //else
            //{
            //    Cv = new List<Cv>();
            //    IsDataFound = false;
            //}
            //return new List<Product>();

        }



        private void SaveProduct()
        {
            var productResult = _productBusiness.Save(this.Product);

            if (productResult != null)
            {
                this.Message = productResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private void Delete()
        {
            var productResult = _productBusiness.DeleteById(Product.ProductId);

            if (productResult != null)
            {
                this.Message = productResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private void Update()
        {
            var productResult = _productBusiness.Update(this.Product);

            if (productResult != null)
            {
                this.Message = productResult.Result.Message;
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

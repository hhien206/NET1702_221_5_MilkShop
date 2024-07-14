using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkShop.common;
using MilkShopBusiness.Business;
using MilkShopData.Models;
using Tasks = System.Threading.Tasks;

namespace MilkShopRazorWebApp.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly CustomerBusiness _customerBusiness = new CustomerBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Customer Customer { get; set; } = default;
        public List<Customer> customers { get; set; } = new List<Customer>();

        public int? PageSize { get; set; } = 5;
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public PaginatedList<Customer> Customers { get; set; } = default!;

        

        public async Tasks.Task  OnGet(int? pageIndex, int pageSize = 5)
        {
            //customers = this.GetCustomers(1,3);
            var paginatedCustomer = await _customerBusiness.GetAllCustomerPagingAsync(pageIndex, pageSize);
            if (paginatedCustomer != null)
            {
                Customers = (PaginatedList<Customer>)paginatedCustomer.Data;
            }

            if (!String.IsNullOrEmpty(Search))
            {
                var searchResult = await _customerBusiness.SearchCustomer(Search, pageIndex, pageSize);
                if (searchResult != null)
                {
                    Customers = (PaginatedList<Customer>)searchResult.Data;
                }
            }
        }



        public IActionResult OnPost()
        {
            this.SaveCustomer();
            return RedirectToPage();
        }

        [HttpPost]
        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            this.UpdateCustomer();
            return RedirectToPage();
        }


        [HttpPost]
        public IActionResult OnPostDelete()
        {
            this.DeleteCustomer();
            return RedirectToPage();
        }

        private List<Customer> GetCustomers(int? pageIndex, int pageSize)
        {
            var customerBusiness = _customerBusiness.GetAllCustomerPagingAsync( pageIndex, pageSize);
            if (customerBusiness.Status > 0 && customerBusiness.Result != null)
            {
                var customers = (List<Customer>)customerBusiness.Result.Data;
                return customers;
            }
            return new List<Customer>();

        }

        private void SaveCustomer()
        {
            var customerBusiness = _customerBusiness.Save(this.Customer);
            if (customerBusiness != null)
            {
                this.Message = customerBusiness.Result.Message;
            }
            {
                this.Message = "Error system";
            }
        }

        private void UpdateCustomer()
        {
            var customerBusiness = _customerBusiness.Update(this.Customer);
            if (customerBusiness != null)
            {
                this.Message = customerBusiness.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

          

        private void DeleteCustomer()
        {
            var customerBusiness = _customerBusiness.DeleteById(this.Customer.CustomerId);
            if (customerBusiness != null)
            {
                this.Message = customerBusiness.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}

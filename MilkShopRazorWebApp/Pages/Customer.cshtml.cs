using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkShopBusiness.Base;
using MilkShopData.Models;

namespace MilkShopRazorWebApp.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly CustomerBusiness _customerBusiness = new CustomerBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Customer Customer { get; set; } = default;
        public List<Customer> customers { get; set; } = new List<Customer>();

        public void OnGet()
        {
            customers = this.GetCustomers();
            //Customer = new Customer
            //{
            //    Gender = true // or false, depending on the default gender
            //};
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

        private List<Customer> GetCustomers()
        {
            var customerBusiness = _customerBusiness.GetAll();
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

using MilkShopBusiness.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MilkShop.WpfApp.UI
{

    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;

        public wCustomer()
        {
            InitializeComponent();
            _business = new CustomerBusiness(); // Ensure this is properly instantiated
            this.LoadGrdCurrencies();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtAccountId.Text, out int id))
                {
                    var item = await _business.GetById(id);

                    if (item.Data == null)
                    {
                        var customer = new Customer()
                        {
                            AccountId = id,
                            Name = txtCustomerName.Text,
                            Email = txtEmail.Text,
                        };

                        var result = await _business.Save(customer);
                        MessageBox.Show(result.Message, "Save");
                    }
                    else
                    {
                        var customer = item.Data as Customer;
                        customer.Name = txtCustomerName.Text;
                        customer.Email = txtEmail.Text;
                        var result = await _business.Update(customer);
                        MessageBox.Show(result.Message, "Update");
                    }

                    txtAccountId.Text = string.Empty;
                    txtCustomerName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    this.LoadGrdCurrencies();
                }
                else
                {
                    MessageBox.Show("Invalid Customer ID", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Add your cancel logic here
            MessageBox.Show("Cancel button clicked");
        }

        private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Customer;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetById(item.CustomerId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Customer;
                            txtAccountId.Text = item.AccountId.ToString(); // Convert int to string
                            txtCustomerName.Text = item.Name;
                            txtEmail.Text = item.Email;
                        }
                    }
                }
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string customerIdString = btn.CommandParameter.ToString();

            if (int.TryParse(customerIdString, out int customerId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(customerId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCurrencies();
                }
            }
            else
            {
                MessageBox.Show("Invalid Customer ID.", "Error");
            }
        }

        private async void LoadGrdCurrencies()
        {
            if (_business == null) // Check if _business is null
            {
                MessageBox.Show("Business layer not initialized.");
                return;
            }

            var result = await _business.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdCustomer.ItemsSource = result.Data as List<Customer>;
            }
            else
            {
                grdCustomer.ItemsSource = new List<Customer>();
            }
        }

        private void grdCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

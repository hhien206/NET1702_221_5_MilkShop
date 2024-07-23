using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MilkShopBusiness.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MilkShop.WpfApp.UI
{

    public partial class wCustomer : System.Windows.Window
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
                if (int.TryParse(txtCustomerId.Text, out int id))
                {
                    var item = await _business.GetById(id); // Adjust this method as per your business logic

                    if (item.Data == null)
                    {
                        var customer = new Customer()
                        {
                            
                            AccountId = int.Parse(txtAccountId.Text),
                            Name = txtCustomerName.Text,
                            Email = txtEmail.Text,
                            Address = txtAddress.Text,
                            PhoneNumber = txtPhoneNumber.Text,
                            Dob = DateOnly.Parse(dpDOB.Text),
                            Point = int.Parse(txtPoint.Text),
                            Status = (cbStatus.SelectedItem as ComboBoxItem)?.Content?.ToString(),
                        };

                        var result = await _business.Save(customer);
                        MessageBox.Show(result.Message, "Save");
                    }
                    else
                    {
                        var customer = item.Data as Customer;

                        // Update customer details
                        customer.CustomerId = id;
                        customer.AccountId = int.Parse(txtAccountId.Text);
                        customer.Name = txtCustomerName.Text;
                        customer.Email = txtEmail.Text;
                        customer.Address = txtAddress.Text;
                        customer.PhoneNumber = txtPhoneNumber.Text;
                        customer.Dob = DateOnly.Parse(dpDOB.Text);
                        customer.Point = int.Parse(txtPoint.Text);
                        customer.Status = cbStatus.Text;

                        var result = await _business.Update(customer);
                        MessageBox.Show(result.Message, "Update");
                    }

                    // Clear input fields after save or update
                    ClearInputFields();

                    // Refresh the data grid after saving or updating
                    this.LoadGrdCurrencies();
                }
                else
                {
                    MessageBox.Show("Invalid Account ID", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ClearInputFields()
        {
            txtCustomerId.Text = string.Empty;
            txtAccountId.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            dpDOB.Text = string.Empty;
            txtPoint.Text = string.Empty;
            cbStatus.SelectedIndex = 0;
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Add your cancel logic here
            ClearInputFields();
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
                            txtCustomerId.Text = item.CustomerId.ToString(); // Convert int to string
                            txtAccountId.Text = item.AccountId.ToString(); // Convert int to string
                            txtCustomerName.Text = item.Name;
                            txtEmail.Text = item.Email;
                            txtAddress.Text = item.Address;
                            txtPhoneNumber.Text = item.PhoneNumber;
                            dpDOB.Text = item.Dob.ToString();
                            txtPoint.Text = item.Point.ToString();
                            cbStatus.Text = item.Status;

                        }
                    }
                }
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;

            // Attempt to convert CommandParameter to int
            int? customerId;
            if (btn.CommandParameter != null && int.TryParse(btn.CommandParameter.ToString(), out int value))
            {
                customerId = value;
            }
            else
            {
                // Handle the case where CommandParameter is not an integer or null
                MessageBox.Show("Invalid Customer ID format. Please try again.", "Error", MessageBoxButton.OK);
                return;
            }

            // Confirmation and deletion logic (assuming InternId is valid)
            if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = await _business.DeleteById(customerId.Value);
                MessageBox.Show($"{result.Message}", "Delete");
                LoadGrdCurrencies();
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

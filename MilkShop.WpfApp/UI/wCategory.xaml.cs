using MilkShopBusiness.Base;
using MilkShopData.Models;
using System;
using System.Collections.Generic; // Add this to use List<Category>
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MilkShop.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wCategory.xaml
    /// </summary>
    public partial class wCategory : Window
    {
        private readonly CategoryBusiness _business;

        public wCategory()
        {
            InitializeComponent();
            _business = new CategoryBusiness(); // Ensure this is properly instantiated
            this.LoadGrdCurrencies();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtCategoryId.Text, out int id))
                {
                    var item = await _business.GetByID(id);

                    if (item.Data == null)
                    {
                        var category = new Category()
                        {
                            CategoryId = id,
                            CategoryName = txtCategoryName.Text,
                            Type = txtType.Text,
                        };

                        var result = await _business.Save(category);
                        MessageBox.Show(result.Message, "Save");
                    }
                    else
                    {
                        var currency = item.Data as Category;
                        //currency.CurrencyCode = txtCurrencyCode.Text;
                        currency.CategoryName = txtCategoryName.Text;
                        currency.Type = txtType.Text;
                        var result = await _business.Update(currency);
                        MessageBox.Show(result.Message, "Update");
                    }

                    txtCategoryId.Text = string.Empty;
                    txtCategoryName.Text = string.Empty;
                    txtType.Text = string.Empty;
                    this.LoadGrdCurrencies();
                }
                else
                {
                    MessageBox.Show("Invalid Category ID", "Error");
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

        private async void grdCategory_MouseDouble_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Category;
                    if (item != null)
                    {
                        var currencyResult = await _business.GetByID(item.CategoryId);

                        if (currencyResult.Status > 0 && currencyResult.Data != null)
                        {
                            item = currencyResult.Data as Category;
                            txtCategoryId.Text = item.CategoryId.ToString(); // Convert int to string
                            txtCategoryName.Text = item.CategoryName;
                            txtType.Text = item.Type;
                        }
                    }
                }
            }
        }

        private async void grdCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            string categoryIdString = btn.CommandParameter.ToString();

            if (int.TryParse(categoryIdString, out int categoryId))
            {
                if (MessageBox.Show("Do you want to delete this item?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _business.DeleteById(categoryId);
                    MessageBox.Show($"{result.Message}", "Delete");
                    this.LoadGrdCurrencies();
                }
            }
            else
            {
                MessageBox.Show("Invalid Category ID.", "Error");
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
                grdCategory.ItemsSource = result.Data as List<Category>;
            }
            else
            {
                grdCategory.ItemsSource = new List<Category>();
            }
        }

        private void grdCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

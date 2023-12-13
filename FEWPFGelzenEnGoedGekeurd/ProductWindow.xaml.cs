using BL.Dtos;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FEWPFGelzenEnGoedGekeurd
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private ICampaignManager _campaignManager;
        private IProductManager _productManager;
        private List<Campaign> availableCampaignsList;
        private List<Campaign> _addedCampaigns;
        private List<Product> existingProducts;
        public ProductWindow(ICampaignManager campaignManager, IProductManager productManager)
        {
            _campaignManager = campaignManager;
            _productManager = productManager;
            InitializeComponent();
            RefreshCampaingListbox();
            _addedCampaigns = new List<Campaign>();


        }

        private async void RefreshCampaingListbox()
        {
            availableCampaignsList =await _campaignManager.GetAllAsync();
            AvailableCampaigns.ItemsSource = availableCampaignsList;
            UsedCampaings.ItemsSource = null;
            UsedCampaings.ItemsSource = _addedCampaigns;
            existingProducts = await _productManager.GetAllProductsAsync();
            AllProductsDatagrid.ItemsSource = existingProducts;
            //_campaignwindowList = _campaignRepo.GetAll();
            //CampaignDatagrid.ItemsSource = null;
            //CampaignDatagrid.ItemsSource = _campaignwindowList;
        }

        private void AddCampaignBtnClick(object sender, RoutedEventArgs e)
        {
            Campaign campaign = (Campaign)AvailableCampaigns.SelectedItem;
            _addedCampaigns.Add(campaign);
            RefreshCampaingListbox();
        }

        private void RemoveCampaignBtnClick(object sender, RoutedEventArgs e)
        {
            Campaign campaignToRemove = (Campaign)UsedCampaings.SelectedItem;
            _addedCampaigns.Remove(campaignToRemove);
            RefreshCampaingListbox();
        }

        private void AvailableCampaigns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //AddPrice.Text = AvailableCampaigns.SelectedItem
            //AddNumberOfFreeSpots.Text;
            //AddProductName.Text;
            //_addedCampaigns;
        }
        private async void AllProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllProductsDatagrid.SelectedItem is Product selectedProduct)
            {
                // Set text fields based on the selected product
                AddPrice.Text = selectedProduct.Price?.ToString(); // Handle null gracefully if needed
                AddNumberOfFreeSpots.Text = selectedProduct.MaxAvailableCapacity?.ToString(); // Handle null gracefully if needed
                AddProductName.Text = selectedProduct.Name;

                // Fetch campaigns linked to the selected product
                _addedCampaigns = await _campaignManager.GetCampaignsByProductId(selectedProduct.Id);

                // Update the _addedCampaigns listbox
                RefreshCampaingListbox();
            }
        }
        private void NavigateToCustomer(object sender, RoutedEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<MainWindow>();
            Window.Left = this.Left;
            Window.Top = this.Top;
            this.Hide();
            Window.Show();
        }
        private void NavigateToProduct(object sender, RoutedEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<ProductWindow>();
            Window.Left = this.Left;
            Window.Top = this.Top;
            this.Hide();
            Window.Show();
        }
        private void NavigateToCampaign(object sender, RoutedEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<CampaignWindow>();
            Window.Left = this.Left;
            Window.Top = this.Top;
            this.Hide();
            Window.Show();
        }
        private void NavigateToPlanning(object sender, RoutedEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<PlannerWindow>();
            Window.Left = this.Left;
            Window.Top = this.Top;
            this.Hide();
            Window.Show();
        }
        private void NavigateToLocation(object sender, RoutedEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<LocationWindow>();
            Window.Left = this.Left;
            Window.Top = this.Top;
            this.Hide();
            Window.Show();
        }

        private async void AddProductClick(object sender, RoutedEventArgs e)
        {
            ProductAddingDto productAddingdto = new ProductAddingDto();
            productAddingdto.Price = AddPrice.Text;
            productAddingdto.Name = AddProductName.Text;

            // Check if AddNumberOfFreeSpots.Text is empty or not a valid number
            if (string.IsNullOrWhiteSpace(AddNumberOfFreeSpots.Text) || !int.TryParse(AddNumberOfFreeSpots.Text, out int numberOfFreeSpots))
            {
                MessageBox.Show("Error: Please enter a valid number for the number of free spots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            productAddingdto.NBROfFreeSpots = numberOfFreeSpots.ToString();

            if (_addedCampaigns.Count > numberOfFreeSpots)
            {
                MessageBox.Show("Error: More campaigns selected than available free spots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int productid = await _productManager.AddAsync(productAddingdto);

                if (productid != 0)
                {
                    // Retrieve the newly created product
                    var newProduct = _productManager.GetById(productid);

                    // Link the product to the campaigns
                    foreach (var campaign in _addedCampaigns)
                    {
                        // Set the navigation property
                        campaign.product = newProduct;

                        // Optionally, set the foreign key if you want to keep it
                        campaign.ProductId = newProduct.Id;

                        // Update the campaign
                        _campaignManager.Update(campaign);
                    }
                }

                RefreshCampaingListbox();
                AddPrice.Text = "price";
                AddNumberOfFreeSpots.Text = "Number Of Free spots";
                AddProductName.Text = "Name";
                _addedCampaigns = null;
            }
        }


    }
}

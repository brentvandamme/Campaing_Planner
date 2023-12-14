using BL.Dtos;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private int lastSelectedProductId = -1;
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

            if (!_addedCampaigns.Any(c => c.Id == campaign.Id))
            {
                _addedCampaigns.Add(campaign);
                RefreshCampaingListbox();
            }
            else
            {
                MessageBox.Show("Campaign with the same ID is already added.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
                AddPrice.Text = selectedProduct.Price?.ToString(); 
                AddNumberOfFreeSpots.Text = selectedProduct.MaxAvailableCapacity?.ToString();
                AddProductName.Text = selectedProduct.Name;
                lastSelectedProductId = selectedProduct.Id; 

                _addedCampaigns = await _campaignManager.GetCampaignsByProductId(selectedProduct.Id);

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
            if (lastSelectedProductId >= 0)
            {
                MessageBox.Show("Error: Can not add an existing product, update it if you want to change it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                ProductAddingDto productAddingdto = new ProductAddingDto();
            productAddingdto.Price = AddPrice.Text;
            productAddingdto.Name = AddProductName.Text;

            // Check if AddNumberOfFreeSpots.Text is empty or not a valid number
            if (string.IsNullOrWhiteSpace(AddNumberOfFreeSpots.Text) || !int.TryParse(AddNumberOfFreeSpots.Text, out int numberOfFreeSpots))
            {
                MessageBox.Show("Error: Please enter a valid number for the number of free spots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            productAddingdto.MaxAvailableCapacity = numberOfFreeSpots;

            if (_addedCampaigns.Count > numberOfFreeSpots)
            {
                MessageBox.Show("Error: More campaigns selected than available free spots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int productid = await _productManager.AddAsync(productAddingdto);

                if (productid != 0)
                {
                    var newProduct = _productManager.GetById(productid);

                    foreach (var campaign in _addedCampaigns)
                    {
                        campaign.product = newProduct;

                        campaign.ProductId = newProduct.Id;

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

        private async void UpdateProductClick(object sender, RoutedEventArgs e)
        {
            
            if (lastSelectedProductId >=0)
            {
                ProductAddingDto productAddingdto = new ProductAddingDto();
                productAddingdto.Price = AddPrice.Text;
                productAddingdto.Name = AddProductName.Text;
                productAddingdto.Id = lastSelectedProductId;
                productAddingdto.MaxAvailableCapacity = Int32.Parse(AddNumberOfFreeSpots.Text);

                // Check if AddNumberOfFreeSpots.Text is empty or not a valid number
                if (string.IsNullOrWhiteSpace(AddNumberOfFreeSpots.Text) || !int.TryParse(AddNumberOfFreeSpots.Text, out int numberOfFreeSpots))
                {
                    MessageBox.Show("Error: Please enter a valid number for the number of free spots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_addedCampaigns.Count > numberOfFreeSpots)
                {
                    MessageBox.Show("Error: More campaigns selected than available free spots.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

               await _productManager.UpdateAsync(productAddingdto);
                Product newProduct = await _productManager.GetByIdAsync(lastSelectedProductId);
                foreach (var campaign in _addedCampaigns)
                {
                    // Set the navigation property
                    campaign.product = newProduct;

                    // Optionally, set the foreign key if you want to keep it
                    campaign.ProductId = newProduct.Id;

                    // Update the campaign
                   await _campaignManager.UpdateAsync(campaign);
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

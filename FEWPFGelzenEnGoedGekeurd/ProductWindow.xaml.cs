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
        public ProductWindow(ICampaignManager campaignManager, IProductManager productManager)
        {
            _campaignManager = campaignManager;
            _productManager = productManager;
            InitializeComponent();
            RefreshCampaingListbox();
            _addedCampaigns = new List<Campaign>();


        }

        private void RefreshCampaingListbox()
        {
            availableCampaignsList = _campaignManager.GetAll();
            AvailableCampaigns.ItemsSource = availableCampaignsList;
            UsedCampaings.ItemsSource = null;
            UsedCampaings.ItemsSource = _addedCampaigns;
            AllProductsDatagrid.ItemsSource = _productManager.GetAll();
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
        private void AllProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        }

        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            ProductAddingDto productAddingdto = new ProductAddingDto();
            productAddingdto.Price = AddPrice.Text;
            productAddingdto.NBROfFreeSpots = AddNumberOfFreeSpots.Text;
            productAddingdto.Name = AddProductName.Text;
            productAddingdto.Campaigns = _addedCampaigns;
            //Customer customer = new Customer();
            //customer.FirstName = AddCustomerName.Text;
            //customer.LastName = AddLastName.Text;
            //customer.Company = AddCompanyName.Text;

            //_repo.Add(customer);
            _productManager.Add(productAddingdto);
        }
    }
}

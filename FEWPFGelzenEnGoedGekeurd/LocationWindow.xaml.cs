using BL.Dtos;
using BL.Managers.Interfaces;
using FEWPFGelzenEnGoedGekeurd.Helpers;
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
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        ILocationManager _locationManager;
        public LocationWindow(ILocationManager locationManager)
        {
            InitializeComponent();
            _locationManager = locationManager;
            RefreshDatagrid();
        }

        private async void RefreshDatagrid()
        {
            LocationsDatagrid.ItemsSource = await _locationManager.GetAllAsync();
        }
        private void NavigateToCustomer(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo<MainWindow>(this);
        }
        private void NavigateToProduct(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo<ProductWindow>(this);
        }
        private void NavigateToCampaign(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo<CampaignWindow>(this);
        }
        private void NavigateToPlanning(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo<PlannerWindow>(this);
        }
        private void NavigateToLocation(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateTo<LocationWindow>(this);
        }


        private async void AddLocationButton_Button_Click(object sender, RoutedEventArgs e)
        {
            LocationCreationDto locationCreationDto = new();

            locationCreationDto.Zip = AddLocationZip.Text;
            locationCreationDto.City = AddLocationCity.Text;
            locationCreationDto.Street = AddLocationStreet.Text;
            locationCreationDto.Number= AddLocationNumber.Text;
            locationCreationDto.Name= AddLocationName.Text;
            locationCreationDto.ExtraInfo= AddLocationExtraInfo.Text;

            await _locationManager.AddLocationAsync(locationCreationDto);
            RefreshDatagrid();
        }
    }
}

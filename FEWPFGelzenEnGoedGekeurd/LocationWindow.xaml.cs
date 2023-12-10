using BL.Dtos;
using BL.Managers.Interfaces;
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


        private void AddLocationButton_Button_Click(object sender, RoutedEventArgs e)
        {
            LocationCreationDto locationCreationDto = new();

            locationCreationDto.Zip = AddLocationZip.Text;
            locationCreationDto.City = AddLocationCity.Text;
            locationCreationDto.Street = AddLocationStreet.Text;
            locationCreationDto.Number= AddLocationNumber.Text;
            locationCreationDto.Name= AddLocationName.Text;
            locationCreationDto.ExtraInfo= AddLocationExtraInfo.Text;

            _locationManager.AddLocation(locationCreationDto);
        }
    }
}

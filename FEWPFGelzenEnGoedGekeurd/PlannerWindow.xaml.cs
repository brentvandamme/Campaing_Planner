using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Entities;
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
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        private string _hourTime;
        ICustomerManager _customerManager;
        IProductManager _productManager;
        IPlanningManager _planningManager;
        ILocationManager _locationManager;
        List<Location> _locations;

        public PlannerWindow(ICustomerManager customerManager, IProductManager productManager, IPlanningManager planningmanager, ILocationManager locationManager)
        {
            InitializeComponent();
            _customerManager = customerManager;
            _productManager = productManager;
            _planningManager = planningmanager;
            _locationManager = locationManager;

            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            ProductListbox.ItemsSource = await _productManager.GetAllProductsWithFreeSpots();
            CustomerListbox.ItemsSource = await _customerManager.GetAllAsync();
            LocationDatagrid.ItemsSource = await _locationManager.GetAllAsync();
            planningDatagrid.ItemsSource =  _planningManager.GetAllWithIncludes();
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
        private void Time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _hourTime = sender.ToString();
        }

        private void ProductListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private DateTime GetSelectedStartTime()
        {
            DateTime startTime = datePickerStart.SelectedDate ?? DateTime.Now.Date;
            int selectedHour = Convert.ToInt32(((ComboBoxItem)StartHours.SelectedItem).Content);
            int selectedMinute = Convert.ToInt32(((ComboBoxItem)StartMin.SelectedItem).Content);

            DateTime selectedStartDateTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                selectedHour,
                selectedMinute,
                0 // Seconds
            );

            return selectedStartDateTime;
        }

        private DateTime GetSelectedEndTime()
        {
            DateTime endTime = datePickerEnd.SelectedDate ?? DateTime.Now.Date;
            int selectedHour = Convert.ToInt32(((ComboBoxItem)EndHour.SelectedItem).Content);
            int selectedMinute = Convert.ToInt32(((ComboBoxItem)EndMin.SelectedItem).Content);

            DateTime selectedendDateTime = new DateTime(
                endTime.Year,
                endTime.Month,
                endTime.Day,
                selectedHour,
                selectedMinute,
                0 // Seconds
            );

            return selectedendDateTime;
        }

        private async void AddToPlaningBtn_Click(object sender, RoutedEventArgs e)
        {
            Planning newplanning = new Planning();
            List<Product> Products = new();
            Location location = new();

            foreach (var selectedItem in ProductListbox.SelectedItems)
            {
                if (selectedItem is Product selectedProduct)
                {
                    Products.Add((Product)selectedItem);
                }
            }

            Customer existingCustomer = CustomerListbox.SelectedItem as Customer;

            if (existingCustomer == null || ProductListbox.SelectedItems == null)
            {
                MessageBox.Show("Please select all required fields.");
            }

            newplanning.StartVerhuur = GetSelectedStartTime();
            newplanning.EndVerhuur = GetSelectedEndTime();
            location = LocationDatagrid.SelectedItem as Location;

            await _planningManager.AddAsync(newplanning, existingCustomer, Products, location);

            planningDatagrid.ItemsSource = _planningManager.GetAllWithIncludes();
        }

        

    }
}

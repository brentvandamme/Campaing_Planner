using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!_planningManager.GenerateCSV())
            {
                MessageBox.Show($"Error: whilst generating csv", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("CSV file generated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

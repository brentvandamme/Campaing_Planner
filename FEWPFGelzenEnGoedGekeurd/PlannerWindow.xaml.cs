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

        public PlannerWindow(ICustomerManager customerManager, IProductManager productManager, IPlanningManager planningmanager)
        {
            InitializeComponent();
            _customerManager= customerManager;
            _productManager= productManager;
            _planningManager = planningmanager;
            ProductListbox.ItemsSource = _productManager.GetAll();
            CustomerListbox.ItemsSource = _customerManager.GetAll();
            planningDatagrid.ItemsSource= _planningManager.GetAllWithIncludes();
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

            // Create a new DateTime object with the selected date, hour, and minute
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

            // Create a new DateTime object with the selected date, hour, and minute
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
            newplanning.PlanningProduct = new List<PlanningProduct>();

            //if (ProductListbox.SelectedItems != null && ProductListbox.SelectedItems.Count > 0)
            //{
            //    foreach (var selectedItem in ProductListbox.SelectedItems)
            //    {
            //        if (selectedItem is Product selectedProduct)
            //        {
            //            newplanning.PlanningProduct.Add(new PlanningProduct { Product = selectedProduct });
            //        }
            //    }
            //}

            // Get the existing Customer object from the ListBox
            Customer existingCustomer = CustomerListbox.SelectedItem as Customer;

            if (existingCustomer != null)
            {
                
            }
            else
            {
                // Handle the case where no customer is selected
                MessageBox.Show("Please select a customer.");
            }

            // Set other properties of newPlanning, StartVerhuur, EndVerhuur, etc.
            newplanning.StartVerhuur = GetSelectedStartTime();
            newplanning.EndVerhuur = GetSelectedEndTime();

            // Add the new Planning object to the DbContext
            await _planningManager.AddAsync(newplanning, existingCustomer);

            // Refresh the data grid with the updated data
            planningDatagrid.ItemsSource = _planningManager.GetAll();
        }

        

    }
}

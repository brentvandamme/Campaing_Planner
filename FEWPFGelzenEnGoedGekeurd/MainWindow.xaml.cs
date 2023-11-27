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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BL.Dtos;
using BL.Managers;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FEWPFGelzenEnGoedGekeurd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICustomerRepository _repo;
        private List<Customer> _customerList;

        public MainWindow(ICustomerRepository repo)
        {
            _repo = repo;
            InitializeComponent();
            CustomerDatagrid.ItemsSource= _customerList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            customer.FirstName = AddCustomerName.Text;
            customer.LastName = AddLastName.Text;
            customer.Company = AddCompanyName.Text;

            _repo.Add(customer);
        }
        private void MouseDown_onCustomer(object sender, MouseButtonEventArgs e)
        {
            // Handle MouseDown event for Customer TabItem
            // Add your code here
        }

        private void MouseDown_onCampaign(object sender, MouseButtonEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<CampaignWindow>();
            Window.WindowState = WindowState.Maximized;
            Window.Show();
        }

        private void MouseDown_onProduct(object sender, MouseButtonEventArgs e)
        {
            // Handle MouseDown event for Product TabItem
            // Add your code here
        }

        private void MouseDown_onLocation(object sender, MouseButtonEventArgs e)
        {
            // Handle MouseDown event for Location TabItem
            // Add your code here
        }

        private void MouseDown_onPlanning(object sender, MouseButtonEventArgs e)
        {
            // Handle MouseDown event for Planning TabItem
            // Add your code here
        }
    }
}


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
        private ICustomerManager _manager;
        private List<Customer> _customerList;

        public MainWindow(ICustomerManager manager)
        {
            _manager = manager;
            InitializeComponent();
            RefreshCustomerList();
        }

        private void RefreshCustomerList()
        {
            _customerList = _manager.GetAll();
            CustomerDatagrid.ItemsSource = _customerList;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer();
            customer.FirstName = AddCustomerName.Text;
            customer.LastName = AddLastName.Text;
            customer.Company = AddCompanyName.Text;

            await _manager.AddAsync(customer);
            RefreshCustomerList();
        }

        //todo eric: volgende code zit in elk scherm, eventueel in een helper steken

        private void NavigateToCustomer(object sender, RoutedEventArgs e)
        {
            var Window = App.ServiceProvider.GetService<MainWindow>();
            Window.Left = this.Left;
            Window.Top = this.Top;
            //todo eric: door de vensters te hiden sluit de applicatie niet meer af als je eerst naar eenderd welk ander scherm gaat
            //die blijven in de achtergrond open staan, ik ben geen wpf expert maar ik denk dat enkel de mainwindow die je als singleton hebt levend gaat moeten blijven
            //bij de andere schermen ga je een this.close() kunnen doen als je weg gaat
            //of 
            //bij de schermen de OnClosing overriden en daar voor de mainwindow ook close oproepen
            //maar al de geopende schermen blijven in mem zitten
            //check process mem als je de app via visual studio draait, customer blijven openen doet niet veel (mainwindow singleton altijd dezelfde) maar bij de rest altijd mem jump die niet weg gaat
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

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDatagrid.SelectedItem is Customer selectedCustomer)
            {
                await _manager.DeleteCustomerByIdAsync(selectedCustomer.Id);
                RefreshCustomerList();
            }
        }

        private void CustomerDatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerDatagrid.SelectedItem is Customer selectedCustomer)
            {
                AddCustomerName.Text = selectedCustomer.FirstName;
                AddLastName.Text = selectedCustomer.LastName;
                AddCompanyName.Text = selectedCustomer.Company;
            }
        }

        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDatagrid.SelectedItem is Customer selectedCustomer)
            {
                selectedCustomer.FirstName = AddCustomerName.Text;
                selectedCustomer.LastName = AddLastName.Text;
                selectedCustomer.Company = AddCompanyName.Text;

                await _manager.UpdateCustomerAsync(selectedCustomer);
                RefreshCustomerList();
            }
        }
    }
}


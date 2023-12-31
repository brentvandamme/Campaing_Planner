﻿using System;
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
using FEWPFGelzenEnGoedGekeurd.Helpers;
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


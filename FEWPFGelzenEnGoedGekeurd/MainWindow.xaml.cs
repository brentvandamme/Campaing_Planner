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

namespace FEWPFGelzenEnGoedGekeurd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ICustomerManager _customerManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(ICustomerManager customerManager) : this()
        {
            _customerManager = customerManager;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //CustomerCreationDto customerCreation = new CustomerCreationDto();
            //customerCreation.FirstName = AddCustomerName.Text;
            //customerCreation.LastName = AddLastName.Text;
            //customerCreation.Company = AddCompanyName.Text;

            //_customerManager.Add(customerCreation);
        }
    }
}

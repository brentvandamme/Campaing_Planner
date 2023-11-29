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
        public PlannerWindow()
        {
            InitializeComponent();
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
    }
}

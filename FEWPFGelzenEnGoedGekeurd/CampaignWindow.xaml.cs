using BL.Dtos;
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
    /// Interaction logic for CampaignWindow.xaml
    /// </summary>
    public partial class CampaignWindow : Window
    {
        private KindOfCampaign _chosenCampaignVal;
        private ICampaignManager _manager;
        private List<Campaign> _campaignwindowList;

        public CampaignWindow(ICampaignManager manager)
        {
            _manager = manager;
            InitializeComponent();
            KindOfCampaignsListBox.ItemsSource = Enum.GetValues(typeof(KindOfCampaign));
            RefreshCampaingListbox();
        }

        private async void RefreshCampaingListbox()
        {
            _campaignwindowList = await _manager.GetAllAsync();
            CampaignDatagrid.ItemsSource = null;
            CampaignDatagrid.ItemsSource = _campaignwindowList;
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

        }

        private void KindOfCampaignsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KindOfCampaignsListBox.SelectedItem != null)
            {
                KindOfCampaign selectedKind = (KindOfCampaign)KindOfCampaignsListBox.SelectedItem;
                _chosenCampaignVal = selectedKind;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            CampaignDto campaign = new();
            campaign.Name= AddCampaignName.Text;
            campaign.SoortCampagne = _chosenCampaignVal;

            await _manager.AddAsync(campaign);
            RefreshCampaingListbox();
        }
    }
}

using BL.Dtos;
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

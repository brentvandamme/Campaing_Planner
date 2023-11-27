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
        private ICampaignRepository _repo;
        private List<Campaign> _campaignwindowList;

        public CampaignWindow(ICampaignRepository repo)
        {
            _repo = repo;
            InitializeComponent();
            KindOfCampaignsListBox.ItemsSource = Enum.GetValues(typeof(KindOfCampaign));
            RefreshCampaingListbox();
        }

        private void RefreshCampaingListbox()
        {
            _campaignwindowList = _repo.GetAll();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Campaign campaign= new Campaign();
            campaign.Name= AddCampaignName.Text;
            campaign.LastUpdate= DateTime.Now;
            campaign.SoortCampagne = _chosenCampaignVal;

            _repo.Add(campaign);
            RefreshCampaingListbox();
        }
    }
}

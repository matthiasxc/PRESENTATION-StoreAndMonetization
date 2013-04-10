using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Monetization_Demo.Resources;
using Monetization_Demo.ViewModels;
using Microsoft.Phone.Marketplace;
using Microsoft.Advertising.Mobile.UI;
using Microsoft.Phone.Tasks;

namespace Monetization_Demo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LicenseInformation info = new LicenseInformation();
#if DEBUG
            if (true) 
#else
            if (info.IsTrial())
#endif
            {
                // running in trial mode
#if DEBUG
                AdControl ac = new AdControl("TextAd", "test_client", true);
                // Other options in case you want to test other ads
                //AdControl ac = new AdControl("Image480_80", "test_client", true);
                //AdControl ac = new AdControl("Image300_50", "test_client", true);
#else
                AdControl ac = new AdControl("012345", "01234567-1234-1234-1234-0123456789012", true);
#endif
                ac.Width = 480;
                ac.Height = 80;

                AdControlHolder.Child = ac;
            }

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
                
            }
        }

        void ac_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            MessageBox.Show(e.Error.Message);
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }
    }
}
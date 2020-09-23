using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Songs;
using System.Timers;
using Xamarin.Essentials;
namespace ProjectorProMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        private Timer searchTimer;
        public MainPage()
        {
            InitializeComponent();
            SetStatusMessage();

            txtIp.Text = Preferences.Get("ip", "192.168.0.1");
            txtPort.Text = Preferences.Get("port", "2444");

        }


        SongCollection searchItems;
        Querier querier;
        private async void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetStatusMessage("Searching...");
            querier = new Querier();
            searchItems = await querier.GetSearchResultsAsync(((Entry)sender).Text);

            if (searchItems != null)
            {
                ListResultsView.ItemsSource = searchItems.GetTitleArray();
                SetStatusMessage();
            }
            else
            {
                SetStatusMessage("No results found.");
            }

        }


        private async void ListResultsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selIndex = e.ItemIndex;

            await DisplayAlert(searchItems.Collection[selIndex].Title, await searchItems.Collection[selIndex].SetBodyAsync(), "Finished");
        }

        private void SetStatusMessage(string message = "")
        {

            if (message != "")
            {
                ListResultsView.ItemsSource = null;
            }
            lblMessage.Text = message;
        }
        private void txtIp_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSavePrefs.IsEnabled = HaveSettingsChanged();
        }

        private void txtPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnSavePrefs.IsEnabled = HaveSettingsChanged();
        }

        private bool HaveSettingsChanged()
        {
            return (Preferences.Get("port", "2444") != txtPort.Text) || (Preferences.Get("ip", "192.168.0.2") != txtIp.Text);
        }

        private void btnSavePrefs_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("port", txtPort.Text);
            Preferences.Set("ip", txtIp.Text);
            btnSavePrefs.IsEnabled = false;
            querier = new Querier();
        }
    }
}

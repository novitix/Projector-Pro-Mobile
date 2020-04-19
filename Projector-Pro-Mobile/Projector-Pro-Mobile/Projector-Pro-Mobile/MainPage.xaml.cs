using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Songs;
using System.Timers;
using System.Threading.Tasks;

namespace Projector_Pro_Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            SetStatusMessage();
        }

        SongCollection searchItems;
        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = ((Entry)sender).Text;
            Querier querier = new Querier();
            searchItems = await querier.GetSearchResultsAsync(searchTerm);
            if (searchItems != null)
            {
                ListResultsView.ItemsSource = searchItems.GetTitleArray();
                SetStatusMessage();
            } else
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

            if(message != "")
            {
                ListResultsView.ItemsSource = null;
            }
            lblMessage.Text = message;
        }
    }
}

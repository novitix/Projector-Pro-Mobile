using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Songs;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSearch : ContentPage
    {
        public PageSearch()
        {
            InitializeComponent();
            SetStatusMessage();
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
            Song selSong = searchItems.Collection[e.ItemIndex];
            string msgTitle = selSong.GetDisplayHeader();
            Task msgTask = DisplayAlert(msgTitle, await selSong.SetBodyAsync(), "Close");
            Console.WriteLine("ishost: " + SessionManager.IsHost.ToString());
            if (SessionManager.IsHost)
            {
                SessionManager.UpdateSessionAsync(selSong.ID);
            }
            await msgTask;
        }

        private void SetStatusMessage(string message = "")
        {
            if (message != "")
            {
                ListResultsView.ItemsSource = null;
            }
            lblMessage.Text = message;
        }
    }
}
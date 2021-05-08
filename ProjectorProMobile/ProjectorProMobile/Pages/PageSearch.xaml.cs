using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Songs;
using System.Threading;

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
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        int searchDelay = 200;
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInit(e.NewTextValue);
        }

        private void SearchInit(string searchTerm)
        {
            btnClearSearch.IsVisible = !string.IsNullOrEmpty(searchTerm);
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                SetStatusMessage();
                ListResultsView.ItemsSource = null;
                return;
            }
            else
            {
                SetStatusMessage("Searching for \"" + searchTerm + "\"");
                ListResultsView.SeparatorVisibility = SeparatorVisibility.None;
            }

            Interlocked.Exchange(ref throttleCts, new CancellationTokenSource()).Cancel();
            Task.Delay(TimeSpan.FromMilliseconds(searchDelay), throttleCts.Token).ContinueWith(
                async delegate { await Search(searchTerm); }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task Search(string searchTerm)
        {
            querier = new Querier();
            searchItems = await querier.GetSearchResultsAsync(searchTerm);

            if (searchItems == null)
            {
                SetStatusMessage("Please check your internet connection and/or the server address then try again.");
            }
            else
            {
                if ((searchItems.Count > 0) && (!string.IsNullOrEmpty(txtSearch.Text)))
                {
                    ListResultsView.ItemsSource = searchItems.GetTitleArray();
                    ListResultsView.SeparatorVisibility = SeparatorVisibility.Default;
                    SetStatusMessage();
                }
                else if(searchItems.Count == 0)
                {
                    SetStatusMessage("No results found for \"" + searchTerm + "\"");
                }

            }
        }


        private async void ListResultsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Song selSong = searchItems.Collection[e.ItemIndex];
            //string msgTitle = selSong.GetDisplayHeader();
            //Task msgTask = DisplayAlert(msgTitle, await selSong.SetBodyAsync(), "Close");
            Task pushNavigation = Navigation.PushAsync(new PageDisplay(selSong.ID));

            if (SessionManager.Hosting == SessionManager.HostStatus.Host)
            {
                bool result = await SessionManager.UpdateSessionAsync(selSong.ID);
                if (result == false)
                {
                    await DisplayAlert("Error 2", "Unable connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
                }
            }
            await pushNavigation;
        }

        private void SetStatusMessage(string message = "")
        {
            if (message != "")
            {
                ListResultsView.ItemsSource = null;
            }
            lblMessage.Text = message;
        }

        void btnClearSearch_Clicked(System.Object sender, System.EventArgs e)
        {
            txtSearch.Text = string.Empty;
            txtSearch.Focus();
            SearchInit(string.Empty);
        }
    }
}
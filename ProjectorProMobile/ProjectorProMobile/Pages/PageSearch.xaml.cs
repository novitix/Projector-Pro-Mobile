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
            var selIndex = e.ItemIndex;
            string msgTitle = string.Format("{0} ({1}) #{2}", searchItems.Collection[selIndex].Title, searchItems.Collection[selIndex].Key, searchItems.Collection[selIndex].Number);
            await DisplayAlert(msgTitle, await searchItems.Collection[selIndex].SetBodyAsync(), "Close");
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
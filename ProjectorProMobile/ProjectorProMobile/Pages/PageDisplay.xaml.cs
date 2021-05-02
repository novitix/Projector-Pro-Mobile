using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Songs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDisplay : ContentPage
    {
        Song currentSong;
        public PageDisplay(int songId = -1)
        {
            InitializeComponent();
            currentSong = new Song();
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                SessionManager.IdChanged += UpdateText;
            }
            else
            {
                UpdateText(songId);
            }
            txtContent.BindingContext = currentSong;
            currentSong.Body = "Waiting for connection...";

        }



        protected override void OnDisappearing()
        {
            SessionManager.Hosting = SessionManager.HostStatus.Solo;
            Navigation.PopAsync();
            base.OnDisappearing();
            SessionManager.StopUpdateChecks();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {

                SessionManager.BeginUpdateChecks();
            }
            else
            {
                
            }
        }


        private async void UpdateText(int newId)
        {
            if (newId != -1)
            {
                currentSong.ID = newId;
                string res = await currentSong.SetBodyAsync();
                // workaround for Xamarin bug where scrollview does not scroll unless the size of is changed.
                //scrollVw.VerticalOptions = LayoutOptions.FillAndExpand;

                if (res == null)
                {
                    await DisplayAlert("Error 4", "Unable connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
                    ExitDisplay();
                }
            }
            
        }
        protected override bool OnBackButtonPressed()
        {
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }

        private void btnExitDisplay_Clicked(object sender, EventArgs e)
        {
            ExitDisplay();
        }

        private void ExitDisplay()
        {
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                SessionManager.Hosting = SessionManager.HostStatus.Solo;
            }
            Navigation.PopAsync();
        }
    }
}
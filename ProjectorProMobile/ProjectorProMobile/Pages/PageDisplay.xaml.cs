using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Songs;
using ProjectorProMobile.DependencyServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageDisplay : ContentPage
    {
        public LyricStyler lyricStyler;
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
            lyricStyler = new LyricStyler(Resources);
            txtContent.BindingContext = lyricStyler;
            currentSong.Body = "Waiting for connection...";
        }



        protected override void OnDisappearing()
        {
            Navigation.PopModalAsync();
            base.OnDisappearing();
            MessagingCenter.Send(this, "PreventLandscape");
            SessionManager.StopUpdateChecks();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "AllowLandscape");
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
            if (newId == -1) return; // id = -1 means that the host has not sent a song id yet
            
            currentSong.ID = newId;
            string body = await currentSong.SetBodyAsync();
            if (body == null)
            {
                await DisplayAlert("Error 4", "Unable connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
                ExitDisplay();
                return;
            }
            currentSong.Body = LyricFormatter.GetFormattedBody(body);
            lyricStyler.FormatAndSet(currentSong.Body);
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
            Navigation.PopModalAsync();
        }

    }
}
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
        private Song currentSong = new Song();
        public PageDisplay(int songId = -1)
        {
            InitializeComponent();
            
            lyricStyler = new LyricStyler(Resources);
            txtContent.BindingContext = lyricStyler;

            if (SessionManager.Hosting != SessionManager.HostStatus.Follow)
            {
                currentSong.ID = songId;
            }
            
            currentSong.PropertyChanged += currentSong_PropertyChanged;
            currentSong.Body = "Waiting for connection...";
        }



        protected override void OnDisappearing()
        {
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                currentSong = new Song();
                SessionManager.ResetId();
            }
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
                SessionManager.IdChanged += SessionManager_IdChanged;
                SessionManager.BeginUpdateChecks();
            }
            else
            {
                UpdateSong();
            }
        }


        private void SessionManager_IdChanged(int newId)
        {
            if (newId == -1) return; // id = -1 means that the host has not sent a song id yet
            
            currentSong.ID = newId;
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

        private async void currentSong_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ID")
            {
                await UpdateSong();
            }
        }

        private async Task UpdateSong()
        {
            currentSong = await new Querier().GetSongAsync(currentSong.ID);
            lblTitle.BindingContext = currentSong;
            lblKey.BindingContext = currentSong;
            if (currentSong.Body == null)
            {
                await DisplayAlert("Error 4", "Unable connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
                ExitDisplay();
                return;
            }
            lyricStyler.FormatAndSet(LyricFormatter.GetFormattedBody(currentSong.Body));
        }

    }
}
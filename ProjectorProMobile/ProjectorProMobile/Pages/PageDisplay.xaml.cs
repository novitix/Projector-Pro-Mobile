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
            currentSong.ID = newId;
            await currentSong.SetBodyAsync();
        }
    }
}
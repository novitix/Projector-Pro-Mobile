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
    public partial class PageJoinFollow : ContentPage
    {
        Song currentSong;
        public PageJoinFollow()
        {
            InitializeComponent();
            currentSong = new Song();
            txtContent.BindingContext = currentSong;
            currentSong.Body = "Waiting for connection...";
            SessionManager.IdChanged += UpdateText;
        }

        

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SessionManager.StopUpdateChecks();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SessionManager.BeginUpdateChecks();
        }


        private async void UpdateText(int newId)
        {
            currentSong.ID = newId;
            await currentSong.SetBodyAsync();
        }
    }
}
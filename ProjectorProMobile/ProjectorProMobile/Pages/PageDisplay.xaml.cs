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

        public PageDisplay(int songId = -1)
        {
            InitializeComponent();
            BindingContext = new DisplayViewModel(songId);
        }

        protected override void OnDisappearing()
        {
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
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
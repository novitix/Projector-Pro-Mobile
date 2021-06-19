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
            var displayViewModel = BindingContext as DisplayViewModel;
            displayViewModel.InitialiseWithID(songId);
            lblTitle.BindingContext = displayViewModel;
            lblKey.BindingContext = displayViewModel;
            svTitle.PropertyChanged += SvTitle_PropertyChanged;
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

        
        private void scrollVw_Scrolled(object sender, ScrolledEventArgs e)
        {
            svTitle.ScrollToAsync(0, svTitle.ContentSize.Height, false);
        }
        private void SvTitle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HeightRequest")
            {
                svTitle.IsVisible = (svTitle.HeightRequest > 0.01);
            }
        }
    }
}
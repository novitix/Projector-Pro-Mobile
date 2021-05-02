using System;
using ProjectorProMobile.Themes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new[] { "Brush_Experimental" });
            InitializeComponent();
            if (Preferences.Get("darkMode", "True").ToLower() == "true")
            {
                App.Current.Resources = new DarkTheme();
            }
            else
            {
                App.Current.Resources = new LightTheme();
            }
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            SessionManager.StopUpdateChecks();
        }

        protected override void OnResume()
        {
            SessionManager.BeginUpdateChecks();
        }
    }
}

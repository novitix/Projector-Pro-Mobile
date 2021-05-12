using System;
using ProjectorProMobile.Themes;
using ShaXam.DependencyServices;
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
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (Application.Current.RequestedTheme == OSAppTheme.Dark)
                {
                    DependencyService.Get<IStatusBarStyleManager>().SetColoredStatusBar("#000000");
                }
                else
                {
                    DependencyService.Get<IStatusBarStyleManager>().SetColoredStatusBar("#ffffff");
                }
            }
            Pages.Settings.PageSettingsLyricsColourPicker.CreateDefaultColourPreferencesIfNotExist();

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

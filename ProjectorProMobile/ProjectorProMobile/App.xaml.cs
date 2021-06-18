using System;
using ProjectorProMobile.Themes;
using ShaXam.DependencyServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectorProMobile.DependencyServices;
using System.Threading.Tasks;

namespace ProjectorProMobile
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new[] { "Brush_Experimental" });
            InitializeComponent();
            if (SettingsManager.Get("DarkMode") == "True")
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

            MainPage = new MainPage();

        }



        protected async override void OnStart()
        {
            
            if (await VersionChecking.IsServerCompatible() == false)
            {
                if (await MainPage.DisplayAlert("Update", "There is a new update available.", "Update", "Close"))
                {
                    await OpenStoreToUpdate();
                }
                else
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
        }

        private async Task OpenStoreToUpdate()
        {
            string storeUrl = await GetStoreUrl();
            if (storeUrl == null) await StoreOpenFailed();

            Uri uri = new Uri(storeUrl);
            if (await Launcher.TryOpenAsync(uri) == false)
            {
                await StoreOpenFailed();
            }

            
        }

        private async Task StoreOpenFailed()
        {
            await MainPage.DisplayAlert("Error", "The store page could not be launched from the app. Please update from the app store or www.ppmserver.tk to continue using this app.", "OK");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async Task<string> GetStoreUrl()
        {
            Querier querier = new Querier();
            if (Device.RuntimePlatform == Device.Android)
            {
                return await querier.GetQueryAsync(VersionChecking.UpdatesServer + "/android-play-store");
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                return await querier.GetQueryAsync(VersionChecking.UpdatesServer + "/ios-app-store");
            }
            else
            {
                return null;
            }
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

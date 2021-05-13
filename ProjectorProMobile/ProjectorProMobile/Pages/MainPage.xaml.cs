using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Songs;
using System.Timers;
using ShaXam.DependencyServices;
using ProjectorProMobile.DependencyServices;

namespace ProjectorProMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            SetSessionManager();
            InitializeComponent();
            Xamarin.Essentials.DeviceDisplay.KeepScreenOn = true;
            Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            UpdateStatusBarColour();
        }

        private void SetSessionManager()
        {
            string strSeshCode = SettingsManager.Get("SessionCode");
            SessionManager.SessionCode = int.Parse(strSeshCode);
            Enum.TryParse(SettingsManager.Get("HostStatus"), out SessionManager.HostStatus hostStatus);
            SessionManager.Hosting = hostStatus;
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                SessionManager.Hosting = SessionManager.HostStatus.Solo;
            }
        }

        private void UpdateStatusBarColour()
        {
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
        }
    }
}

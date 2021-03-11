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
        }

        private void SetSessionManager()
        {
            string strSeshCode = Preferences.Get("sessionCode", "0000");
            SessionManager.SessionCode = int.Parse(strSeshCode);
            SessionManager.Hosting = (SessionManager.HostStatus)Preferences.Get("hostStatus", 0);
            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                SessionManager.Hosting = SessionManager.HostStatus.Solo;
            }
        }
    }
}

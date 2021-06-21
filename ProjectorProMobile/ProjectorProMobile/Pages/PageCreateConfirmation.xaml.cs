using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateConfirmation : ContentPage
    {

        bool _createNewSession;
        int? _code;
        bool firstCreation = true;
        public PageCreateConfirmation(bool createNewSession, int? code = null)
        {
            InitializeComponent();
            _createNewSession = createNewSession;
            _code = code;
        }

        protected async override void OnAppearing()
        {
            if (_createNewSession && firstCreation) _code = await SessionManager.CreateSessionAsync();
            firstCreation = false;
            await DisplayCode();
        }

        private async Task DisplayCode()
        {
            if (_code == null)
            {
                await DisplayAlert("Error 1", "Unable to connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
                await Navigation.PopAsync();
            }
            else
            {
                string strCode = _code.ToString();
                code1.Text = strCode.Substring(0, 1);
                code2.Text = strCode.Substring(1, 1);
                code3.Text = strCode.Substring(2, 1);
                code4.Text = strCode.Substring(3, 1);
                lblStatusMessage.Text = "As the host, songs displayed on this device will be mirrored to joined devices.";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void frameExitSession_Tapped(object sender, EventArgs e)
        {
            SessionManager.Hosting = SessionManager.HostStatus.Solo;
            Navigation.PopAsync();
        }
    }
}
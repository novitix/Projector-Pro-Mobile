using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectorProMobile;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageJoin : ContentPage
    {
        public PageJoin()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ClearCode();
            if (SessionManager.Hosting == SessionManager.HostStatus.Host)
            {
                await Navigation.PushAsync(new PageCreateConfirmation(SessionManager.SessionCode));
            }
        }

        async private void frameCreateSession_Clicked(object sender, EventArgs e)
        {
            int? res = await SessionManager.CreateSessionAsync();
            if (res == null)
            {
                await DisplayAlert("Error 1", "Unable to connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
            }
            else
            {
                await Navigation.PushAsync(new PageCreateConfirmation(SessionManager.SessionCode));
            }
        }

        async private void txtHiddenCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string code = txtHiddenCode.Text;
            //Console.WriteLine(code);
            code1.Text = "";
            code2.Text = "";
            code3.Text = "";
            code4.Text = "";
            if (code.Length >= 1) code1.Text = code.Substring(0, 1);
            if (code.Length >= 2) code2.Text = code.Substring(1, 1);
            if (code.Length >= 3) code3.Text = code.Substring(2, 1);
            if (code.Length == 4)
            {
                code4.Text = code.Substring(3, 1);
                txtHiddenCode.Unfocus();
                SessionManager.SessionCode = int.Parse(code);

                bool? sessionExists = await SessionManager.CheckSessionExists();
                if(sessionExists == null)
                {
                    await DisplayAlert("Error 3", "Unable to connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
                    ClearCode();
                }
                else
                {
                    if ((bool)sessionExists)
                    {
                        SessionManager.Hosting = SessionManager.HostStatus.Follow;
                        await Navigation.PushModalAsync(new PageDisplay());
                    }
                    else
                    {
                        await DisplayAlert("Join Error", "The session does not exist. Please enter a valid session code and try again.", "Close");
                        ClearCode();
                    }
                }
            }
        }

        private void ClearCode()
        {
            txtHiddenCode.TextChanged -= txtHiddenCode_TextChanged;
            txtHiddenCode.Text = "";
            txtHiddenCode.TextChanged += txtHiddenCode_TextChanged;
            code1.Text = "";
            code2.Text = "";
            code3.Text = "";
            code4.Text = "";
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            txtHiddenCode.Focus();
        }

    }
}
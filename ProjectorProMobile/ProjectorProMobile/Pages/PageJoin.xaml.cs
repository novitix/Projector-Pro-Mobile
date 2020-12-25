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

        async private void btnCreateSesh_Clicked(object sender, EventArgs e)
        {
            await SessionManager.CreateSessionAsync();
            await Navigation.PushAsync(new PageCreateConfirmation(SessionManager.SessionCode));
        }

        async private void txtHiddenCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string code = txtHiddenCode.Text;
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
                
                if (await SessionManager.CheckSessionExists())
                {
                    SessionManager.Hosting = SessionManager.HostStatus.Follow;
                    await Navigation.PushAsync(new PageJoinFollow());
                }
                else
                {
                    await DisplayAlert("Join Error", "The session does not exist. Please enter a valid session code and try again.", "Close");
                    clearCode();
                }
            }
        }

        private void clearCode()
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
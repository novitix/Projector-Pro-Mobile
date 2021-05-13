using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSettingsLyricsProperty : ContentPage
    {
        public PageSettingsLyricsProperty()
        {
            InitializeComponent();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnNavLyricColour_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsLanguage("Colour"));
        }

        private void btnNavLyricFont_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsLanguage("Font"));
        }

    }
}
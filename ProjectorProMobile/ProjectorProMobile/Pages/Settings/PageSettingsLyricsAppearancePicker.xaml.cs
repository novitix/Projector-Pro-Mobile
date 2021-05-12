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
    public partial class PageSettingsLyricsAppearancePicker : ContentPage
    {
        private string _lyricLanguage;
        public PageSettingsLyricsAppearancePicker(string lyricLanguage)
        {
            InitializeComponent();
            lblPageTitle.Text = "Change " + lyricLanguage + " Lyric Appearance";
            _lyricLanguage = lyricLanguage;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnNavLyricColour_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsColourPicker(_lyricLanguage));
        }

        private void btnNavLyricFont_Clicked(object sender, EventArgs e)
        {
            
        }

    }
}
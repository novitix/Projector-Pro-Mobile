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
    public partial class PageSettingsLyricsLanguage : ContentPage
    {
        private string _lyricProperty;
        public PageSettingsLyricsLanguage(string lyricProperty)
        {
            InitializeComponent();
            lblPageTitle.Text = "Change Lyric " + lyricProperty;
            _lyricProperty = lyricProperty;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnNavChangeEnglishAppearance_Clicked(object sender, EventArgs e)
        {
            OpenPage("English");
        }

        private void btnNavChangeChineseAppearance_Clicked(object sender, EventArgs e)
        {
            OpenPage("Chinese");
        }

        private void btnNavChangePinyinAppearance_Clicked(object sender, EventArgs e)
        {
            OpenPage("Pinyin");
        }

        private void OpenPage(string language)
        {
            if (_lyricProperty == "Colour")
            {
                Navigation.PushAsync(new PageSettingsLyricsColourPicker(language));
            }
            else if (_lyricProperty == "Font")
            {
                Navigation.PushAsync(new PageSettingsLyricsFontPicker(language));
            }
        }
    }
}
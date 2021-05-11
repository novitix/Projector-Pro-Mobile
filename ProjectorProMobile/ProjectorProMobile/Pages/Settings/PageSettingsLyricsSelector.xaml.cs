using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSettingsLyricsSelector : ContentPage
    {
        public PageSettingsLyricsSelector()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            swhShowEnglish.IsToggled = (Preferences.Get("showEnglishLyrics", "true").ToLower() == "true");
            swhShowChinese.IsToggled = (Preferences.Get("showChineseLyrics", "true").ToLower() == "true");
            swhShowPinyin.IsToggled = (Preferences.Get("showPinyinLyrics", "true").ToLower() == "true");
            swhAllowLyricOverride.IsToggled = (Preferences.Get("allowLyricOverride", "true").ToLower() == "true");
        }

        private void swhShowEnglish_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("showEnglishLyrics", swhShowEnglish.IsToggled.ToString());
        }

        private void swhShowChinese_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("showChineseLyrics", swhShowChinese.IsToggled.ToString());
        }

        private void swhShowPinyin_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("showPinyinLyrics", swhShowPinyin.IsToggled.ToString());
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void swhAllowLyricOverride_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("allowLyricOverride", swhAllowLyricOverride.IsToggled.ToString());
        }
    }
}
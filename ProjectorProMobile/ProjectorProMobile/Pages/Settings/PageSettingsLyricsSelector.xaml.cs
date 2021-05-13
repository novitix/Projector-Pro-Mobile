using ProjectorProMobile.DependencyServices;
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
            swhShowEnglish.IsToggled = (SettingsManager.Get("EnglishShow") == "True");
            swhShowChinese.IsToggled = (SettingsManager.Get("ChineseShow") == "True");
            swhShowPinyin.IsToggled = (SettingsManager.Get("PinyinShow") == "True");
            swhAllowLyricOverride.IsToggled = (SettingsManager.Get("LyricOverride") == "True");
        }

        private void swhShowEnglish_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set("EnglishShow", swhShowEnglish.IsToggled.ToString());
        }

        private void swhShowChinese_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set("ChineseShow", swhShowChinese.IsToggled.ToString());
        }

        private void swhShowPinyin_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set("PinyinShow", swhShowPinyin.IsToggled.ToString());
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void swhAllowLyricOverride_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set("LyricOverride", swhAllowLyricOverride.IsToggled.ToString());
        }
    }
}
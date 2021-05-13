using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using ProjectorProMobile.DependencyServices;

namespace ProjectorProMobile.Pages.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSettingsLyricsFontPicker : ContentPage
    {
        private string _lyricLanguage;
        private string _fontSize;
        public string FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if (value != _fontSize)
                {
                    _fontSize = value;
                    SettingsManager.Set(_lyricLanguage + "FontSize", value);
                }
            }
        }
        public PageSettingsLyricsFontPicker(string lyricLanguage)
        {
            InitializeComponent();
            _lyricLanguage = lyricLanguage;
            lblPageTitle.Text = lyricLanguage + " Lyric Font";
            BindingContext = this;
            LoadSettings();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void swhBold_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set(_lyricLanguage + "Bold", swhBold.IsToggled.ToString());
        }

        private void swhItalics_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set(_lyricLanguage + "Italic", swhItalics.IsToggled.ToString());
        }

        private void swhUnderline_Toggled(object sender, ToggledEventArgs e)
        {
            SettingsManager.Set(_lyricLanguage + "Underline", swhUnderline.IsToggled.ToString());
        }

        private void LoadSettings()
        {
            swhBold.IsToggled = bool.Parse(SettingsManager.Get(_lyricLanguage + "Bold"));
            swhItalics.IsToggled = bool.Parse(SettingsManager.Get(_lyricLanguage + "Italic"));
            swhUnderline.IsToggled = bool.Parse(SettingsManager.Get(_lyricLanguage + "Underline"));
            txtFontSize.Text = SettingsManager.Get(_lyricLanguage + "FontSize");
        }
    }
}
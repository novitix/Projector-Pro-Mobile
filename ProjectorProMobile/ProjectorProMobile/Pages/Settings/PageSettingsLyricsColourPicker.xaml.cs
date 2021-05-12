using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSettingsLyricsColourPicker : ContentPage
    {
        private string _lyricLanguage;
        private Color _selColour;
        public Color SelColour {
            get
            {
                return _selColour;
            }
            set
            {
                if (value != _selColour)
                {
                    _selColour = value;
                    Preferences.Set(_lyricLanguage + "Colour", SelColour.ToHex());
                }
            }
        }
        public PageSettingsLyricsColourPicker(string lyricLanguage)
        {
            InitializeComponent();

            _lyricLanguage = lyricLanguage;
            
            string prefColour = Preferences.Get(_lyricLanguage + "Colour", "#000000");
            SelColour = Color.FromHex(prefColour);
            colPicker.BindingContext = this;
            lblPageTitle.Text = _lyricLanguage + " Lyric Colour";
        }

        public static void CreateDefaultColourPreferencesIfNotExist()
        {
            if (!Preferences.ContainsKey("EnglishColour"))
            {
                Preferences.Set("EnglishColour", "#fff8f5");
            }
            if (!Preferences.ContainsKey("ChineseColour"))
            {
                Preferences.Set("ChineseColour", "#cccccc");
            }
            if (!Preferences.ContainsKey("PinyinColour"))
            {
                Preferences.Set("PinyinColour", "#2b2421");
            }
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
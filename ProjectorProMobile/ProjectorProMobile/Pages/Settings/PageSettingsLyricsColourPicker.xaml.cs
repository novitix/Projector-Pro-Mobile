using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectorProMobile.DependencyServices;

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
                    SettingsManager.Set(_lyricLanguage + "Colour", SelColour.ToHex());
                }
            }
        }
        public PageSettingsLyricsColourPicker(string lyricLanguage)
        {
            InitializeComponent();

            _lyricLanguage = lyricLanguage;
            
            string prefColour = SettingsManager.Get(_lyricLanguage + "Colour");
            SelColour = Color.FromHex(prefColour);
            colPicker.BindingContext = this;
            lblPageTitle.Text = _lyricLanguage + " Lyric Colour";
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
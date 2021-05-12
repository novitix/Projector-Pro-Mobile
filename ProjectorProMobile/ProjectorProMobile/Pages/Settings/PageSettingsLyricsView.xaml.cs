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
    public partial class PageSettingsLyricsView : ContentPage
    {
        public PageSettingsLyricsView()
        {
            InitializeComponent();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnNavChangeEnglishAppearance_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsAppearancePicker("English"));
        }

        private void btnNavChangeChineseAppearance_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsAppearancePicker("Chinese"));
        }

        private void btnNavChangePinyinAppearance_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsAppearancePicker("Pinyin"));
        }
    }
}
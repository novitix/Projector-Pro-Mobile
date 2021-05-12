﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectorProMobile.Themes;
//using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ShaXam.DependencyServices;
using Android.Views;
using ProjectorProMobile.Pages.Settings;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSettings : ContentPage
    {
        public PageSettings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtServerAddress.Text = Preferences.Get("serverAddress", "");
            string dmToggled = Preferences.Get("darkMode", "True");
            swhDarkMode.IsToggled = dmToggled.ToLower() == "true";
        }

        private void txtServerAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            Preferences.Set("serverAddress", txtServerAddress.Text);
        }

        private void swhDarkMode_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("darkMode", swhDarkMode.IsToggled.ToString());
            if (swhDarkMode.IsToggled)
            {
                App.Current.Resources = new DarkTheme();
            }
            else
            {
                App.Current.Resources = new LightTheme();
            }

            MessagingCenter.Send(this, "UpdateNavBarColour");
        }

        private void btnNavLyricSelector_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsSelector());
        }

        private void btnNavLyricAppearance_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSettingsLyricsView());
        }
    }
}
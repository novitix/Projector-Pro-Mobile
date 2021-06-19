using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using Xamarin.Forms;
using ProjectorProMobile.Pages;
using Xamarin.Essentials;
using ProjectorProMobile.DependencyServices;

namespace ProjectorProMobile.Droid
{
    [Activity(Label = "Lyrical", Icon = "@drawable/ic_launcher", RoundIcon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, ScreenOrientation=ScreenOrientation.Portrait )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            UpdateNavBarColour();

            //allowing the device to change the screen orientation based on the rotation 
            MessagingCenter.Subscribe<PageDisplay>(this, "AllowLandscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.FullUser;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<PageDisplay>(this, "PreventLandscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });

            //this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            MessagingCenter.Subscribe<PageSettings>(this, "UpdateNavBarColour", sender =>
            {
                UpdateNavBarColour();
            });

            LoadApplication(new App());
            
            
        }

        private void UpdateNavBarColour()
        {
            string darkMode = SettingsManager.Get("DarkMode").ToString().ToLower();
            this.Window.SetNavigationBarColor((darkMode == "true") ? Android.Graphics.Color.ParseColor("#111111") : Android.Graphics.Color.White);
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Android.Graphics.Color color;
            if (darkMode == "true")
            {
                color = Android.Graphics.Color.ParseColor("#111111");
            }
            else
            {
                color = Android.Graphics.Color.ParseColor("#907E7A");
            }
            Window.SetStatusBarColor(color);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
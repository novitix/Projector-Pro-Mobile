using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Songs;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageJoinFollow : ContentPage
    {
        int dispId = -1;
        bool finishedUpdating = true;
        bool checkUpdates;
        Song currentSong;
        public PageJoinFollow()
        {
            InitializeComponent();
            currentSong = new Song();
            txtContent.BindingContext = currentSong;
            currentSong.Body = "Waiting for connection...";

            BeginUpdateChecks();
        }

        public void BeginUpdateChecks()
        {
            checkUpdates = true;
            int updateInterval = 2; // check for updates every 2 seconds
            Device.StartTimer(TimeSpan.FromSeconds(updateInterval), () =>
            {
                Task.Run(async () =>
                {
                    if (finishedUpdating)
                    {
                        await CheckUpdates();
                    }
                });
                return checkUpdates;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            checkUpdates = false;
        }

        private async Task CheckUpdates()
        {
            finishedUpdating = false;
            int id = await SessionManager.CheckSessionChanges(dispId);
            if (id != dispId)
            {
                dispId = id;
                await UpdateText();
            }
            finishedUpdating = true;
        }

        private async Task UpdateText()
        {
            currentSong.ID = dispId;
            await currentSong.SetBodyAsync();
        }
    }
}
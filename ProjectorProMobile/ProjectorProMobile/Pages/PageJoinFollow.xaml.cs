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
        bool updating = true;
        public PageJoinFollow()
        {
            InitializeComponent();
            CheckUpdates();
        }

        private async Task CheckUpdates()
        {
            while (updating)
            {
                int id = await SessionManager.CheckSessionChanges(dispId);
                if (id != dispId)
                {
                    dispId = id;
                    await UpdateText();
                }
                Thread.Sleep(2000);
            }
        }

        private async Task UpdateText()
        {
            Song currentSong = new Song();
            currentSong.ID = dispId;
            await currentSong.SetBodyAsync();
            txtContent.Text = currentSong.Body;
        }
    }
}
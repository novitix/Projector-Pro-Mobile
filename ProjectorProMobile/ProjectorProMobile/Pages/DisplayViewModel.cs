using ProjectorProMobile.DependencyServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using Songs;
using System.Threading.Tasks;

namespace ProjectorProMobile.Pages
{
    public class DisplayViewModel : INotifyPropertyChanged
    {
        FormattedString _formattedLyrics;
        private Song _currentSong = new Song();
        public Song CurrentSong
        {
            get
            {
                return _currentSong;
            }
            set
            {
                if (_currentSong != value)
                {
                    _currentSong = value;
                    OnPropertyChanged("CurrentSong");
                }
            }
        }

        //public string Title
        //{
        //    get
        //    {
        //        return currentSong.Title;
        //    }
        //}
        //public string Key
        //{
        //    get
        //    {
        //        return currentSong.Key;
        //    }
        //}
        public FormattedString FormattedLyrics
        {
            get
            {
                return _formattedLyrics;
            }

            private set
            {
                if (_formattedLyrics != value)
                {
                    _formattedLyrics = value;
                    OnPropertyChanged("FormattedLyrics");
                }
            }
        }
        public DisplayViewModel(int songId)
        {
            CurrentSong.ID = songId;
            //if (SessionManager.Hosting != SessionManager.HostStatus.Follow)
            //{
            //}

            if (SessionManager.Hosting == SessionManager.HostStatus.Follow)
            {
                SessionManager.IdChanged += SessionManager_IdChanged;
                SessionManager.BeginUpdateChecks();
            }
            else
            {
                UpdateSong();
            }

            CurrentSong.PropertyChanged += currentSong_PropertyChanged;
            CurrentSong.Body = "Waiting for connection...";
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async void currentSong_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ID")
            {
                await UpdateSong();
            }
        }

        public async Task UpdateSong()
        {
            CurrentSong = await new Querier().GetSongAsync(CurrentSong.ID);

            //if (currentSong.Body == null)
            //{
            //    await DisplayAlert("Error 4", "Unable connect to the server. Please check your internet connection and/or the server address then try again.", "Close");
            //    ExitDisplay();
            //    return;
            //}
            FormattedLyrics = LyricStyler.Style(LyricFormatter.GetFormattedBody(CurrentSong.Body));
        }

        private async void SessionManager_IdChanged(int newId)
        {
            if (newId == -1) return; // id = -1 means that the host has not sent a song id yet
            CurrentSong.ID = newId;
            await UpdateSong();
        }
    }

}

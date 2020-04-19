using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Projector_Pro_Mobile
{
    public class StatusMessage : INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StatusMessage(string message = "")
        {
            _message = message;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Songs {
    public class Song : INotifyPropertyChanged
    {
        private int _id;
        public int ID {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        private string _title;
        public string Title {
            get
            {
                return _title;
            } 
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private int _number;
        public int Number {
            get
            {
                return _number;
            }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        private string _key;
        public string Key {
            get
            {
                return _key;
            }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged("Key");
                }
            }
        }

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                if (_body != value)
                {
                    _body = value;
                    OnPropertyChanged("Body");
                }
            }
        }
        public bool IsBodySet { get { return !string.IsNullOrEmpty(Body); } }


        public Song() { }

        public string GetDisplayHeader()
        {
            return string.Format("{0} ({1}) #{2}", Title, Key, Number);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SongCollection
    {
        public List<Song> Collection { get; set; }
        public SongCollection(List<Song> itemList)
        {
            Collection = itemList;
        }

        public string[] GetTitleArray()
        {
            return Collection.Select(x => x.Title).ToArray();
        }

        public int Count
        {
            get
            {
                return Collection.Count;
            }
        }
    }


}

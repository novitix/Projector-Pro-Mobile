using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Songs {
    public class Song : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string Key { get; set; }
        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
                OnPropertyChanged("Body");
            }
        }
        public bool IsBodySet { get { return !string.IsNullOrEmpty(Body); } }


        public Song() { }

        public async Task<string> SetBodyAsync()
    {
            Querier querier = new Querier();
            Body = await querier.GetBodyAsync(ID);
            return Body;
        }

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

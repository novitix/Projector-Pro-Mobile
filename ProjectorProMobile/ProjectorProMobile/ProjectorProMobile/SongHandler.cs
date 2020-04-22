using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Songs {
    public class Song
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string Key { get; set; }

        public bool IsBodySet = false;

        private string _body;
        public string Body {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
                IsBodySet = true;
            }
        }



        public Song() { }

        public async Task<string> SetBodyAsync()
        {
            Querier querier = new Querier();
            Body = await querier.GetBodyAsync(ID);
            return Body;
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
    }


}

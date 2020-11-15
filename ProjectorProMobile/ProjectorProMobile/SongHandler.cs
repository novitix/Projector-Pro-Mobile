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
        public string Body { get; set; }
        public bool IsBodySet { get { return !string.IsNullOrEmpty(Body); } }


        public Song() { }

        public async Task<string> SetBodyAsync()
        {
            if (!IsBodySet)
            {
                Querier querier = new Querier();
                Body = await querier.GetBodyAsync(ID);
            }
            return Body;
        }

        public string GetDisplayHeader()
        {
            return string.Format("{0} ({1}) #{2}", Title, Key, Number);
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

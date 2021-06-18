using System;
using System.Collections.Generic;
using System.Text;
using Songs;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using Xamarin.Essentials;
using Xamarin;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.IO;
using ProjectorProMobile.DependencyServices;

    class Querier
{
    private string baseUri = string.Empty;
    private HttpClient client = new HttpClient();

    public Querier()
    {
        string url = SettingsManager.Get("ServerAddress");
        baseUri = string.Format("http://{0}/api/songs", url);
        int httpTimeout = int.Parse(SettingsManager.Get("HttpTimeout"));
        client.Timeout = TimeSpan.FromSeconds(httpTimeout);
    }

    private enum SearchType
    {
        number = 1,
        filter = 2
    }

    public async Task<SongCollection> GetSearchResultsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return null;
        SearchType searchType;
        searchType = IsDigitsOnly(searchTerm) ? SearchType.number : SearchType.filter;

        string uri = string.Format("{0}/search?{1}={2}", baseUri, searchType.ToString(), searchTerm);
        string jsonRes;
        jsonRes = await GetQueryAsync(uri);

        if (string.IsNullOrWhiteSpace(jsonRes)) return null;

        List<Song> collection = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(jsonRes);
        return new SongCollection(collection);
    }
    bool IsDigitsOnly(string str)
    {
        return str.All(c => c >= '0' && c <= '9');
    }

    public async Task<string> GetQueryAsync(string uri)
    {
        string jsonRes = "";
        try
        {
            jsonRes = await client.GetStringAsync(uri);
        }
        catch (Exception)
        {
            return null;
        }

        return jsonRes;
    }


    public async Task<Song> GetSongAsync(int id)
    {
        string uri = string.Format("{0}/get-song?id={1}", baseUri, id.ToString());
        string jsonRes = await GetQueryAsync(uri);
        if (string.IsNullOrWhiteSpace(jsonRes)) return null;

        var song = Newtonsoft.Json.JsonConvert.DeserializeObject<Song>(jsonRes);
        return song;
    }
}
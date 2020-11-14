using System;
using System.Collections.Generic;
using System.Text;
using Songs;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using Xamarin.Essentials;
using Xamarin;

    class Querier
{
    private string baseUri = string.Empty;
    private HttpClient client = new HttpClient();

    public Querier()
    {
        string defaultUrl = "projector-pro-server.herokuapp.com";
        string url = Preferences.Get("serverAddress", defaultUrl);
        baseUri = string.IsNullOrWhiteSpace(url) ? string.Format("http://{0}/api/songs", defaultUrl) : string.Format("http://{0}/api/songs", url);
        client.Timeout = TimeSpan.FromSeconds(10);
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

        string uri = string.Format("{0}?{1}={2}", baseUri, searchType.ToString(), searchTerm);
        string jsonRes;
        jsonRes = await GetQueryAsync(uri);
        
        if (jsonRes == null || jsonRes == "") return null;

        List<Song> collection = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Song>>(jsonRes);
        return new SongCollection(collection);
    }

    private async Task<string> GetQueryAsync(string uri)
    {
        string jsonRes = "";
        try
        {
            jsonRes = await client.GetStringAsync(uri);
        }
        catch (Exception ex)
        when (ex is System.Net.WebException || ex is System.IO.IOException || ex is OperationCanceledException)
        {
            return null;
        }

        return jsonRes;
    }

    bool IsDigitsOnly(string str)
    {
        return str.All(c => c >= '0' && c <= '9');
    }

    public async Task<string> GetBodyAsync(int id)
    {
        HttpClient client = new HttpClient();
        string uri = string.Format("{0}?id={1}", baseUri, id.ToString());
        string jsonRes;

        try
        {
            jsonRes = await client.GetStringAsync(uri);
        }
        catch (System.Net.WebException)
        {
            return null;
        }

        var keyValPair = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string,string>>(jsonRes);
        string body = keyValPair.Values.First();

        return body;
    }
}
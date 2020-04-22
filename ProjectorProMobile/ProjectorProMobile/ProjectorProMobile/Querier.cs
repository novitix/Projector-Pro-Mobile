using System;
using System.Collections.Generic;
using System.Text;
using Songs;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;

    class Querier
{
    private string baseUri = "http://192.168.56.1:2444/api/songs";
    private HttpClient client = new HttpClient();

    public Querier()
    {
        client.Timeout = TimeSpan.FromSeconds(8);
    }

    public async Task<SongCollection> GetSearchResultsAsync(string searchTerm)
        {
        if (string.IsNullOrWhiteSpace(searchTerm)) return null;
        if (!IsDigitsOnly(searchTerm)) return null;

        string uri = string.Format("{0}?number={1}", baseUri, searchTerm);
        string jsonRes = await GetQueryAsync(uri);
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
        {
            if (ex is System.Net.WebException || ex is System.IO.IOException || ex is OperationCanceledException)
            {
                return null;
            }
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
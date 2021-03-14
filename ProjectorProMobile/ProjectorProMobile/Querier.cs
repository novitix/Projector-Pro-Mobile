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

        if (string.IsNullOrWhiteSpace(jsonRes)) return null;

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
        catch (Exception)
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
        string jsonRes = await GetQueryAsync(uri);
        if (string.IsNullOrWhiteSpace(jsonRes)) return null;

        var keyValPair = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string,string>>(jsonRes);
        string body = keyValPair.Values.First();
        body = ParseSongBody(body);
        return body;
    }

    private string ParseSongBody(string body)
    {
        // Some new lines were showing up as \r\n.
        body = body.Replace("\\r\\n", Environment.NewLine);
        body = body.Replace("\r\n", Environment.NewLine);
        body = body.Replace("\\n\\n", Environment.NewLine);
        body = body.Replace("\\n", Environment.NewLine);
        body = body.Replace("\n", Environment.NewLine);

        // separate chinese and english and english lines
        StringReader reader = new StringReader(body);
        string temp = string.Empty;
        for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            int lastChinesePos = LastChineseChar(line);
            if (lastChinesePos != -1)
            {
                string firstHalf = line.Substring(0, lastChinesePos);
                string secondHalf = line.Substring(lastChinesePos);
                temp += firstHalf + Environment.NewLine + secondHalf + Environment.NewLine;
            }
            else
            {
                temp += line + Environment.NewLine;
            }
        }



        return temp;
    }

    //private string stripPunctuation(string line)
    //{
    //    Regex.Replace(line, )
    //}

    private int LastChineseChar(string line)
    {
        Match chineseMatch = Regex.Match(line, @"[^\x00-\x7F]+");
        bool containsChinese = chineseMatch.Success;
        if (!containsChinese)
        {
            return -1;
        }

        Match englishMatch = Regex.Match(line, @"[a-zA-Z]");
        return englishMatch.Index;
    }
}
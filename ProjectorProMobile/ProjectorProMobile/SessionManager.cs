using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace ProjectorProMobile
{
    public static class SessionManager
    {
        public static int SessionCode = 0000;
        public static bool IsHost;
        public static async Task<int> CreateSessionAsync()
        {
            string baseUrl = Preferences.Get("serverAddress", "projector-pro-server.herokuapp.com");
            string createEndPt = string.Format("http://{0}/api/session/create-session", baseUrl);
            HttpClient client = new HttpClient();
            HttpResponseMessage task = await client.PostAsync(createEndPt, null);

            string lastSeshEndPt = string.Format("http://{0}/api/session/last-session-id", baseUrl);
            string res = await client.GetStringAsync(lastSeshEndPt);
            SessionCode = int.Parse(res);
            IsHost = true;
            return SessionCode;
        }

        public static async void UpdateSessionAsync(int id)
        {
            string baseUrl = Preferences.Get("serverAddress", "projector-pro-server.herokuapp.com");
            string updateEndPt = string.Format("http://{0}/api/session/update-session", baseUrl);
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new
            {
                sessionCode = SessionCode,
                songId = id
            });
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage task = await client.PostAsync(updateEndPt, stringContent);
        }
    }
}